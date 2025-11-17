using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static FieldDeclarationSyntax BuildInvocationSetupsField(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationSetupsFieldType =
            WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
                method.MethodInvocationImposterGroup.Syntax
            );

        return SingleVariableField(
            invocationSetupsFieldType,
            method.MethodImposter.InvocationImpostersField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            invocationSetupsFieldType.New()
        );
    }
}
