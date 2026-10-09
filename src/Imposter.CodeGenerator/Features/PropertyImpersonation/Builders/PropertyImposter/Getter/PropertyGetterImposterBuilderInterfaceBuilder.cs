using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            BuildCallbackInterface(property),
            BuildContinuationInterface(property),
            BuildVerificationInterface(property),
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

    private static InterfaceDeclarationSyntax BuildCallbackInterface(
        in ImposterPropertyMetadata property
    )
    {
        var callback = property.GetterImposterBuilderInterface.CallbackMethod;

        return new InterfaceDeclarationBuilder(
            property.GetterImposterBuilderInterface.CallbackInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                InterfaceMethod(callback.ReturnType, callback.Name, callback.CallbackParameter)
            )
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildContinuationInterface(
        in ImposterPropertyMetadata property
    )
    {
        var then = property.GetterImposterBuilderInterface.ThenMethod;

        return new InterfaceDeclarationBuilder(
            property.GetterImposterBuilderInterface.ContinuationInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(
                SimpleBaseType(property.GetterImposterBuilderInterface.CallbackInterfaceTypeSyntax)
            )
            .AddMember(InterfaceMethod(then.ReturnType, then.Name))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildVerificationInterface(
        in ImposterPropertyMetadata property
    )
    {
        var called = property.GetterImposterBuilderInterface.CalledMethod;

        return new InterfaceDeclarationBuilder(
            property.GetterImposterBuilderInterface.VerificationInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(InterfaceMethod(called.ReturnType, called.Name, called.CountParameter))
            .Build();
    }

    private static MethodDeclarationSyntax[] BuildReturnsMethods(
        PropertyGetterImposterBuilderInterfaceMetadata getterInterface
    )
    {
        var returns = getterInterface.ReturnsMethod;

        return
        [
            InterfaceMethod(returns.ReturnType, returns.Name, returns.ValueParameter),
            InterfaceMethod(returns.ReturnType, returns.Name, returns.ValueGeneratorParameter),
        ];
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
