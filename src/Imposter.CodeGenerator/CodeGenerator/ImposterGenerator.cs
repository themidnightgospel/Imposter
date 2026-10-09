using System;
using System.Linq;
using System.Text;
using System.Threading;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.CodeGenerator.Logging;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Imposter.CodeGenerator.Features.EventImpersonation.Builders;
using Imposter.CodeGenerator.Features.Imposter;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Arguments;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Delegates;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory.Collection;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Collection;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.GenericInterface;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.InvocationVerifierInterface;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.NonGenericInterface;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Getter;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
#if ROSLYN4_14_OR_GREATER
using Imposter.CodeGenerator.Features.Imposter.ImposterExtensions;
#endif

namespace Imposter.CodeGenerator.CodeGenerator;

[Generator]
public sealed class ImposterGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context) =>
        InitializeCore(in context);

    private static void InitializeCore(in IncrementalGeneratorInitializationContext context)
    {
        var optionsProvider = context.GetGeneratorOptions();

        context.ReportDiagnostics(optionsProvider.GetLanguageVersionDiagnostics());

        context.RegisterSourceOutput(
            optionsProvider,
            static (sourceProductionContext, options) =>
                new DiagnosticLogger(
                    sourceProductionContext,
                    options.IsLoggingEnabled
                ).LogLanguageVersion(options.LanguageVersion)
        );

        var declarations = context.GetGenerateImposterDeclarations();

        // Diagnostics need the compilation to point at their source, where pragmas apply, so only declarations that
        // have diagnostics depend on it.
        context.RegisterSourceOutput(
            declarations
                .Where(static declaration => declaration.Diagnostics.Count > 0)
                .Combine(optionsProvider)
                .Combine(context.CompilationProvider),
            static (sourceProductionContext, inputs) =>
                ReportDeclarationDiagnostics(
                    sourceProductionContext,
                    inputs.Left.Left,
                    inputs.Left.Right,
                    inputs.Right
                )
        );

        var targets = declarations
            .Where(static declaration => declaration.Target is not null)
            .Select(
                static (declaration, _) =>
                    new ImposterGenerationTarget(
                        declaration.Target!,
                        declaration.PutInTheSameNamespace,
                        declaration.ExtensionClassNameIncludesArity
                    )
            )
#if ROSLYN4_4_OR_GREATER
            .WithTrackingName("ImposterTargets")
#endif
        ;

        context.RegisterSourceOutput(
            targets.Combine(optionsProvider),
            static (sourceProductionContext, inputs) =>
                GenerateImposter(sourceProductionContext, inputs.Left, inputs.Right)
        );
    }

    private static void ReportDeclarationDiagnostics(
        in SourceProductionContext sourceProductionContext,
        GenerateImposterDeclaration declaration,
        GeneratorOptions options,
        Compilation compilation
    )
    {
        // An unsupported C# version is reported once for the compilation (IMP003) instead.
        if (!options.IsLanguageVersionSupported)
        {
            return;
        }

        foreach (var diagnostic in declaration.Diagnostics)
        {
            sourceProductionContext.ReportDiagnostic(diagnostic.ToDiagnostic(compilation));
        }
    }

    private static void GenerateImposter(
        in SourceProductionContext sourceProductionContext,
        in ImposterGenerationTarget target,
        GeneratorOptions options
    )
    {
        // Returning instead would leave the driver a cached result without the imposter, which a later run with the
        // same inputs would reuse; throwing makes the driver discard the cancelled run.
        sourceProductionContext.CancellationToken.ThrowIfCancellationRequested();

        // An unsupported C# version is reported once for the compilation (IMP003) instead.
        if (!options.IsLanguageVersionSupported)
        {
            return;
        }

        try
        {
            var imposterGenerationContext = new ImposterGenerationContext(
                target,
                new SupportedCSharpFeatures(options.LanguageVersion)
            );

            sourceProductionContext.AddSource(
                imposterGenerationContext.HintName,
                SourceText.From(
                    GeneratedCodeWriter.Write(
                        BuildImposter(
                            imposterGenerationContext,
                            sourceProductionContext.CancellationToken
                        )
                    ),
                    Encoding.UTF8
                )
            );

            new DiagnosticLogger(sourceProductionContext, options.IsLoggingEnabled).LogImposter(
                imposterGenerationContext
            );
        }
        // Cancellation must propagate: reporting it as a crash would leave the driver with a cached result
        // that has no source and an error.
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            CrashDiagnosticsReporter.Report(sourceProductionContext, ex);
#if DEBUG
            throw;
#endif
        }
    }

    private static CompilationUnitSyntax BuildImposter(
        in ImposterGenerationContext imposterGenerationContext,
        in CancellationToken cancellationToken
    )
    {
        var imposterBuilder = ImposterBuilder.Create(imposterGenerationContext);

        BuildMethodImposter(imposterBuilder, imposterGenerationContext, cancellationToken);
        BuildPropertyImposter(imposterBuilder, imposterGenerationContext, cancellationToken);
        BuildEventImposter(imposterBuilder, imposterGenerationContext, cancellationToken);
        BuildIndexerImposter(imposterBuilder, imposterGenerationContext, cancellationToken);
        imposterBuilder.AddInterfaceSetupViews(imposterGenerationContext);

        Action<MemberDeclarationSyntax> AddMember;
        Func<MemberDeclarationSyntax[]> GetTopLevelMembers;

        if (imposterGenerationContext.ImposterNamespaceName is null)
        {
            var globalMembers = new System.Collections.Generic.List<MemberDeclarationSyntax>();

            AddMember = globalMembers.Add;
            GetTopLevelMembers = globalMembers.ToArray;
        }
        else
        {
            var imposterNamespaceBuilder = new NamespaceDeclarationSyntaxBuilder(
                imposterGenerationContext.ImposterNamespaceName
            );

            AddMember = syntax => imposterNamespaceBuilder.AddMember(syntax);
            GetTopLevelMembers = () => [imposterNamespaceBuilder.Build()];
        }

        AddMember(imposterBuilder.Build());

#if ROSLYN4_14_OR_GREATER
        if (imposterGenerationContext.SupportedCSharpFeatures.SupportsTypeExtensions)
        {
            AddMember(
                ImposterExtensionsBuilder.Build(
                    imposterGenerationContext,
                    imposterGenerationContext.ImposterNamespaceName
                )
            );
        }
#endif

        var compilationUnit = CompilationUnit(
            externs: List<ExternAliasDirectiveSyntax>(),
            usings: List(
                UsingStatements.Build(imposterGenerationContext.Target.ContainingNamespace)
            ),
            attributeLists: List<AttributeListSyntax>(),
            members: List<MemberDeclarationSyntax>(GetTopLevelMembers())
        );

        return compilationUnit
            .WithLeadingTrivia(
                TriviaList(
                    Comment("// <auto-generated />"),
                    CarriageReturnLineFeed,
                    Trivia(SyntaxFactoryHelper.EnableNullableTrivia())
#if !KEEP_WARNINGS_IN_GENERATED_FILES
                    ,
                    Trivia(SyntaxFactoryHelper.DisableWarnings())
#endif
                )
            )
            .WithTrailingTrivia(
                TriviaList(
                    Trivia(SyntaxFactoryHelper.RestoreNullableTrivia())
#if !KEEP_WARNINGS_IN_GENERATED_FILES
                    ,
                    Trivia(SyntaxFactoryHelper.RestoreWarnings())
#endif
                )
            );
    }

    private static void BuildMethodImposter(
        ImposterBuilder imposterBuilder,
        in ImposterGenerationContext imposterGenerationContext,
        in CancellationToken cancellationToken
    )
    {
        foreach (
            var method in imposterGenerationContext
                .Imposter.Methods.OrderBy(
                    method => method.Model.MetadataName,
                    StringComparer.Ordinal
                )
                .ThenBy(method => method.DisplayName, StringComparer.Ordinal)
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            imposterBuilder
                .AddMembers(MethodDelegateTypeBuilder.Build(method))
                .AddMember(ArgumentsBuilder.Build(method))
                .AddMember(ArgumentsCriteriaBuilder.Build(method))
                .AddMember(InvocationHistoryInterfaceBuilder.Build(method))
                .AddMember(InvocationHistoryBuilder.Build(method))
                .AddMember(InvocationHistoryCollectionBuilder.Build(method))
                .AddMember(MethodImposterCollectionBuilder.Build(method))
                .AddMember(InvocationSetupBuilder.Build(method))
                .AddMembers(InvocationSetupBuilder.BuildInvocationSetupInterfaces(method))
                .AddMember(MethodImposterNonGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterInvocationVerifierInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilderInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilder.Build(method));
        }
    }

    private static void BuildPropertyImposter(
        ImposterBuilder imposterBuilder,
        in ImposterGenerationContext imposterGenerationContext,
        in CancellationToken cancellationToken
    )
    {
        foreach (var targetProperty in imposterGenerationContext.Imposter.Properties)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var property = imposterGenerationContext.Imposter.CreatePropertyMetadata(
                targetProperty,
                imposterBuilder.MemberNameSet
            );

            imposterBuilder
                .AddPropertyImposter(property)
                .AddInterfaceSetupMember(
                    targetProperty.Setup,
                    property.SetupName,
                    property.ImposterBuilderInterface.Syntax
                )
                .AddMembers(PropertyGetterImposterBuilderInterfaceBuilder.Build(property))
                .AddMembers(PropertySetterImposterBuilderInterfaceBuilder.Build(property))
                .AddMember(PropertyImposterBuilderInterfaceBuilder.Build(property))
                .AddMember(PropertyImposterBuilder.Build(property));
        }
    }

    private static void BuildEventImposter(
        ImposterBuilder imposterBuilder,
        in ImposterGenerationContext imposterGenerationContext,
        in CancellationToken cancellationToken
    )
    {
        foreach (var targetEvent in imposterGenerationContext.Imposter.Events)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var @event = imposterGenerationContext.Imposter.CreateEventMetadata(targetEvent);

            imposterBuilder
                .AddEventImposter(@event)
                .AddInterfaceSetupMember(
                    targetEvent.Setup,
                    @event.SetupName,
                    @event.BuilderInterface.TypeSyntax
                )
                .AddMembers(EventImposterBuilderInterfaceBuilder.Build(@event))
                .AddMember(EventImposterBuilder.Build(@event));
        }
    }

    private static void BuildIndexerImposter(
        ImposterBuilder imposterBuilder,
        in ImposterGenerationContext imposterGenerationContext,
        in CancellationToken cancellationToken
    )
    {
        foreach (var targetIndexer in imposterGenerationContext.Imposter.Indexers)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var indexer = imposterGenerationContext.Imposter.CreateIndexerMetadata(targetIndexer);

            imposterBuilder
                .AddIndexerImposter(indexer)
                .AddInterfaceSetupMember(
                    targetIndexer.Setup,
                    indexer.Core.UniqueName,
                    indexer.BuilderInterface.TypeSyntax,
                    isSetUpByMethod: indexer.RequiresExplicitInterfaceImplementation
                )
                .AddMembers(IndexerDelegatesBuilder.Build(indexer))
                .AddMember(IndexerArgumentsBuilder.Build(indexer))
                .AddMember(IndexerArgumentsCriteriaBuilder.Build(indexer))
                .AddMember(IndexerImposterBuilder.Build(indexer))
                .AddMembers(IndexerGetterImposterBuilderInterfaceBuilder.Build(indexer))
                .AddMembers(IndexerSetterImposterBuilderInterfaceBuilder.Build(indexer))
                .AddMember(IndexerImposterBuilderInterfaceBuilder.Build(indexer));
        }
    }
}
