using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

internal static class ThenMethodBuilder
{
    internal static MethodDeclarationSyntax ThenReturningThis(in ThenMethodMetadata then) =>
        new MethodDeclarationBuilder(then.ReturnType, then.Name)
            .WithExplicitInterfaceSpecifier(then.InterfaceSyntax)
            .WithBody(Block(ReturnThis))
            .Build();
}
