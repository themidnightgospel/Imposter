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
        context.ReportDiagnostics(context.GetCompilationDiagnostics());

        context.RegisterSourceOutput(
            context.GetGenerateImposterDeclarations().Combine(context.GetCompilationContext()),
            (sourceProductionContext, contexts) =>
                GenerateImposter(sourceProductionContext, contexts.Left, contexts.Right)
        );
    }

    private static void GenerateImposter(
        in SourceProductionContext sourceProductionContext,
        GenerateImposterDeclaration generateImposterDeclaration,
        in CompilationContext compilationContext
    )
    {
        if (sourceProductionContext.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (!ImposterTargetValidator.Validate(sourceProductionContext, generateImposterDeclaration))
        {
            return;
        }

        try
        {
            var supportedCSharpFeatures = new SupportedCSharpFeatures(
                compilationContext.Compilation
            );
            var logger = GeneratorLoggerFactory.Create(
                sourceProductionContext,
                compilationContext.IsLoggingEnabled
            );
            var imposterGenerationContext = new ImposterGenerationContext(
                generateImposterDeclaration,
                supportedCSharpFeatures,
                logger
            );

            logger.LogSupportedCSharpFeatures(supportedCSharpFeatures);
            logger.LogCompilation(compilationContext.Compilation);

            sourceProductionContext.AddSource(
                $"{compilationContext.NameSet.Use(imposterGenerationContext.Imposter.Name)}.g.cs",
                // NOTE: NormalizeWhitespace has a performance impact.
                SourceText.From(
                    BuildImposter(
                            imposterGenerationContext,
                            sourceProductionContext.CancellationToken
                        )
                        .NormalizeWhitespace()
                        .ToFullString(),
                    Encoding.UTF8
                )
            );
        }
        catch (Exception ex)
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

        var imposterNamespaceBuilder = new NamespaceDeclarationSyntaxBuilder(
            imposterGenerationContext.ImposterNamespaceName
        );

        imposterNamespaceBuilder.AddMember(imposterBuilder.Build());

#if ROSLYN4_14_OR_GREATER
        if (imposterGenerationContext.SupportedCSharpFeatures.SupportsTypeExtensions)
        {
            imposterNamespaceBuilder.AddMember(
                ImposterExtensionsBuilder.Build(
                    imposterGenerationContext,
                    imposterGenerationContext.ImposterNamespaceName
                )
            );

            imposterGenerationContext.Logger.Log("Generated imposter extensions.");
        }
        else
        {
            imposterGenerationContext.Logger.Log(
                "Skipping generation of Imposter Extensions because the current C# version does not support type extensions."
            );
        }
#else
        imposterGenerationContext.Logger.Log(
            "Skipping generation of Imposter Extensions because it requires ROSLYN 4.14 or greater."
        );
#endif

        var imposterNamespace = imposterNamespaceBuilder.Build();

        var compilationUnit = CompilationUnit(
            externs: List<ExternAliasDirectiveSyntax>(),
            usings: List(
                UsingStatements.Build(imposterGenerationContext.TargetSymbol.ContainingNamespace)
            ),
            attributeLists: List<AttributeListSyntax>(),
            members: List<MemberDeclarationSyntax>([imposterNamespace])
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
                    method => method.Symbol.MetadataName,
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
        foreach (
            var propertySymbol in imposterGenerationContext
                .Imposter.PropertySymbols.OrderBy(
                    property => property.MetadataName,
                    StringComparer.Ordinal
                )
                .ThenBy(
                    property => property.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    StringComparer.Ordinal
                )
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var property = imposterGenerationContext.Imposter.CreatePropertyMetadata(
                propertySymbol,
                imposterBuilder.MemberNameSet
            );

            imposterBuilder
                .AddPropertyImposter(property)
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
        foreach (
            var eventSymbol in imposterGenerationContext
                .Imposter.EventSymbols.OrderBy(
                    @event => @event.MetadataName,
                    StringComparer.Ordinal
                )
                .ThenBy(
                    @event => @event.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    StringComparer.Ordinal
                )
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var @event = imposterGenerationContext.Imposter.CreateEventMetadata(eventSymbol);

            imposterBuilder
                .AddEventImposter(@event)
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
        foreach (
            var indexerSymbol in imposterGenerationContext
                .Imposter.IndexerSymbols.OrderBy(
                    indexer => indexer.MetadataName,
                    StringComparer.Ordinal
                )
                .ThenBy(
                    indexer => indexer.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                    StringComparer.Ordinal
                )
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var indexer = imposterGenerationContext.Imposter.CreateIndexerMetadata(indexerSymbol);

            imposterBuilder
                .AddIndexerImposter(indexer)
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
