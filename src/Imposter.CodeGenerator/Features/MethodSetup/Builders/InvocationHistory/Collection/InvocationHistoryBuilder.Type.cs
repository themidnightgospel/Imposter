using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        return new ClassDeclarationBuilder(method.InvocationHistory.Collection.Name)
                .AddModifier(Token(SyntaxKind.InternalKeyword))
                .AddMember(
                    SingleVariableField(
                        ConcurrentStack(method.InvocationHistory.Interface.Syntax),
                        InvocationHistoryTypeMetadata.CollectionMetadata.InvocationHistoryCollectionFieldName,
                        TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
                        ConcurrentStack(method.InvocationHistory.Interface.Syntax).New()
                    )
                )
                .AddMember(BuildAddMethod(method))
                .AddMember(BuildCountMethod(method))
                .Build()
            ;
    }
}