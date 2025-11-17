using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.NonGenericInterface;

internal static class MethodImposterNonGenericInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }

        return new InterfaceDeclarationBuilder(method.MethodImposter.Interface.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                new MethodDeclarationBuilder(
                    NullableType(
                        method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments
                    ),
                    "As"
                )
                    .WithTypeParameters(
                        SyntaxFactoryHelper.TypeParameterListSyntax(
                            method.TargetGenericTypeArguments
                        )
                    )
                    .WithSemicolon()
                    .Build()
            )
            .Build();
    }
}
