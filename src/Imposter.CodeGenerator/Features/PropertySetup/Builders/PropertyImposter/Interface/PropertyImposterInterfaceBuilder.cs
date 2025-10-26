using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Interface;

internal static class PropertyImposterInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterTargetPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.PropertyImposterInterface.Name)
            .AddMembers(BuildReturnsValueMethod(property))
            .AddMembers(BuildThrowsValueMethod(property))
            .AddMember(BuildSetterCallbackMethod(property))
            .AddMember(BuildGetterCallbackMethod(property))
            .AddMember(BuildGetterCalledMethod(property))
            .AddMember(BuildSetterCalledMethod(property))
            .Build(TokenList(Token(SyntaxKind.PublicKeyword)));

    internal static MethodDeclarationSyntax? BuildSetterCalledMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.SetterCalledMethod.ReturnType, property.SetterCalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterCalledMethod.CriteriaParameter))
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterCalledMethod.CountParameter))
            .WithSemicolon()
            .Build();
    }
    
    internal static MethodDeclarationSyntax? BuildGetterCalledMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.GetterCalledMethod.ReturnType, property.GetterCalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterCalledMethod.CountParameter))
            .WithSemicolon()
            .Build();
    }
    
    internal static MethodDeclarationSyntax? BuildGetterCallbackMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.GetterCallbackMethod.ReturnType, property.GetterCallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterCallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();
    }

    internal static MethodDeclarationSyntax? BuildSetterCallbackMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.SetterCallbackMethod.ReturnType, property.SetterCallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterCallbackMethod.CriteriaParameter))
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterCallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();
    }
    
    internal static MethodDeclarationSyntax[]? BuildThrowsValueMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return
        [
            new MethodDeclarationBuilder(property.ThrowsMethod.ReturnType, property.ThrowsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.ThrowsMethod.ExceptionParameter))
                .WithSemicolon()
                .Build(),

            new MethodDeclarationBuilder(property.ThrowsMethod.ReturnType, property.ThrowsMethod.Name)
                .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
                .AddConstraintClause(TypeParameterConstraintClause("TException").AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
                .WithSemicolon()
                .Build(),
        ];
    }

    internal static MethodDeclarationSyntax[]? BuildReturnsValueMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return
        [
            new MethodDeclarationBuilder(property.ReturnsMethod.ReturnType, property.ReturnsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.ReturnsMethod.ValueParameter))
                .WithSemicolon()
                .Build(),

            new MethodDeclarationBuilder(property.ReturnsMethod.ReturnType, property.ReturnsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.ReturnsMethod.ValueGeneratorParameter))
                .WithSemicolon()
                .Build(),
        ];
    }
}