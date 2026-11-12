using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

internal static partial class PropertyImposterBuilder
{
    private static List<MemberDeclarationSyntax> BuildVerificationSources(
        in ImposterPropertyMetadata property
    )
    {
        var members = new List<MemberDeclarationSyntax>();
        var source = WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource;
        var resultType = WellKnownTypes.System.Collections.Generic.IEnumerable(
            WellKnownTypes.String
        );
        foreach (
            var method in new[]
            {
                VerificationSourceNames.InteractionsMethodName,
                VerificationSourceNames.UncoveredInteractionsMethodName,
                VerificationSourceNames.UnusedSetupsMethodName,
            }
        )
        {
            var statements = new List<StatementSyntax>
            {
                LocalVariableDeclarationSyntax(
                    Var,
                    property.Core.Locals["findings"],
                    WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String).New()
                ),
            };
            if (property.Core.HasGetter)
                statements.Add(
                    AddFindings(
                        property.ImposterBuilder.GetterImposterBuilderField.Name,
                        source,
                        method,
                        property.Core.Locals["findings"]
                    )
                );
            if (property.Core.HasSetter)
                statements.Add(
                    AddFindings(
                        property.ImposterBuilder.SetterImposterField.Name,
                        source,
                        method,
                        property.Core.Locals["findings"]
                    )
                );
            statements.Add(ReturnStatement(IdentifierName(property.Core.Locals["findings"])));
            members.Add(
                new MethodDeclarationBuilder(resultType, method)
                    .WithExplicitInterfaceSpecifier(source)
                    .WithBody(Block(statements))
                    .Build()
            );
        }
        return members;
    }

    private static ExpressionStatementSyntax AddFindings(
        string field,
        TypeSyntax source,
        string method,
        string findings
    ) =>
        IdentifierName(findings)
            .Dot(IdentifierName("AddRange"))
            .Call(
                Argument(
                    ParenthesizedExpression(CastExpression(source, IdentifierName(field)))
                        .Dot(IdentifierName(method))
                        .Call()
                )
            )
            .ToStatementSyntax();

    private static MethodDeclarationSyntax BuildSnapshot(in ImposterPropertyMetadata property)
    {
        var invocation = WellKnownTypes.Imposter.Abstractions.IImposterInvocation;
        var orderBySequence = property.Core.CreateNameSet().Use("orderBySequence");
        ExpressionSyntax result = property.Core.HasSetter
            ? IdentifierName(property.ImposterBuilder.SetterImposterField.Name)
                .Dot(IdentifierName("Snapshot"))
                .Call(Argument(IdentifierName(orderBySequence)))
            : WellKnownTypes
                .Imposter.Abstractions.InvocationSnapshots.Dot(IdentifierName("Order"))
                .Call([
                    Argument(WellKnownTypes.System.Collections.Generic.List(invocation).New()),
                    Argument(IdentifierName(orderBySequence)),
                ]);
        return new MethodDeclarationBuilder(
            WellKnownTypes.System.Collections.Generic.IReadOnlyList(invocation),
            "Snapshot"
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(WellKnownTypes.Bool, orderBySequence)
                    .WithDefault(EqualsValueClause(True))
            )
            .WithBody(Block(ReturnStatement(result)))
            .Build();
    }
}
