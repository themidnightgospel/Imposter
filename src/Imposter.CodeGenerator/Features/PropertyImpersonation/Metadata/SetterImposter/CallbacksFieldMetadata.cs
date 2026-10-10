using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;

internal readonly struct CallbacksFieldMetadata
{
    public static string Name => "_callbacks";

    public TypeSyntax Type { get; }

    // A callback with the criteria the value has to match, or a bare callback for a value passed through.
    internal readonly TypeSyntax? TupleTypeSyntax;

    internal CallbacksFieldMetadata(in ImposterPropertyCoreMetadata property)
    {
        TupleTypeSyntax = property.AsArgType is { } argType
            ? WellKnownTypes.System.Tuple(argType, property.SetterCallbackType)
            : null;
        Type = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            TupleTypeSyntax ?? property.SetterCallbackType
        );
    }
}
