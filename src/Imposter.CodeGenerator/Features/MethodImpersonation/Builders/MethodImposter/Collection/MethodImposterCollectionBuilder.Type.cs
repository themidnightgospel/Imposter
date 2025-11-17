using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }

        var historyCollectionField = BuildInvocationHistoryCollectionField(method);
        var invocationBehaviorField = SyntaxFactoryHelper.SingleVariableField(
            WellKnownTypes.Imposter.Abstractions.ImposterMode,
            "_invocationBehavior",
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))
        );

        return new ClassDeclarationBuilder(method.MethodImposter.Collection.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(historyCollectionField)
            .AddMember(invocationBehaviorField)
            .AddMember(
                SyntaxFactoryHelper.BuildConstructorAndInitializeMembers(
                    method.MethodImposter.Collection.Name,
                    [historyCollectionField, invocationBehaviorField]
                )
            )
            .AddMember(BuildImpostersField(method))
            .AddMember(BuildAddNewMethod(method))
            .AddMember(BuildGetImposterWithMatchingInvocationImposterGroup(method))
            .Build();
    }

    private static FieldDeclarationSyntax BuildInvocationHistoryCollectionField(
        in ImposterTargetMethodMetadata method
    ) =>
        SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
            method.InvocationHistory.Collection.Syntax,
            method.InvocationHistory.Collection.AsField.Name
        );

    private static FieldDeclarationSyntax BuildImpostersField(
        in ImposterTargetMethodMetadata method
    )
    {
        var impostersFieldType = WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
            method.MethodImposter.Interface.Syntax
        );

        return SyntaxFactoryHelper.SingleVariableField(
            impostersFieldType,
            "_imposters",
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            impostersFieldType.New()
        );
    }
}
