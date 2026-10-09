using System;
using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;

internal static class PropertySetterImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasSetter)
        {
            return Array.Empty<MemberDeclarationSyntax>();
        }

        var members = new List<MemberDeclarationSyntax>
        {
            BuildCallbackInterface(property),
            BuildContinuationInterface(property),
            BuildFluentInterface(property),
            BuildVerificationInterface(property),
            BuildBuilderInterface(property),
        };

        if (
            property.SetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceTypeSyntax
            is not null
        )
        {
            members.Add(BuildUseBaseImplementationEntryInterface(property));
        }

        return members.ToArray();
    }

    private static InterfaceDeclarationSyntax BuildBuilderInterface(
        in ImposterPropertyMetadata property
    )
    {
        var builder = new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(
                SimpleBaseType(property.SetterImposterBuilderInterface.CallbackInterfaceTypeSyntax)
            )
            .AddBaseType(
                SimpleBaseType(
                    property.SetterImposterBuilderInterface.VerificationInterfaceTypeSyntax
                )
            );

        if (
            property.SetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceTypeSyntax is
            { } useBaseImplementationInterface
        )
        {
            builder = builder.AddBaseType(SimpleBaseType(useBaseImplementationInterface));
        }

        return builder.Build();
    }

    private static InterfaceDeclarationSyntax BuildFluentInterface(
        in ImposterPropertyMetadata property
    ) =>
        new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.FluentInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(
                SimpleBaseType(property.SetterImposterBuilderInterface.CallbackInterfaceTypeSyntax)
            )
            .AddBaseType(
                SimpleBaseType(
                    property.SetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax
                )
            )
            .Build();

    private static InterfaceDeclarationSyntax BuildCallbackInterface(
        in ImposterPropertyMetadata property
    )
    {
        var callback = property.SetterImposterBuilderInterface.CallbackMethod;

        return new InterfaceDeclarationBuilder(
            property.SetterImposterBuilderInterface.CallbackInterfaceName
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
        var then = property.SetterImposterBuilderInterface.ThenMethod;

        return new InterfaceDeclarationBuilder(
            property.SetterImposterBuilderInterface.ContinuationInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(
                SimpleBaseType(property.SetterImposterBuilderInterface.CallbackInterfaceTypeSyntax)
            )
            .AddMember(InterfaceMethod(then.ReturnType, then.Name))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildVerificationInterface(
        in ImposterPropertyMetadata property
    )
    {
        var called = property.SetterImposterBuilderInterface.CalledMethod;

        return new InterfaceDeclarationBuilder(
            property.SetterImposterBuilderInterface.VerificationInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(InterfaceMethod(called.ReturnType, called.Name, called.CountParameter))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildUseBaseImplementationEntryInterface(
        in ImposterPropertyMetadata property
    )
    {
        var method = property
            .SetterImposterBuilderInterface
            .UseBaseImplementationEntryMethod!
            .Value;

        return new InterfaceDeclarationBuilder(
            property.SetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceName!
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(
                SimpleBaseType(property.SetterImposterBuilderInterface.FluentInterfaceTypeSyntax)
            )
            .AddMember(InterfaceMethod(method.ReturnType, method.Name))
            .Build();
    }
}
