using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static FieldDeclarationSyntax BuildInvocationSetupsField(in ImposterTargetMethodMetadata method)
    {
        var invocationSetupsFieldType = WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(method.InvocationSetup.Syntax);

        return SingleVariableField(
            invocationSetupsFieldType,
            method.MethodImposter.InvocationSetupsField.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            invocationSetupsFieldType.New()
        );
    }
}