using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

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

        return SinglePrivateReadonlyVariableField(
            invocationSetupsFieldType,
            method.MethodImposter.InvocationImpostersField.Name,
            invocationSetupsFieldType.New()
        );
    }
}
