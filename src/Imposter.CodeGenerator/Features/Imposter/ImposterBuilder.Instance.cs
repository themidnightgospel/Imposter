using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal readonly partial struct ImposterBuilder
{
    private static MethodDeclarationSyntax InstanceMethod(
        in ImposterGenerationContext imposterGenerationContext,
        string imposterInstanceFieldName
    ) =>
        new MethodDeclarationBuilder(
            SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol),
            "Instance"
        )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(
                        SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol)
                    )
                )
            )
            .WithBody(Block(ReturnStatement(IdentifierName(imposterInstanceFieldName))))
            .Build();
}
