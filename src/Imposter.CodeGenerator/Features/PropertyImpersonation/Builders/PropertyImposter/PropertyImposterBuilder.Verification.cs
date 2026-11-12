using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

internal static partial class PropertyImposterBuilder
{
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
