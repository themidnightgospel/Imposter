using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

// The MissingImposterException a generated member throws when it has nothing to run: no setup in Explicit mode, or no
// base implementation to fall back to.
internal static class MissingImposterBuilder
{
    // An accessor that was never set up throws in Explicit mode. The caller passes how it reads its configured flag and
    // the statement that throws, so each feature keeps the code it generates.
    internal static MethodDeclarationSyntax EnsureConfiguredMethod(
        string methodName,
        string modeFieldName,
        ExpressionSyntax isConfigured,
        StatementSyntax throwMissingImposter
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, methodName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(
                        IsExplicit(IdentifierName(modeFieldName)).And(Not(isConfigured)),
                        throwMissingImposter
                    )
                )
            )
            .Build();

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

    // When the imposter is set up to use the base implementation, it calls it, or throws when there's none to call.
    internal static IfStatementSyntax CallBaseImplementationIfUsed(
        ExpressionSyntax useBaseImplementation,
        ExpressionSyntax baseImplementation,
        BlockSyntax callBaseImplementation,
        ThrowStatementSyntax throwMissingImposter
    ) =>
        IfStatement(
            useBaseImplementation,
            Block(
                IfStatement(
                    baseImplementation.IsNotNull(),
                    callBaseImplementation,
                    ElseClause(throwMissingImposter)
                )
            )
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
