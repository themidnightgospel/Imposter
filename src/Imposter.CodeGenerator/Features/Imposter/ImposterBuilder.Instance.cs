using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal static partial class ImposterBuilder
{
    private static MemberDeclarationSyntax InstanceMethod(in ImposterGenerationContext imposterGenerationContext, string imposterInstanceFieldName) =>
        MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol),
                Identifier("Instance")
            )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))
                )
            )
            .WithBody(Block(
                ReturnStatement(IdentifierName(imposterInstanceFieldName))
            ));
}