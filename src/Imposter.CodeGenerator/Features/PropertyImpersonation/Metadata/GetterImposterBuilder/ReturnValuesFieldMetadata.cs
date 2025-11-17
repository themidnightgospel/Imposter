using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;

internal readonly struct ReturnValuesFieldMetadata
{
    internal readonly string Name = "_returnValues";

    internal readonly TypeSyntax TypeSyntax;

    internal ReturnValuesFieldMetadata(TypeSyntax handlerType)
    {
        TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(handlerType);
    }
}
