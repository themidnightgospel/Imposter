using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    private static InterfaceDeclarationSyntax BuildInvocationSetupInterface(ImposterTargetMethodMetadata method, ClassDeclarationSyntax invocationSetupBuilderClass) =>
        SyntaxFactoryHelper
            .InterfaceDeclarationBuilder(method.Symbol, method.InvocationSetupType.Interface)
            .AddMembers(invocationSetupBuilderClass
                .Members
                .OfType<MethodDeclarationSyntax>()
                .Where(m => m.Modifiers.Any(SyntaxKind.PublicKeyword) && m.Identifier.ValueText != "Invoke")
                .Select(publicMethod => publicMethod
                    .WithBody(null)
                    .WithReturnType(method.InvocationSetupType.Interface.Syntax)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))))
            .Build(SyntaxTokenList.Create(Token(SyntaxKind.PublicKeyword)));
}