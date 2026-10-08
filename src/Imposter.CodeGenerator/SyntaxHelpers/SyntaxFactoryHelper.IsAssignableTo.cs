using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    // Written as right.IsAssignableFrom(left): Type.IsAssignableTo is missing from .NET Standard 2.0 and .NET Framework.
    internal static InvocationExpressionSyntax IsAssignableTo(
        this ExpressionSyntax left,
        ExpressionSyntax right
    ) => right.Dot(IdentifierName("IsAssignableFrom")).Call(Argument(left));
}
