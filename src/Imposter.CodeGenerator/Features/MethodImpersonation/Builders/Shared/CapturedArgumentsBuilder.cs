using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;

// Kept values include a span's array snapshot and exclude passed-through ref structs and out parameters.
// Readonly references avoid copying large value types for each candidate setup.
internal static class CapturedArgumentsBuilder
{
    internal static ParameterListSyntax Parameters(
        in ImposterTargetMethodMetadata method,
        bool byReference = true
    ) =>
        ParameterList(
            SeparatedList(
                method.Parameters.InputParameterMetadata.Select(p =>
                    ParameterSyntax(p.NullableAwareKeptTypeSyntax, p.Name)
                        .WithModifiers(
                            byReference ? TokenList(Token(SyntaxKind.InKeyword)) : default
                        )
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

    internal static ArgumentListSyntax Fields(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax? owner = null
    )
    {
        var fields = method.InvocationHistory.ArgumentFields;
        return ArgumentList(
            SeparatedList(
                method.Parameters.InputParameterMetadata.Select(p =>
                    Argument(
                        owner is null
                            ? IdentifierName(fields[p.Name])
                            : owner.Dot(IdentifierName(fields[p.Name]))
                    )
                )
            )
        );
    }
}
