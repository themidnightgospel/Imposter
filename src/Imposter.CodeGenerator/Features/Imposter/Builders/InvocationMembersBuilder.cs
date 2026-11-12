using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal static class InvocationMembersBuilder
{
    internal static MethodDeclarationSyntax Build(
        IEnumerable<ExpressionSyntax> snapshots,
        string resultName
    )
    {
        var listType = WellKnownTypes.System.Collections.Generic.List(
            WellKnownTypes.Imposter.Abstractions.IImposterInvocation
        );
        var result = IdentifierName(resultName);
        return new MethodDeclarationBuilder(
            WellKnownTypes.System.Collections.Generic.IReadOnlyList(
                WellKnownTypes.Imposter.Abstractions.IImposterInvocation
            ),
            "Invocations"
        )
            .WithExplicitInterfaceSpecifier(WellKnownTypes.Imposter.Abstractions.IImposter)
            .WithBody(
                new BlockBuilder()
                    .AddStatement(LocalVariableDeclarationSyntax(Var, resultName, listType.New()))
                    .AddStatements(
                        snapshots.Select(snapshot =>
                            result
                                .Dot(IdentifierName("AddRange"))
                                .Call(Argument(snapshot))
                                .ToStatementSyntax()
                        )
                    )
                    .AddStatement(
                        ReturnStatement(
                            WellKnownTypes
                                .Imposter.Abstractions.InvocationSnapshots.Dot(
                                    IdentifierName("Order")
                                )
                                .Call(Argument(result))
                        )
                    )
                    .Build()
            )
            .Build();
    }
}
