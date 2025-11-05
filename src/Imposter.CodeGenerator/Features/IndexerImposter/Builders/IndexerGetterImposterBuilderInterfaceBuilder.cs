using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerGetterImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterIndexerMetadata indexer)
    {
        if (!indexer.Core.HasGetter)
        {
            return null;
        }

        var getterInterface = indexer.GetterBuilderInterface;

        return new InterfaceDeclarationBuilder(getterInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(BuildReturnsMethods(getterInterface))
            .AddMembers(BuildThrowsMethods(getterInterface))
            .AddMember(BuildCallbackMethod(getterInterface))
            .AddMember(BuildCalledMethod(getterInterface))
            .AddMember(BuildThenMethod(getterInterface))
            .Build();
    }

    private static MethodDeclarationSyntax[] BuildReturnsMethods(IndexerGetterImposterBuilderInterfaceMetadata getterInterface)
    {
        return
        [
            new MethodDeclarationBuilder(getterInterface.ReturnsMethod.ReturnType, getterInterface.ReturnsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.ReturnsMethod.ValueParameter))
                .WithSemicolon()
                .Build(),

            new MethodDeclarationBuilder(getterInterface.ReturnsMethod.ReturnType, getterInterface.ReturnsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.ReturnsMethod.FuncParameter))
                .WithSemicolon()
                .Build(),

            new MethodDeclarationBuilder(getterInterface.ReturnsMethod.ReturnType, getterInterface.ReturnsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.ReturnsMethod.DelegateParameter))
                .WithSemicolon()
                .Build(),
        ];
    }

    private static MethodDeclarationSyntax[] BuildThrowsMethods(IndexerGetterImposterBuilderInterfaceMetadata getterInterface)
    {
        return
        [
            new MethodDeclarationBuilder(getterInterface.ThrowsMethod.ReturnType, getterInterface.ThrowsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.ThrowsMethod.ExceptionParameter))
                .WithSemicolon()
                .Build(),

            new MethodDeclarationBuilder(getterInterface.ThrowsMethod.ReturnType, getterInterface.ThrowsMethod.Name)
                .WithTypeParameters(
                    TypeParameterList(
                        SingletonSeparatedList(
                            TypeParameter("TException"))))
                .AddConstraintClause(
                    TypeParameterConstraintClause("TException")
                        .AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
                .WithSemicolon()
                .Build(),

            new MethodDeclarationBuilder(getterInterface.ThrowsMethod.ReturnType, getterInterface.ThrowsMethod.Name)
                .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.ThrowsMethod.DelegateParameter))
                .WithSemicolon()
                .Build(),
        ];
    }

    private static MethodDeclarationSyntax BuildCallbackMethod(IndexerGetterImposterBuilderInterfaceMetadata getterInterface) =>
        new MethodDeclarationBuilder(getterInterface.CallbackMethod.ReturnType, getterInterface.CallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.CallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildCalledMethod(IndexerGetterImposterBuilderInterfaceMetadata getterInterface) =>
        new MethodDeclarationBuilder(getterInterface.CalledMethod.ReturnType, getterInterface.CalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.CalledMethod.CountParameter))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildThenMethod(IndexerGetterImposterBuilderInterfaceMetadata getterInterface) =>
        new MethodDeclarationBuilder(getterInterface.ThenMethod.ReturnType, getterInterface.ThenMethod.Name)
            .WithSemicolon()
            .Build();
}
