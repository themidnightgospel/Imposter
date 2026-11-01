using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

// The SetterImposter's nested Builder class, which configures and verifies assignments for some criteria.
internal static partial class IndexerSetterBuilder
{
    private static ClassDeclarationSyntax BuildSetterBuilder(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var imposterField = new FieldMetadata(
            builderMetadata.ImposterFieldName,
            indexer.SetterImplementation.TypeSyntax
        );
        var criteriaField = new FieldMetadata(
            builderMetadata.CriteriaFieldName,
            indexer.ArgumentsCriteria.TypeSyntax
        );

        return new ClassDeclarationBuilder(builderMetadata.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.FluentInterfaceTypeSyntax))
            .AddMember(SinglePrivateReadonlyVariableField(imposterField))
            .AddMember(SinglePrivateReadonlyVariableField(criteriaField))
            .AddMember(
                new ConstructorWithFieldInitializationBuilder(builderMetadata.Name)
                    .WithModifiers(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(imposterField)
                    .AddParameter(criteriaField)
                    .Build()
            )
            .AddMember(BuildSetterBuilderCallbackMethod(indexer))
            .AddMember(BuildSetterBuilderCalledMethod(indexer))
            .AddMember(BuildSetterBuilderThenMethod(indexer))
            .AddMember(
                indexer.SetterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildSetterBuilderUseBaseImplementationMethod(indexer)
                    : null
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var callbackMetadata = indexer.SetterBuilderInterface.CallbackMethod;
        var parameter = ParameterSyntax(callbackMetadata.CallbackParameter);

        return new MethodDeclarationBuilder(callbackMetadata.ReturnType, callbackMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                indexer.SetterBuilderInterface.CallbackMethod.InterfaceSyntax
            )
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("Callback"))
                        .Call([
                            Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                            Argument(IdentifierName(parameter.Identifier)),
                        ])
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var calledMetadata = indexer.SetterBuilderInterface.CalledMethod;
        var parameter = ParameterSyntax(calledMetadata.CountParameter);

        return new MethodDeclarationBuilder(calledMetadata.ReturnType, calledMetadata.Name)
            .WithExplicitInterfaceSpecifier(
                indexer.SetterBuilderInterface.VerificationInterfaceTypeSyntax
            )
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("Called"))
                        .Call([
                            Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                            Argument(IdentifierName(parameter.Identifier)),
                        ])
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderThenMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var thenMetadata = indexer.SetterBuilderInterface.ThenMethod;

        return new MethodDeclarationBuilder(thenMetadata.ReturnType, thenMetadata.Name)
            .WithExplicitInterfaceSpecifier(thenMetadata.InterfaceSyntax)
            .WithBody(Block(ReturnThis))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var metadata = indexer.SetterBuilderInterface.UseBaseImplementationMethod!.Value;

        return new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name)
            .WithExplicitInterfaceSpecifier(metadata.InterfaceSyntax)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("UseBaseImplementation"))
                        .Call(Argument(IdentifierName(builderMetadata.CriteriaFieldName)))
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }
}
