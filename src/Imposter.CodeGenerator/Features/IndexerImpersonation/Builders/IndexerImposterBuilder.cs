using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                SinglePrivateReadonlyVariableField(
                    indexer.Builder.DefaultBehaviourField,
                    indexer.DefaultIndexerBehaviour.TypeSyntax.New()
                )
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
            .AddMember(DefaultIndexerBehaviourBuilder.Build(indexer))
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
        var invocationBehaviorParameter = Parameter(Identifier("invocationBehavior"))
            .WithType(WellKnownTypes.Imposter.Abstractions.ImposterMode);
        var propertyDisplayNameParameter = Parameter(Identifier("propertyDisplayName"))
            .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)));

        var getterInitialization = indexer.Core.HasGetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.GetterImposterField.Name))
                .Assign(
                    IdentifierName("GetterImposter")
                        .New(
                            ArgumentListSyntax([
                                Argument(
                                    IdentifierName(indexer.Builder.DefaultBehaviourField.Name)
                                ),
                                Argument(
                                    IdentifierName(invocationBehaviorParameter.Identifier.Text)
                                ),
                                Argument(
                                    IdentifierName(propertyDisplayNameParameter.Identifier.Text)
                                ),
                            ])
                        )
                )
                .ToStatementSyntax()
            : null;

        var setterInitialization = indexer.Core.HasSetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.SetterImposterField.Name))
                .Assign(
                    IdentifierName("SetterImposter")
                        .New(
                            ArgumentListSyntax([
                                Argument(
                                    IdentifierName(indexer.Builder.DefaultBehaviourField.Name)
                                ),
                                Argument(
                                    IdentifierName(invocationBehaviorParameter.Identifier.Text)
                                ),
                                Argument(
                                    IdentifierName(propertyDisplayNameParameter.Identifier.Text)
                                ),
                            ])
                        )
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
        var body = Block(
            ReturnStatement(
                QualifiedName(IdentifierName("GetterImposter"), IdentifierName("Builder"))
                    .New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(indexer.Builder.GetterImposterField.Name)),
                            Argument(IdentifierName("criteria")),
                        ])
                    )
            )
        );

        return new MethodDeclarationBuilder(
            indexer.GetterBuilderInterface.TypeSyntax,
            "CreateGetter"
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier("criteria")).WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildCreateSetterMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var bodyBuilder = new BlockBuilder();

        if (indexer.Core.HasSetter)
        {
            bodyBuilder.AddStatement(
                IdentifierName(indexer.Builder.SetterImposterField.Name)
                    .Dot(IdentifierName("MarkConfigured"))
                    .Call()
                    .ToStatementSyntax()
            );
        }

        bodyBuilder.AddStatement(
            ReturnStatement(
                QualifiedName(IdentifierName("SetterImposter"), IdentifierName("Builder"))
                    .New(
                        ArgumentListSyntax([
                            Argument(IdentifierName(indexer.Builder.SetterImposterField.Name)),
                            Argument(IdentifierName("criteria")),
                        ])
                    )
            )
        );

        return new MethodDeclarationBuilder(
            indexer.SetterBuilderInterface.TypeSyntax,
            "CreateSetter"
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier("criteria")).WithType(indexer.ArgumentsCriteria.TypeSyntax)
            )
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static ClassDeclarationSyntax BuildInvocationBuilder(in ImposterIndexerMetadata indexer)
    {
        var invocationBuilder = new ClassDeclarationBuilder("InvocationBuilder")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.BuilderInterface.TypeSyntax))
            .AddMember(SinglePrivateReadonlyVariableField(indexer.Builder.TypeSyntax, "_builder"))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    "_criteria"
                )
            )
            .AddMember(
                new ConstructorBuilder("InvocationBuilder")
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .AddParameter(
                        Parameter(Identifier("builder")).WithType(indexer.Builder.TypeSyntax)
                    )
                    .AddParameter(
                        Parameter(Identifier("criteria"))
                            .WithType(indexer.ArgumentsCriteria.TypeSyntax)
                    )
                    .WithBody(
                        new BlockBuilder()
                            .AddStatement(
                                ThisExpression()
                                    .Dot(IdentifierName("_builder"))
                                    .Assign(IdentifierName("builder"))
                                    .ToStatementSyntax()
                            )
                            .AddStatement(
                                ThisExpression()
                                    .Dot(IdentifierName("_criteria"))
                                    .Assign(IdentifierName("criteria"))
                                    .ToStatementSyntax()
                            )
                            .Build()
                    )
                    .Build()
            );

        if (indexer.Core.HasGetter)
        {
            invocationBuilder = invocationBuilder.AddMember(
                new MethodDeclarationBuilder(
                    indexer.GetterBuilderInterface.TypeSyntax,
                    indexer.BuilderInterface.GetterMethod.Name
                )
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithBody(
                        Block(
                            ReturnStatement(
                                IdentifierName("_builder")
                                    .Dot(IdentifierName("CreateGetter"))
                                    .Call(Argument(IdentifierName("_criteria")))
                            )
                        )
                    )
                    .Build()
            );
        }

        if (indexer.Core.HasSetter)
        {
            invocationBuilder = invocationBuilder.AddMember(
                new MethodDeclarationBuilder(
                    indexer.SetterBuilderInterface.TypeSyntax,
                    indexer.BuilderInterface.SetterMethod.Name
                )
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithBody(
                        Block(
                            ReturnStatement(
                                IdentifierName("_builder")
                                    .Dot(IdentifierName("CreateSetter"))
                                    .Call(Argument(IdentifierName("_criteria")))
                            )
                        )
                    )
                    .Build()
            );
        }

        return invocationBuilder.Build();
    }
}
