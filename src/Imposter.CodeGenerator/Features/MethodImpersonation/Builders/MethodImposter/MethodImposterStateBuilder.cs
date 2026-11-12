using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

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

        var constructor = BuildConstructorAndInitializeMembers(
            className,
            [historyField, modeField]
        );
        if (method.Model.IsGenericMethod && className == method.MethodImposter.Collection.Name)
            constructor = constructor.WithBody(
                constructor.Body!.AddStatements(
                    IdentifierName(method.InvocationHistory.Collection.AsField.Name)
                        .Dot(
                            IdentifierName(InvocationHistoryCollectionMetadata.SetupSourceFieldName)
                        )
                        .Assign(ThisExpression())
                        .ToStatementSyntax()
                )
            );

        return [historyField, modeField, constructor];
    }

    internal static FieldDeclarationSyntax BuildHistoryField(
        in ImposterTargetMethodMetadata method
    ) =>
        SinglePrivateReadonlyVariableField(
            method.InvocationHistory.Collection.Syntax,
            method.InvocationHistory.Collection.AsField.Name
        );
}
