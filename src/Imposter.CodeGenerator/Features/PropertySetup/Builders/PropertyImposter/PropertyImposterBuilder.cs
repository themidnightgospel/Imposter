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
            .AddMember(SingleVariableField(property.ImposterBuilder.SetterImposterField, SyntaxKind.InternalKeyword))
            .AddMember(SingleVariableField(property.ImposterBuilder.GetterImposterBuilderField, SyntaxKind.InternalKeyword))
            .AddMember(BuildConstructor(property.ImposterBuilder))
            .AddMember(DefaultPropertyBehaviourBuilder.Build(property.DefaultPropertyBehaviour))
            .AddMember(GetterImposterBuilderBuilder.Build(property))
            .AddMember(SetterImposterBuilder.Build(property))
            .AddMember(BuildGetterMethod(property))
            .AddMember(BuildSetterMethod(property))
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

    internal static ConstructorDeclarationSyntax BuildConstructor(in PropertyImposterBuilderMetadata imposterBuilder) =>
        new ConstructorBuilder(imposterBuilder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .WithBody(Block(
                IdentifierName(imposterBuilder.DefaultPropertyBehaviourField.Name).Assign(imposterBuilder.DefaultPropertyBehaviourField.Type.New())
                    .ToStatementSyntax(),
                IdentifierName(imposterBuilder.SetterImposterField.Name)
                    .Assign(imposterBuilder.SetterImposterField.Type
                        .New(Argument(IdentifierName(imposterBuilder.DefaultPropertyBehaviourField.Name)).AsSingleArgumentListSyntax())
                    )
                    .ToStatementSyntax(),
                IdentifierName(imposterBuilder.GetterImposterBuilderField.Name).Assign(
                        imposterBuilder.GetterImposterBuilderField.Type
                            .New(Argument(IdentifierName(imposterBuilder.DefaultPropertyBehaviourField.Name)).AsSingleArgumentListSyntax())
                    )
                    .ToStatementSyntax()
            ))
            .Build();
}