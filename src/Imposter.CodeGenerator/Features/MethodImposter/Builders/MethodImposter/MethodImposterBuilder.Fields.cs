using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static FieldDeclarationSyntax BuildInvocationSetupsField(in ImposterTargetMethodMetadata method)
    {
        var invocationSetupsFieldType = WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(method.InvocationSetup.Syntax);

        return SingleVariableField(
            invocationSetupsFieldType,
            method.MethodImposter.InvocationImpostersField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            invocationSetupsFieldType.New()
        );
    }
}