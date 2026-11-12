using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Getter;

internal static partial class GetterImposterBuilderBuilder
{
    private static ClassDeclarationSyntax BuildShared(in ImposterPropertyMetadata property)
    {
        var getter = property.GetterImposterBuilder;
        var builderInterface = property.GetterImposterBuilderInterface;
        var builder = new ClassDeclarationBuilder(getter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.SharedRuntime.GetterType))
            .AddBaseType(SimpleBaseType(builderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(builderInterface.FluentInterfaceTypeSyntax))
            .AddMember(
                SharedPropertyBuilder.BuildConstructor(property, getter.Name, withOwner: true)
            );

        var returns = builderInterface.ReturnsMethod;
        builder.AddMember(
            Forward(
                returns.ReturnType,
                returns.Name,
                returns.InterfaceSyntax,
                returns.ValueParameter!.Value,
                property.Core.KeptValue(IdentifierName(returns.ValueParameter.Value.Name))
            )
        );
        builder.AddMember(
            Forward(
                returns.ReturnType,
                returns.Name,
                returns.InterfaceSyntax,
                returns.ValueGeneratorParameter
            )
        );

        var throws = builderInterface.ThrowsMethod;
        builder.AddMember(
            Forward(
                throws.ReturnType,
                throws.Name,
                throws.InterfaceSyntax,
                throws.ExceptionParameter
            )
        );
        builder.AddMember(
            new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
                .WithTypeParameters(throws.ExceptionTypeParameter.TypeParameterList)
                .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
                .WithBody(
                    Block(
                        BaseExpression()
                            .Dot(
                                GenericName(
                                    Identifier(throws.Name),
                                    TypeArgumentList(
                                        SingletonSeparatedList<TypeSyntax>(
                                            IdentifierName(throws.ExceptionTypeParameter.Name)
                                        )
                                    )
                                )
                            )
                            .Call()
                            .ToStatementSyntax(),
                        ReturnThis
                    )
                )
                .Build()
        );

        var callback = builderInterface.CallbackMethod;
        builder.AddMember(
            Forward(
                callback.ReturnType,
                callback.Name,
                callback.InterfaceSyntax,
                callback.CallbackParameter
            )
        );

        var called = builderInterface.CalledMethod;
        builder.AddMember(
            new MethodDeclarationBuilder(called.ReturnType, called.Name)
                .WithExplicitInterfaceSpecifier(builderInterface.VerificationInterfaceTypeSyntax)
                .AddParameter(ParameterSyntax(called.CountParameter))
                .AddParameter(ParameterSyntax(called.BecauseParameter))
                .WithBody(
                    Block(
                        BaseExpression()
                            .Dot(IdentifierName(called.Name))
                            .Call([
                                Argument(IdentifierName(called.CountParameter.Name)),
                                Argument(IdentifierName(called.BecauseParameter.Name)),
                            ])
                            .ToStatementSyntax()
                    )
                )
                .Build()
        );
        builder.AddMember(
            new MethodDeclarationBuilder(
                builderInterface.ContinuationInterfaceTypeSyntax,
                VerificationSourceNames.LenientMethodName
            )
                .WithExplicitInterfaceSpecifier(builderInterface.ContinuationInterfaceTypeSyntax)
                .WithBody(
                    Block(
                        BaseExpression()
                            .Dot(IdentifierName("MarkLenient"))
                            .Call()
                            .ToStatementSyntax(),
                        ReturnThis
                    )
                )
                .Build()
        );
        builder.AddMember(
            new MethodDeclarationBuilder(called.ReturnType, called.Name)
                .WithExplicitInterfaceSpecifier(builderInterface.VerificationInterfaceTypeSyntax)
                .AddParameter(ParameterSyntax(called.CountParameter))
                .WithBody(
                    Block(
                        BaseExpression()
                            .Dot(IdentifierName(called.Name))
                            .Call(Argument(IdentifierName(called.CountParameter.Name)))
                            .ToStatementSyntax()
                    )
                )
                .Build()
        );

        if (builderInterface.UseBaseImplementationEntryMethod is { } entry)
        {
            builder.AddBaseType(SimpleBaseType(entry.InterfaceSyntax));
        }

        return builder
            .AddMembers(BuildThenMethods(property))
            .AddMembers(BuildUseBaseImplementationMethods(property))
            .Build();
    }

    private static MethodDeclarationSyntax Forward(
        TypeSyntax returnType,
        string name,
        NameSyntax interfaceSyntax,
        ParameterMetadata parameter,
        ExpressionSyntax? argument = null
    ) =>
        new MethodDeclarationBuilder(returnType, name)
            .WithExplicitInterfaceSpecifier(interfaceSyntax)
            .AddParameter(ParameterSyntax(parameter))
            .WithBody(
                Block(
                    BaseExpression()
                        .Dot(IdentifierName(name))
                        .Call(Argument(argument ?? IdentifierName(parameter.Name)))
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
}
