using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter.Interface;

internal static class PropertyImposterInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterTargetPropertyMetadata property)
    {
        return new InterfaceDeclarationBuilder(property.Interface.Name)
            .AddMembers(ReturnsValueMethod(property))
            .AddMember(SetterCallbackMethod(property))
            .AddMember(GetterCallbackMethod(property))
            .AddMember(AutoPropertyMethod(property))
            .AddMember(GetterCalledMethod(property))
            .AddMember(SetterCalledMethod(property))
            .Build();
    }
    
    internal static MethodDeclarationSyntax? SetterCalledMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.SetterCalledMethod.ReturnType, property.SetterCalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterCalledMethod.CriteriaParameter))
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.SetterCalledMethod.CountParameter))
            .WithSemicolon()
            .Build();
    }
    
    internal static MethodDeclarationSyntax? GetterCalledMethod(in ImposterTargetPropertyMetadata property)
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
 
    internal static MethodDeclarationSyntax? AutoPropertyMethod(in ImposterTargetPropertyMetadata property)
    {
        if (!property.HasGetter)
        {
            return null;
        }

        return new MethodDeclarationBuilder(property.AutoPropertyMethod.ReturnType, property.AutoPropertyMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(property.AutoPropertyMethod.InitialValueParameter))
            .WithSemicolon()
            .Build();
    }
    
    internal static MethodDeclarationSyntax? GetterCallbackMethod(in ImposterTargetPropertyMetadata property)
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

    internal static MethodDeclarationSyntax? SetterCallbackMethod(in ImposterTargetPropertyMetadata property)
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

    internal static MethodDeclarationSyntax[]? ReturnsValueMethod(in ImposterTargetPropertyMetadata property)
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