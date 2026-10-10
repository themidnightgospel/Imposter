using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Imposter.CodeGenerator.SyntaxHelpers.VolatileSyntaxHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class DefaultIndexerBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        return new ClassDeclarationBuilder(indexer.DefaultIndexerBehaviour.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SingleVariableField(
                    indexer.DefaultIndexerBehaviour.IsOnBackingField.Type,
                    indexer.DefaultIndexerBehaviour.IsOnBackingField.Name,
                    TokenList(Token(SyntaxKind.PrivateKeyword)),
                    True
                )
            )
            .AddMember(BuildIsOnProperty(indexer))
            .AddMember(
                indexer.DefaultIndexerBehaviour.BackingField is { } backingField
                    ? SingleVariableField(
                        backingField,
                        SyntaxKind.InternalKeyword,
                        backingField.Type.New(EmptyArgumentListSyntax)
                    )
                    : null
            )
            .AddMember(indexer.Core.HasGetter ? BuildGetMethod(indexer) : null)
            .AddMember(
                indexer.DefaultIndexerBehaviour.BackingField is { } setBackingField
                && indexer.Core.HasSetter
                    ? BuildSetMethod(indexer, setBackingField)
                    : null
            )
            .Build();
    }

    private static PropertyDeclarationSyntax BuildIsOnProperty(in ImposterIndexerMetadata indexer)
    {
        var isOnBackingField = indexer.DefaultIndexerBehaviour.IsOnBackingField.Name;
        var getBody = Block(ReturnStatement(VolatileRead(isOnBackingField)));
        var setBody = Block(
            VolatileWrite(isOnBackingField, IdentifierName("value")).ToStatementSyntax()
        );

        return new PropertyDeclarationBuilder(
            WellKnownTypes.Bool,
            indexer.DefaultIndexerBehaviour.IsOnPropertyName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithGetterBody(getBody)
            .WithSetterBody(setBody)
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsParam = ParameterSyntax(indexer.Arguments.TypeSyntax, "arguments");
        var baseImplementationParam = ParameterSyntax(
            indexer.DefaultIndexerBehaviour.GetBaseImplementationParameter
        );
        var valueIdentifier = IdentifierName("value");

        // A value passed through isn't kept, so it comes from the base getter or is the default.
        var returnKeptValue = indexer.DefaultIndexerBehaviour.BackingField is { } backingField
            ? IfStatement(
                IdentifierName(backingField.Name)
                    .Dot(IdentifierName("TryGetValue"))
                    .Call(
                        ArgumentListSyntax([
                            Argument(IdentifierName("arguments")),
                            Argument(
                                null,
                                Token(SyntaxKind.OutKeyword),
                                DeclarationExpression(
                                    Var,
                                    SingleVariableDesignation(valueIdentifier.Identifier)
                                )
                            ),
                        ])
                    ),
                ReturnStatement(valueIdentifier)
            )
            : null;

        // The base getter takes the keys passed through, which Get takes beside the arguments, under names of their
        // own.
        var keyNames = new NameSet([
            argumentsParam.Identifier.Text,
            baseImplementationParam.Identifier.Text,
            valueIdentifier.Identifier.Text,
        ]);
        var passedThroughKeys = indexer
            .Core.PassedThroughParameters.Select(parameter =>
                ParameterSyntax(parameter.TypeSyntax, keyNames.Use(parameter.Name))
            )
            .ToArray();

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareStoredTypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParam)
            .AddParameters(passedThroughKeys)
            .AddParameter(baseImplementationParam)
            .WithBody(
                new BlockBuilder()
                    .AddStatement(returnKeptValue)
                    .AddStatement(
                        IfStatement(
                            IdentifierName(baseImplementationParam.Identifier).IsNotNull(),
                            ReturnStatement(
                                IdentifierName(baseImplementationParam.Identifier)
                                    .Call(
                                        ArgumentListSyntax(
                                            passedThroughKeys.Select(key =>
                                                Argument(IdentifierName(key.Identifier))
                                            )
                                        )
                                    )
                            )
                        )
                    )
                    .AddStatement(ReturnDefaultNonNullable)
                    .Build()
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetMethod(
        in ImposterIndexerMetadata indexer,
        in FieldMetadata backingField
    )
    {
        var baseImplementationParam = ParameterSyntax(
            indexer.DefaultIndexerBehaviour.SetBaseImplementationParameter
        );

        var argumentsParameter = ParameterSyntax(indexer.Arguments.TypeSyntax, "arguments");

        var assignment = ElementAccessExpression(IdentifierName(backingField.Name))
            .WithArgumentList(
                BracketedArgumentList(SingletonSeparatedList(Argument(IdentifierName("arguments"))))
            )
            .Assign(IdentifierName("value"))
            .ToStatementSyntax();

        var method = new MethodDeclarationBuilder(WellKnownTypes.Void, "Set")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParameter)
            .AddParameter(ParameterSyntax(indexer.Core.NullableAwareStoredTypeSyntax, "value"));

        // The setter never gives Set a base setter. A generated one takes the keys passed through, which Set doesn't
        // have, so Set doesn't take one at all.
        if (indexer.Core.HasGeneratedValueDelegates)
        {
            return method.WithBody(Block(assignment)).Build();
        }

        var baseInvocation = IfStatement(
            IdentifierName(baseImplementationParam.Identifier).IsNotNull(),
            Block(
                IdentifierName(baseImplementationParam.Identifier).Call().ToStatementSyntax(),
                ReturnStatement()
            )
        );

        return method
            .AddParameter(baseImplementationParam)
            .WithBody(Block(baseInvocation, assignment))
            .Build();
    }
}
