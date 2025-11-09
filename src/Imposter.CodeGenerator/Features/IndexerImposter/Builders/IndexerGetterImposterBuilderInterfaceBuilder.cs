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
    internal static MemberDeclarationSyntax[] Build(in ImposterIndexerMetadata indexer)
    {
        if (!indexer.Core.HasGetter)
        {
            return [];
        }

        return
        [
            BuildOutcomeInterface(indexer),
            BuildCallbackInterface(indexer),
            BuildContinuationInterface(indexer),
            BuildVerificationInterface(indexer),
            BuildFluentInterface(indexer),
            BuildBuilderInterface(indexer),
        ];
    }

    private static InterfaceDeclarationSyntax BuildBuilderInterface(in ImposterIndexerMetadata indexer) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.OutcomeInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.VerificationInterfaceTypeSyntax))
            .Build();

    private static InterfaceDeclarationSyntax BuildFluentInterface(in ImposterIndexerMetadata indexer) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.FluentInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.OutcomeInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.ContinuationInterfaceTypeSyntax))
            .Build();

    private static InterfaceDeclarationSyntax BuildContinuationInterface(in ImposterIndexerMetadata indexer) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.ContinuationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddMember(BuildThenMethod(indexer.GetterBuilderInterface))
            .Build();

    private static InterfaceDeclarationSyntax BuildCallbackInterface(in ImposterIndexerMetadata indexer) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.CallbackInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildCallbackMethod(indexer.GetterBuilderInterface))
            .Build();

    private static InterfaceDeclarationSyntax BuildOutcomeInterface(in ImposterIndexerMetadata indexer) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.OutcomeInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(BuildReturnsMethods(indexer.GetterBuilderInterface))
            .AddMembers(BuildThrowsMethods(indexer.GetterBuilderInterface))
            .Build();

    private static InterfaceDeclarationSyntax BuildVerificationInterface(in ImposterIndexerMetadata indexer) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.VerificationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildCalledMethod(indexer.GetterBuilderInterface))
            .Build();

    private static MethodDeclarationSyntax[] BuildReturnsMethods(IndexerGetterImposterBuilderInterfaceMetadata getterInterface) =>
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

    private static MethodDeclarationSyntax[] BuildThrowsMethods(IndexerGetterImposterBuilderInterfaceMetadata getterInterface) =>
    [
        new MethodDeclarationBuilder(getterInterface.ThrowsMethod.ReturnType, getterInterface.ThrowsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.ThrowsMethod.ExceptionParameter))
            .WithSemicolon()
            .Build(),

        new MethodDeclarationBuilder(getterInterface.ThrowsMethod.ReturnType, getterInterface.ThrowsMethod.Name)
            .WithTypeParameters(
                TypeParameterList(
                    SingletonSeparatedList(
                        TypeParameter(getterInterface.ThrowsMethod.GenericTypeParameterName))))
            .AddConstraintClause(
                TypeParameterConstraintClause(getterInterface.ThrowsMethod.GenericTypeParameterName)
                    .AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
            .WithSemicolon()
            .Build(),

        new MethodDeclarationBuilder(getterInterface.ThrowsMethod.ReturnType, getterInterface.ThrowsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(getterInterface.ThrowsMethod.DelegateParameter))
            .WithSemicolon()
            .Build(),
    ];

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
