using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter;

internal static class PropertyImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterPropertyMetadata property) =>
        new InterfaceDeclarationBuilder(property.ImposterBuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(property.Core.HasGetter ? BuildGetterMethod(property) : null)
            .AddMember(property.Core.HasSetter ? BuildSetterMethod(property) : null)
            .AddMember(BuildUseBaseImplementationMethod(property))
            .Build();

    internal static MethodDeclarationSyntax? BuildGetterMethod(
        in ImposterPropertyMetadata property
    ) =>
        new MethodDeclarationBuilder(
            property.ImposterBuilderInterface.GetterMethod.ReturnType,
            property.ImposterBuilderInterface.GetterMethod.Name
        )
            .WithSemicolon()
            .Build();

    internal static MethodDeclarationSyntax? BuildSetterMethod(
        in ImposterPropertyMetadata property
    ) =>
        new MethodDeclarationBuilder(
            property.ImposterBuilderInterface.SetterMethod.ReturnType,
            property.ImposterBuilderInterface.SetterMethod.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    property.ImposterBuilderInterface.SetterMethod.CriteriaParameter
                )
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax? BuildUseBaseImplementationMethod(
        in ImposterPropertyMetadata property
    )
    {
        if (property.ImposterBuilderInterface.UseBaseImplementationMethod is not { } method)
        {
            return null;
        }

        return new MethodDeclarationBuilder(method.ReturnType, method.Name).WithSemicolon().Build();
    }
}
