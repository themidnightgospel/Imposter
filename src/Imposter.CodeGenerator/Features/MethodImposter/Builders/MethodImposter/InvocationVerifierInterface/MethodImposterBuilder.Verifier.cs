using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.InvocationVerifierInterface;

internal static class MethodImposterInvocationVerifierInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.InvocationVerifierInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                new MethodDeclarationBuilder(WellKnownTypes.Void, CalledMethodMetadata.Name)
                    .AddParameter(Parameter(Identifier("count")).WithType(IdentifierName("Count")))
                    .WithSemicolon()
                    .Build()
            )
            .Build();
}
