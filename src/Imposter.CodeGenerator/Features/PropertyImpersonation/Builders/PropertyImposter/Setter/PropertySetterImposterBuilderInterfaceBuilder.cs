using System;
using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FluentInterfaceBuilder;
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
            CallbackInterface(
                property.SetterImposterBuilderInterface.CallbackInterfaceName,
                property.SetterImposterBuilderInterface.CallbackMethod
            ),
            ContinuationInterface(
                property.SetterImposterBuilderInterface.ContinuationInterfaceName,
                property.SetterImposterBuilderInterface.CallbackInterfaceTypeSyntax,
                property.SetterImposterBuilderInterface.ThenMethod
            ),
            BuildFluentInterface(property),
            VerificationInterface(
                property.SetterImposterBuilderInterface.VerificationInterfaceName,
                property.SetterImposterBuilderInterface.CalledMethod
            ),
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
