using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator;

internal class ImposterReferent
{
    internal INamedTypeSymbol TypeSymbol { get; }

    internal ImposterClass ImposterClass { get; }

    internal IReadOnlyList<ImposterReferentMethod> Methods { get; }

    internal ImposterReferent(INamedTypeSymbol typeSymbol)
    {
        TypeSymbol = typeSymbol;
        ImposterClass = new ImposterClass(typeSymbol);
        Methods = GetMethods(typeSymbol, new MethodUniqueNames());
    }

    private static IReadOnlyList<ImposterReferentMethod> GetMethods(INamedTypeSymbol typeSymbol, MethodUniqueNames methodUniqueNames)
    {
        if (typeSymbol.TypeKind is TypeKind.Interface)
        {
            return typeSymbol
                .GetAllInterfaceMethods()
                .Select(method => new ImposterReferentMethod(method, methodUniqueNames.GetUniqueNameForMethod(method)))
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

internal class ImposterReferentMethodParameter(IParameterSymbol symbol)
{
    public IParameterSymbol Symbol { get; } = symbol;

    private static readonly SymbolDisplayFormat FullyQualifiedIncludingNullabilityFormat = new SymbolDisplayFormat(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
    );

    private static string GetParameterEnclosedInArgType(IParameterSymbol parameter)
    {
        var argType = parameter.RefKind switch
        {
            RefKind.Out => "OutArg",
            _ => "Arg"
        };

        return $"{argType}<{parameter.Type.ToDisplayString(FullyQualifiedIncludingNullabilityFormat)}>";
    }

    internal string TypeWithRefKind { get; } =
        symbol.IsParams
            ? "params "
            : string.Empty
              + symbol.RefKind switch
              {
                  RefKind.Out => "out ",
                  RefKind.Ref => "ref ",
                  RefKind.In => "in ",
                  _ => string.Empty
              }
              + symbol.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);


    public string EnclosedInArgType { get; } = GetParameterEnclosedInArgType(symbol);

    public string EnclosedInArgName { get; } = $"{symbol.Name}Arg";
}

internal class ImposterReferentMethod
{
    // As method names aren't unique in the class, we add a number at the end to make it so
    private string _uniqueName;

    internal IMethodSymbol Symbol { get; }

    internal bool HasParameters => Symbol.Parameters.Length > 0;

    internal bool HasReturnValue => !Symbol.ReturnsVoid;

    internal IReadOnlyList<ImposterReferentMethodParameter> Parameters { get; }

    internal const string CallBeforeCallbackFieldName = "_callBefore";

    internal const string CallAfterCallbackFieldName = "_callAfter";

    internal const string ExceptionGeneratorFieldName = "_exceptionGenerator";

    internal const string ResultGeneratorFieldName = "_resultGenerator";

    internal ImposterReferentMethod(IMethodSymbol symbol, string uniqueName)
    {
        Symbol = symbol;
        Parameters = symbol.Parameters.Select(parameter => new ImposterReferentMethodParameter(parameter)).ToList();
        uniqueName = uniqueName;
        MethodInvocationHistoryClassName = $"{uniqueName}MethodInvocationHistory";
        InvocationBehaviorClassName = $"{uniqueName}MethodInvocationBehaviour";
        MethodInvocationVerifierClassName = $"{uniqueName}MethodInvocationVerifier";
        MethodClassName = $"{uniqueName}Method";
        DelegateName = $"{uniqueName}Delegate";
        ReturnTypeInFullyQualifiedFormat = symbol.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        ParametersDeclaration = string.Join(", ", Parameters.Select(parameter => $"{parameter.TypeWithRefKind} {parameter.Symbol.Name}"));
        ParametersTupleDeclaration = GetParameterTupleType(symbol.Parameters);
        ReturnType = GetReturnType(symbol);
        ParametersEnclosedInArgType = GetParametersEnclosedInArgType(Parameters);
        InitializeOutParametersWithDefault = GetInitializeOutParametersWithDefault(symbol);
    }

    private string GetParametersEnclosedInArgType(IEnumerable<ImposterReferentMethodParameter> parameters)
    {
        return string.Join(", ", parameters.Select(p => $"Arg<{p.EnclosedInArgType}> {p.EnclosedInArgName}"));
    }

    internal string ParametersEnclosedInArgType { get; }

    internal string ReturnTypeInFullyQualifiedFormat { get; }

    internal string MethodInvocationHistoryClassName { get; }

    internal string InvocationBehaviorClassName { get; }

    internal string MethodInvocationVerifierClassName { get; }

    internal string MethodClassName { get; }

    internal string DelegateName { get; }

    internal IReadOnlyList<string> InitializeOutParametersWithDefault { get; }

    private static IReadOnlyList<string> GetInitializeOutParametersWithDefault(IMethodSymbol method)
    {
        return method
            .Parameters
            .Where(parameter => parameter.RefKind is RefKind.Out)
            .Select(parameter => $"{parameter.Name} = default!;")
            .ToList();
    }

    internal string ReturnType { get; }

    private static string GetReturnType(IMethodSymbol method)
    {
        var formatWithNullability = new SymbolDisplayFormat(
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
        );

        return method.ReturnsVoid ? "void" : method.ReturnType.ToDisplayString(formatWithNullability);
    }

    internal string ParametersTupleDeclaration { get; }

    private static string GetParameterTupleType(ImmutableArray<IParameterSymbol> parameters) =>
        parameters.Length switch
        {
            0 => throw new InvalidOperationException("No parameters"), // TODO Log diagnostics instead,
            1 => parameters[0].Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            _ => $"({string.Join(", ", parameters.Select(p => p.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)))})"
        };

    /// <example>
    /// Given a method <code> void InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)</code>
    /// This will contain <code> "in int input, out int output, ref int temp, params int[] parameters" </code>
    /// </example>>
    internal string ParametersDeclaration { get; }
    
    /// <example>
    /// Given a method <code> void InOutRefParam(in int input, out int output, ref int temp, params int[] parameters)</code>
    /// This will contain <code> "in input, out output, ref temp, parameters" </code>
    /// </example>>
    internal string ParametersPassedAsArgumentsDeclaration { get; }

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

public class ImposterClass
{
    public string Name { get; }

    public string Namespace { get; }

    public ImposterClass(INamedTypeSymbol typeSymbol)
    {
        Name = typeSymbol.Name + "Imposter";
        Namespace = $"Imposters{(typeSymbol.ContainingNamespace.IsGlobalNamespace ? string.Empty : "." + typeSymbol.ContainingNamespace.ToDisplayString())}";
    }
}

internal class MethodUniqueNames
{
    private readonly Dictionary<IMethodSymbol, string> _methodUniqueNames = new();

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