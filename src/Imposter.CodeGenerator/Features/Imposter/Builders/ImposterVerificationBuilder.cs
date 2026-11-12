using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal static class ImposterVerificationBuilder
{
    internal static IEnumerable<MemberDeclarationSyntax> Build(
        IReadOnlyList<ExpressionSyntax> sources
    )
    {
        foreach (
            var name in new[]
            {
                "VerifyNoOtherCalls",
                "VerifyNoInteractions",
                "VerifyAllSetupsUsed",
            }
        )
            yield return new MethodDeclarationBuilder(WellKnownTypes.Void, name)
                .WithExplicitInterfaceSpecifier(WellKnownTypes.Imposter.Abstractions.IImposter)
                .WithBody(
                    Block(
                        WellKnownTypes
                            .Imposter.Abstractions.ImposterVerification.Dot(IdentifierName(name))
                            .Call(
                                ArgumentListSyntax(
                                    sources.Select(expression => Argument(expression))
                                )
                            )
                            .ToStatementSyntax()
                    )
                )
                .Build();
    }
}
