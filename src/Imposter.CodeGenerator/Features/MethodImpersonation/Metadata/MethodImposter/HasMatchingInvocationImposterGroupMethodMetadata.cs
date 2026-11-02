using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal static class HasMatchingInvocationImposterGroupMethodMetadata
{
    internal const string Name = "HasMatchingInvocationImposterGroup";

    internal static readonly TypeSyntax ReturnType = WellKnownTypes.Bool;
}
