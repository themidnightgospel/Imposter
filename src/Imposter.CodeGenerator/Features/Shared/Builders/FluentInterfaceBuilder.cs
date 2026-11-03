using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

internal static class FluentInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax CallbackInterface(
        string name,
        in CallbackMethodMetadata callback
    ) =>
        new InterfaceDeclarationBuilder(name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                InterfaceMethod(callback.ReturnType, callback.Name, callback.CallbackParameter)
            )
            .Build();

    internal static InterfaceDeclarationSyntax ContinuationInterface(
        string name,
        NameSyntax callbackInterface,
        in ThenMethodMetadata then
    ) =>
        new InterfaceDeclarationBuilder(name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(callbackInterface))
            .AddMember(InterfaceMethod(then.ReturnType, then.Name))
            .Build();

    internal static InterfaceDeclarationSyntax VerificationInterface(
        string name,
        in CalledMethodMetadata called
    ) =>
        new InterfaceDeclarationBuilder(name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(InterfaceMethod(called.ReturnType, called.Name, called.CountParameter))
            .Build();
}
