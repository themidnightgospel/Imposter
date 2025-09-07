using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator;

internal class ImposterReferentMethodParameter
{
    public ImposterReferentMethodParameter(IParameterSymbol symbol)
    {
        Symbol = symbol;
        RefKindPrefix = symbol.RefKind switch
        {
            RefKind.Out => "out",
            RefKind.Ref => "ref",
            RefKind.In => "in",
            _ => string.Empty
        };

        RefKindOrParamsPrefix = symbol.IsParams ? "params " : string.Empty + RefKindPrefix;
        Type = symbol.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        TypeWithRefKind = $"{RefKindOrParamsPrefix} {Type}";
        EnclosedInArgType = GetParameterEnclosedInArgType(symbol);
        EnclosedInArgName = $"{symbol.Name}Arg";
        EnclosedInArgNameDeclaredAsField = $"_{EnclosedInArgName}";
    }

    public IParameterSymbol Symbol { get; }

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

    internal string RefKindOrParamsPrefix { get; }

    internal string RefKindPrefix { get; }

    internal string TypeWithRefKind { get; }

    public string EnclosedInArgType { get; }

    public string EnclosedInArgName { get; }
    
    public string EnclosedInArgNameDeclaredAsField { get; }
    
    internal string Type { get; }
}