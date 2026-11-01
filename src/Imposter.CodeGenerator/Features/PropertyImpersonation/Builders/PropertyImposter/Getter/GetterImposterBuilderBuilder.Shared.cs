using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
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
        var api = property.GetterImposterBuilderInterface;
        var runtime = new SharedPropertyMetadata(property.Core);
        var builder = new ClassDeclarationBuilder(getter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(runtime.GetterType))
            .AddBaseType(SimpleBaseType(api.TypeSyntax))
            .AddBaseType(SimpleBaseType(api.FluentInterfaceTypeSyntax))
            .AddMember(SharedPropertyBuilder.Constructor(property, getter.Name));

        var returns = api.ReturnsMethod;
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
        var throws = api.ThrowsMethod;
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
        var callback = api.CallbackMethod;
        builder.AddMember(
            Forward(
                callback.ReturnType,
                callback.Name,
                callback.InterfaceSyntax,
                callback.CallbackParameter
            )
        );
        var called = api.CalledMethod;
        builder.AddMember(
            new MethodDeclarationBuilder(called.ReturnType, called.Name)
                .WithExplicitInterfaceSpecifier(api.VerificationInterfaceTypeSyntax)
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
        builder.AddMember(BuildThenMethod(api));
        if (api.InitialThenMethod is not null)
            builder.AddMember(BuildInitialThenMethod(property));
        if (api.UseBaseImplementationEntryMethod is { } entry)
        {
            builder.AddBaseType(SimpleBaseType(entry.InterfaceSyntax));
            builder.AddMember(BuildUseBaseImplementationInterfaceMethod(getter, entry));
        }
        if (api.UseBaseImplementationMethod is { } useBase)
            builder.AddMember(BuildUseBaseImplementationInterfaceMethod(getter, useBase));
        return builder.Build();
    }

    private static MethodDeclarationSyntax Forward(
        TypeSyntax result,
        string name,
        NameSyntax api,
        ParameterMetadata parameter,
        ExpressionSyntax? argument = null
    ) =>
        new MethodDeclarationBuilder(result, name)
            .WithExplicitInterfaceSpecifier(api)
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
