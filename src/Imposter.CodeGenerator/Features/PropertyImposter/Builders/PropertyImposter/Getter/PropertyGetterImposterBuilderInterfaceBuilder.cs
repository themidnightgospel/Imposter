using System;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Getter;

internal static class PropertyGetterImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasGetter)
        {
            return [];
        }
        
        return
        [
            BuildOutcomeInterface(property),
            BuildContinuationInterface(property),
            BuildVerificationInterface(property),
            BuildFullInterface(property)
        ];
    }

    private static InterfaceDeclarationSyntax BuildFullInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.GetterImposterBuilderInterface.Name)
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.OutcomeInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(property.GetterImposterBuilderInterface.VerificationInterfaceTypeSyntax))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();

    private static InterfaceDeclarationSyntax BuildOutcomeInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.GetterImposterBuilderInterface.OutcomeInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(BuildReturnsValueMethod(property))
            .AddMembers(BuildThrowsValueMethod(property))
            .Build();

    private static InterfaceDeclarationSyntax BuildContinuationInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.GetterImposterBuilderInterface.ContinuationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildGetterCallbackMethod(property))
            .AddMember(BuildThenMethod(property))
            .Build();

    private static InterfaceDeclarationSyntax BuildVerificationInterface(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.GetterImposterBuilderInterface.VerificationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildGetterCalledMethod(property))
            .Build();

    internal static MethodDeclarationSyntax BuildGetterCalledMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.CalledMethod.ReturnType, property.GetterImposterBuilderInterface.CalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterImposterBuilderInterface.CalledMethod.CountParameter))
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax BuildGetterCallbackMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.CallbackMethod.ReturnType, property.GetterImposterBuilderInterface.CallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterImposterBuilderInterface.CallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax[] BuildThrowsValueMethod(in ImposterPropertyMetadata property) =>
    [
        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.ThrowsMethod.ReturnType, property.GetterImposterBuilderInterface.ThrowsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterImposterBuilderInterface.ThrowsMethod.ExceptionParameter))
            .WithSemicolon()
            .Build(),

        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.ThrowsMethod.ReturnType, property.GetterImposterBuilderInterface.ThrowsMethod.Name)
            .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
            .AddConstraintClause(TypeParameterConstraintClause("TException").AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
            .WithSemicolon()
            .Build(),
    ];

    internal static MethodDeclarationSyntax[] BuildReturnsValueMethod(in ImposterPropertyMetadata property) =>
    [
        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.ReturnsMethod.ReturnType, property.GetterImposterBuilderInterface.ReturnsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterImposterBuilderInterface.ReturnsMethod.ValueParameter))
            .WithSemicolon()
            .Build(),

        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.ReturnsMethod.ReturnType, property.GetterImposterBuilderInterface.ReturnsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterImposterBuilderInterface.ReturnsMethod.ValueGeneratorParameter))
            .WithSemicolon()
            .Build(),
    ];

    internal static MethodDeclarationSyntax BuildThenMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.ThenMethod.ReturnType, property.GetterImposterBuilderInterface.ThenMethod.Name)
            .WithSemicolon()
            .Build();
}
