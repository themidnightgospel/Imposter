using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class VolatileSyntaxHelper
{
    internal static InvocationExpressionSyntax VolatileRead(string fieldName) =>
        VolatileRead(IdentifierName(fieldName));

    internal static InvocationExpressionSyntax VolatileRead(ExpressionSyntax field) =>
        WellKnownTypes
            .System.Threading.Volatile.Dot(IdentifierName("Read"))
            .Call(RefArgument(field));

    internal static InvocationExpressionSyntax VolatileWrite(
        string fieldName,
        ExpressionSyntax value
    ) =>
        WellKnownTypes
            .System.Threading.Volatile.Dot(IdentifierName("Write"))
            .Call(ArgumentListSyntax([RefArgument(fieldName), Argument(value)]));
}
