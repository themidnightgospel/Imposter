using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

// The MissingImposterException a generated member throws when it has nothing to run: no setup in Explicit mode, or no
// base implementation to fall back to.
internal static class MissingImposterBuilder
{
    internal static IfStatementSyntax ThrowIfExplicit(
        ExpressionSyntax mode,
        ExpressionSyntax memberDisplayName
    ) => IfStatement(IsExplicit(mode), Block(ThrowMissingImposter(memberDisplayName)));

    internal static BinaryExpressionSyntax IsExplicit(ExpressionSyntax mode) =>
        BinaryExpression(
            SyntaxKind.EqualsExpression,
            mode,
            WellKnownTypes.Imposter.Abstractions.ImposterMode.Dot(IdentifierName("Explicit"))
        );

    // Names the member by its display name, read from a field, followed by a suffix such as " (getter)" or " (event)".
    internal static ThrowStatementSyntax ThrowMissingImposter(
        string displayNameFieldName,
        string suffix
    ) => ThrowMissingImposter(IdentifierName(displayNameFieldName).Add(suffix.StringLiteral()));

    private static ThrowStatementSyntax ThrowMissingImposter(ExpressionSyntax memberDisplayName) =>
        ThrowStatement(MissingImposterException(memberDisplayName));

    internal static ObjectCreationExpressionSyntax MissingImposterException(
        ExpressionSyntax memberDisplayName
    ) =>
        WellKnownTypes.Imposter.Abstractions.MissingImposterException.New(
            memberDisplayName.ToSingleArgumentList()
        );
}
