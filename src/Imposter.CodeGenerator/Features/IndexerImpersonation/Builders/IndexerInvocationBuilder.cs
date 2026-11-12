using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerInvocationBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterIndexerMetadata indexer, bool getter)
    {
        if (getter ? !indexer.Core.HasGetter : !indexer.Core.HasSetter)
            return null;
        var metadata = getter ? indexer.GetterRecord : indexer.SetterRecord;
        var builder = new ClassDeclarationBuilder(metadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.InvocationRecord))
            .AddMember(
                new PropertyDeclarationBuilder(WellKnownTypes.String, "Member")
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .AddModifier(Token(SyntaxKind.OverrideKeyword))
                    .WithGetterBody(
                        Block(
                            ReturnStatement(
                                (
                                    indexer.Core.UniqueName + (getter ? " (getter)" : " (setter)")
                                ).StringLiteral()
                            )
                        )
                    )
                    .Build()
            )
            .AddMember(
                SingleVariableField(
                    indexer.Arguments.TypeSyntax,
                    "Arguments",
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                )
            );
        var constructor = new ConstructorBuilder(metadata.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(
                ParameterSyntax(indexer.Arguments.TypeSyntax, indexer.Locals["arguments"])
            );
        var body = new BlockBuilder().AddStatement(
            IdentifierName("Arguments")
                .Assign(IdentifierName(indexer.Locals["arguments"]))
                .ToStatementSyntax()
        );
        foreach (var parameter in indexer.Core.MatchedParameters)
        {
            TypeSyntax type = parameter.KeptTypeSyntax;
            if (parameter.Model.Span is { } span)
                type = WellKnownTypes.System.ReadOnlyMemory(
                    TypeSyntaxIncludingNullable(span.ElementType)
                );
            builder.AddMember(
                new PropertyDeclarationBuilder(type, metadata.Properties[parameter.Name])
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithGetterBody(
                        Block(
                            ReturnStatement(
                                IdentifierName("Arguments").Dot(IdentifierName(parameter.FieldName))
                            )
                        )
                    )
                    .Build()
            );
        }
        if (!indexer.Core.IsValuePassedThrough)
        {
            var name = getter ? "Result" : "Value";
            var storedType = indexer.Core.NullableAwareKeptTypeSyntax;
            builder.AddMember(
                SingleVariableField(
                    storedType,
                    "_value",
                    TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                )
            );
            TypeSyntax exposedType = storedType;
            ExpressionSyntax exposedValue = IdentifierName("_value");
            if (indexer.Core.HasSpanValue)
            {
                var array = storedType is NullableTypeSyntax nullable
                    ? (ArrayTypeSyntax)nullable.ElementType
                    : (ArrayTypeSyntax)storedType;
                exposedType = WellKnownTypes.System.ReadOnlyMemory(array.ElementType);
            }
            else if (getter)
            {
                exposedType =
                    exposedType is NullableTypeSyntax ? exposedType : NullableType(exposedType);
                exposedValue = ConditionalExpression(
                    IdentifierName("Exception").IsNull(),
                    CastExpression(exposedType, exposedValue),
                    DefaultExpression(exposedType)
                );
            }
            builder.AddMember(
                new PropertyDeclarationBuilder(exposedType, name)
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithGetterBody(Block(ReturnStatement(exposedValue)))
                    .Build()
            );
            constructor.AddParameter(ParameterSyntax(storedType, indexer.Locals["value"]));
            body.AddStatement(
                IdentifierName("_value")
                    .Assign(IdentifierName(indexer.Locals["value"]))
                    .ToStatementSyntax()
            );
        }
        constructor
            .AddParameter(ParameterSyntax(WellKnownTypes.Long, indexer.Locals["sequence"]))
            .AddParameter(
                ParameterSyntax(
                    WellKnownTypes.System.Exception.ToNullableType(),
                    indexer.Locals["exception"]
                )
            )
            .WithInitializer(
                ConstructorInitializer(
                    SyntaxKind.BaseConstructorInitializer,
                    ArgumentListSyntax([
                        Argument(IdentifierName(indexer.Locals["sequence"])),
                        Argument(IdentifierName(indexer.Locals["exception"])),
                    ])
                )
            )
            .WithBody(body.Build());
        builder.AddMember(constructor.Build());
        ExpressionSyntax description = (getter ? "get " : "set ")
            .StringLiteral()
            .Add(IdentifierName("Member"))
            .Add("(".StringLiteral());
        var first = true;
        foreach (var parameter in indexer.Core.MatchedParameters)
        {
            description = description
                .Add(((first ? "" : ", ") + parameter.Model.Name + ": ").StringLiteral())
                .Add(
                    WellKnownTypes
                        .Imposter.Abstractions.DiagnosticValue.Dot(IdentifierName("Format"))
                        .Call(
                            Argument(
                                IdentifierName("Arguments").Dot(IdentifierName(parameter.FieldName))
                            )
                        )
                );
            first = false;
        }
        description = description.Add(")".StringLiteral());
        if (!indexer.Core.IsValuePassedThrough)
        {
            var valueDescription = (getter ? " => " : " = ")
                .StringLiteral()
                .Add(
                    WellKnownTypes
                        .Imposter.Abstractions.DiagnosticValue.Dot(IdentifierName("Format"))
                        .Call(Argument(IdentifierName("_value")))
                );
            description = description.Add(
                getter
                    ? ParenthesizedExpression(
                        ConditionalExpression(
                            IdentifierName("Exception").IsNull(),
                            valueDescription,
                            string.Empty.StringLiteral()
                        )
                    )
                    : valueDescription
            );
        }
        description = description.Add(
            ParenthesizedExpression(
                ConditionalExpression(
                    IdentifierName("Exception").IsNull(),
                    string.Empty.StringLiteral(),
                    " threw "
                        .StringLiteral()
                        .Add(
                            WellKnownTypes
                                .Imposter.Abstractions.DiagnosticValue.Dot(IdentifierName("Format"))
                                .Call(Argument(IdentifierName("Exception")))
                        )
                )
            )
        );
        builder.AddMember(
            new MethodDeclarationBuilder(WellKnownTypes.String, "ToString")
                .AddModifier(Token(SyntaxKind.PublicKeyword))
                .AddModifier(Token(SyntaxKind.OverrideKeyword))
                .WithBody(Block(ReturnStatement(description)))
                .Build()
        );
        return builder.Build();
    }
}
