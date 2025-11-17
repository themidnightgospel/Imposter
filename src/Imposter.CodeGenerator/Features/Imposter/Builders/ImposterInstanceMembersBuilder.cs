using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal static class ImposterInstanceMembersBuilder
{
    internal static MethodDeclarationSyntax InstanceMethod(
        in ImposterGenerationContext imposterGenerationContext,
        string imposterInstanceFieldName
    ) =>
        new MethodDeclarationBuilder(
            imposterGenerationContext.Imposter.TargetTypeSyntax,
            "Instance"
        )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(
                        imposterGenerationContext.Imposter.TargetTypeSyntax
                    )
                )
            )
            .WithBody(Block(ReturnStatement(IdentifierName(imposterInstanceFieldName))))
            .Build();
}
