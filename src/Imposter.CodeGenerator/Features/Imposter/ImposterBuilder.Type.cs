using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal readonly ref  partial struct ImposterBuilder
{
    private readonly ClassDeclarationBuilder _imposterBuilder;
    private readonly ImposterInstanceBuilder _imposterInstanceBuilder;
    private readonly string _invocationBehaviorFieldName;
    private readonly ConstructorBuilder _constructorBuilder;
    private readonly BlockBuilder _constructorBodyBuilder;
    private readonly string _invocationBehaviorParameterName;

    private ImposterBuilder(
        ClassDeclarationBuilder imposterBuilder,
        ImposterInstanceBuilder imposterInstanceBuilder,
        string invocationBehaviorFieldName,
        ConstructorBuilder constructorBuilder,
        BlockBuilder constructorBodyBuilder,
        string invocationBehaviorParameterName)
    {
        _imposterBuilder = imposterBuilder;
        _imposterInstanceBuilder = imposterInstanceBuilder;
        _invocationBehaviorFieldName = invocationBehaviorFieldName;
        _constructorBuilder = constructorBuilder;
        _constructorBodyBuilder = constructorBodyBuilder;
        _invocationBehaviorParameterName = invocationBehaviorParameterName;
    }

    internal ImposterBuilder AddMembers(IEnumerable<MemberDeclarationSyntax>? members)
    {
        _imposterBuilder.AddMembers(members);
        return this;
    }

    internal ImposterBuilder AddMember(MemberDeclarationSyntax? member)
    {
        _imposterBuilder.AddMember(member);
        return this;
    }

    internal ImposterBuilder AddPropertyImposter(in ImposterPropertyMetadata property)
    {
        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.ReadOnlyPropertyDeclarationSyntax(
                property.ImposterBuilderInterface.Syntax,
                property.Core.Name,
                IdentifierName(property.AsField.Name)
            ));

        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                property.ImposterBuilder.Syntax,
                property.AsField.Name));

        _constructorBodyBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(property.AsField.Name))
                .Assign(
                    property.ImposterBuilder.Syntax
                        .New(
                            SyntaxFactoryHelper.ArgumentListSyntax(
                                [
                                    Argument(IdentifierName(_invocationBehaviorParameterName))
                                ])))
                .ToStatementSyntax());

        _imposterInstanceBuilder.AddImposterProperty(property);

        return this;
    }

    internal ClassDeclarationSyntax Build()
    {
        var constructor = _constructorBuilder.WithBody(_constructorBodyBuilder.Build()).Build();

        return _imposterBuilder
            .AddMember(constructor)
            .AddMember(_imposterInstanceBuilder.Build())
            .Build();
    }

    internal static ImposterBuilder Create(in ImposterGenerationContext imposterGenerationContext)
    {
        var imposterBuilder = new ClassDeclarationBuilder(imposterGenerationContext.Imposter.Name)
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))))
            .AddMembers(MethodImposterFields(imposterGenerationContext))
            .AddMembers(InvocationHistoryCollectionFields(imposterGenerationContext))
            .AddMembers(BuildImposterMethods(imposterGenerationContext));

        var imposterClassMemberUniqueName = new NameSet(MemberNamesHelper.GetNames(imposterBuilder.Members));
        var imposterTargetInstanceClassName = imposterClassMemberUniqueName.Use("ImposterTargetInstance");
        var imposterInstanceFieldName = imposterClassMemberUniqueName.Use("_imposterInstance");
        var invocationBehaviorFieldName = imposterClassMemberUniqueName.Use("_invocationBehavior");
        var invocationBehaviorField = SyntaxFactoryHelper.SingleVariableField(
            WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior,
            invocationBehaviorFieldName,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));

        var constructorParameterName = "invocationBehavior";
        var (constructorBuilder, constructorBodyBuilder) = CreateConstructorBuilder(
            imposterGenerationContext,
            imposterTargetInstanceClassName,
            imposterInstanceFieldName,
            invocationBehaviorFieldName,
            constructorParameterName);

        var imposterClassBuilder = imposterBuilder
            .AddMember(invocationBehaviorField)
            .AddMember(ImposterInstanceField(imposterTargetInstanceClassName, imposterInstanceFieldName))
            .AddMember(InstanceMethod(imposterGenerationContext, imposterInstanceFieldName))
            .AddModifier(Token(SyntaxKind.PublicKeyword));

        var imposterInstanceBuilder = ImposterInstanceBuilder.Create(imposterGenerationContext, imposterTargetInstanceClassName);

        return new ImposterBuilder(
            imposterClassBuilder,
            imposterInstanceBuilder,
            invocationBehaviorFieldName,
            constructorBuilder,
            constructorBodyBuilder,
            constructorParameterName);
    }

    private static (ConstructorBuilder constructorBuilder, BlockBuilder bodyBuilder) CreateConstructorBuilder(
        in ImposterGenerationContext imposterGenerationContext,
        in string imposterTargetInstanceClassName,
        string imposterInstanceFieldName,
        string invocationBehaviorFieldName,
        string invocationBehaviorParameterName)
    {
        var constructorBuilder = new ConstructorBuilder(imposterGenerationContext.Imposter.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));

        var blockBuilder = new BlockBuilder()
            .AddStatements(
                imposterGenerationContext
                    .Imposter
                    .Methods
                    .Select(method =>
                    {
                        var constructorArguments = new List<ArgumentSyntax>
                        {
                            Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name)),
                            Argument(IdentifierName(invocationBehaviorParameterName))
                        };

                        return ThisExpression()
                            .Dot(method.Symbol.IsGenericMethod
                                ? IdentifierName(method.MethodImposter.Collection.AsField.Name)
                                : IdentifierName(method.MethodImposter.AsField.Name))
                            .Assign((method.Symbol.IsGenericMethod ? method.MethodImposter.Collection.Syntax : method.MethodImposter.Syntax)
                                .New(ArgumentList(SeparatedList(constructorArguments)))
                            )
                            .ToStatementSyntax();
                    })
            )
            .AddExpression(
                ThisExpression()
                    .Dot(IdentifierName(imposterInstanceFieldName))
                    .Assign(IdentifierName(imposterTargetInstanceClassName).New(ThisExpression().ToSingleArgumentList()))
            )
            .AddExpression(
                ThisExpression()
                    .Dot(IdentifierName(invocationBehaviorFieldName))
                    .Assign(IdentifierName(invocationBehaviorParameterName))
            );

        var invocationBehaviorParameter = Parameter(Identifier(invocationBehaviorParameterName))
            .WithType(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior)
            .WithDefault(EqualsValueClause(
                QualifiedName(
                    WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior,
                    IdentifierName("Implicit"))));

        constructorBuilder = constructorBuilder.AddParameter(invocationBehaviorParameter);

        return (constructorBuilder, blockBuilder);
    }

    private static FieldDeclarationSyntax ImposterInstanceField(in string imposterTargetInstanceClassName, string imposterInstanceFieldName) =>
        SyntaxFactoryHelper.SingleVariableField(IdentifierName(imposterTargetInstanceClassName), imposterInstanceFieldName, SyntaxKind.PrivateKeyword);
}
