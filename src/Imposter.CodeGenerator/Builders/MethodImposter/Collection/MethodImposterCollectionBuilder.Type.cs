using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var historyCollectionField = BuildInvocationHistoryCollectionField(method);

        return ClassDeclaration(method.MethodImposter.Collection.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddMembers(historyCollectionField)
            .AddMembers(SyntaxFactoryHelper.DeclareConstructorAndInitializeMembers(method.MethodImposter.Collection.Name, [historyCollectionField]))
            .AddMembers(
                BuildImpostersField(method),
                BuildAddNewMethod(method),
                BuildGetImposterWithMatchingSetup(method)
            );
    }

    private static FieldDeclarationSyntax BuildInvocationHistoryCollectionField(in ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper
            .SinglePrivateReadonlyVariableField(method.InvocationHistory.Collection.Syntax, method.InvocationHistory.Collection.AsField.Name);

    private static MemberDeclarationSyntax BuildImpostersField(in ImposterTargetMethodMetadata method)
    {
        var impostersFieldType = SyntaxFactoryHelper.ConcurrentStack(method.MethodImposter.Interface.Syntax);

        return SyntaxFactoryHelper.SingleVariableField(
            impostersFieldType,
            "_imposters",
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            impostersFieldType.New()
        );
    }
}