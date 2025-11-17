using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var builderInterface = indexer.BuilderInterface;

        return new InterfaceDeclarationBuilder(builderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                indexer.Core.HasGetter ? BuildGetterMethod(builderInterface.GetterMethod) : null
            )
            .AddMember(
                indexer.Core.HasSetter ? BuildSetterMethod(builderInterface.SetterMethod) : null
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterMethod(
        in GetterMethodMetadata getterMethod
    ) =>
        new MethodDeclarationBuilder(getterMethod.ReturnType, getterMethod.Name)
            .AddParameters(
                getterMethod
                    .Parameters.Select(it => SyntaxFactoryHelper.ParameterSyntax(it))
                    .ToArray()
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildSetterMethod(
        in SetterMethodMetadata setterMethod
    ) =>
        new MethodDeclarationBuilder(setterMethod.ReturnType, setterMethod.Name)
            .AddParameters(
                setterMethod
                    .Parameters.Select(it => SyntaxFactoryHelper.ParameterSyntax(it))
                    .ToArray()
            )
            .WithSemicolon()
            .Build();
}
