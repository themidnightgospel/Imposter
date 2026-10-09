using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static FieldDeclarationSyntax BuildInvocationImposterGroupsField(
        in ImposterTargetMethodMetadata method
    )
    {
        var invocationImposterGroupsFieldType =
            WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
                method.MethodInvocationImposterGroup.Syntax
            );

        return SinglePrivateReadonlyVariableField(
            invocationImposterGroupsFieldType,
            method.MethodImposter.InvocationImposterGroupsField.Name,
            invocationImposterGroupsFieldType.New()
        );
    }
}
