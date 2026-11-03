using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static ExpressionStatementSyntax ToStatementSyntax(this ExpressionSyntax source) =>
        ExpressionStatement(source);

    // ArgumentNullException.ThrowIfNull is missing from .NET Standard 2.0 and .NET Framework.
    internal static IfStatementSyntax ThrowIfNull(string parameterName) =>
        IfStatement(
            IsPatternExpression(IdentifierName(parameterName), ConstantPattern(Null)),
            Block(
                ThrowStatement(
                    WellKnownTypes.System.ArgumentNullException.New(
                        IdentifierName("nameof")
                            .Call(Argument(IdentifierName(parameterName)))
                            .ToSingleArgumentList()
                    )
                )
            )
        );
}
