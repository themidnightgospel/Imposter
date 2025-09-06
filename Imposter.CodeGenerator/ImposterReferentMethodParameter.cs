using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator;

internal class ImposterReferentMethodParameter
{
    public ImposterReferentMethodParameter(IParameterSymbol symbol)
    {
        Symbol = symbol;
        RefKindPrefix = symbol.IsParams
            ? "params "
            : string.Empty
              + symbol.RefKind switch
              {
                  RefKind.Out => "out ",
                  RefKind.Ref => "ref ",
                  RefKind.In => "in ",
                  _ => string.Empty
              };
        TypeWithRefKind = RefKindPrefix + symbol.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        EnclosedInArgType = GetParameterEnclosedInArgType(symbol);
        EnclosedInArgName = $"{symbol.Name}Arg";
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

    internal string RefKindPrefix { get; }

    internal string TypeWithRefKind { get; }

    public string EnclosedInArgType { get; }

    public string EnclosedInArgName { get; }
}