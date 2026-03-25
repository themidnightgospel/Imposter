using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly struct MethodParameterMetadata
{
    internal readonly IParameterSymbol Symbol;

    internal readonly string Name;

    internal MethodParameterMetadata(IParameterSymbol symbol)
    {
        Symbol = symbol;
        Name = SyntaxFactoryHelper.EscapeKeyword(symbol.Name);
    }
}
