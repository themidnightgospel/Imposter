using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;

// Kept values include a span's array snapshot and exclude passed-through ref structs and out parameters.
// Readonly references avoid copying large value types for each candidate setup.
internal static class CapturedArgumentsBuilder
{
    internal static ParameterListSyntax InParameters(in ImposterTargetMethodMetadata method) =>
        Parameters(method, TokenList(Token(SyntaxKind.InKeyword)));

    internal static ParameterListSyntax ValueParameters(in ImposterTargetMethodMetadata method) =>
        Parameters(method, default);

    private static ParameterListSyntax Parameters(
        in ImposterTargetMethodMetadata method,
        SyntaxTokenList modifiers
    ) =>
        ParameterList(
            SeparatedList(
                method.Parameters.InputParameterMetadata.Select(p =>
                    ParameterSyntax(p.NullableAwareKeptTypeSyntax, p.Name).WithModifiers(modifiers)
                )
            )
        );

    internal static ArgumentListSyntax Arguments(in ImposterTargetMethodMetadata method) =>
        ArgumentList(
            SeparatedList(
                method.Parameters.InputParameterMetadata.Select(p =>
                    Argument(IdentifierName(p.Name))
                )
            )
        );

    // Invoke's locals and the history entry's fields share these names.
    internal static ArgumentListSyntax CapturedValues(in ImposterTargetMethodMetadata method)
    {
        var names = method.InvocationHistory.ArgumentFields;
        return ArgumentList(
            SeparatedList(
                method.Parameters.InputParameterMetadata.Select(p =>
                    Argument(IdentifierName(names[p.Name]))
                )
            )
        );
    }
}
