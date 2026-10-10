using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

// What the raise history keeps for each raise: true for an event without parameters, the value of its only parameter,
// or a tuple of its values.
internal readonly struct EventHistoryEntryMetadata
{
    internal readonly TypeSyntax Type;

    private readonly EventParameterMetadata[] _parameters;

    internal EventHistoryEntryMetadata(EventParameterMetadata[] parameters)
    {
        _parameters = parameters;
        Type = parameters.Length switch
        {
            0 => WellKnownTypes.Bool,
            1 => parameters[0].TypeSyntax,
            _ => TupleType(
                SeparatedList(
                    parameters.Select(parameter =>
                        TupleElement(parameter.TypeSyntax)
                            .WithIdentifier(Identifier(parameter.TupleElementName))
                    )
                )
            ),
        };
    }

    // The entry a raise adds, from the raise's parameters.
    internal ExpressionSyntax Entry =>
        _parameters.Length switch
        {
            0 => SyntaxFactoryHelper.True,
            1 => _parameters[0].KeptValue,
            _ => TupleExpression(
                SeparatedList(_parameters.Select(parameter => Argument(parameter.KeptValue)))
            ),
        };

    // A parameter's value in an entry of the history.
    internal ExpressionSyntax ParameterValue(
        ExpressionSyntax entry,
        in EventParameterMetadata parameter
    ) => _parameters.Length == 1 ? entry : entry.Dot(IdentifierName(parameter.TupleElementName));
}
