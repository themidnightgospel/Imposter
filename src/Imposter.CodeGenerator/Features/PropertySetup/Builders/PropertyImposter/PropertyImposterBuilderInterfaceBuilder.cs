using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter;

internal static class PropertyImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.ImposterBuilderInterface.Name)
            .AddMember(BuildGetterMethod(property))
            .AddMember(BuildSetterMethod(property))
            .Build(TokenList(Token(SyntaxKind.PublicKeyword)));
    
    internal static MethodDeclarationSyntax? BuildSetterMethod(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.ImposterBuilderInterface.SetterMethod.ReturnType, property.ImposterBuilderInterface.SetterMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.ImposterBuilderInterface.SetterMethod.CriteriaParameter))
            .WithSemicolon()
            .Build();
    }
    
    internal static MethodDeclarationSyntax? BuildGetterMethod(in ImposterPropertyMetadata property)
    {
        if (!property.Core.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.ImposterBuilderInterface.GetterMethod.ReturnType, property.ImposterBuilderInterface.GetterMethod.Name)
                .WithSemicolon()
                .Build();
    }
}