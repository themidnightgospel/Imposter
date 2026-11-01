using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static class MethodImposterStateBuilder
{
    internal static MemberDeclarationSyntax[] BuildMembers(
        in ImposterTargetMethodMetadata method,
        string className
    )
    {
        var historyField = BuildHistoryField(method);
        var modeField = SinglePrivateReadonlyVariableField(
            WellKnownTypes.Imposter.Abstractions.ImposterMode,
            method.MethodImposter.InvocationBehaviorFieldName
        );

        return
        [
            historyField,
            modeField,
            BuildConstructorAndInitializeMembers(className, [historyField, modeField]),
        ];
    }

    internal static FieldDeclarationSyntax BuildHistoryField(
        in ImposterTargetMethodMetadata method
    ) =>
        SinglePrivateReadonlyVariableField(
            method.InvocationHistory.Collection.Syntax,
            method.InvocationHistory.Collection.AsField.Name
        );
}
