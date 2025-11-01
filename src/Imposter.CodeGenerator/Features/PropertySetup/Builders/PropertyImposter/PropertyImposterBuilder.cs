using Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Getter;
using Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Setter;
using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter;

internal static class PropertyImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterPropertyMetadata property) =>
        new ClassDeclarationBuilder(property.ImposterBuilder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.ImposterBuilderInterface.Syntax))
            .AddMember(SinglePrivateReadonlyVariableField(property.ImposterBuilder.DefaultPropertyBehaviourField))
            .AddMember(property.Core.HasSetter ? SingleVariableField(property.ImposterBuilder.SetterImposterField, SyntaxKind.InternalKeyword) : null)
            .AddMember(property.Core.HasGetter ? SingleVariableField(property.ImposterBuilder.GetterImposterBuilderField, SyntaxKind.InternalKeyword) : null)
            .AddMember(BuildConstructor(property))
            .AddMember(DefaultPropertyBehaviourBuilder.Build(property.DefaultPropertyBehaviour))
            .AddMember(property.Core.HasGetter ? GetterImposterBuilderBuilder.Build(property) : null)
            .AddMember(property.Core.HasSetter ? SetterImposterBuilder.Build(property) : null)
            .AddMember(property.Core.HasGetter ? BuildGetterMethod(property) : null)
            .AddMember(property.Core.HasSetter ? BuildSetterMethod(property) : null)
            .Build();

    internal static MethodDeclarationSyntax? BuildSetterMethod(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.ImposterBuilderInterface.SetterMethod.ReturnType, property.ImposterBuilderInterface.SetterMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax))
            .AddParameter(ParameterSyntax(property.ImposterBuilderInterface.SetterMethod.CriteriaParameter))
            .WithBody(Block(
                ReturnStatement(
                    property.SetterImposter.Builder.TypeSyntax.New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(property.ImposterBuilder.SetterImposterField.Name)),
                            Argument(IdentifierName(property.ImposterBuilderInterface.SetterMethod.CriteriaParameter.Name)),
                        ])
                    )
                )
            ))
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildGetterMethod(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.ImposterBuilderInterface.GetterMethod.ReturnType, property.ImposterBuilderInterface.GetterMethod.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax))
            .WithBody(Block(
                ReturnStatement(
                    IdentifierName(property.ImposterBuilder.GetterImposterBuilderField.Name)
                )
            ))
            .Build();
    }

    internal static ConstructorDeclarationSyntax BuildConstructor(in ImposterPropertyMetadata property) =>
        new ConstructorBuilder(property.ImposterBuilder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .WithBody(new BlockBuilder()
                .AddExpression(IdentifierName(property.ImposterBuilder.DefaultPropertyBehaviourField.Name).Assign(property.ImposterBuilder.DefaultPropertyBehaviourField.Type.New()))
                .AddExpression(
                    property.Core.HasGetter
                        ? IdentifierName(property.ImposterBuilder.GetterImposterBuilderField.Name).Assign(
                            property.ImposterBuilder.GetterImposterBuilderField.Type
                                .New(Argument(IdentifierName(property.ImposterBuilder.DefaultPropertyBehaviourField.Name)).AsSingleArgumentListSyntax())
                        )
                        : null)
                .AddExpression(
                    property.Core.HasSetter
                        ? IdentifierName(property.ImposterBuilder.SetterImposterField.Name)
                            .Assign(property.ImposterBuilder.SetterImposterField.Type
                                .New(Argument(IdentifierName(property.ImposterBuilder.DefaultPropertyBehaviourField.Name)).AsSingleArgumentListSyntax())
                            )
                        : null)
                .Build())
            .Build();
}