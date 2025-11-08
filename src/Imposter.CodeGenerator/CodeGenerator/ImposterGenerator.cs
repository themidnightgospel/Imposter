using System;
using System.Text;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Imposter.CodeGenerator.Features.EventImposter.Builders;
using Imposter.CodeGenerator.Features.Imposter;
using Imposter.CodeGenerator.Features.Imposter.ImposterExtensions;
using Imposter.CodeGenerator.Features.IndexerImposter.Builders;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.Arguments;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.Delegates;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationHistory.Collection;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Collection;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.GenericInterface;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.InvocationVerifierInterface;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.NonGenericInterface;
using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter;
using Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Getter;
using Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Setter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.CodeGenerator;

#pragma warning disable RS1014

[Generator]
public class ImposterGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.ReportDiagnostics(context.GetCompilationDiagnostics());
        context.RegisterSourceOutput(context
                .GetGenerateImposterDeclarations()
                .Combine(context.GetCompilationContext()),
            (sourceProductionContext, contexts) => GenerateImposter(sourceProductionContext, contexts.Left, contexts.Right));
    }

    private static void GenerateImposter(
        SourceProductionContext sourceProductionContext,
        GenerateImposterDeclaration generateImposterDeclaration,
        in CompilationContext compilationContext)
    {
        if (sourceProductionContext.CancellationToken.IsCancellationRequested)
        {
            return;
        }
        
        if (!IsInterfaceOrNonSealedClass(generateImposterDeclaration.ImposterTarget))
        {
            var targetLocation = generateImposterDeclaration.ImposterTarget.Locations.Length > 0
                ? generateImposterDeclaration.ImposterTarget.Locations[0]
                : Location.None;

            var targetDisplayName = generateImposterDeclaration.ImposterTarget.ToDisplayString();

            sourceProductionContext.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptors.ImposterTargetMustBeInterface,
                    targetLocation,
                    targetDisplayName,
                    generateImposterDeclaration.ImposterTarget.TypeKind));
            return;
        }

        if (generateImposterDeclaration.ImposterTarget.TypeKind == TypeKind.Class &&
            !HasAccessibleConstructor(generateImposterDeclaration.ImposterTarget))
        {
            var targetLocation = generateImposterDeclaration.ImposterTarget.Locations.Length > 0
                ? generateImposterDeclaration.ImposterTarget.Locations[0]
                : Location.None;

            sourceProductionContext.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor,
                    targetLocation,
                    generateImposterDeclaration.ImposterTarget.ToDisplayString()));
            return;
        }

        try
        {
            var supportedCSharpFeatures = new SupportedCSharpFeatures(compilationContext.Compilation);
            var imposterGenerationContext = new ImposterGenerationContext(generateImposterDeclaration, supportedCSharpFeatures);
            sourceProductionContext.AddSource(
                $"{compilationContext.NameSet.Use(imposterGenerationContext.Imposter.Name)}.g.cs",
                SourceText.From(
                    BuildImposter(imposterGenerationContext).NormalizeWhitespace().ToFullString(),
                    Encoding.UTF8));
        }
        // TODO
        catch (Exception ex)
        {
            sourceProductionContext.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor(
                    id: "IMP001",
                    title: "Generator crash",
                    messageFormat: $"Exception: {ex.Message} {ex.StackTrace.Replace("\r", " ").Replace("\n", " ")}",
                    category: "ImposterGenerator",
                    DiagnosticSeverity.Error,
                    isEnabledByDefault: true),
                Location.None));
            throw;
        }
        
        bool IsInterfaceOrNonSealedClass(INamedTypeSymbol typeSymbol)
            => typeSymbol.TypeKind == TypeKind.Interface || typeSymbol is { TypeKind: TypeKind.Class, IsSealed: false };

        bool HasAccessibleConstructor(INamedTypeSymbol typeSymbol)
        {
            if (typeSymbol.InstanceConstructors.Length == 0)
            {
                return true;
            }

            foreach (var constructor in typeSymbol.InstanceConstructors)
            {
                if (constructor.DeclaredAccessibility is Accessibility.Private)
                {
                    continue;
                }

                return true;
            }

            return false;
        }
    }

    private static CompilationUnitSyntax BuildImposter(in ImposterGenerationContext imposterGenerationContext)
    {
        var imposterBuilder = ImposterBuilder.Create(imposterGenerationContext);
        var targetNamespaceName = imposterGenerationContext.GenerateImposterDeclaration.ImposterTarget.ContainingNamespace.ToDisplayString();
        var imposterNamespaceName = imposterGenerationContext.GenerateImposterDeclaration.PutInTheSameNamespace
            ? targetNamespaceName
            : imposterGenerationContext.ImposterComponentsNamespace;

        foreach (var method in imposterGenerationContext.Imposter.Methods)
        {
            // TODO move all this to MethodSetupBuilder
            imposterBuilder
                .AddMembers(MethodDelegateTypeBuilder.Build(method))
                .AddMember(ArgumentsBuilder.Build(method))
                .AddMember(ArgumentsCriteriaBuilder.Build(method))
                .AddMember(InvocationHistoryInterfaceBuilder.Build(method))
                .AddMember(InvocationHistoryBuilder.Build(method))
                .AddMember(InvocationHistoryCollectionBuilder.Build(method))
                .AddMember(MethodImposterCollectionBuilder.Build(method))
                .AddMember(InvocationSetupBuilder.Build(method))
                .AddMember(InvocationSetupBuilder.BuildInvocationSetupInterface(method))
                .AddMember(MethodImposterNonGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterInvocationVerifierInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilderInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilder.Build(method));
        }

        foreach (var propertySymbol in imposterGenerationContext.Imposter.PropertySymbols)
        {
            var property = imposterGenerationContext.Imposter.CreatePropertyMetadata(propertySymbol);

            imposterBuilder
                .AddPropertyImposter(property)
                .AddMember(PropertyGetterImposterBuilderInterfaceBuilder.Build(property))
                .AddMember(PropertySetterImposterBuilderInterfaceBuilder.Build(property))
                .AddMember(PropertyImposterBuilderInterfaceBuilder.Build(property))
                .AddMember(PropertyImposterBuilder.Build(property));
        }

        foreach (var eventSymbol in imposterGenerationContext.Imposter.EventSymbols)
        {
            var @event = imposterGenerationContext.Imposter.CreateEventMetadata(eventSymbol);

            imposterBuilder
                .AddEventImposter(@event)
                .AddMember(EventImposterBuilderInterfaceBuilder.Build(@event))
                .AddMember(EventImposterBuilderBuilder.Build(@event));
        }

        foreach (var indexerSymbol in imposterGenerationContext.Imposter.IndexerSymbols)
        {
            var indexer = imposterGenerationContext.Imposter.CreateIndexerMetadata(indexerSymbol);

            imposterBuilder
                .AddIndexerImposter(indexer)
                .AddMembers(IndexerDelegatesBuilder.Build(indexer))
                .AddMember(IndexerArgumentsBuilder.Build(indexer))
                .AddMember(IndexerArgumentsCriteriaBuilder.Build(indexer))
                .AddMember(IndexerImposterBuilderBuilder.Build(indexer))
                .AddMember(IndexerGetterImposterBuilderInterfaceBuilder.Build(indexer))
                .AddMember(IndexerSetterImposterBuilderInterfaceBuilder.Build(indexer))
                .AddMember(IndexerImposterBuilderInterfaceBuilder.Build(indexer));
        }

        var imposterNamespaceBuilder = new NamespaceDeclarationSyntaxBuilder(imposterNamespaceName);

        imposterNamespaceBuilder.AddMember(imposterBuilder.Build());

        if (imposterGenerationContext.SupportedCSharpFeatures.SupportsTypeExtensions)
        {
            imposterNamespaceBuilder.AddMember(ImposterExtensionsBuilder.Build(
                imposterGenerationContext,
                imposterNamespaceName));
        }

        var imposterNamespace = imposterNamespaceBuilder
            .Build()
            // TODO this will cause copying of enitere namespace syntax
            .WithLeadingTrivia(Trivia(SyntaxFactoryHelper.EnableNullableTrivia()));

        return CompilationUnit(
            externs: List<ExternAliasDirectiveSyntax>(),
            usings: List(UsingStatements.Build(imposterGenerationContext.TargetSymbol.ContainingNamespace)),
            attributeLists: List<AttributeListSyntax>(),
            members: List<MemberDeclarationSyntax>([
                    imposterNamespace
                ]
            )
        );
    }
}
