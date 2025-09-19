using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Diagnostics;
using Imposter.CodeGenerator.ImposterParts;
using Imposter.CodeGenerator.ImposterParts.InvocationSetup;
using Imposter.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Imposter.CodeGenerator;

#pragma warning disable RS1014

// Add auto-
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
[Generator]
public class ImposterGenerator : IIncrementalGenerator
{
    private const string ImposterNamespace = "Imposter";

    private static string ArgTypeFullName = typeof(Arg<>).FullName!;

    // TODO use it
    private static string MethodImplAggresiveInliningAttribute = "[MethodImpl(MethodImplOptions.AggressiveInlining)]";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
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
                    messageFormat: $"Exception: {ex} \r\n {ex.StackTrace}",
                    category: "ImposterGenerator",
                    DiagnosticSeverity.Error,
                    isEnabledByDefault: true),
                Location.None));
        }
    }

    private static void AddUsingStatementsAndNamespace(ImposterGenerationContext imposterGenerationContext)
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

        // Add the containing namespace as a using directive if it's not the global namespace
        if (!imposterGenerationContext.Target.TypeSymbol.ContainingNamespace.IsGlobalNamespace)
        {
            usingDirectives.Add(
                SyntaxFactory.UsingDirective(
                    SyntaxFactory.ParseName(imposterGenerationContext.Target.TypeSymbol.ContainingNamespace.ToDisplayString())));
        }

        // Create the namespace declaration
        var namespaceDeclaration = SyntaxFactory.FileScopedNamespaceDeclaration(SyntaxFactory.ParseName(imposterGenerationContext.Target.ImposterClass.Namespace));

        // Create the compilation unit with nullable enable directive
        var nullableEnable = SyntaxFactory.PragmaWarningDirectiveTrivia(
            SyntaxFactory.Token(SyntaxKind.DisableKeyword),
            SyntaxFactory.SeparatedList<ExpressionSyntax>().Add(SyntaxFactory.ParseExpression("nullable")),
            true);

        // Build the compilation unit with all components
        var compilationUnit = SyntaxFactory
            .CompilationUnit()
            .WithUsings(SyntaxFactory.List(usingDirectives))
            .WithMembers(SyntaxFactory.SingletonList<MemberDeclarationSyntax>(namespaceDeclaration))
            .WithLeadingTrivia(SyntaxFactory.Trivia(nullableEnable));

        // Format the syntax and append to the StringBuilder
        imposterGenerationContext.SourceBuilder.AppendLine(compilationUnit.NormalizeWhitespace().ToFullString());
    }

    private static string GenerateImposter(ImposterGenerationContext imposterGenerationContext)
    {
        var sb = imposterGenerationContext.SourceBuilder;

        AddUsingStatementsAndNamespace(imposterGenerationContext);

        foreach (var method in imposterGenerationContext.Target.Methods)
        {
            DelegateTypeGenerator.AddMethodDelegate(imposterGenerationContext, method);
            DelegateTypeGenerator.AddCallbackDelegate(imposterGenerationContext, method);
            DelegateTypeGenerator.AddExceptionGeneratorDelegate(imposterGenerationContext, method);
            ArgumentsTypeGenerator.AddArgumentsType(imposterGenerationContext, method);
            ArgumentsTypeGenerator.AddArgArgumentsType(imposterGenerationContext, method);
            MethodInvocationHistoryTypeGenerator.AddMethodInvocationHistoryClass(imposterGenerationContext, method);
            InvocationSetupBuilder.AddInvocationSetupBuilder(imposterGenerationContext, method);
            AddMethodClass(imposterGenerationContext, method);
            AddMethodInvocationVerifierClass(imposterGenerationContext, method);
        }

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
            imposterGenerationContext.SourceBuilder.AppendLine($"    private readonly {method.MethodClass.Name} {method.MethodClass.DeclaredAsFieldName};");
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
            imposterGenerationContext.SourceBuilder.AppendLine($"       {method.MethodClass.DeclaredAsFieldName} = new();");
        }

        imposterGenerationContext.SourceBuilder.AppendLine($$"""
                                                                    {{referent.ImposterClass.VerifierFieldName}} = new({{string.Join(",", referent.Methods.Select(it => it.MethodClass.DeclaredAsFieldName))}});
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
                                    {{method.MethodClass.DeclaredAsFieldName}}.Behaviours.Add(invocationBehaviour);
                                    return invocationBehaviour;
                                }
                                
                            """);
        }

        // Add as an inner class
        AddImposterInstanceClass(imposterGenerationContext);

        sb.AppendLine("}");

        sb.AppendLine("#nullable restore");
    }

    private static void AddMethodClass(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        // todo
        aa
        var initializeOutParameters = method.InitializeOutParametersWithDefault.Aggregate(new StringBuilder(), (acc, cur) => acc.AppendLine(cur));
        var createArgumentClassInstance = $"new {method.ArgumentsClassName}({method.ParametersPassedAsArgumentsWithoutRefKind})";

        imposterGenerationContext.SourceBuilder
            .AppendLine($$$"""
                           {{{method.GetSummaryTag("method class")}}}
                           public class {{{method.MethodClass.Name}}}
                           {
                               public List<{{{method.InvocationsSetupBuilder}}}> Behaviours { get; } = new();
                               public List<{{{method.MethodInvocationHistoryClassName}}}> InvocationHistory { get; } = new();
                               
                               public {{{method.ReturnType}}} Invoke({{{method.ParametersDeclaration}}})
                               {

                                   try
                                   {
                                       {{{method.InvocationsSetupBuilder}}}? matchingBehaviour = null;
                                       foreach(var behaviour in Enumerable.Reverse(Behaviours))
                                       {
                                           if(behaviour.Matches({{{method.ParametersExceptOutPassedAsArguments}}}))
                                           {
                                              matchingBehaviour = behaviour;
                                              break;
                                           }
                                       }
                                       {{{(method.HasReturnValue ? $"{method.ReturnType} result;" : string.Empty)}}}
                                       
                                       if(matchingBehaviour is not null)
                                       {
                                       {{{$"   {(method.HasReturnValue ? "result = " : string.Empty)}matchingBehaviour.Execute({method.ParametersPassedAsArguments});"}}}
                                       }
                                       else
                                       {
                                           {{{initializeOutParameters}}}
                                           {{{(method.HasReturnValue ? $"result = {method.ReturnTypeDefaultValue};" : string.Empty)}}}
                                       }
                                       
                                       InvocationHistory.Add(new {{{method.MethodInvocationHistoryClassName}}}({{{(method.HasParameters ? createArgumentClassInstance + "," : string.Empty)}}}{{{(method.HasReturnValue ? "result, " : string.Empty)}}}null));
                                       {{{(method.HasReturnValue ? "return result;" : string.Empty)}}}
                                   }
                                   catch (Exception ex)
                                   {
                                       {{{initializeOutParameters}}}
                                       InvocationHistory.Add(new {{{method.MethodInvocationHistoryClassName}}}({{{(method.HasParameters ? createArgumentClassInstance + "," : string.Empty)}}}{{{(method.HasReturnValue ? $"{method.ReturnTypeDefaultValue}, " : string.Empty)}}}ex));
                                       throw;
                                   }
                               }
                           }

                           """);
    }

    private static void AddMethodInvocationVerifierClass(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        var sb = imposterGenerationContext.SourceBuilder;

        var constructorParameters = new[] { new ConstructorParameter(method.MethodClass.Name, method.MethodClass.DeclaredAsParameterName) }
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
                                var invocationCount = {{method.MethodClass.DeclaredAsFieldName}}.InvocationHistory.Count({{(method.ParametersExceptOut.Count > 0 ? "it => Matches(it.Arguments)" : string.Empty)}});
                            
                                if (!count.Matches(invocationCount))
                                {
                                    throw new VerificationFailedException("TODO");
                                }
                            }

                        """);

        if (method.ParametersExceptOut.Count > 0)
        {
            sb.AppendLine($$"""
                                public bool Matches({{method.ArgumentsClassName}} arguments)
                                {
                                    return {{string.Join(" && ", method.ParametersExceptOut.Select(parameter => $"{parameter.EnclosedInArgNameDeclaredAsField}.Matches(arguments.{parameter.Symbol.Name})"))}};
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
                              referent.Methods.Select(method => new ConstructorParameter(method.MethodClass.Name, method.MethodClass.DeclaredAsParameterName)).ToList(),
                              numberOfTabs: 1)}}

                          """);

        foreach (var method in referent.Methods)
        {
            sb.AppendLine($$"""
                                public {{method.MethodInvocationVerifierClassName}} {{method.Symbol.Name}}({{method.ParametersEnclosedInArgType}})
                                      => new({{method.MethodClass.DeclaredAsFieldName}}{{(method.Parameters.Any() ? $", {method.ParametersEnclosedInArgTypePassedAsArgument}" : string.Empty)}});
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
                                        {{(method.HasReturnValue ? "return " : string.Empty)}}{{referent.ImposterInstanceClass.ImposterFieldName}}.{{method.MethodClass.DeclaredAsFieldName}}.Invoke({{method.ParametersPassedAsArguments}});
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