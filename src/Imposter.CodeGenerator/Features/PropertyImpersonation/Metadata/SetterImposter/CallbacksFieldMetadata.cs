using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;

internal readonly struct CallbacksFieldMetadata
{
    public static string Name => "_callbacks";

    public TypeSyntax Type { get; }

    internal readonly TypeSyntax TupleTypeSyntax;

    internal CallbacksFieldMetadata(in ImposterPropertyCoreMetadata property)
    {
        Type = WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
            TupleTypeSyntax = WellKnownTypes.System.Tuple(
                property.AsArgType,
                property.AsSystemActionType
            )
        );
    }
}
