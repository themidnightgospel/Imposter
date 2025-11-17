using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Getter;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

internal static class PropertyImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterPropertyMetadata property) =>
        new ClassDeclarationBuilder(property.ImposterBuilder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.ImposterBuilderInterface.Syntax))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    property.ImposterBuilder.DefaultPropertyBehaviourField
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    WellKnownTypes.Imposter.Abstractions.ImposterMode,
                    "_invocationBehavior"
                )
            )
            .AddMember(
                property.Core.HasSetter
                    ? SingleVariableField(
                        property.ImposterBuilder.SetterImposterField,
                        SyntaxKind.InternalKeyword
                    )
                    : null
            )
            .AddMember(
                property.Core.HasGetter
                    ? SingleVariableField(
                        property.ImposterBuilder.GetterImposterBuilderField,
                        SyntaxKind.InternalKeyword
                    )
                    : null
            )
            .AddMember(BuildConstructor(property))
            .AddMember(DefaultPropertyBehaviourBuilder.Build(property.DefaultPropertyBehaviour))
            .AddMember(
                property.Core.HasGetter ? GetterImposterBuilderBuilder.Build(property) : null
            )
            .AddMember(property.Core.HasSetter ? SetterImposterBuilder.Build(property) : null)
            .AddMember(property.Core.HasGetter ? BuildGetterMethod(property) : null)
            .AddMember(property.Core.HasSetter ? BuildSetterMethod(property) : null)
            .AddMember(BuildUseBaseImplementationMethod(property))
            .Build();

    internal static MethodDeclarationSyntax? BuildSetterMethod(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(
            property.ImposterBuilderInterface.SetterMethod.ReturnType,
            property.ImposterBuilderInterface.SetterMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax)
            )
            .AddParameter(
                ParameterSyntax(property.ImposterBuilderInterface.SetterMethod.CriteriaParameter)
            )
            .WithBody(
                Block(
                    IdentifierName(property.ImposterBuilder.SetterImposterField.Name)
                        .Dot(IdentifierName("MarkConfigured"))
                        .Call()
                        .ToStatementSyntax(),
                    ReturnStatement(
                        property.SetterImposter.Builder.TypeSyntax.New(
                            ArgumentListSyntax([
                                Argument(
                                    IdentifierName(
                                        property.ImposterBuilder.SetterImposterField.Name
                                    )
                                ),
                                Argument(
                                    IdentifierName(
                                        property
                                            .ImposterBuilderInterface
                                            .SetterMethod
                                            .CriteriaParameter
                                            .Name
                                    )
                                ),
                            ])
                        )
                    )
                )
            )
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildGetterMethod(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(
            property.ImposterBuilderInterface.GetterMethod.ReturnType,
            property.ImposterBuilderInterface.GetterMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax)
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(property.ImposterBuilder.GetterImposterBuilderField.Name)
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax? BuildUseBaseImplementationMethod(
        in ImposterPropertyMetadata property
    )
    {
        if (property.ImposterBuilderInterface.UseBaseImplementationMethod is not { } methodMetadata)
        {
            return null;
        }

        var statements = new List<StatementSyntax>();

        if (property.Core.HasGetter && property.Core.GetterSupportsBaseImplementation)
        {
            statements.Add(
                IdentifierName(property.ImposterBuilder.GetterImposterBuilderField.Name)
                    .Dot(IdentifierName("EnableBaseImplementation"))
                    .Call()
                    .ToStatementSyntax()
            );
        }

        if (property.Core.HasSetter && property.Core.SetterSupportsBaseImplementation)
        {
            statements.Add(
                IdentifierName(property.ImposterBuilder.SetterImposterField.Name)
                    .Dot(IdentifierName("UseBaseImplementation"))
                    .Call()
                    .ToStatementSyntax()
            );
        }

        statements.Add(ReturnStatement(ThisExpression()));

        return new MethodDeclarationBuilder(methodMetadata.ReturnType, methodMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax)
            )
            .WithBody(Block(statements.ToArray()))
            .Build();
    }

    internal static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterPropertyMetadata property
    )
    {
        var invocationBehaviorParameter = ParameterSyntax(
            WellKnownTypes.Imposter.Abstractions.ImposterMode,
            "invocationBehavior"
        );
        var propertyDisplayLiteral = property.Core.DisplayName.StringLiteral();

        var constructorBuilder = new ConstructorBuilder(property.ImposterBuilder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(invocationBehaviorParameter);

        var bodyBuilder = new BlockBuilder()
            .AddExpression(
                IdentifierName(property.ImposterBuilder.DefaultPropertyBehaviourField.Name)
                    .Assign(property.ImposterBuilder.DefaultPropertyBehaviourField.Type.New())
            )
            .AddExpression(
                IdentifierName("_invocationBehavior").Assign(IdentifierName("invocationBehavior"))
            );

        if (property.Core.HasGetter)
        {
            var getterInitialization = IdentifierName(
                    property.ImposterBuilder.GetterImposterBuilderField.Name
                )
                .Assign(
                    property.ImposterBuilder.GetterImposterBuilderField.Type.New(
                        ArgumentListSyntax([
                            Argument(
                                IdentifierName(
                                    property.ImposterBuilder.DefaultPropertyBehaviourField.Name
                                )
                            ),
                            Argument(IdentifierName("_invocationBehavior")),
                            Argument(propertyDisplayLiteral),
                        ])
                    )
                );

            bodyBuilder.AddExpression(getterInitialization);
        }

        if (property.Core.HasSetter)
        {
            var setterArguments = new[]
            {
                Argument(
                    IdentifierName(property.ImposterBuilder.DefaultPropertyBehaviourField.Name)
                ),
                Argument(IdentifierName("_invocationBehavior")),
                Argument(propertyDisplayLiteral),
            };

            var setterInitialization = IdentifierName(
                    property.ImposterBuilder.SetterImposterField.Name
                )
                .Assign(
                    property.ImposterBuilder.SetterImposterField.Type.New(
                        ArgumentListSyntax(setterArguments)
                    )
                );

            bodyBuilder.AddExpression(setterInitialization);
        }

        return constructorBuilder.WithBody(bodyBuilder.Build()).Build();
    }
}
