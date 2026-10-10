using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var classBuilder = new ClassDeclarationBuilder(indexer.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                indexer.Core.HasDefaultBehaviour
                    ? SinglePrivateReadonlyVariableField(
                        indexer.Builder.DefaultBehaviourField,
                        indexer.DefaultIndexerBehaviour.TypeSyntax.New()
                    )
                    : null
            )
            .AddMember(
                indexer.Core.HasGetter
                    ? SinglePrivateReadonlyVariableField(indexer.Builder.GetterImposterField)
                    : null
            )
            .AddMember(
                indexer.Core.HasSetter
                    ? SinglePrivateReadonlyVariableField(indexer.Builder.SetterImposterField)
                    : null
            )
            .AddMember(
                indexer.Core.HasDefaultBehaviour
                    ? DefaultIndexerBehaviourBuilder.Build(indexer)
                    : null
            )
            .AddMember(BuildConstructor(indexer))
            .AddMember(indexer.Core.HasGetter ? BuildCreateGetterMethod(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildCreateSetterMethod(indexer) : null)
            .AddMember(BuildInvocationBuilder(indexer))
            .AddMember(
                indexer.Core.HasGetter ? IndexerGetterBuilder.BuildGetForwarder(indexer) : null
            )
            .AddMember(
                indexer.Core.HasSetter ? IndexerSetterBuilder.BuildSetForwarder(indexer) : null
            )
            .AddMember(
                indexer.Core.HasGetter ? IndexerGetterBuilder.BuildGetterImposter(indexer) : null
            )
            .AddMember(
                indexer.Core.HasSetter ? IndexerSetterBuilder.BuildSetterImposter(indexer) : null
            )
            .AddMember(FormatValueMethodBuilder.Build());

        return classBuilder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterIndexerMetadata indexer)
    {
        var invocationBehaviorParameter = ParameterSyntax(
            WellKnownTypes.Imposter.Abstractions.ImposterMode,
            InvocationBehaviorParameterName
        );
        var propertyDisplayNameParameter = ParameterSyntax(
            WellKnownTypes.String,
            PropertyDisplayNameParameterName
        );

        var getterInitialization = indexer.Core.HasGetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.GetterImposterField.Name))
                .Assign(
                    indexer.GetterImplementation.TypeSyntax.New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(indexer.Builder.DefaultBehaviourField.Name)),
                            Argument(IdentifierName(invocationBehaviorParameter.Identifier.Text)),
                            Argument(IdentifierName(propertyDisplayNameParameter.Identifier.Text)),
                        ])
                    )
                )
                .ToStatementSyntax()
            : null;

        // The setter keeps the values set in the default behaviour, unless it can't keep them.
        var setterArguments = new List<ArgumentSyntax>();
        if (!indexer.Core.IsPassedThrough)
        {
            setterArguments.Add(
                Argument(IdentifierName(indexer.Builder.DefaultBehaviourField.Name))
            );
        }

        setterArguments.Add(Argument(IdentifierName(invocationBehaviorParameter.Identifier.Text)));
        setterArguments.Add(Argument(IdentifierName(propertyDisplayNameParameter.Identifier.Text)));
        var setterInitialization = indexer.Core.HasSetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.SetterImposterField.Name))
                .Assign(
                    indexer.SetterImplementation.TypeSyntax.New(ArgumentListSyntax(setterArguments))
                )
                .ToStatementSyntax()
            : null;

        var bodyBuilder = new BlockBuilder()
            .AddStatement(getterInitialization)
            .AddStatement(setterInitialization);

        return new ConstructorBuilder(indexer.Builder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(invocationBehaviorParameter)
            .AddParameter(propertyDisplayNameParameter)
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildCreateGetterMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builder = indexer.Builder;
        var getter = indexer.GetterImplementation;
        var body = Block(
            ReturnStatement(
                QualifiedName(getter.TypeSyntax, IdentifierName(getter.Builder.Name))
                    .New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(builder.GetterImposterField.Name)),
                            Argument(IdentifierName(builder.CriteriaParameter.Name)),
                        ])
                    )
            )
        );

        return new MethodDeclarationBuilder(
            builder.CreateGetterMethod.ReturnType,
            builder.CreateGetterMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(builder.CriteriaParameter))
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildCreateSetterMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builder = indexer.Builder;
        var setter = indexer.SetterImplementation;
        var bodyBuilder = new BlockBuilder();

        if (indexer.Core.HasSetter)
        {
            bodyBuilder.AddStatement(
                IdentifierName(builder.SetterImposterField.Name)
                    .Dot(IdentifierName(setter.MarkConfiguredMethod.Name))
                    .Call()
                    .ToStatementSyntax()
            );
        }

        bodyBuilder.AddStatement(
            ReturnStatement(
                QualifiedName(setter.TypeSyntax, IdentifierName(setter.Builder.Name))
                    .New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(builder.SetterImposterField.Name)),
                            Argument(IdentifierName(builder.CriteriaParameter.Name)),
                        ])
                    )
            )
        );

        return new MethodDeclarationBuilder(
            builder.CreateSetterMethod.ReturnType,
            builder.CreateSetterMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(builder.CriteriaParameter))
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static ClassDeclarationSyntax BuildInvocationBuilder(in ImposterIndexerMetadata indexer)
    {
        var invocationBuilder = indexer.Builder.InvocationBuilder;

        return new ClassDeclarationBuilder(invocationBuilder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.BuilderInterface.TypeSyntax))
            .AddMember(SinglePrivateReadonlyVariableField(invocationBuilder.BuilderField))
            .AddMember(SinglePrivateReadonlyVariableField(invocationBuilder.CriteriaField))
            .AddMember(
                new ConstructorWithFieldInitializationBuilder(invocationBuilder.Name)
                    .WithModifiers(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(
                        new ParameterMetadata("builder", invocationBuilder.BuilderField.Type),
                        invocationBuilder.BuilderField.Name
                    )
                    .AddParameter(
                        new ParameterMetadata("criteria", invocationBuilder.CriteriaField.Type),
                        invocationBuilder.CriteriaField.Name
                    )
                    .Build()
            )
            .AddMember(
                indexer.Core.HasGetter
                    ? BuildInvocationBuilderAccessorMethod(
                        indexer.BuilderInterface.GetterMethod.Name,
                        indexer.Builder.CreateGetterMethod,
                        invocationBuilder
                    )
                    : null
            )
            .AddMember(
                indexer.Core.HasSetter
                    ? BuildInvocationBuilderAccessorMethod(
                        indexer.BuilderInterface.SetterMethod.Name,
                        indexer.Builder.CreateSetterMethod,
                        invocationBuilder
                    )
                    : null
            )
            .Build();
    }

    // Getter() or Setter(): the builder of the accessor for the criteria this invocation builder keeps.
    private static MethodDeclarationSyntax BuildInvocationBuilderAccessorMethod(
        string name,
        in MethodMetadata createMethod,
        in IndexerImposterBuilderMetadata.InvocationBuilderMetadata invocationBuilder
    ) =>
        new MethodDeclarationBuilder(createMethod.ReturnType, name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(invocationBuilder.BuilderField.Name)
                            .Dot(IdentifierName(createMethod.Name))
                            .Call(Argument(IdentifierName(invocationBuilder.CriteriaField.Name)))
                    )
                )
            )
            .Build();
}
