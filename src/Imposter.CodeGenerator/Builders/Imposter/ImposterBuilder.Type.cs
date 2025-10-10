using System.Collections.Generic;
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

    internal static ClassDeclarationBuilder Build(ImposterGenerationContext imposterGenerationContext)
    {
        var imposterBuilder = new ClassDeclarationBuilder(imposterGenerationContext.ImposterType.Name)
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))))
            .AddMembers(MethodImposterFields(imposterGenerationContext))
            .AddMembers(InvocationHistoryCollectionFields(imposterGenerationContext))
            .AddMembers(SetupBuilderMethods(imposterGenerationContext));

        var imposterClassMemberUniqueName = new ClassMemberUniqueName(imposterBuilder.Members);
        var imposterTargetInstanceClassName = imposterClassMemberUniqueName.New("ImposterTargetInstance");

        return imposterBuilder
            .AddMember(ImposterInstanceField(imposterTargetInstanceClassName))
            .AddMembers(Constructor(imposterGenerationContext, imposterTargetInstanceClassName))
            .AddMember(ImposterTargetInstanceBuilder.Build(imposterGenerationContext, imposterTargetInstanceClassName))
            .AddMember(InstanceMethod(imposterGenerationContext))
            .AddModifier(Token(SyntaxKind.PublicKeyword));
    }


    private static IEnumerable<ConstructorDeclarationSyntax> Constructor(ImposterGenerationContext imposterGenerationContext, string imposterTargetInstanceClassName)
    {
        yield return ConstructorDeclaration(imposterGenerationContext.ImposterType.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithBody(new BlockBuilder()
                .AddStatements(
                    imposterGenerationContext
                        .Methods
                        .Select(method => ThisExpression()
                            .Dot(method.Symbol.IsGenericMethod ? IdentifierName(method.MethodImposter.Collection.AsField.Name) : IdentifierName(method.MethodImposter.AsField.Name))
                            .Assign(
                                (method.Symbol.IsGenericMethod ? method.MethodImposter.Collection.Syntax : method.MethodImposter.Syntax)
                                .New()
                            )
                            .AsStatement()
                        )
                )
                .AddStatement(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            ThisExpression(),
                            IdentifierName(ImposterInstanceFieldName)
                        ),
                        ObjectCreationExpression(IdentifierName(imposterTargetInstanceClassName))
                            .WithArgumentList(ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        ThisExpression()
                                    )
                                )
                            ))
                    )
                ))
                .Build());
    }

    private static FieldDeclarationSyntax ImposterInstanceField(string imposterTargetInstanceClassName) =>
        SyntaxFactoryHelper.SingleVariableField(IdentifierName(imposterTargetInstanceClassName), ImposterInstanceFieldName);
}