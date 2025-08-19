using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Imposter.CodeGenerator;

[Generator]
public class ImposterGenerator : IIncrementalGenerator
{
    private const string ImposterNamespace = "Imposter";

    private const string GenerateImposterAttributeShortName = "GenerateImposter";

    private const string GenerateImposterAttributeName = "GenerateImposterAttribute";

    private const string GenerateImposterAttributeFullName = $"{ImposterNamespace}.{GenerateImposterAttributeName}";

    private const string GenerateImposterAttributeSourceText =
        /*lang=c#*/
        $$"""
          using System;
                  
          namespace {{ImposterNamespace}};

          [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
          public class {{GenerateImposterAttributeName}} : Attribute
          {
              public Type Type { get; }

              public GenerateImposterAttribute(Type type)
              {
                  Type = type;
              }
          }
          """;

    private const string ArgSourceText =
        /*lang=c#*/
        /*lang=c#*/
        $$"""
          using System;

          namespace {{ImposterNamespace}};

          public class Arg<T>(Func<T, bool> predicate)
          {
              public Func<T, bool> Predicate = predicate;

              public static implicit operator Arg<T>(T value)
              {
                  // TODO possible null check
                  return new Arg<T>(t => t.Equals(value));
              }

              public static Arg<T> Is(Func<T, bool> predicate) => new(predicate);
              
              public static Arg<T> Is(T? value) => Is(it => it?.Equals(value) == true);
          
              public static Arg<T> IsDefault() => Is(default(T));
          
              public static Arg<T> Any = new(_ => true);

              // TODO Add more utility factory methods similar to Moq.It
          } 
          """;
    
    // TODO split into 3
    private const string ImposterVerifierAndInstanceSourceText =
        /*lang=c#*/
        $$"""
          using System;
                  
          namespace {{ImposterNamespace}};

          public interface IHaveImposterVerifier<TVerifier>
          {
              TVerifier Verify();
          }
          
          public interface IHaveImposterInstance<TInstance>
          {
              TInstance Instance();
          }
          
          public static class ImposterExtensions
          {
              public static TVerifier Verify<TVerifier>(this IHaveImposterVerifier<TVerifier> imposter)
                  => imposter.Verify();
          
              public static TInstance Instance<TInstance>(this IHaveImposterInstance<TInstance> imposter)
                  => imposter.Instance();
          }
          """;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register the attribute
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource(
                "GenerateImposterAttribute.g.cs",
                SourceText.From(GenerateImposterAttributeSourceText, Encoding.UTF8));

            ctx.AddSource(
                "Arg.g.cs",
                SourceText.From(ArgSourceText, Encoding.UTF8));
            
            ctx.AddSource(
                "ImposterExtensions.g.cs",
                SourceText.From(ImposterVerifierAndInstanceSourceText, Encoding.UTF8));
        });

        var syntaxProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
            .Where(static m => m is not null);

        context.RegisterSourceOutput(syntaxProvider,
            static (spc, source) => Generate(source!, spc));
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node) =>
        node is AttributeSyntax attributeSyntax &&
        attributeSyntax.Name.ToString() is GenerateImposterAttributeShortName or GenerateImposterAttributeName;

    private static ITypeSymbol? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var attributeSyntax = (AttributeSyntax)context.Node;

        if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol
            || attributeSymbol.ContainingType.ToDisplayString() is not GenerateImposterAttributeFullName)
        {
            return null;
        }

        return GetTypeArgument();

        ITypeSymbol? GetTypeArgument()
        {
            if (attributeSyntax.ArgumentList?.Arguments.FirstOrDefault()?.Expression is TypeOfExpressionSyntax
                typeOfExpr)
            {
                if (context.SemanticModel.GetSymbolInfo(typeOfExpr.Type).Symbol is ITypeSymbol typeSymbol)
                {
                    return typeSymbol;
                }
            }

            return null;
        }
    }

    private static void Generate(ITypeSymbol typeSymbol, SourceProductionContext context)
    {
        if (typeSymbol.TypeKind != TypeKind.Interface)
        {
            // TODO move to diagnostic reporter
            context.ReportDiagnostic(Diagnostic.Create(
                new DiagnosticDescriptor(
                    "IMPOSTER001",
                    "Type must be an interface",
                    "Type '{0}' is not an interface",
                    "Imposter",
                    DiagnosticSeverity.Error,
                    true),
                Location.None,
                typeSymbol.Name));
            return;
        }

        var interfaceSymbol = (INamedTypeSymbol)typeSymbol;
        var sourceText = GenerateImposterClass(interfaceSymbol);
        var fileName = $"{GetImposterClassName(interfaceSymbol)}.g.cs";

        context.AddSource(fileName, SourceText.From(sourceText, Encoding.UTF8));
    }
    
    private static string GenerateImposterClass(INamedTypeSymbol interfaceSymbol)
    {
        var imposterMethodClassNames = new ImposterMethodClassNames();

        var className = GetImposterClassName(interfaceSymbol);
        var namespaceName =
            $"Imposters{(interfaceSymbol.ContainingNamespace.IsGlobalNamespace ? string.Empty : "." + interfaceSymbol.ContainingNamespace.ToDisplayString())}";

        var sb = new StringBuilder();

        // Add using statements
        sb.AppendLine($$"""
                  using System;
                  using System.Linq;
                  using System.Collections.Generic;
                  using System.Threading.Tasks;
                  using System.Diagnostics;
                  using {{ImposterNamespace}};
                  """);
        if (!interfaceSymbol.ContainingNamespace.IsGlobalNamespace)
        {
            sb.AppendLine($"using {interfaceSymbol.ContainingNamespace.ToDisplayString()};");
        }
        sb.AppendLine();

        sb.AppendLine("""
                      #nullable enable
                      
                      namespace {namespaceName};
                      
                      """);

        // Generate behavior classes for each method
        var methods = GetAllInterfaceMethods(interfaceSymbol);
        foreach (var method in methods)
        {
            GenerateMethodBehaviorClasses(sb, method, imposterMethodClassNames);
        }
        
        // Generate verifier class
        GenerateVerifierClass(sb, className, methods, imposterMethodClassNames);

        // Generate main imposter class
        sb.AppendLine($"public class {className} : IHaveImposterVerifier<{className}Verifier>, IHaveImposterInstance<{interfaceSymbol.Name}>");
        sb.AppendLine("{");

        // Generate fields
        sb.AppendLine($"    private readonly {className}Instance _imposterInstance;");
        sb.AppendLine($"    private readonly {className}Verifier _verifier;");

        foreach (var method in methods)
        {
            var methodBehaviorName = imposterMethodClassNames.GetMethodBehaviorClassName(method);
            sb.AppendLine($"    private readonly {methodBehaviorName} _{GetMethodBehaviorFieldName(method)};");
        }

        sb.AppendLine();

        // Generate explicit interface implementations
        sb.AppendLine($"    {interfaceSymbol.Name} IHaveImposterInstance<{interfaceSymbol.Name}>.Instance() => _imposterInstance;");
        sb.AppendLine($"    {className}Verifier IHaveImposterVerifier<{className}Verifier>.Verify() => _verifier;");
        sb.AppendLine();

        // Generate constructor
        sb.AppendLine($"    public {className}()");
        sb.AppendLine("    {");
        sb.AppendLine($"        _imposterInstance = new {className}Instance(this);");

        foreach (var method in methods)
        {
            var methodBehaviorName = imposterMethodClassNames.GetMethodBehaviorClassName(method);
            sb.AppendLine($"        _{GetMethodBehaviorFieldName(method)} = new {methodBehaviorName}();");
        }

        sb.AppendLine(
            $"        _verifier = new {className}Verifier({string.Join(", ", (IEnumerable<string>)methods.Select(m => $"_{GetMethodBehaviorFieldName(m)}"))});");
        sb.AppendLine("    }");
        sb.AppendLine();

        // Generate setup methods
        foreach (var method in methods)
        {
            GenerateSetupMethod(sb, method, imposterMethodClassNames);
        }

        // Generate inner instance class
        GenerateInstanceClass(sb, className, interfaceSymbol, methods);

        sb.AppendLine("}");

        sb.AppendLine("#nullable restore");

        return sb.ToString();
    }

    private static readonly SymbolDisplayFormat MethodDisplayFormatForDebuggerDisplayName = new SymbolDisplayFormat(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        memberOptions: SymbolDisplayMemberOptions.IncludeParameters |
                       SymbolDisplayMemberOptions.IncludeType |
                       SymbolDisplayMemberOptions.IncludeContainingType |
                       SymbolDisplayMemberOptions.IncludeModifiers,
        parameterOptions: SymbolDisplayParameterOptions.IncludeType |
                          SymbolDisplayParameterOptions.IncludeName,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
    );

    private static string GetDebuggerDisplayAttributeForMethodClass(IMethodSymbol method, string classDescription)
    {
        return
            $"[DebuggerDisplay(\"'{method.ToDisplayString(MethodDisplayFormatForDebuggerDisplayName)}' {classDescription}\")]";
    }

    internal class ImposterMethod
    {
        internal IMethodSymbol Method { get; }
        
        internal ImposterMethodClassNames ClassNames { get; }
        
        public ImposterMethod(IMethodSymbol method)
        {
            Method = method;
            ClassNames = new ImposterMethodClassNames(method);
        }
    }

    private static void GenerateMethodBehaviorClasses(StringBuilder sb, IMethodSymbol method,
        ImposterMethodClassNames imposterMethodClassNames)
    {
        var behaviorClassName = imposterMethodClassNames.GetMethodBehaviorClassName(method);
        var invocationClassName = imposterMethodClassNames.GetMethodInvocationClassName(method);
        var invocationBehaviorClassName = imposterMethodClassNames.GetMethodInvocationBehaviorClassName(method);
        var verifierClassName = imposterMethodClassNames.GetMethodInvocationVerifierClassName(method);

        var parameters = method.Parameters;
        var parameterTuple = GetParameterTupleType(parameters);
        var returnType = GetReturnType(method);
        var hasParameters = parameters.Length > 0;

        // Generate invocation class
        GenerateMethodInvocationClass();

        // Generate invocation behavior class
        GenerateMethodInvocationBehaviourClass();

        // Generate method behavior class
        GenerateMethodBehaviourClass();

        // Generate verifier class for this method
        GenerateMethodInvocationVerifierClass();

        void GenerateMethodInvocationClass()
        {
            sb.Append($$"""
                          {{GetDebuggerDisplayAttributeForMethodClass(method, "invocation class")}}
                          public class {{invocationClassName}}
                          {
                              public {{invocationClassName}}(
                          """);
            if (hasParameters)
            {
                sb.Append($"({parameterTuple}) arguments, ");
            }

            var hasReturn = !method.ReturnsVoid;
            if (hasReturn)
            {
                sb.Append($"{returnType} result, ");
            }

            sb.Append("Exception? exception");
            sb.AppendLine($")");

            sb.AppendLine("    {");
            if (hasParameters)
            {
                sb.AppendLine("        Arguments = arguments;");
            }

            if (hasReturn)
            {
                sb.AppendLine("        Result = result;");
            }

            sb.AppendLine("        Exception = exception;");
            sb.AppendLine("    }");
            sb.AppendLine();

            if (hasParameters)
            {
                sb.AppendLine($"    public ({parameterTuple}) Arguments {{ get; }}");
            }

            if (!method.ReturnsVoid)
            {
                sb.AppendLine($"    public {GetNullableReturnType(method)} Result {{ get; }}");
            }

            sb.AppendLine("    public Exception? Exception { get; }");
            sb.AppendLine("}");
            sb.AppendLine();
        }

        void GenerateMethodInvocationBehaviourClass()
        {
            sb.AppendLine(GetDebuggerDisplayAttributeForMethodClass(method, "invocation behaviour class"));
            sb.AppendLine($"public class {invocationBehaviorClassName}");
            sb.AppendLine("{");
            if (hasParameters)
            {
                sb.AppendLine($"    private readonly Func<{parameterTuple}, bool> _parameterCriteria;");
                if (!method.ReturnsVoid)
                {
                    sb.AppendLine($"    private Func<{parameterTuple}, {returnType}>? _resultGenerator;");
                }
            }
            else
            {
                sb.AppendLine($"    private readonly Func<bool> _parameterCriteria;");
                if (!method.ReturnsVoid)
                {
                    sb.AppendLine($"    private Func<{returnType}>? _resultGenerator;");
                }
            }

            sb.AppendLine("    private Func<Exception>? _exceptionGenerator;");
            sb.AppendLine();

            if (hasParameters)
            {
                sb.AppendLine(
                    $"    public {invocationBehaviorClassName}(Func<{parameterTuple}, bool> parameterCriteria)");
            }
            else
            {
                sb.AppendLine($"    public {invocationBehaviorClassName}(Func<bool> parameterCriteria)");
            }

            sb.AppendLine("    {");
            sb.AppendLine("        _parameterCriteria = parameterCriteria;");
            sb.AppendLine("    }");
            sb.AppendLine();

            if (!method.ReturnsVoid)
            {
                if (hasParameters)
                {
                    sb.AppendLine(
                        $"    public {invocationBehaviorClassName} Returns(Func<{parameterTuple}, {returnType}> resultGenerator)");
                    sb.AppendLine("    {");
                    sb.AppendLine("        _resultGenerator = resultGenerator;");
                    sb.AppendLine("        return this;");
                    sb.AppendLine("    }");
                    sb.AppendLine();
                }
                else
                {
                    sb.AppendLine(
                        $"    public {invocationBehaviorClassName} Returns(Func<{returnType}> resultGenerator)");
                    sb.AppendLine("    {");
                    sb.AppendLine("        _resultGenerator = resultGenerator;");
                    sb.AppendLine("        return this;");
                    sb.AppendLine("    }");
                    sb.AppendLine();
                }

                sb.AppendLine($"    public {invocationBehaviorClassName} Returns({returnType} result)");
                sb.AppendLine("    {");
                if (hasParameters)
                {
                    sb.AppendLine("        _resultGenerator = _ => result;");
                }
                else
                {
                    sb.AppendLine("        _resultGenerator = () => result;");
                }

                sb.AppendLine("        return this;");
                sb.AppendLine("    }");
                sb.AppendLine();
            }

            sb.AppendLine(
                $"    public {invocationBehaviorClassName} Throws<TException>() where TException : Exception, new()");
            sb.AppendLine("    {");
            sb.AppendLine("        _exceptionGenerator = () => new TException();");
            sb.AppendLine("        return this;");
            sb.AppendLine("    }");
            sb.AppendLine();

            if (hasParameters)
            {
                sb.AppendLine(
                    $"    public bool Matches({parameterTuple} parameters) => _parameterCriteria(parameters);");
            }
            else
            {
                sb.AppendLine($"    public bool Matches() => _parameterCriteria();");
            }

            sb.AppendLine();

            if (!method.ReturnsVoid)
            {
                if (hasParameters)
                {
                    sb.AppendLine($"    public {returnType} Execute({parameterTuple} parameters)");
                    sb.AppendLine("    {");
                    sb.AppendLine("        if (_exceptionGenerator != null)");
                    sb.AppendLine("            throw _exceptionGenerator();");
                    sb.AppendLine();
                    sb.AppendLine("        if (_resultGenerator != null)");
                    sb.AppendLine("            return _resultGenerator(parameters);");
                    sb.AppendLine();
                    sb.AppendLine($"        return default({returnType});");
                    sb.AppendLine("    }");
                }
                else
                {
                    sb.AppendLine($"    public {returnType} Execute()");
                    sb.AppendLine("    {");
                    sb.AppendLine("        if (_exceptionGenerator != null)");
                    sb.AppendLine("            throw _exceptionGenerator();");
                    sb.AppendLine();
                    sb.AppendLine("        if (_resultGenerator != null)");
                    sb.AppendLine("            return _resultGenerator();");
                    sb.AppendLine();
                    sb.AppendLine($"        return default({returnType});");
                    sb.AppendLine("    }");
                }
            }
            else
            {
                if (hasParameters)
                {
                    sb.AppendLine($"    public void Execute({parameterTuple} parameters)");
                    sb.AppendLine("    {");
                    sb.AppendLine("        if (_exceptionGenerator != null)");
                    sb.AppendLine("            throw _exceptionGenerator();");
                    sb.AppendLine("    }");
                }
                else
                {
                    sb.AppendLine($"    public void Execute()");
                    sb.AppendLine("    {");
                    sb.AppendLine("        if (_exceptionGenerator != null)");
                    sb.AppendLine("            throw _exceptionGenerator();");
                    sb.AppendLine("    }");
                }
            }

            sb.AppendLine("}");
            sb.AppendLine();
        }

        void GenerateMethodBehaviourClass()
        {
            sb.AppendLine(GetDebuggerDisplayAttributeForMethodClass(method, "behaviour class"));
            sb.AppendLine($"public class {behaviorClassName}");
            sb.AppendLine("{");
            sb.AppendLine($"    public List<{invocationBehaviorClassName}> InvocationBehaviours {{ get; }} = new();");
            sb.AppendLine($"    public List<{invocationClassName}> Invocations {{ get; }} = new();");
            sb.AppendLine();

            if (returnType != "void")
            {
                sb.AppendLine($"    public {returnType} Invoke({GetParameterList(parameters, false)})");
                sb.AppendLine("    {");
                if (hasParameters)
                {
                    sb.AppendLine($"        var arguments = {GetParameterTupleConstruction(parameters)};");
                    sb.AppendLine();
                    sb.AppendLine("        try");
                    sb.AppendLine("        {");
                    sb.AppendLine(
                        "            var matchingBehaviour = InvocationBehaviours.FirstOrDefault(b => b.Matches(arguments));");
                    sb.AppendLine("            if (matchingBehaviour != null)");
                    sb.AppendLine("            {");
                    sb.AppendLine("                var result = matchingBehaviour.Execute(arguments);");
                    sb.AppendLine(
                        $"                Invocations.Add(new {invocationClassName}(arguments, result, null));");
                    sb.AppendLine("                return result;");
                    sb.AppendLine("            }");
                    sb.AppendLine();
                    sb.AppendLine($"            var defaultResult = default({returnType});");
                    sb.AppendLine(
                        $"            Invocations.Add(new {invocationClassName}(arguments, defaultResult, null));");
                    sb.AppendLine("            return defaultResult;");
                    sb.AppendLine("        }");
                    sb.AppendLine("        catch (Exception ex)");
                    sb.AppendLine("        {");
                    sb.AppendLine(
                        $"            Invocations.Add(new {invocationClassName}(arguments, default({returnType}), ex));");
                    sb.AppendLine("            throw;");
                    sb.AppendLine("        }");
                }
                else
                {
                    sb.AppendLine("        try");
                    sb.AppendLine("        {");
                    sb.AppendLine(
                        "            var matchingBehaviour = InvocationBehaviours.FirstOrDefault(b => b.Matches());");
                    sb.AppendLine("            if (matchingBehaviour != null)");
                    sb.AppendLine("            {");
                    sb.AppendLine("                var result = matchingBehaviour.Execute();");
                    sb.AppendLine($"                Invocations.Add(new {invocationClassName}(result, null));");
                    sb.AppendLine("                return result;");
                    sb.AppendLine("            }");
                    sb.AppendLine();
                    sb.AppendLine($"            var defaultResult = default({returnType});");
                    sb.AppendLine($"            Invocations.Add(new {invocationClassName}(defaultResult, null));");
                    sb.AppendLine("            return defaultResult;");
                    sb.AppendLine("        }");
                    sb.AppendLine("        catch (Exception ex)");
                    sb.AppendLine("        {");
                    sb.AppendLine(
                        $"            Invocations.Add(new {invocationClassName}(default({returnType}), ex));");
                    sb.AppendLine("            throw;");
                    sb.AppendLine("        }");
                }

                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine($"    public void Invoke({GetParameterList(parameters, false)})");
                sb.AppendLine("    {");
                if (hasParameters)
                {
                    sb.AppendLine($"        var arguments = {GetParameterTupleConstruction(parameters)};");
                    sb.AppendLine();
                    sb.AppendLine("        try");
                    sb.AppendLine("        {");
                    sb.AppendLine(
                        "            var matchingBehaviour = InvocationBehaviours.FirstOrDefault(b => b.Matches(arguments));");
                    sb.AppendLine("            matchingBehaviour?.Execute(arguments);");
                    sb.AppendLine($"            Invocations.Add(new {invocationClassName}(arguments, null));");
                    sb.AppendLine("        }");
                    sb.AppendLine("        catch (Exception ex)");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            Invocations.Add(new {invocationClassName}(arguments, ex));");
                    sb.AppendLine("            throw;");
                    sb.AppendLine("        }");
                }
                else
                {
                    sb.AppendLine("        try");
                    sb.AppendLine("        {");
                    sb.AppendLine(
                        "            var matchingBehaviour = InvocationBehaviours.FirstOrDefault(b => b.Matches());");
                    sb.AppendLine("            matchingBehaviour?.Execute();");
                    sb.AppendLine($"            Invocations.Add(new {invocationClassName}(null));");
                    sb.AppendLine("        }");
                    sb.AppendLine("        catch (Exception ex)");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            Invocations.Add(new {invocationClassName}(ex));");
                    sb.AppendLine("            throw;");
                    sb.AppendLine("        }");
                }

                sb.AppendLine("    }");
            }

            sb.AppendLine("}");
            sb.AppendLine();
        }

        void GenerateMethodInvocationVerifierClass()
        {
            sb.AppendLine(GetDebuggerDisplayAttributeForMethodClass(method, "invocation verifier class"));
            sb.AppendLine($"public class {verifierClassName}");
            sb.AppendLine("{");
            sb.AppendLine($"    private readonly {behaviorClassName} _{GetMethodBehaviorFieldName(method)};");
            if (hasParameters)
            {
                sb.AppendLine($"    private readonly Func<{parameterTuple}, bool> _parameterCriteria;");
                sb.AppendLine();
                sb.AppendLine(
                    $"    public {verifierClassName}({behaviorClassName} {GetMethodBehaviorFieldName(method)}, Func<{parameterTuple}, bool> parameterCriteria)");
                sb.AppendLine("    {");
                sb.AppendLine($"        _{GetMethodBehaviorFieldName(method)} = {GetMethodBehaviorFieldName(method)};");
                sb.AppendLine("        _parameterCriteria = parameterCriteria;");
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine($"    private readonly Func<bool> _parameterCriteria;");
                sb.AppendLine();
                sb.AppendLine(
                    $"    public {verifierClassName}({behaviorClassName} {GetMethodBehaviorFieldName(method)}, Func<bool> parameterCriteria)");
                sb.AppendLine("    {");
                sb.AppendLine($"        _{GetMethodBehaviorFieldName(method)} = {GetMethodBehaviorFieldName(method)};");
                sb.AppendLine("        _parameterCriteria = parameterCriteria;");
                sb.AppendLine("    }");
            }

            sb.AppendLine();
            sb.AppendLine("    public void Once()");
            sb.AppendLine("    {");
            if (hasParameters)
            {
                sb.AppendLine(
                    $"        var matchingInvocations = _{GetMethodBehaviorFieldName(method)}.Invocations.Where(i => _parameterCriteria(i.Arguments)).ToList();");
            }
            else
            {
                sb.AppendLine(
                    $"        var matchingInvocations = _{GetMethodBehaviorFieldName(method)}.Invocations.Where(i => _parameterCriteria()).ToList();");
            }

            sb.AppendLine("        if (matchingInvocations.Count != 1)");
            sb.AppendLine(
                $"            throw new Exception($\"Expected 1 invocation but found {{matchingInvocations.Count}}\");");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public void Never()");
            sb.AppendLine("    {");
            if (hasParameters)
            {
                sb.AppendLine(
                    $"        var matchingInvocations = _{GetMethodBehaviorFieldName(method)}.Invocations.Where(i => _parameterCriteria(i.Arguments)).ToList();");
            }
            else
            {
                sb.AppendLine(
                    $"        var matchingInvocations = _{GetMethodBehaviorFieldName(method)}.Invocations.Where(i => _parameterCriteria()).ToList();");
            }

            sb.AppendLine("        if (matchingInvocations.Count > 0)");
            sb.AppendLine(
                $"            throw new Exception($\"Expected 0 invocations but found {{matchingInvocations.Count}}\");");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public void Times(int expectedCount)");
            sb.AppendLine("    {");
            if (hasParameters)
            {
                sb.AppendLine(
                    $"        var matchingInvocations = _{GetMethodBehaviorFieldName(method)}.Invocations.Where(i => _parameterCriteria(i.Arguments)).ToList();");
            }
            else
            {
                sb.AppendLine(
                    $"        var matchingInvocations = _{GetMethodBehaviorFieldName(method)}.Invocations.Where(i => _parameterCriteria()).ToList();");
            }

            sb.AppendLine("        if (matchingInvocations.Count != expectedCount)");
            sb.AppendLine(
                $"            throw new Exception($\"Expected {{expectedCount}} invocations but found {{matchingInvocations.Count}}\");");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine();
        }
    }

    private static void GenerateVerifierClass(StringBuilder sb, string className, IList<IMethodSymbol> methods, ImposterMethodClassNames imposterMethodClassNames)
    {
        sb.AppendLine($"public class {className}Verifier");
        sb.AppendLine("{");

        foreach (var method in methods)
        {
            var methodBehaviorName = imposterMethodClassNames.GetMethodBehaviorClassName(method);
            sb.AppendLine($"    private readonly {methodBehaviorName} _{GetMethodBehaviorFieldName(method)};");
        }

        sb.AppendLine();

        sb.AppendLine(
            $"    public {className}Verifier({string.Join(", ", methods.Select(m => $"{imposterMethodClassNames.GetMethodBehaviorClassName(m)} {GetMethodBehaviorFieldName(m)}"))})");
        sb.AppendLine("    {");

        foreach (var method in methods)
        {
            sb.AppendLine($"        _{GetMethodBehaviorFieldName(method)} = {GetMethodBehaviorFieldName(method)};");
        }

        sb.AppendLine("    }");
        sb.AppendLine();

        foreach (var method in methods)
        {
            GenerateVerifierMethod(sb, method, imposterMethodClassNames);
        }

        sb.AppendLine("}");
        sb.AppendLine();
    }

    private static void GenerateVerifierMethod(StringBuilder sb, IMethodSymbol method,
        ImposterMethodClassNames imposterMethodClassNames)
    {
        var methodName = method.Name;
        var verifierClassName = imposterMethodClassNames.GetMethodInvocationVerifierClassName(method);
        var parameters = method.Parameters;
        var argParameters = GetArgParameterList(parameters);

        sb.AppendLine($"    public {verifierClassName} {methodName}({argParameters})");
        sb.AppendLine(
            $"        => new(_{GetMethodBehaviorFieldName(method)}, {GetParameterCriteriaLambda(parameters)});");
        sb.AppendLine();
    }

    private static void GenerateSetupMethod(StringBuilder sb, IMethodSymbol method,
        ImposterMethodClassNames imposterMethodClassNames)
    {
        var methodName = method.Name;
        var invocationBehaviorClassName = imposterMethodClassNames.GetMethodInvocationBehaviorClassName(method);
        var parameters = method.Parameters;
        var argParameters = GetArgParameterList(parameters);

        sb.AppendLine($"    public {invocationBehaviorClassName} {methodName}({argParameters})");
        sb.AppendLine("    {");
        sb.AppendLine(
            $"        var invocationBehaviour = new {invocationBehaviorClassName}({GetParameterCriteriaLambda(parameters)});");
        sb.AppendLine($"        _{GetMethodBehaviorFieldName(method)}.InvocationBehaviours.Add(invocationBehaviour);");
        sb.AppendLine("        return invocationBehaviour;");
        sb.AppendLine("    }");
        sb.AppendLine();
    }

    private static void GenerateInstanceClass(StringBuilder sb, string className, INamedTypeSymbol interfaceSymbol,
        IList<IMethodSymbol> methods)
    {
        sb.AppendLine(
            $"    public class {className}Instance({className} behaviour) : {interfaceSymbol.Name}");
        sb.AppendLine("    {");

        foreach (var method in methods)
        {
            GenerateInstanceMethod(sb, method);
        }

        sb.AppendLine("    }");
    }

    private static void GenerateInstanceMethod(StringBuilder sb, IMethodSymbol method)
    {
        var returnType = GetReturnType(method);
        var methodName = method.Name;
        var parameters = method.Parameters;
        var parameterList = GetParameterList(parameters, true);
        var parameterNames = GetParameterNames(parameters);

        sb.AppendLine($"        public {returnType} {methodName}({parameterList})");
        sb.AppendLine("        {");

        if (returnType == "void")
        {
            sb.AppendLine($"            behaviour._{GetMethodBehaviorFieldName(method)}.Invoke({parameterNames});");
        }
        else
        {
            sb.AppendLine(
                $"            return behaviour._{GetMethodBehaviorFieldName(method)}.Invoke({parameterNames});");
        }

        sb.AppendLine("        }");
        sb.AppendLine();
    }

    // Helper methods
    private static string GetImposterClassName(INamedTypeSymbol interfaceSymbol)
    {
        var interfaceName = interfaceSymbol.Name;
        return interfaceName.StartsWith("I") ? interfaceName.Substring(1) + "Imposter" : interfaceName + "Imposter";
    }

    public class ImposterMethodInfo
    {
        public IMethodSymbol Symbol;

        public ImposterMethodInfo(IMethodSymbol symbol)
        {
            Symbol = symbol;
        }
        
        
    }

    public class ImposterInterfaceInfo
    {
        public INamedTypeSymbol Imposter;

        public IReadOnlyList<IMethodSymbol> Methods;
        
        public ImposterInterfaceInfo(INamedTypeSymbol imposter)
        {
            Imposter = imposter;
            Methods = GetAllInterfaceMethods(imposter);
        }
        
        private static IReadOnlyList<IMethodSymbol> GetAllInterfaceMethods(INamedTypeSymbol interfaceSymbol)
        {
            var methods = new List<IMethodSymbol>();

            // Get methods from this interface
            methods.AddRange(interfaceSymbol.GetMembers().OfType<IMethodSymbol>()
                .Where(m => m.MethodKind == MethodKind.Ordinary));

            // Get methods from base interfaces
            foreach (var baseInterface in interfaceSymbol.AllInterfaces)
            {
                methods.AddRange(baseInterface.GetMembers().OfType<IMethodSymbol>()
                    .Where(m => m.MethodKind == MethodKind.Ordinary));
            }

            return methods;
        }
    }

    private static string GetReturnType(IMethodSymbol method)
    {
        if (method.ReturnsVoid)
            return "void";

        return method.ReturnType.ToDisplayString();
    }

    private static string GetParameterList(ImmutableArray<IParameterSymbol> parameters, bool includeModifiers)
    {
        if (parameters.Length == 0)
            return "";

        return string.Join(", ", parameters.Select(p =>
        {
            var result = "";
            if (includeModifiers)
            {
                if (p.RefKind == RefKind.In) result += "in ";
                else if (p.RefKind == RefKind.Out) result += "out ";
                else if (p.RefKind == RefKind.Ref) result += "ref ";

                if (p.IsParams) result += "params ";
            }

            result += p.Type.ToDisplayString();
            if (includeModifiers && p.HasExplicitDefaultValue && p.ExplicitDefaultValue == null)
                result += "?";

            result += " " + p.Name;

            if (includeModifiers && p.HasExplicitDefaultValue)
            {
                if (p.ExplicitDefaultValue == null)
                    result += " = null";
                else if (p.ExplicitDefaultValue is string str)
                    result += $" = \"{str}\"";
                else
                    result += $" = {p.ExplicitDefaultValue}";
            }

            return result;
        }));
    }

    private static string GetParameterTupleType(ImmutableArray<IParameterSymbol> parameters)
    {
        // For no parameters, we don't use tuples - we just use special handling
        if (parameters.Length == 0)
            return "object"; // Placeholder, won't be used for 0 parameters

        if (parameters.Length == 1)
            return parameters[0].Type.ToDisplayString();

        var types = parameters.Select(p => p.Type.ToDisplayString());
        return $"({string.Join(", ", types)})";
    }

    private static string GetParameterTupleConstruction(ImmutableArray<IParameterSymbol> parameters)
    {
        // For no parameters, this won't be called
        if (parameters.Length == 0)
            return ""; // This shouldn't be used

        if (parameters.Length == 1)
            return parameters[0].Name;

        var names = parameters.Select(p => p.Name);
        return $"({string.Join(", ", names)})";
    }

    private static string GetParameterNames(ImmutableArray<IParameterSymbol> parameters)
    {
        return string.Join(", ", parameters.Select(p => p.Name));
    }

    private static string GetArgParameterList(ImmutableArray<IParameterSymbol> parameters)
    {
        if (parameters.Length == 0)
            return "";

        return string.Join(", ", parameters.Select(p => $"Arg<{p.Type.ToDisplayString()}> {p.Name}Criteria"));
    }

    private static string GetParameterCriteriaLambda(ImmutableArray<IParameterSymbol> parameters)
    {
        if (parameters.Length == 0)
            return "() => true";

        if (parameters.Length == 1)
            return $"{parameters[0].Name}Criteria.Predicate";

        var paramNames = string.Join(", ", parameters.Select(p => p.Name));
        var conditions = string.Join(" && ", parameters.Select(p => $"{p.Name}Criteria.Predicate({p.Name})"));
        return $"({paramNames}) => {conditions}";
    }


    private static string GetMethodBehaviorFieldName(IMethodSymbol method)
    {
        var baseName =
            $"{char.ToLower(method.Name[0])}{method.Name.Substring(1)}{GetMethodSignatureSuffix(method)}MethodBehaviour";
        return baseName;
    }

    private static string GetMethodSignatureSuffix(IMethodSymbol method)
    {
        if (method.Parameters.Length == 0)
            return "";

        // Create a suffix based on parameter types to make overloads unique
        var parameterTypes = method.Parameters.Select(p =>
        {
            var typeName = p.Type.Name;
            // Handle generic types and arrays
            if (p.Type is IArrayTypeSymbol arrayType)
            {
                typeName = arrayType.ElementType.Name + "Array";
            }
            else if (p.Type is INamedTypeSymbol namedType && namedType.IsGenericType)
            {
                typeName = namedType.Name + "Of" + string.Join("", namedType.TypeArguments.Select(ta => ta.Name));
            }

            // Remove special characters that aren't valid in C# identifiers
            typeName = typeName.Replace("?", "Nullable").Replace("<", "").Replace(">", "").Replace("[]", "Array");

            return typeName;
        });

        return string.Join("", parameterTypes);
    }

    internal class ImposterMethodClassNames
    {
        private readonly Dictionary<IMethodSymbol, string> _methodBaseNames = new();

        private string GetMethodClassBaseName(IMethodSymbol method)
        {
            if (_methodBaseNames.TryGetValue(method, out var name))
            {
                return name;
            }

            var baseName = method.Name;
            var postfixCounter = 0;
            while (_methodBaseNames.Values.Any(it => it == baseName))
            {
                baseName = $"{method.Name}_{postfixCounter}_";
            }

            _methodBaseNames.Add(method, baseName);
            return baseName;
        }

        internal string GetMethodInvocationClassName(IMethodSymbol method)
            => $"{GetMethodClassBaseName(method)}MethodInvocation";

        internal string GetMethodInvocationBehaviorClassName(IMethodSymbol method)
            => $"{GetMethodClassBaseName(method)}MethodInvocationBehaviour";

        internal string GetMethodInvocationVerifierClassName(IMethodSymbol method)
            => $"{GetMethodClassBaseName(method)}MethodInvocationVerifier";

        internal string GetMethodBehaviorClassName(IMethodSymbol method)
            => $"{GetMethodClassBaseName(method)}MethodBehaviour";
    }

    // Add this new helper method
    private static string GetNullableReturnType(IMethodSymbol method)
    {
        if (method.ReturnsVoid)
            return "void";

        return method.ReturnType.NullableAnnotation switch
        {
            NullableAnnotation.Annotated => method.ReturnType.ToDisplayString(),
            NullableAnnotation.NotAnnotated => method.ReturnType.ToDisplayString() + "?",
            _ => method.ReturnType.ToDisplayString()
        };
    }
}