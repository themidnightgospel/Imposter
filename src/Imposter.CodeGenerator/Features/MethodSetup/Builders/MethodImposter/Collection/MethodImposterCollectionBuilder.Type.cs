using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }

        var historyCollectionField = BuildInvocationHistoryCollectionField(method);

        return ClassDeclaration(method.MethodImposter.Collection.Name)
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddMembers(historyCollectionField)
            .AddMembers(SyntaxFactoryHelper.BuildConstructorAndInitializeMembers(method.MethodImposter.Collection.Name, [historyCollectionField]))
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
        var impostersFieldType = WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(method.MethodImposter.Interface.Syntax);

        return SyntaxFactoryHelper.SingleVariableField(
            impostersFieldType,
            "_imposters",
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            impostersFieldType.New()
        );
    }
}