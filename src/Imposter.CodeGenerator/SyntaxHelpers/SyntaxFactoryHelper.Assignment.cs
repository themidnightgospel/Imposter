using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static AssignmentExpressionSyntax Assign(
        this ExpressionSyntax left,
        ExpressionSyntax right
    ) => AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, right);

    internal static StatementSyntax AssignDefaultValueStatementSyntax(ParameterModel parameter) =>
        IdentifierName(EscapeKeyword(parameter.Name))
            .Assign(DefaultNonNullable)
            .ToStatementSyntax();
}
