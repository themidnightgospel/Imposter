using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        return new ClassDeclarationBuilder(method.InvocationHistory.Collection.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SingleVariableField(
                    WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
                        method.InvocationHistory.Interface.Syntax
                    ),
                    InvocationHistoryCollectionMetadata.InvocationHistoryCollectionFieldName,
                    TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
                    WellKnownTypes
                        .System.Collections.Concurrent.ConcurrentStack(
                            method.InvocationHistory.Interface.Syntax
                        )
                        .New()
                )
            )
            .AddMember(BuildAddMethod(method))
            .AddMember(BuildCountMethod(method))
            .Build();
    }
}
