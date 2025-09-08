using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator;

internal class ImposterTarget
{
    internal INamedTypeSymbol TypeSymbol { get; }

    internal string FullName { get; }

    internal ImposterClass ImposterClass { get; }

    internal ImposterVerifierClass VerifierClass { get; }

    internal ImposterInstanceClass ImposterInstanceClass { get; }

    internal IReadOnlyList<ImposterTargetMethod> Methods { get; }

    internal ImposterTarget(INamedTypeSymbol typeSymbol)
    {
        TypeSymbol = typeSymbol;
        FullName = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        ImposterClass = new ImposterClass(typeSymbol);
        VerifierClass = new ImposterVerifierClass(this);
        ImposterInstanceClass = new ImposterInstanceClass(this);
        Methods = GetMethods(typeSymbol, new MethodUniqueNames());
    }

    private static IReadOnlyList<ImposterTargetMethod> GetMethods(INamedTypeSymbol typeSymbol, MethodUniqueNames methodUniqueNames)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterTargetMethod(method, methodUniqueNames.GetUniqueNameForMethod(method)))
                .ToList();
        }

        throw new InvalidOperationException("Only interfaces are supported");
        // TODO Add class support.
        /*
        if (typeSymbol.TypeKind is TypeKind.Class && typeSymbol.IsSealed is false)
        { }
    */
    }
}

internal class ImposterInstanceClass
{
    internal string Name { get; }

    internal string ImposterParameterName { get; } = "imposter";

    internal string ImposterFieldName { get; }

    internal ImposterInstanceClass(ImposterTarget target)
    {
        Name = target.ImposterClass.Name + "Instance";
        ImposterFieldName = "_" + ImposterParameterName;
    }
}

internal record ImposterVerifierClass
{
    internal string Name { get; }

    internal ImposterVerifierClass(ImposterTarget target)
    {
        Name = $"{target.ImposterClass.Name}Verifier";
    }
}

internal class MethodClass
{
    public string DeclaredAsParameterName { get; }

    public string DeclaredAsFieldName { get; }

    public string Name { get; }

    public MethodClass(ImposterTargetMethod method)
    {
        Name = $"{method.UniqueName}Method";
        DeclaredAsParameterName = char.ToLower(Name[0]) + Name.Substring(1);
        DeclaredAsFieldName = "_" + DeclaredAsParameterName;
    }
}

internal class ImposterTargetMethod
{
    // As method names aren't unique in the class, we add a number at the end to make it so
    internal string UniqueName;

    internal IMethodSymbol Symbol { get; }

    internal bool HasParameters => Symbol.Parameters.Length > 0;

    internal bool HasReturnValue => !Symbol.ReturnsVoid;

    internal IReadOnlyList<ImposterReferentMethodParameter> Parameters { get; }

    internal IReadOnlyList<ImposterReferentMethodParameter> ParametersExceptOut { get; }

    internal bool HasOutParameters { get; }

    internal const string CallBeforeCallbackFieldName = "_callBefore";

    internal const string CallAfterCallbackFieldName = "_callAfter";

    internal const string ExceptionGeneratorFieldName = "_exceptionGenerator";

    internal const string ResultGeneratorFieldName = "_resultGenerator";

    internal ImposterTargetMethod(IMethodSymbol symbol, string uniqueName)
    {
        Symbol = symbol;
        Parameters = symbol.Parameters.Select(parameter => new ImposterReferentMethodParameter(parameter)).ToList();
        ParametersExceptOut = Parameters.Where(it => it.Symbol.RefKind is not RefKind.Out).ToList();
        UniqueName = uniqueName;
        MethodInvocationHistoryClassName = $"{uniqueName}MethodInvocationHistory";
        InvocationBehaviorClassName = $"{uniqueName}MethodInvocationBehaviour";
        MethodInvocationVerifierClassName = $"{uniqueName}MethodInvocationVerifier";
        DelegateName = $"{uniqueName}Delegate";
        ArgumentsClassName = $"{uniqueName}Arguments";
        ReturnTypeInFullyQualifiedFormat = symbol.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        ParametersDeclaration = string.Join(", ", Parameters.Select(parameter => $"{parameter.TypeWithRefKind} {parameter.Symbol.Name}"));
        ParametersExceptOutDeclaration = string.Join(", ", ParametersExceptOut.Select(parameter => $"{parameter.TypeWithRefKind} {parameter.Symbol.Name}"));
        ParametersPassedAsArguments = string.Join(", ", Parameters.Select(parameter => $"{parameter.RefKindPrefix} {parameter.Symbol.Name}"));
        ParametersExceptOutPassedAsArguments = string.Join(", ", ParametersExceptOut.Select(parameter => $"{parameter.RefKindPrefix} {parameter.Symbol.Name}"));
        ParametersPassedAsArgumentsWithoutRefKind = string.Join(", ", Parameters.Select(parameter => $"{parameter.Symbol.Name}"));
        ReturnType = GetReturnType(symbol);
        ReturnTypeDefaultValue = $"default({ReturnType})";
        ParametersEnclosedInArgType = string.Join(", ", Parameters.Select(p => $"{p.EnclosedInArgType} {p.EnclosedInArgName}"));
        ParametersEnclosedInArgTypePassedAsArgument = string.Join(", ", Parameters.Select(p => p.EnclosedInArgName));
        InitializeOutParametersWithDefault = GetInitializeOutParametersWithDefault();
        HasOutParameters = symbol.Parameters.Any(it => it.RefKind is RefKind.Out);
        MethodClass = new MethodClass(this);
    }

    internal MethodClass MethodClass { get; }

    internal string ParametersEnclosedInArgType { get; }

    internal string ParametersEnclosedInArgTypePassedAsArgument { get; }

    internal string ReturnTypeInFullyQualifiedFormat { get; }

    internal string MethodInvocationHistoryClassName { get; }

    internal string InvocationBehaviorClassName { get; }

    internal string MethodInvocationVerifierClassName { get; }

    internal string DelegateName { get; }

    internal string ArgumentsClassName { get; }

    internal IReadOnlyList<string> InitializeOutParametersWithDefault { get; }

    private IReadOnlyList<string> GetInitializeOutParametersWithDefault()
    {
        return
            Parameters
                .Where(parameter => parameter.Symbol.RefKind is RefKind.Out)
                .Select(parameter => $"{parameter.Symbol.Name} = default({parameter.Type});")
                .ToList();
    }

    internal string ReturnType { get; }

    internal string ReturnTypeDefaultValue { get; }

    private static string GetReturnType(IMethodSymbol method)
    {
        var formatWithNullability = new SymbolDisplayFormat(
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters
        );

        return method.ReturnsVoid ? "void" : method.ReturnType.ToDisplayString(formatWithNullability);
    }

    /// <example>
    /// Given a method <code> void InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)</code>
    /// This will contain <code> "in int input, out int output, ref int temp, params int[] parameters" </code>
    /// </example>>
    internal string ParametersDeclaration { get; }

    internal string ParametersExceptOutDeclaration { get; }

    internal string CheckParametersMatchCriteria(bool argsAreFields)
    {
        return string.Join(" && ", ParametersExceptOut.Select(parameter => $"{(argsAreFields ? parameter.EnclosedInArgNameDeclaredAsField : parameter.EnclosedInArgName)}.Predicate({parameter.Symbol.Name})"));
    }

    /// <example>
    /// Given a method <code> void InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)</code>
    /// This will contain <code> "in input, out output, ref temp, parameters" </code>
    /// </example>>
    internal string ParametersPassedAsArguments { get; }

    internal string ParametersExceptOutPassedAsArguments { get; }

    /// <example>
    /// Given a method <code> void InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)</code>
    /// This will contain <code> "input, output, temp, parameters" </code>
    /// </example>>
    internal string ParametersPassedAsArgumentsWithoutRefKind { get; }

    internal string GetSummaryTag(string description)
    {
        return $"""
                /// <summary>
                /// {description}
                /// <code>
                /// {Symbol.ToDisplayString(MethodDisplayFormatForDebuggerDisplayName)}
                /// </code>
                /// </summary>
                """;
    }

    private static readonly SymbolDisplayFormat MethodDisplayFormatForDebuggerDisplayName = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
        memberOptions: SymbolDisplayMemberOptions.IncludeParameters |
                       SymbolDisplayMemberOptions.IncludeType |
                       SymbolDisplayMemberOptions.IncludeContainingType |
                       SymbolDisplayMemberOptions.IncludeModifiers,
        parameterOptions: SymbolDisplayParameterOptions.IncludeType |
                          SymbolDisplayParameterOptions.IncludeName |
                          SymbolDisplayParameterOptions.IncludeParamsRefOut |
                          SymbolDisplayParameterOptions.IncludeExtensionThis |
                          SymbolDisplayParameterOptions.IncludeDefaultValue,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
    );
}

internal class InvocationHistoryClass
{
    internal const string ArgumentsPropertyName = "Arguments";

    internal const string ArgumentsParameterName = "arguments";

    internal const string ResultPropertyName = "Result";

    internal const string ResultParameterName = "result";
}

internal class ImposterClass
{
    internal string Name { get; }

    internal string Namespace { get; }

    internal string VerifierFieldName { get; }

    internal string VerifierParameterName { get; } = "verifier";

    internal string ImposterInstanceFieldName { get; }

    internal string ImposterInstanceParameterName { get; } = "imposterInstance";

    internal ImposterClass(INamedTypeSymbol typeSymbol)
    {
        Name = typeSymbol.Name + "Imposter";
        Namespace = $"Imposters{(typeSymbol.ContainingNamespace.IsGlobalNamespace ? string.Empty : "." + typeSymbol.ContainingNamespace.ToDisplayString())}";
        VerifierFieldName = "_" + VerifierParameterName;
        ImposterInstanceFieldName = "_" + ImposterInstanceParameterName;
    }
}

internal class MethodUniqueNames
{
    private readonly Dictionary<IMethodSymbol, string> _methodUniqueNames = new(SymbolEqualityComparer.Default);

    internal string GetUniqueNameForMethod(IMethodSymbol method)
    {
        if (_methodUniqueNames.TryGetValue(method, out var name))
        {
            return name;
        }

        var baseName = method.Name;
        var postfixCounter = 0;
        while (_methodUniqueNames.Values.Any(it => it == baseName))
        {
            baseName = $"{method.Name}_{postfixCounter}_";
        }

        _methodUniqueNames.Add(method, baseName);
        return baseName;
    }
}