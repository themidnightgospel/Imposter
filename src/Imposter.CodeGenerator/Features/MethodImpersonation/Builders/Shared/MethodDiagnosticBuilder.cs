using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;

internal static class MethodDiagnosticBuilder
{
    internal static BinaryExpressionSyntax Expected(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax criteria
    )
    {
        ExpressionSyntax description = (method.Model.Name + "(").StringLiteral();
        var first = true;
        foreach (var parameter in method.Parameters.AllParameterMetadata)
        {
            description = description.Add(
                ((first ? "" : ", ") + parameter.Model.Name + ": ").StringLiteral()
            );
            first = false;
            ExpressionSyntax matcher =
                parameter.Model.IsPassedThrough ? "<unrecorded>".StringLiteral()
                : parameter.Model.RefKind == RefKind.Out ? "any".StringLiteral()
                : WellKnownTypes
                    .Imposter.Abstractions.MatcherDescription.Dot(IdentifierName("Format"))
                    .Call(Argument(criteria.Dot(IdentifierName(parameter.Name))));
            description = description.Add(matcher);
        }
        return description.Add(")".StringLiteral());
    }
}
