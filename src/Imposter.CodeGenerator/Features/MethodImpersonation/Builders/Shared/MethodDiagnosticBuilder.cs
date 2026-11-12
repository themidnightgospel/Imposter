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
                : criteria
                    .Dot(IdentifierName(parameter.Name))
                    .Dot(IdentifierName("ToString"))
                    .Call();
            description = description.Add(matcher);
        }
        return description.Add(")".StringLiteral());
    }

    internal static BinaryExpressionSyntax Actual(in ImposterTargetMethodMetadata method)
    {
        ExpressionSyntax description = (method.Model.Name + "(").StringLiteral();
        var first = true;
        foreach (var parameter in method.Parameters.AllParameterMetadata)
        {
            description = description.Add(
                ((first ? "" : ", ") + parameter.Model.Name + ": ").StringLiteral()
            );
            first = false;
            ExpressionSyntax value =
                parameter.Model.IsPassedThrough || parameter.Model.RefKind == RefKind.Out
                    ? "<unrecorded>".StringLiteral()
                    : WellKnownTypes
                        .Imposter.Abstractions.DiagnosticValue.Dot(IdentifierName("Format"))
                        .Call(
                            Argument(
                                IdentifierName(
                                    method.InvocationHistory.ArgumentFields[parameter.Name]
                                )
                            )
                        );
            description = description.Add(value);
        }
        return description.Add(")".StringLiteral());
    }
}
