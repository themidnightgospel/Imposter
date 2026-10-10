using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static class InterlockedSyntaxHelper
{
    internal static InvocationExpressionSyntax InterlockedIncrement(string fieldName) =>
        WellKnownTypes
            .System.Threading.Interlocked.Dot(IdentifierName("Increment"))
            .Call(RefArgument(fieldName));
}
