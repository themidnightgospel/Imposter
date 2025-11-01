using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal partial class ImposterBuilder
{
    private readonly ClassDeclarationBuilder _imposterBuilder;
    private readonly ImposterInstanceBuilder _imposterInstanceBuilder;

    private ImposterBuilder(ClassDeclarationBuilder imposterBuilder, ImposterInstanceBuilder imposterInstanceBuilder)
    {
        _imposterBuilder = imposterBuilder;
        _imposterInstanceBuilder = imposterInstanceBuilder;
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
                property.AsField.Name,
                property.ImposterBuilder.Syntax.New()));

        _imposterInstanceBuilder.AddImposterProperty(property);

        return this;
    }

    internal ClassDeclarationSyntax Build() => _imposterBuilder
        .AddMember(_imposterInstanceBuilder.Build())
        .Build();

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

        var imposterClassBuilder = imposterBuilder
            .AddMember(ImposterInstanceField(imposterTargetInstanceClassName, imposterInstanceFieldName))
            .AddMember(BuildConstructor(imposterGenerationContext, imposterTargetInstanceClassName, imposterInstanceFieldName))
            .AddMember(InstanceMethod(imposterGenerationContext, imposterInstanceFieldName))
            .AddModifier(Token(SyntaxKind.PublicKeyword));

        var imposterInstanceBuilder = ImposterInstanceBuilder.Create(imposterGenerationContext, imposterTargetInstanceClassName);

        return new ImposterBuilder(imposterClassBuilder, imposterInstanceBuilder);
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
                .AddExpression(
                    ThisExpression()
                        .Dot(IdentifierName(imposterInstanceFieldName))
                        .Assign(IdentifierName(imposterTargetInstanceClassName).New(ThisExpression().ToSingleArgumentList()))
                )
                .Build());

    private static FieldDeclarationSyntax ImposterInstanceField(in string imposterTargetInstanceClassName, string imposterInstanceFieldName) =>
        SyntaxFactoryHelper.SingleVariableField(IdentifierName(imposterTargetInstanceClassName), imposterInstanceFieldName, SyntaxKind.PrivateKeyword);
}