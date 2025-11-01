using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;

internal readonly struct ReturnValuesFieldMetadata
{
    internal readonly string Name = "_returnValues";

    internal readonly TypeSyntax TypeSyntax;

    internal ReturnValuesFieldMetadata(in ImposterPropertyCoreMetadata property)
    {
        TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            WellKnownTypes.System.FuncOfT(property.TypeSyntax)
        );
    }
}