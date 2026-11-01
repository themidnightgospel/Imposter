using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;

internal readonly struct CallbacksFieldMetadata
{
    internal readonly string Name = "_callbacks";

    internal readonly TypeSyntax TypeSyntax;

    public CallbacksFieldMetadata()
    {
        TypeSyntax = WellKnownTypes.Imposter.Abstractions.CallbackCollection(
            WellKnownTypes.System.Action
        );
    }
}
