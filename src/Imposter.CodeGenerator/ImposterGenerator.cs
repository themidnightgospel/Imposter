using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.ImposterParts;
using Imposter.CodeGenerator.ImposterParts.InvocationSetup;
using Imposter.CodeGenerator.ImposterParts.MethodType;
using Imposter.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

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
    private const string ImposterNamespace = "Imposter";

    private static string ArgTypeFullName = typeof(Arg<>).FullName!;

    // TODO use it
    private static string MethodImplAggresiveInliningAttribute = "[MethodImpl(MethodImplOptions.AggressiveInlining)]";

    // TODO do we need it ?
    private const string EmptyArgumentsClass =
        /* lang=c# */
        $$"""
          namespace {{ImposterNamespace}}
          {    
              internal class EmptyArguments
              {
                  internal EmptyArguments Instance = new EmptyArguments();
                  
                  private EmptyArguments()
                  {}
              }
          }
          """;

    private const string EmptyArgArgumentsClass =
        /* lang=c# */
        $$"""
          namespace {{ImposterNamespace}}
          {    
              internal class EmptyArgArguments
              {
                  internal EmptyArgArguments Instance = new EmptyArgArguments();
                  
                  private EmptyArgArguments()
                  {}
              }
          }
          """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static initializationContext =>
        {
            initializationContext.AddSource("EmptyArguments.cs", EmptyArgumentsClass);
            initializationContext.AddSource("EmptyArgArguments.cs", EmptyArgArgumentsClass);
        });
        context.ReportDiagnostics(context.GetCompilationDiagnostics());
        context.RegisterSourceOutput(context.GetGenerateImposterDeclarations(), GenerateImposter);
    }

    private static void GenerateImposter(SourceProductionContext sourceProductionContext, GenerateImposterDeclaration generateImposterDeclaration)
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
            var imposterGenerationContext = new ImposterGenerationContext(new ImposterTarget((INamedTypeSymbol)generateImposterDeclaration.ImposterTarget), new StringBuilder());
            var sourceText = GenerateImposter(imposterGenerationContext);
            sourceProductionContext.AddSource($"{imposterGenerationContext.Target.ImposterClass.Name}.g.cs", SourceText.From(sourceText, Encoding.UTF8));
            // Note: In the future, we'll fully transition to SyntaxFactory and return CompilationUnitSyntax instead of a string
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

    private static CompilationUnitSyntax CreateUsingStatementsAndNamespace(ImposterGenerationContext imposterGenerationContext)
    {
        // Create using directives
        var usingDirectives = new List<UsingDirectiveSyntax>
        {
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Threading.Tasks")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Diagnostics")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Runtime.CompilerServices")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("Imposter.Abstractions")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Concurrent")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(ImposterNamespace))
        };

        if (!imposterGenerationContext.Target.TypeSymbol.ContainingNamespace.IsGlobalNamespace)
        {
            usingDirectives.Add(
                SyntaxFactory.UsingDirective(
                    SyntaxFactory.ParseName(imposterGenerationContext.Target.TypeSymbol.ContainingNamespace.ToDisplayString())));
        }

        var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(imposterGenerationContext.Target.ImposterClass.Namespace));

        // TODO move to factory helper
        var nullableEnable = SyntaxFactory.PragmaWarningDirectiveTrivia(
            SyntaxFactory.Token(SyntaxKind.DisableKeyword),
            SyntaxFactory.SeparatedList<ExpressionSyntax>().Add(SyntaxFactory.ParseExpression("nullable")),
            true);

        return SyntaxFactory
            .CompilationUnit()
            .WithUsings(SyntaxFactory.List(usingDirectives))
            .WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(namespaceDeclaration))
            .WithLeadingTrivia(SyntaxFactory.Trivia(nullableEnable));
    }

    private static string GenerateImposter(ImposterGenerationContext imposterGenerationContext)
    {
        var sb = imposterGenerationContext.SourceBuilder;

        var imposterCompilationUnit = CreateUsingStatementsAndNamespace(imposterGenerationContext);

        foreach (var method in imposterGenerationContext.Target.Methods)
        {
            imposterCompilationUnit = imposterCompilationUnit.AddMembers(MethodDelegateTypeBuilder.BuildDelegateTypeDeclarations(method).ToArray());
            imposterCompilationUnit = imposterCompilationUnit.AddMembers(ArgumentsTypeGenerator.GetArgumentsType(method).ToArray());
            imposterCompilationUnit = imposterCompilationUnit.AddMembers(MethodInvocationHistoryTypeGenerator.GetMethodInvocationHistoryClass(method));
            imposterCompilationUnit = imposterCompilationUnit.AddMembers(InvocationSetupBuilder.GetInvocationSetupBuilder(method));
            imposterCompilationUnit = imposterCompilationUnit.AddMembers(MethodImposterBuilder.Build(method));

            AddMethodInvocationVerifierClass(imposterGenerationContext, method);
        }


        // TODO
        sb.Append(imposterCompilationUnit.NormalizeWhitespace().ToFullString());

        AddImposterVerifierClass(imposterGenerationContext);
        AddImposterClass(imposterGenerationContext);

        return sb.ToString();
    }

    private static void AddImposterClass(ImposterGenerationContext imposterGenerationContext)
    {
        var sb = imposterGenerationContext.SourceBuilder;
        var referent = imposterGenerationContext.Target;

        // Generate main imposter class

        imposterGenerationContext
            .SourceBuilder
            .AppendLine($$"""
                          public class {{referent.ImposterClass.Name}} : IHaveImposterVerifier<{{referent.VerifierClass.Name}}>, IHaveImposterInstance<{{referent.FullName}}>
                          {
                          """);

        foreach (var method in referent.Methods)
        {
            imposterGenerationContext.SourceBuilder.AppendLine($"    private readonly {method.MethodImposter.Name} {method.MethodImposter.DeclaredAsFieldName};");
        }

        imposterGenerationContext.SourceBuilder.AppendLine($"    private readonly {referent.VerifierClass.Name} {referent.ImposterClass.VerifierFieldName};");
        imposterGenerationContext.SourceBuilder.AppendLine($"    private readonly {referent.FullName} {referent.ImposterClass.ImposterInstanceFieldName};");

        imposterGenerationContext
            .SourceBuilder
            .AppendLine(
                $$"""

                      public {{referent.ImposterClass.Name}}()
                      {
                  """);

        foreach (var method in referent.Methods)
        {
            imposterGenerationContext.SourceBuilder.AppendLine($"       {method.MethodImposter.DeclaredAsFieldName} = new();");
        }

        imposterGenerationContext.SourceBuilder.AppendLine($$"""
                                                                    {{referent.ImposterClass.VerifierFieldName}} = new({{string.Join(",", referent.Methods.Select(it => it.MethodImposter.DeclaredAsFieldName))}});
                                                                    {{referent.ImposterClass.ImposterInstanceFieldName}} = new {{referent.ImposterInstanceClass.Name}}(this);
                                                                 }

                                                             """);

        // Generate explicit interface implementations
        imposterGenerationContext
            .SourceBuilder
            .AppendLine(
                $$"""
                      {{referent.FullName}} IHaveImposterInstance<{{referent.FullName}}>.Instance() => {{referent.ImposterClass.ImposterInstanceFieldName}};

                      {{referent.VerifierClass.Name}} IHaveImposterVerifier<{{referent.VerifierClass.Name}}>.Verify() => {{referent.ImposterClass.VerifierFieldName}};

                  """);

        // Generate setup methods
        foreach (var method in imposterGenerationContext.Target.Methods)
        {
            sb.AppendLine($$"""
                                public {{method.InvocationsSetupBuilder}} {{method.Symbol.Name}}({{method.ParametersEnclosedInArgType}})
                                {
                                    var invocationBehaviour = new {{method.InvocationsSetupBuilder}}({{method.ParametersEnclosedInArgTypePassedAsArgument}});
                                    {{method.MethodImposter.DeclaredAsFieldName}}.Behaviours.Add(invocationBehaviour);
                                    return invocationBehaviour;
                                }
                                
                            """);
        }

        // Add as an inner class
        AddImposterInstanceClass(imposterGenerationContext);

        sb.AppendLine("}");

        sb.AppendLine("#nullable restore");
    }

    private static void AddMethodInvocationVerifierClass(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        var sb = imposterGenerationContext.SourceBuilder;

        var constructorParameters = new[] { new ConstructorParameter(method.MethodImposter.Name, method.MethodImposter.DeclaredAsParameterName) }
            .Concat(method
                .Parameters
                .Select(it => new ConstructorParameter(it.EnclosedInArgType, it.EnclosedInArgName)));

        sb.AppendLine($$"""
                        {{method.GetSummaryTag("invocation verifier class")}}
                        public class {{method.MethodInvocationVerifierClassName}}
                        {             
                        {{GenerateFieldsAndInitializeFromConstructor(method.MethodInvocationVerifierClassName, constructorParameters.ToList(), numberOfTabs: 1)}}
                            
                            public void WasInvoked(InvocationCount count)
                            {
                                var invocationCount = {{method.MethodImposter.DeclaredAsFieldName}}.InvocationHistory.Count({{(method.ParametersExceptOut__.Count > 0 ? "it => Matches(it.Arguments)" : string.Empty)}});
                            
                                if (!count.Matches(invocationCount))
                                {
                                    throw new VerificationFailedException("TODO");
                                }
                            }

                        """);

        if (method.ParametersExceptOut__.Count > 0)
        {
            sb.AppendLine($$"""
                                public bool Matches({{method.ArgumentsClassName}} arguments)
                                {
                                    return {{string.Join(" && ", method.ParametersExceptOut__.Select(parameter => $"{parameter.EnclosedInArgNameDeclaredAsField}.Matches(arguments.{parameter.Symbol.Name})"))}};
                                }
                            """);
        }

        sb.AppendLine("}");
        sb.AppendLine();
    }

    private static void AddImposterVerifierClass(ImposterGenerationContext imposterGenerationContext)
    {
        var referent = imposterGenerationContext.Target;
        var sb = imposterGenerationContext.SourceBuilder;

        imposterGenerationContext.SourceBuilder
            .AppendLine($$"""
                          public class {{imposterGenerationContext.Target.VerifierClass.Name}}
                          {
                          {{GenerateFieldsAndInitializeFromConstructor(
                              imposterGenerationContext.Target.VerifierClass.Name,
                              referent.Methods.Select(method => new ConstructorParameter(method.MethodImposter.Name, method.MethodImposter.DeclaredAsParameterName)).ToList(),
                              numberOfTabs: 1)}}

                          """);

        foreach (var method in referent.Methods)
        {
            sb.AppendLine($$"""
                                public {{method.MethodInvocationVerifierClassName}} {{method.Symbol.Name}}({{method.ParametersEnclosedInArgType}})
                                      => new({{method.MethodImposter.DeclaredAsFieldName}}{{(method.Parameters.Any() ? $", {method.ParametersEnclosedInArgTypePassedAsArgument}" : string.Empty)}});
                            """);
        }

        sb.AppendLine("}");
        sb.AppendLine();
    }

    private static void AddImposterInstanceClass(ImposterGenerationContext imposterGenerationContext)
    {
        var referent = imposterGenerationContext.Target;

        imposterGenerationContext
            .SourceBuilder
            .AppendLine($$"""
                          public class {{referent.ImposterInstanceClass.Name}} : {{referent.FullName}}
                          {
                            private readonly {{referent.ImposterClass.Name}} {{referent.ImposterInstanceClass.ImposterFieldName}};
                            public {{referent.ImposterInstanceClass.Name}}({{referent.ImposterClass.Name}} {{referent.ImposterInstanceClass.ImposterParameterName}})
                            {
                                {{referent.ImposterInstanceClass.ImposterFieldName}} = {{referent.ImposterInstanceClass.ImposterParameterName}};
                            }
                            
                          """);


        foreach (var method in referent.Methods)
        {
            imposterGenerationContext
                .SourceBuilder
                .AppendLine($$"""
                                    public {{method.ReturnType}} {{method.Symbol.Name}}({{method.ParametersDeclaration}})
                                    {
                                        {{(method.HasReturnValue ? "return " : string.Empty)}}{{referent.ImposterInstanceClass.ImposterFieldName}}.{{method.MethodImposter.DeclaredAsFieldName}}.Invoke({{method.ParametersPassedAsArguments}});
                                    }
                              """);
        }

        imposterGenerationContext
            .SourceBuilder
            .AppendLine("    }");
    }

    private record ConstructorParameter(string Type, string Name, bool DeclareAsField = true);

    private static string GenerateFieldsAndInitializeFromConstructor(
        string className,
        IReadOnlyList<ConstructorParameter> constructorParameters,
        int numberOfTabs)
    {
        var sb = new StringBuilder();
        var tabsPrefix = new string(' ', numberOfTabs * 4);

        void AppendLine(string line)
        {
            sb.AppendLine($"{tabsPrefix}{line}");
        }

        // Fields
        foreach (var param in constructorParameters)
        {
            if (param.DeclareAsField)
            {
                AppendLine($"private readonly {param.Type} _{param.Name};");
            }
            else
            {
                AppendLine($"internal {param.Type} {param.Name} {{ get; }}");
            }
        }

        sb.AppendLine();

        // Constructor signature
        AppendLine($"public {className}(");
        AppendLine("    " + string.Join(", ", constructorParameters.Select(p => $"{p.Type} {p.Name}")));
        AppendLine(")");
        AppendLine("{");
        // Assignments
        foreach (var param in constructorParameters)
        {
            AppendLine($"    this.{(param.DeclareAsField ? "_" : string.Empty)}{param.Name} = {param.Name};");
        }

        AppendLine("}");

        return sb.ToString();
    }
}

internal record ImposterGenerationContext(ImposterTarget Target, StringBuilder SourceBuilder);