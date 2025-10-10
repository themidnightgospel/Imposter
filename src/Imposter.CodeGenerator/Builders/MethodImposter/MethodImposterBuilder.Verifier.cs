using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{

    private static MemberDeclarationSyntax BuildMethodInvocationVerifierInterface(ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper
            .InterfaceDeclarationBuilder(method.Symbol, method.InvocationVerifierInterface)
            .AddMember(Shared.CalledMethodDeclaration)
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)
                .WithLeadingTriviaComment(method.DisplayName)));
}