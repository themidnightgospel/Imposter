using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;

internal readonly struct CallbacksFieldMetadata
{
    internal readonly string Name = "_callbacks";

    internal readonly TypeSyntax TypeSyntax;

    public CallbacksFieldMetadata()
    {
        TypeSyntax = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            WellKnownTypes.System.Action
        );
    }
}
