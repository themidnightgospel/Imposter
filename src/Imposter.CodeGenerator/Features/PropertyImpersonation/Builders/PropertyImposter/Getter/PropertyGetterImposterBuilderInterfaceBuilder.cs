using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FluentInterfaceBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Getter;

internal static class PropertyGetterImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasGetter)
        {
            return [];
        }

        var members = new List<MemberDeclarationSyntax>
        {
            BuildOutcomeInterface(property),
            CallbackInterface(
                property.GetterImposterBuilderInterface.CallbackInterfaceName,
                property.GetterImposterBuilderInterface.CallbackMethod
            ),
            ContinuationInterface(
                    property.GetterImposterBuilderInterface.ContinuationInterfaceName,
                    property.GetterImposterBuilderInterface.CallbackInterfaceTypeSyntax,
                    property.GetterImposterBuilderInterface.ThenMethod
                )
                .AddMembers(
                    InterfaceMethod(
                        property.GetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax,
                        VerificationSourceNames.LenientMethodName
                    )
                ),
            VerificationInterface(
                property.GetterImposterBuilderInterface.VerificationInterfaceName,
                property.GetterImposterBuilderInterface.CalledMethod
            ),
            BuildFluentInterface(property),
            BuildBuilderInterface(property),
        };

        if (property.GetterImposterBuilderInterface.UseBaseImplementationEntryMethod is not null)
        {
            members.Add(BuildUseBaseImplementationEntryInterface(property));
        }

        return members.ToArray();
    }

    private static InterfaceDeclarationSyntax BuildBuilderInterface(
        in ImposterPropertyMetadata property
    )
    {
        var builder = new InterfaceDeclarationBuilder(property.GetterImposterBuilderInterface.Name)
            .AddBaseType(
                SimpleBaseType(property.GetterImposterBuilderInterface.OutcomeInterfaceTypeSyntax)
            )
            .AddBaseType(
                SimpleBaseType(property.GetterImposterBuilderInterface.CallbackInterfaceTypeSyntax)
            )
            .AddBaseType(
                SimpleBaseType(
                    property.GetterImposterBuilderInterface.VerificationInterfaceTypeSyntax
                )
            );

        if (
            property.GetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceTypeSyntax is
            { } useBaseImplementationInterface
        )
        {
            builder = builder.AddBaseType(SimpleBaseType(useBaseImplementationInterface));
        }

        return builder
            .AddMember(
                property.GetterImposterBuilderInterface.InitialThenMethod is { } initialThen
                    ? InterfaceMethod(initialThen.ReturnType, initialThen.Name)
                    : null
            )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildFluentInterface(
        in ImposterPropertyMetadata property
    ) =>
        new InterfaceDeclarationBuilder(property.GetterImposterBuilderInterface.FluentInterfaceName)
            .AddBaseType(
                SimpleBaseType(property.GetterImposterBuilderInterface.OutcomeInterfaceTypeSyntax)
            )
            .AddBaseType(
                SimpleBaseType(
                    property.GetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax
                )
            )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                property.GetterImposterBuilderInterface.UseBaseImplementationMethod is { } method
                    ? InterfaceMethod(method.ReturnType, method.Name)
                    : null
            )
            .Build();

    private static InterfaceDeclarationSyntax BuildUseBaseImplementationEntryInterface(
        in ImposterPropertyMetadata property
    )
    {
        var method = property
            .GetterImposterBuilderInterface
            .UseBaseImplementationEntryMethod!
            .Value;

        return new InterfaceDeclarationBuilder(
            property.GetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceName!
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(InterfaceMethod(method.ReturnType, method.Name))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildOutcomeInterface(
        in ImposterPropertyMetadata property
    ) =>
        new InterfaceDeclarationBuilder(
            property.GetterImposterBuilderInterface.OutcomeInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(BuildReturnsMethods(property.GetterImposterBuilderInterface))
            .AddMembers(BuildThrowsMethods(property.GetterImposterBuilderInterface))
            .Build();

    private static MethodDeclarationSyntax[] BuildReturnsMethods(
        PropertyGetterImposterBuilderInterfaceMetadata getterInterface
    )
    {
        var returns = getterInterface.ReturnsMethod;
        var returnsGeneratedValue = InterfaceMethod(
            returns.ReturnType,
            returns.Name,
            returns.ValueGeneratorParameter
        );

        return returns.ValueParameter is { } value
            ? [InterfaceMethod(returns.ReturnType, returns.Name, value), returnsGeneratedValue]
            : [returnsGeneratedValue];
    }

    private static MethodDeclarationSyntax[] BuildThrowsMethods(
        PropertyGetterImposterBuilderInterfaceMetadata getterInterface
    )
    {
        var throws = getterInterface.ThrowsMethod;

        return
        [
            InterfaceMethod(throws.ReturnType, throws.Name, throws.ExceptionParameter),
            new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
                .WithTypeParameters(throws.ExceptionTypeParameter.TypeParameterList)
                .AddConstraintClause(throws.ExceptionTypeParameter.ConstraintClause)
                .WithSemicolon()
                .Build(),
        ];
    }
}
