using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
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
                SinglePrivateReadonlyVariableField(property.SetterImposter.Builder.CriteriaField)
            )
            .AddMember(BuildConstructor(property))
            .AddMember(BuildCallbackMethod(property))
            .AddMember(BuildCalledMethod(property))
            .AddMember(BuildThenMethod(property))
            .AddMember(BuildInitialThenMethod(property))
            .AddMember(BuildUseBaseImplementationEntryMethod(property))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterPropertyMetadata property
    ) =>
        new ConstructorWithFieldInitializationBuilder(property.SetterImposter.Builder.Name)
            .WithModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameter(property.SetterImposter.Builder.SetterImposterField)
            .AddParameter(property.SetterImposter.Builder.CriteriaField)
            .Build();

    internal static MethodDeclarationSyntax BuildCalledMethod(
        in ImposterPropertyMetadata property
    ) =>
        new MethodDeclarationBuilder(
            property.SetterImposterBuilderInterface.CalledMethod.ReturnType,
            property.SetterImposterBuilderInterface.CalledMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    property.SetterImposterBuilderInterface.VerificationInterfaceTypeSyntax
                )
            )
            .AddParameter(
                ParameterSyntax(property.SetterImposterBuilderInterface.CalledMethod.CountParameter)
            )
            .WithBody(
                Block(
                    IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                        .Dot(IdentifierName(property.SetterImposter.CalledMethod.Name))
                        .Call(
                            ArgumentListSyntax([
                                Argument(
                                    IdentifierName(
                                        property.SetterImposter.Builder.CriteriaField.Name
                                    )
                                ),
                                Argument(
                                    IdentifierName(
                                        property
                                            .SetterImposterBuilderInterface
                                            .CalledMethod
                                            .CountParameter
                                            .Name
                                    )
                                ),
                            ])
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
                ExplicitInterfaceSpecifier(
                    property.SetterImposterBuilderInterface.CallbackMethod.InterfaceSyntax
                )
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
                            ArgumentListSyntax([
                                Argument(
                                    IdentifierName(
                                        property.SetterImposter.Builder.CriteriaField.Name
                                    )
                                ),
                                Argument(
                                    IdentifierName(
                                        property
                                            .SetterImposterBuilderInterface
                                            .CallbackMethod
                                            .CallbackParameter
                                            .Name
                                    )
                                ),
                            ])
                        )
                        .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildThenMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(
            property.SetterImposterBuilderInterface.ThenMethod.ReturnType,
            property.SetterImposterBuilderInterface.ThenMethod.Name
        )
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    property.SetterImposterBuilderInterface.ThenMethod.InterfaceSyntax
                )
            )
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();

    private static MethodDeclarationSyntax? BuildInitialThenMethod(
        in ImposterPropertyMetadata property
    )
    {
        if (property.SetterImposterBuilderInterface.InitialThenMethod is not { } method)
        {
            return null;
        }

        return new MethodDeclarationBuilder(method.ReturnType, method.Name)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InterfaceSyntax))
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();
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
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InterfaceSyntax))
            .WithBody(
                Block(
                    IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name)
                        .Dot(IdentifierName("UseBaseImplementation"))
                        .Call()
                        .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            )
            .Build();
    }
}
