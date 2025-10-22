using System;
using System.Text;
using Imposter.CodeGenerator.Builders;
using Imposter.CodeGenerator.Builders.Arguments;
using Imposter.CodeGenerator.Builders.Delegates;
using Imposter.CodeGenerator.Builders.Imposter;
using Imposter.CodeGenerator.Builders.InvocationHistory;
using Imposter.CodeGenerator.Builders.InvocationHistory.Collection;
using Imposter.CodeGenerator.Builders.InvocationHistory.Interface;
using Imposter.CodeGenerator.Builders.InvocationSetup;
using Imposter.CodeGenerator.Builders.MethodImposter;
using Imposter.CodeGenerator.Builders.MethodImposter.Collection;
using Imposter.CodeGenerator.Builders.MethodImposter.GenericInterface;
using Imposter.CodeGenerator.Builders.MethodImposter.ImposterBuilderInterface;
using Imposter.CodeGenerator.Builders.MethodImposter.InvocationVerifierInterface;
using Imposter.CodeGenerator.Builders.MethodImposter.NonGenericInterface_;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Imposter.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator;

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
        CompilationContext compilationContext)
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
                $"{compilationContext.GeneratedCsFileUniqueName.New(imposterGenerationContext.Imposter.Name)}.g.cs",
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
        }
    }

    private static CompilationUnitSyntax BuildImposter(ImposterGenerationContext imposterGenerationContext)
    {
        var imposter = ImposterBuilder.Build(imposterGenerationContext);

        foreach (var method in imposterGenerationContext.Imposter.Methods)
        {
            imposter
                .AddMembers(MethodDelegateTypeBuilder.Build(method))
                .AddMemberIfNotNull(ArgumentsBuilder.Build(method))
                .AddMemberIfNotNull(ArgumentsCriteriaBuilder.Build(method))
                .AddMember(InvocationHistoryInterfaceBuilder.Build(method))
                .AddMember(InvocationHistoryBuilder.Build(method))
                .AddMember(InvocationHistoryCollectionBuilder.Build(method))
                .AddMemberIf(method.Symbol.IsGenericMethod, () => MethodImposterCollectionBuilder.Build(method));

            // TODO clean it up
            var invocationSetup = InvocationSetupBuilder.Build(method);
            var invocationSetupInterface = InvocationSetupBuilder.BuildInvocationSetupInterface(method);
            
            imposter
                .AddMember(invocationSetup)
                .AddMember(invocationSetupInterface)
                .AddMember(MethodImposterNonGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterGenericInterfaceBuilder.Build(method))
                .AddMember(MethodImposterInvocationVerifierInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilderInterfaceBuilder.Build(method))
                .AddMember(MethodImposterBuilder.Build(method, invocationSetupInterface));
        }

        var imposterNamespaceBuilder = new NamespaceDeclarationSyntaxBuilder(
            imposterGenerationContext.GenerateImposterDeclaration.PutInTheSameNamespace
                ? imposterGenerationContext.GenerateImposterDeclaration.ImposterTarget.ContainingNamespace.ToDisplayString()
                : imposterGenerationContext.ImposterComponentsNamespace);

        var imposterNamespace = imposterNamespaceBuilder
            .AddMember(imposter.Build())
            .Build()
            // TODO this will cause copying of enitere namespace syntax
            .WithLeadingTrivia(Trivia(SyntaxFactoryHelper.EnableNullableTrivia()));

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