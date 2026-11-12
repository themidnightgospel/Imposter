using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.ThenMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;

internal static partial class SetterImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterPropertyMetadata property)
    {
        var builder = new ClassDeclarationBuilder(property.SetterImposter.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.Syntax))
            .AddBaseType(
                SimpleBaseType(property.SetterImposterBuilderInterface.FluentInterfaceTypeSyntax)
            );

        if (
            property.SetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceTypeSyntax
            is not null
        )
        {
            builder = builder.AddBaseType(
                SimpleBaseType(
                    property
                        .SetterImposterBuilderInterface
                        .UseBaseImplementationEntryInterfaceTypeSyntax
                )
            );
        }

        return builder
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    property.SetterImposter.Builder.SetterImposterField
                )
            )
            .AddMember(
                property.SetterImposter.Builder.CriteriaField is { } criteriaField
                && !property.Core.UsesSharedRuntime
                    ? SinglePrivateReadonlyVariableField(criteriaField)
                    : null
            )
            .AddMember(
                !property.Core.IsPassedThrough
                    ? SinglePrivateReadonlyVariableField(
                        property.SetterImposter.Builder.RuntimeCriteriaField
                    )
                    : null
            )
            .AddMember(BuildConstructor(property))
            .AddMember(
                SinglePrivateReadonlyVariableField(property.SetterImposter.Builder.SetupField)
            )
            .AddMembers(BuildSharedVerificationMembers(property))
            .AddMember(BuildCallbackMethod(property))
            .AddMember(BuildCalledMethod(property))
            .AddMember(ThenReturningThis(property.SetterImposterBuilderInterface.ThenMethod))
            .AddMember(BuildUseBaseImplementationEntryMethod(property))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterPropertyMetadata property
    )
    {
        var constructor = new ConstructorWithFieldInitializationBuilder(
            property.SetterImposter.Builder.Name
        )
            .WithModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameter(property.SetterImposter.Builder.SetterImposterField);

        if (property.SetterImposter.Builder.CriteriaField is { } criteriaField)
        {
            constructor.AddParameter(criteriaField);
        }

        if (!property.Core.IsPassedThrough)
        {
            var metadata = property.SetterImposter.Builder;
            ExpressionSyntax criteria = IdentifierName(metadata.CriteriaField!.Value.Name);
            if (!property.Core.UsesSharedRuntime)
                criteria = metadata
                    .RuntimeCriteriaField.Type.Dot(IdentifierName("Is"))
                    .Call(
                        Argument(
                            SimpleLambdaExpression(
                                Parameter(Identifier(property.Core.Locals["setterValue"])),
                                criteria
                                    .Dot(IdentifierName("Matches"))
                                    .Call(
                                        Argument(
                                            IdentifierName(property.Core.Locals["setterValue"])
                                        )
                                    )
                            )
                        )
                    );
            if (!property.Core.UsesSharedRuntime)
                constructor.AddStatements([
                    IdentifierName(metadata.RuntimeCriteriaField.Name)
                        .Assign(criteria)
                        .ToStatementSyntax(),
                ]);
            constructor.AddStatements([
                IdentifierName(metadata.SetupField.Name)
                    .Assign(
                        metadata.SetupField.Type.New(
                            IdentifierName(metadata.RuntimeCriteriaField.Name)
                                .ToSingleArgumentList()
                        )
                    )
                    .ToStatementSyntax(),
            ]);
        }
        if (property.Core.IsPassedThrough)
            constructor.AddStatements(
                new StatementSyntax[]
                {
                    IdentifierName(property.SetterImposter.Builder.SetupField.Name)
                        .Assign(property.SetterImposter.Builder.SetupField.Type.New())
                        .ToStatementSyntax(),
                }
            );
        return constructor.Build();
    }

    private static MethodDeclarationSyntax BuildCalledMethod(in ImposterPropertyMetadata property)
    {
        var called = property.SetterImposterBuilderInterface.CalledMethod;
        var arguments = SetterImposterArguments(property, called.CountParameter.Name);
        var builder = new MethodDeclarationBuilder(
            property.SetterImposterBuilderInterface.CalledMethod.ReturnType,
            property.SetterImposterBuilderInterface.CalledMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                property.SetterImposterBuilderInterface.VerificationInterfaceTypeSyntax
            )
            .AddParameter(
                ParameterSyntax(property.SetterImposterBuilderInterface.CalledMethod.CountParameter)
            );
        return builder
            .WithBody(
                Block(
                    IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                        .Dot(IdentifierName(property.SetterImposter.CalledMethod.Name))
                        .Call(arguments)
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildCallbackMethod(
        in ImposterPropertyMetadata property
    ) =>
        new MethodDeclarationBuilder(
            property.SetterImposterBuilderInterface.CallbackMethod.ReturnType,
            property.SetterImposterBuilderInterface.CallbackMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                property.SetterImposterBuilderInterface.CallbackMethod.InterfaceSyntax
            )
            .AddParameter(
                ParameterSyntax(
                    property.SetterImposterBuilderInterface.CallbackMethod.CallbackParameter
                )
            )
            .WithBody(
                Block(
                    IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                        .Dot(IdentifierName(property.SetterImposter.CallbackMethod.Name))
                        .Call(
                            SetterImposterArguments(
                                property,
                                property
                                    .SetterImposterBuilderInterface
                                    .CallbackMethod
                                    .CallbackParameter
                                    .Name,
                                useSetup: true
                            )
                        )
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();

    // The setter imposter's methods take the builder's criteria first, when it has any.
    private static ArgumentListSyntax SetterImposterArguments(
        in ImposterPropertyMetadata property,
        string argumentName,
        bool useSetup = false
    )
    {
        var arguments = new List<ArgumentSyntax>();
        if (useSetup || property.SetterImposter.Builder.CriteriaField is not null)
        {
            arguments.Add(
                Argument(
                    IdentifierName(
                        useSetup
                            ? property.SetterImposter.Builder.SetupField.Name
                            : property.SetterImposter.Builder.RuntimeCriteriaField.Name
                    )
                )
            );
        }

        arguments.Add(Argument(IdentifierName(argumentName)));
        return ArgumentListSyntax(arguments);
    }

    private static MethodDeclarationSyntax? BuildUseBaseImplementationEntryMethod(
        in ImposterPropertyMetadata property
    )
    {
        if (
            property.SetterImposterBuilderInterface.UseBaseImplementationEntryMethod
            is not { } method
        )
        {
            return null;
        }

        return new MethodDeclarationBuilder(method.ReturnType, method.Name)
            .WithExplicitInterfaceSpecifier(method.InterfaceSyntax)
            .WithBody(
                Block(
                    IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                        .Dot(
                            IdentifierName(property.SetterImposter.UseBaseImplementationMethod.Name)
                        )
                        .Call(
                            IdentifierName(property.SetterImposter.Builder.SetupField.Name)
                                .ToSingleArgumentList()
                        )
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }
}
