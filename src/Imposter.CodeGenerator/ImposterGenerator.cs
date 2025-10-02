using System;
using System.Linq;
using System.Text;
using Imposter.CodeGenerator.Builders;
using Imposter.CodeGenerator.Builders.Arguments;
using Imposter.CodeGenerator.Builders.Delegates;
using Imposter.CodeGenerator.Builders.Imposter;
using Imposter.CodeGenerator.Builders.InvocationHistory;
using Imposter.CodeGenerator.Builders.InvocationSetup;
using Imposter.CodeGenerator.Builders.MethodImposter;
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

// Add auto-generated comment at the beggining
// Async method
// TODO Generic interface ?
// TODO Generic methods
// TODO Add benchamrks for the code generator itself.
// TODO Support async calbacks and async result generators.
// TODO Add validation that Throws and Returns are used exclusively. As well as Throws and CallAfter.
// TODO Might have to avoid using modern c# features to make it usable by projects using lower c# version
// TODO Use cancellation token
// Error CS8400 : Feature 'primary constructors' is not available in C# 8.0. Please use language version 12.0 or greater
// Error CS8400 : Feature 'file-scoped namespace' is not available in C# 8.0. Please use language version 10.0 or greater.
// Add GeneratedCode attributes
// Create builder classes similar to BlockBuilder for better perf
// Thread safety
// Cache some of the syntaxes (add benchmark to validate)
// Use fully qualified names for all the types
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
                $"{compilationContext.GeneratedCsFileUniqueName.New(imposterGenerationContext.ImposterType.Name)}.g.cs",
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
        var namespaceDeclarationBuilder = new NamespaceDeclarationSyntaxBuilder(imposterGenerationContext.ImposterComponentsNamespace);

        foreach (var method in imposterGenerationContext.Methods)
        {
            namespaceDeclarationBuilder.AddMembers(MethodDelegateTypeBuilder.Build(method));
            namespaceDeclarationBuilder.AddMembers(ArgumentsTypeGenerator.Build(method));
            namespaceDeclarationBuilder.AddMember(InvocationHistory.Build(method));
            // TODO clean it up
            var (invocationSetupBuilder, invocationSetupBuilderInterface) = InvocationSetup.Build(method);
            namespaceDeclarationBuilder.AddMember(invocationSetupBuilder);
            namespaceDeclarationBuilder.AddMember(invocationSetupBuilderInterface);
            namespaceDeclarationBuilder.AddMembers(MethodImposterBuilder.Build(method, invocationSetupBuilderInterface));
        }

        var imposterNamespace = new NamespaceDeclarationSyntaxBuilder(
                imposterGenerationContext.GenerateImposterDeclaration.PutInTheSameNamespace
                    ? imposterGenerationContext.GenerateImposterDeclaration.ImposterTarget.ContainingNamespace.ToDisplayString()
                    : imposterGenerationContext.ImposterComponentsNamespace)
            ;

        imposterNamespace.AddMember(ImposterBuilder.Build(imposterGenerationContext));

        var namespaceDeclaration = namespaceDeclarationBuilder
            .Build()
            // TODO this will cause copying of enitere namespace syntax
            .WithLeadingTrivia(Trivia(SyntaxFactoryHelper.EnableNullableTrivia()));


        /* TODO
        var imposterExtensionsNamespaceBuilder = new NamespaceDeclarationSyntaxBuilder("Imposters.Extensions")
            .AddMember(ImposterTargetExtensionsBuilder.Build(imposterGenerationContext))
            .Build();
            */

        return CompilationUnit(
            externs: List<ExternAliasDirectiveSyntax>(),
            usings: List(UsingStatements
                .Build(imposterGenerationContext.TargetSymbol.ContainingNamespace)
                .Concat([UsingDirective(ParseName(imposterGenerationContext.ImposterComponentsNamespace))])
            ),
            attributeLists: List<AttributeListSyntax>(),
            members: List<MemberDeclarationSyntax>(
                [
                    namespaceDeclaration,
                    imposterNamespace.Build()
                ]
            )
        );
    }
}