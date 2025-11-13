using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Setter;

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
            BuildBuilderInterface(property)
        };

        if (property.SetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceTypeSyntax is not null)
        {
            members.Add(BuildUseBaseImplementationEntryInterface(property));
        }

        return members.ToArray();
    }

    private static InterfaceDeclarationSyntax BuildBuilderInterface(in ImposterPropertyMetadata property)
    {
        var builder = new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.VerificationInterfaceTypeSyntax));

        if (property.SetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceTypeSyntax is { } useBaseImplementationInterface)
        {
            builder = builder.AddBaseType(SimpleBaseType(useBaseImplementationInterface));
        }

        return builder
            .AddMember(BuildInitialThenMethod(property))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildFluentInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.FluentInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax))
            .Build();

    private static InterfaceDeclarationSyntax BuildCallbackInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.CallbackInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildSetterCallbackMethod(property))
            .Build();

    private static InterfaceDeclarationSyntax BuildContinuationInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.ContinuationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddMember(BuildThenMethod(property))
            .Build();

    private static InterfaceDeclarationSyntax BuildVerificationInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.VerificationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildSetterCalledMethod(property))
            .Build();

    private static InterfaceDeclarationSyntax BuildUseBaseImplementationEntryInterface(in ImposterPropertyMetadata property)
    {
        var method = property.SetterImposterBuilderInterface.UseBaseImplementationEntryMethod!.Value;

        return new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.UseBaseImplementationEntryInterfaceName!)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(property.SetterImposterBuilderInterface.FluentInterfaceTypeSyntax))
            .AddMember(
                new MethodDeclarationBuilder(method.ReturnType, method.Name)
                    .WithSemicolon()
                    .Build())
            .Build();
    }

    internal static MethodDeclarationSyntax BuildSetterCalledMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.CalledMethod.ReturnType, property.SetterImposterBuilderInterface.CalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterImposterBuilderInterface.CalledMethod.CountParameter))
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax BuildSetterCallbackMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.CallbackMethod.ReturnType, property.SetterImposterBuilderInterface.CallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterImposterBuilderInterface.CallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax BuildThenMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.ThenMethod.ReturnType, property.SetterImposterBuilderInterface.ThenMethod.Name)
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax? BuildInitialThenMethod(in ImposterPropertyMetadata property)
    {
        if (property.SetterImposterBuilderInterface.InitialThenMethod is not { } method)
        {
            return null;
        }

        return new MethodDeclarationBuilder(method.ReturnType, method.Name)
            .WithSemicolon()
            .Build();
    }
}
