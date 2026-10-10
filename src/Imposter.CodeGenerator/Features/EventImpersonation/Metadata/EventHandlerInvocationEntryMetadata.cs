using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

// What the handler-invocation history keeps for each handler a raise calls: the handler for an event without
// parameters, or a tuple of the handler and the values.
internal readonly struct EventHandlerInvocationEntryMetadata
{
    internal readonly TypeSyntax Type;

    private readonly EventParameterMetadata[] _parameters;

    private readonly string _handlerElementName;

    internal EventHandlerInvocationEntryMetadata(in ImposterEventCoreMetadata core)
    {
        _parameters = core.MatchedParameters;
        _handlerElementName = core.HandlerTupleElementName;
        Type =
            _parameters.Length == 0
                ? core.HandlerTypeSyntax
                : TupleType(
                    SeparatedList(
                        new[]
                        {
                            TupleElement(core.HandlerTypeSyntax)
                                .WithIdentifier(Identifier(_handlerElementName)),
                        }.Concat(
                            _parameters.Select(parameter =>
                                TupleElement(parameter.TypeSyntax)
                                    .WithIdentifier(Identifier(parameter.TupleElementName))
                            )
                        )
                    )
                );
    }

    // The entry a raise adds when it calls the handler, from the raise's parameters.
    internal ExpressionSyntax Entry(ExpressionSyntax handler) =>
        _parameters.Length == 0
            ? handler
            : TupleExpression(
                SeparatedList(
                    new[] { Argument(handler) }.Concat(
                        _parameters.Select(parameter => Argument(parameter.StoredValue))
                    )
                )
            );

    // The handler an entry of the history records.
    internal ExpressionSyntax Handler(ExpressionSyntax entry) =>
        _parameters.Length == 0 ? entry : entry.Dot(IdentifierName(_handlerElementName));

    // A parameter's value in an entry of the history.
    internal static ExpressionSyntax ParameterValue(
        ExpressionSyntax entry,
        in EventParameterMetadata parameter
    ) => entry.Dot(IdentifierName(parameter.TupleElementName));
}
