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
            // The default (auto-property) behaviour stores the last value set so the getter can return it.
            .AddMember(
                property.Core.KeepsValue
                    ? SinglePrivateReadonlyVariableField(
                        property.ImposterBuilder.DefaultPropertyBehaviourField
                    )
                    : null
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(property.ImposterBuilder.InvocationBehaviorField)
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
            .AddMember(
                property.Core.KeepsValue
                    ? DefaultPropertyBehaviourBuilder.Build(property.DefaultPropertyBehaviour)
                    : null
            )
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

        var criteria = property.ImposterBuilderInterface.SetterMethod.CriteriaParameter;
        var builderArguments = new List<ArgumentSyntax>
        {
            Argument(IdentifierName(property.ImposterBuilder.SetterImposterField.Name)),
        };
        if (criteria is not null)
        {
            builderArguments.Add(Argument(IdentifierName(criteria.Value.Name)));
        }

        return new MethodDeclarationBuilder(
            property.ImposterBuilderInterface.SetterMethod.ReturnType,
            property.ImposterBuilderInterface.SetterMethod.Name
        )
            .WithExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax)
            .AddParameter(criteria is null ? null : ParameterSyntax(criteria.Value))
            .WithBody(
                Block(
                    IdentifierName(property.ImposterBuilder.SetterImposterField.Name)
                        .Dot(IdentifierName(property.SetterImposter.MarkConfiguredMethod.Name))
                        .Call()
                        .ToStatementSyntax(),
                    ReturnStatement(
                        property.SetterImposter.Builder.TypeSyntax.New(
                            ArgumentListSyntax(builderArguments)
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
            .WithExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax)
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
                    .Dot(
                        IdentifierName(
                            property.GetterImposterBuilder.EnableBaseImplementationMethod.Name
                        )
                    )
                    .Call()
                    .ToStatementSyntax()
            );
        }

        if (property.Core.HasSetter && property.Core.SetterSupportsBaseImplementation)
        {
            statements.Add(
                IdentifierName(property.ImposterBuilder.SetterImposterField.Name)
                    .Dot(IdentifierName(property.SetterImposter.UseBaseImplementationMethod.Name))
                    .Call()
                    .ToStatementSyntax()
            );
        }

        statements.Add(ReturnThis);

        return new MethodDeclarationBuilder(methodMetadata.ReturnType, methodMetadata.Name)
            .WithExplicitInterfaceSpecifier(property.ImposterBuilderInterface.Syntax)
            .WithBody(Block(statements.ToArray()))
            .Build();
    }

    internal static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterPropertyMetadata property
    )
    {
        var invocationBehaviorField = IdentifierName(
            property.ImposterBuilder.InvocationBehaviorField.Name
        );
        var propertyDisplayLiteral = property.Core.DisplayName.StringLiteral();

        var constructorBuilder = new ConstructorBuilder(property.ImposterBuilder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(ParameterSyntax(property.ImposterBuilder.InvocationBehaviorParameter));

        var bodyBuilder = new BlockBuilder();

        if (property.Core.KeepsValue)
        {
            bodyBuilder.AddExpression(
                IdentifierName(property.ImposterBuilder.DefaultPropertyBehaviourField.Name)
                    .Assign(property.ImposterBuilder.DefaultPropertyBehaviourField.Type.New())
            );
        }

        bodyBuilder.AddExpression(
            invocationBehaviorField.Assign(
                IdentifierName(property.ImposterBuilder.InvocationBehaviorParameter.Name)
            )
        );

        // The getter and setter imposters share the default behaviour, when there is one.
        var accessorImposterArguments = new List<ArgumentSyntax>();
        if (property.Core.KeepsValue)
        {
            accessorImposterArguments.Add(
                Argument(
                    IdentifierName(property.ImposterBuilder.DefaultPropertyBehaviourField.Name)
                )
            );
        }

        accessorImposterArguments.Add(Argument(invocationBehaviorField));
        accessorImposterArguments.Add(Argument(propertyDisplayLiteral));

        if (property.Core.HasGetter)
        {
            var getterInitialization = IdentifierName(
                    property.ImposterBuilder.GetterImposterBuilderField.Name
                )
                .Assign(
                    property.ImposterBuilder.GetterImposterBuilderField.Type.New(
                        ArgumentListSyntax(accessorImposterArguments)
                    )
                );

            bodyBuilder.AddExpression(getterInitialization);
        }

        if (property.Core.HasSetter)
        {
            var setterInitialization = IdentifierName(
                    property.ImposterBuilder.SetterImposterField.Name
                )
                .Assign(
                    property.ImposterBuilder.SetterImposterField.Type.New(
                        ArgumentListSyntax(accessorImposterArguments)
                    )
                );

            bodyBuilder.AddExpression(setterInitialization);
        }

        return constructorBuilder.WithBody(bodyBuilder.Build()).Build();
    }
}
