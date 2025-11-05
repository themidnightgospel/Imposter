using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter.Setter;

internal static class PropertySetterImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasSetter)
        {
            return null;
        }
        
        return new InterfaceDeclarationBuilder(property.SetterImposterBuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildSetterCallbackMethod(property))
            .AddMember(BuildSetterCalledMethod(property))
            .AddMember(BuildThenMethod(property))
            .Build();
    }

    internal static MethodDeclarationSyntax BuildSetterCalledMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.CalledMethod.ReturnType, property.SetterImposterBuilderInterface.CalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterImposterBuilderInterface.CalledMethod.CountParameter))
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax? BuildSetterCallbackMethod(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasSetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.CallbackMethod.ReturnType, property.SetterImposterBuilderInterface.CallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterImposterBuilderInterface.CallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();
    }

    internal static MethodDeclarationSyntax BuildThenMethod(in ImposterPropertyMetadata property) =>
        new MethodDeclarationBuilder(property.SetterImposterBuilderInterface.ThenMethod.ReturnType, property.SetterImposterBuilderInterface.ThenMethod.Name)
            .WithSemicolon()
            .Build();
}
