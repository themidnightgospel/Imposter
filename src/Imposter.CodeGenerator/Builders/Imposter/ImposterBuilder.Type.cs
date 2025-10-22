using System.Linq;
using Imposter.CodeGenerator.Builders.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Imposter;

internal static partial class ImposterBuilder
{
    // TODO this can also collide
    private const string ImposterInstanceFieldName = "_imposterInstance";

    internal static ClassDeclarationBuilder Build(in ImposterGenerationContext imposterGenerationContext)
    {
        var imposterBuilder = new ClassDeclarationBuilder(imposterGenerationContext.Imposter.Name)
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))))
            .AddMembers(MethodImposterFields(imposterGenerationContext))
            .AddMembers(InvocationHistoryCollectionFields(imposterGenerationContext))
            .AddMembers(SetupBuilderMethods(imposterGenerationContext));

        var imposterClassMemberUniqueName = new ClassMemberUniqueName(imposterBuilder.Members);
        var imposterTargetInstanceClassName = imposterClassMemberUniqueName.New("ImposterTargetInstance");

        return imposterBuilder
            .AddMember(ImposterInstanceField(imposterTargetInstanceClassName))
            .AddMember(BuildConstructor(imposterGenerationContext, imposterTargetInstanceClassName))
            .AddMember(ImposterTargetInstanceBuilder.Build(imposterGenerationContext, imposterTargetInstanceClassName))
            .AddMember(InstanceMethod(imposterGenerationContext))
            .AddModifier(Token(SyntaxKind.PublicKeyword));
    }


    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterGenerationContext imposterGenerationContext, in string imposterTargetInstanceClassName) =>
        ConstructorDeclaration(imposterGenerationContext.Imposter.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithBody(new BlockBuilder()
                .AddStatements(
                    imposterGenerationContext
                        .Imposter
                        .Methods
                        .Select(method => ThisExpression()
                            .Dot(method.Symbol.IsGenericMethod
                                ? IdentifierName(method.MethodImposter.Collection.AsField.Name)
                                : IdentifierName(method.MethodImposter.AsField.Name))
                            .Assign((method.Symbol.IsGenericMethod ? method.MethodImposter.Collection.Syntax : method.MethodImposter.Syntax)
                                .New(IdentifierName(method.InvocationHistory.Collection.AsField.Name).ToSingleArgumentList())
                            )
                            .ToStatementSyntax()
                        )
                )
                .AddExpression(
                    ThisExpression()
                        .Dot(IdentifierName(ImposterInstanceFieldName))
                        .Assign(IdentifierName(imposterTargetInstanceClassName).New(ThisExpression().ToSingleArgumentList()))
                )
                .Build());

    private static FieldDeclarationSyntax ImposterInstanceField(in string imposterTargetInstanceClassName) =>
        SyntaxFactoryHelper.SingleVariableField(IdentifierName(imposterTargetInstanceClassName), ImposterInstanceFieldName, SyntaxKind.PrivateKeyword);
}