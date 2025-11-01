using System;
using System.Text;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Imposter.CodeGenerator.Features.Imposter;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.Arguments;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.Delegates;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationHistory.Collection;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationSetup;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.Collection;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.GenericInterface;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.InvocationVerifierInterface;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.NonGenericInterface;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter;
using Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Getter;
using Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Setter;
using Imposter.CodeGenerator.Features.Shared;
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

        if (generateImposterDeclaration.ImposterTarget.TypeKind != TypeKind.Interface)
        {
            throw new InvalidOperationException("TODO: Only interfaces supported");
        }

        try
        {
            var imposterGenerationContext = new ImposterGenerationContext(generateImposterDeclaration);
            sourceProductionContext.AddSource(
                $"{compilationContext.NameSet.Use(imposterGenerationContext.Imposter.Name)}.g.cs",
                SourceText.From(BuildImposter(imposterGenerationContext).NormalizeWhitespace().ToFullString(), Encoding.UTF8));
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
    }

    private static CompilationUnitSyntax BuildImposter(in ImposterGenerationContext imposterGenerationContext)
    {
        var imposterBuilder = ImposterBuilder.Create(imposterGenerationContext);

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
                .AddMember(MethodImposterCollectionBuilder.Build(method));

            // TODO clean it up
            var invocationSetupInterface = InvocationSetupBuilder.BuildInvocationSetupInterface(method);

            imposterBuilder
                .AddMember(InvocationSetupBuilder.Build(method))
                .AddMember(invocationSetupInterface)
                .AddMember(MethodImposterNonGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterInvocationVerifierInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilderInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilder.Build(method, invocationSetupInterface));
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

        var imposterNamespaceBuilder = new NamespaceDeclarationSyntaxBuilder(
            imposterGenerationContext.GenerateImposterDeclaration.PutInTheSameNamespace
                ? imposterGenerationContext.GenerateImposterDeclaration.ImposterTarget.ContainingNamespace.ToDisplayString()
                : imposterGenerationContext.ImposterComponentsNamespace);

        var imposterNamespace = imposterNamespaceBuilder
            .AddMember(imposterBuilder.Build())
            .Build()
            // TODO this will cause copying of enitere namespace syntax
            .WithLeadingTrivia(Trivia(SyntaxFactoryHelper.EnableNullableTrivia())
            );

        return CompilationUnit(
            externs: List<ExternAliasDirectiveSyntax>(),
            usings: List(UsingStatements.Build(imposterGenerationContext.TargetSymbol.ContainingNamespace)),
            attributeLists: List<AttributeListSyntax>(),
            members: List<MemberDeclarationSyntax>(
                [
                    imposterNamespace
                ]
            )
        );
    }
}