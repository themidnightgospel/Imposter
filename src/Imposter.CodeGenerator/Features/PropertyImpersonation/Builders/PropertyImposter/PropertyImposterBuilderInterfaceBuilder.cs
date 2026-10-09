using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

internal static class PropertyImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterPropertyMetadata property)
    {
        var builderInterface = property.ImposterBuilderInterface;
        var getter = builderInterface.GetterMethod;
        var setter = builderInterface.SetterMethod;

        return new InterfaceDeclarationBuilder(builderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                property.Core.HasGetter ? InterfaceMethod(getter.ReturnType, getter.Name) : null
            )
            .AddMember(
                property.Core.HasSetter
                    ? InterfaceMethod(setter.ReturnType, setter.Name, setter.CriteriaParameter)
                    : null
            )
            .AddMember(
                builderInterface.UseBaseImplementationMethod is { } useBaseImplementation
                    ? InterfaceMethod(useBaseImplementation.ReturnType, useBaseImplementation.Name)
                    : null
            )
            .Build();
    }
}
