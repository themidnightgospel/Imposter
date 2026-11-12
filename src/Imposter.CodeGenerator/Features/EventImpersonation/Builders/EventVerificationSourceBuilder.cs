using System.Collections.Generic;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventVerificationSourceBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterEventMetadata @event)
    {
        var resultType = WellKnownTypes.System.Collections.Generic.IEnumerable(
            WellKnownTypes.String
        );
        var members = new List<MemberDeclarationSyntax>();
        foreach (
            var name in new[]
            {
                VerificationSourceNames.InteractionsMethodName,
                VerificationSourceNames.UncoveredInteractionsMethodName,
            }
        )
            members.Add(
                new MethodDeclarationBuilder(resultType, name)
                    .WithExplicitInterfaceSpecifier(
                        WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource
                    )
                    .WithBody(
                        Block(
                            ReturnStatement(
                                WellKnownTypes
                                    .Imposter.Abstractions.ImposterVerification.Dot(
                                        IdentifierName("Describe")
                                    )
                                    .Call([
                                        Argument(IdentifierName("Snapshot").Call()),
                                        Argument(
                                            name
                                            == VerificationSourceNames.UncoveredInteractionsMethodName
                                                ? True
                                                : False
                                        ),
                                    ])
                            )
                        )
                    )
                    .Build()
            );
        var fields = @event.Builder.Fields;
        var unused = new List<StatementSyntax>
        {
            IfStatement(
                IdentifierName(fields.IsLenient.Name),
                YieldStatement(SyntaxKind.YieldBreakStatement)
            ),
            UnusedRegistrations(
                @event,
                fields.SubscribeInterceptors,
                fields.SubscribeUsed,
                @event.Core.DisplayName + " OnSubscribe"
            ),
            UnusedRegistrations(
                @event,
                fields.UnsubscribeInterceptors,
                fields.UnsubscribeUsed,
                @event.Core.DisplayName + " OnUnsubscribe"
            ),
            UnusedRegistrations(
                @event,
                fields.Callbacks,
                fields.CallbacksUsed,
                @event.Core.DisplayName + " Callback"
            ),
        };
        if (@event.Core.SupportsBaseImplementation)
            unused.Add(
                IfStatement(
                    IdentifierName(fields.UseBaseImplementation.Name)
                        .And(Not(IdentifierName(fields.BaseUsed.Name))),
                    YieldStatement(
                        SyntaxKind.YieldReturnStatement,
                        (@event.Core.DisplayName + " UseBaseImplementation").StringLiteral()
                    )
                )
            );
        members.Add(
            new MethodDeclarationBuilder(resultType, VerificationSourceNames.UnusedSetupsMethodName)
                .WithExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource
                )
                .WithBody(Block(unused))
                .Build()
        );
        members.Add(
            new MethodDeclarationBuilder(
                @event.BuilderInterface.SetupInterfaceTypeSyntax,
                VerificationSourceNames.LenientMethodName
            )
                .WithExplicitInterfaceSpecifier(@event.BuilderInterface.SetupInterfaceTypeSyntax)
                .WithBody(
                    Block(
                        IdentifierName(fields.IsLenient.Name).Assign(True).ToStatementSyntax(),
                        ReturnThis
                    )
                )
                .Build()
        );
        return members.ToArray();
    }

    internal static ExpressionStatementSyntax MarkUsed(string field, ExpressionSyntax index) =>
        QualifiedName(WellKnownTypes.Imposter.Abstractions.Namespace, IdentifierName("SetupUsage"))
            .Dot(IdentifierName("MarkUsed"))
            .Call([
                Argument(IdentifierName(field)).WithRefKindKeyword(Token(SyntaxKind.RefKeyword)),
                Argument(index),
            ])
            .ToStatementSyntax();

    private static IfStatementSyntax UnusedRegistrations(
        in ImposterEventMetadata @event,
        in FieldMetadata registrations,
        in FieldMetadata used,
        string description
    ) =>
        IfStatement(
            IdentifierName(registrations.Name).IsNotNull(),
            Block(
                LocalVariableDeclarationSyntax(
                    WellKnownTypes.Int,
                    @event.Locals["index"],
                    NumericLiteral(0)
                ),
                ForEachStatement(
                    Var,
                    Identifier(@event.Locals["registration"]),
                    IdentifierName(registrations.Name),
                    Block(
                        IfStatement(
                            BinaryExpression(
                                SyntaxKind.GreaterThanExpression,
                                PrefixUnaryExpression(
                                    SyntaxKind.PreIncrementExpression,
                                    IdentifierName(@event.Locals["index"])
                                ),
                                IdentifierName(used.Name)
                            ),
                            YieldStatement(
                                SyntaxKind.YieldReturnStatement,
                                description.StringLiteral()
                            )
                        )
                    )
                )
            )
        );
}
