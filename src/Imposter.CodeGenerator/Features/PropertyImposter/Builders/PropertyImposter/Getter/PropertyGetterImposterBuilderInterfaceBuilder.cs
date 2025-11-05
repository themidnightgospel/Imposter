using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Getter;

internal static class PropertyGetterImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasGetter)
        {
            return null;
        }
        
        return new InterfaceDeclarationBuilder(property.GetterImposterBuilderInterface.Name)
            .AddMembers(BuildReturnsValueMethod(property))
            .AddMembers(BuildThrowsValueMethod(property))
            .AddMember(BuildGetterCallbackMethod(property))
            .AddMember(BuildGetterCalledMethod(property))
            .AddMember(BuildThenMethod(property))
            .Build(TokenList(Token(SyntaxKind.PublicKeyword)));
    }

    internal static MethodDeclarationSyntax? BuildGetterCalledMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.CalledMethod.ReturnType, property.GetterImposterBuilderInterface.CalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterImposterBuilderInterface.CalledMethod.CountParameter))
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax? BuildGetterCallbackMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.GetterImposterBuilderInterface.CallbackMethod.ReturnType, property.GetterImposterBuilderInterface.CallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.GetterImposterBuilderInterface.CallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax[]? BuildThrowsValueMethod(in ImposterPropertyMetadata property) =>
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
