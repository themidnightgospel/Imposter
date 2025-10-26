using System.Linq;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal static partial class ImposterBuilder
{
    internal static ClassDeclarationBuilder Build(in ImposterGenerationContext imposterGenerationContext)
    {
        var imposterBuilder = new ClassDeclarationBuilder(imposterGenerationContext.Imposter.Name)
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))))
            .AddMembers(MethodImposterFields(imposterGenerationContext))
            .AddMembers(PropertyImposterFields(imposterGenerationContext))
            .AddMembers(InvocationHistoryCollectionFields(imposterGenerationContext))
            .AddMembers(BuildImposterMethods(imposterGenerationContext))
            .AddMembers(BuildImposterProperties(imposterGenerationContext));

        var imposterClassMemberUniqueName = new SymbolNameNamespace(MemberNamesHelper.GetNames(imposterBuilder.Members));
        var imposterTargetInstanceClassName = imposterClassMemberUniqueName.Use("ImposterTargetInstance");
        var imposterInstanceFieldName = imposterClassMemberUniqueName.Use("_imposterInstance");

        return imposterBuilder
            .AddMember(ImposterInstanceField(imposterTargetInstanceClassName, imposterInstanceFieldName))
            .AddMember(BuildConstructor(imposterGenerationContext, imposterTargetInstanceClassName, imposterInstanceFieldName))
            .AddMember(ImposterTargetInstanceBuilder.Build(imposterGenerationContext, imposterTargetInstanceClassName))
            .AddMember(InstanceMethod(imposterGenerationContext, imposterInstanceFieldName))
            .AddModifier(Token(SyntaxKind.PublicKeyword));
    }

    private static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterGenerationContext imposterGenerationContext,
        in string imposterTargetInstanceClassName,
        string imposterInstanceFieldName) =>
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
                .AddStatements(imposterGenerationContext
                    .Imposter
                    .Properties
                    .Select(property => ThisExpression()
                        .Dot(IdentifierName(property.AsField.Name))
                        .Assign(property.PropertyImposter.Syntax.New())
                        .ToStatementSyntax()
                    )
                )
                .AddExpression(
                    ThisExpression()
                        .Dot(IdentifierName(imposterInstanceFieldName))
                        .Assign(IdentifierName(imposterTargetInstanceClassName).New(ThisExpression().ToSingleArgumentList()))
                )
                .Build());

    private static FieldDeclarationSyntax ImposterInstanceField(in string imposterTargetInstanceClassName, string imposterInstanceFieldName) =>
        SyntaxFactoryHelper.SingleVariableField(IdentifierName(imposterTargetInstanceClassName), imposterInstanceFieldName, SyntaxKind.PrivateKeyword);
}