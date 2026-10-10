using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class VolatileSyntaxHelper
{
    internal static InvocationExpressionSyntax VolatileRead(string fieldName) =>
        WellKnownTypes
            .System.Threading.Volatile.Dot(IdentifierName("Read"))
            .Call(RefArgument(fieldName));

    internal static InvocationExpressionSyntax VolatileWrite(
        string fieldName,
        ExpressionSyntax value
    ) =>
        WellKnownTypes
            .System.Threading.Volatile.Dot(IdentifierName("Write"))
            .Call(ArgumentListSyntax([RefArgument(fieldName), Argument(value)]));

    private static ArgumentSyntax RefArgument(string fieldName) =>
        Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(fieldName));
}
