using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class InterlockedSyntaxHelper
{
    internal static InvocationExpressionSyntax InterlockedExchange(
        string fieldName,
        ExpressionSyntax value
    ) =>
        WellKnownTypes
            .System.Threading.Interlocked.Dot(IdentifierName("Exchange"))
            .Call([RefArgument(fieldName), Argument(value)]);

    internal static InvocationExpressionSyntax InterlockedIncrement(string fieldName) =>
        WellKnownTypes
            .System.Threading.Interlocked.Dot(IdentifierName("Increment"))
            .Call(RefArgument(fieldName));
}
