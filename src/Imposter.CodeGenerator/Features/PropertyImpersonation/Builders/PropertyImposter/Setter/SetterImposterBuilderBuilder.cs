using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;

internal static class SetterImposterBuilderBuilder
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
                    ? SinglePrivateReadonlyVariableField(criteriaField)
                    : null
            )
            .AddMember(BuildConstructor(property))
            .AddMember(BuildCallbackMethod(property))
            .AddMember(BuildCalledMethod(property))
            .AddMember(BuildThenMethod(property))
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

        return constructor.Build();
    }

    internal static MethodDeclarationSyntax BuildCalledMethod(
        in ImposterPropertyMetadata property
    ) =>
        new MethodDeclarationBuilder(
            property.SetterImposterBuilderInterface.CalledMethod.ReturnType,
            property.SetterImposterBuilderInterface.CalledMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                property.SetterImposterBuilderInterface.VerificationInterfaceTypeSyntax
            )
            .AddParameter(
                ParameterSyntax(property.SetterImposterBuilderInterface.CalledMethod.CountParameter)
            )
            .WithBody(
                Block(
                    IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                        .Dot(IdentifierName(property.SetterImposter.CalledMethod.Name))
                        .Call(
                            SetterImposterArguments(
                                property,
                                property
                                    .SetterImposterBuilderInterface
                                    .CalledMethod
                                    .CountParameter
                                    .Name
                            )
                        )
                        .ToStatementSyntax()
                )
            )
            .Build();

    internal static MethodDeclarationSyntax BuildCallbackMethod(
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
                                    .Name
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
        string argumentName
    )
    {
        var arguments = new List<ArgumentSyntax>();
        if (property.SetterImposter.Builder.CriteriaField is { } criteriaField)
        {
            arguments.Add(Argument(IdentifierName(criteriaField.Name)));
        }

        arguments.Add(Argument(IdentifierName(argumentName)));
        return ArgumentListSyntax(arguments);
    }

    private static MethodDeclarationSyntax BuildThenMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(
            property.SetterImposterBuilderInterface.ThenMethod.ReturnType,
            property.SetterImposterBuilderInterface.ThenMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                property.SetterImposterBuilderInterface.ThenMethod.InterfaceSyntax
            )
            .WithBody(Block(ReturnThis))
            .Build();

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
                        .Call()
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }
}
