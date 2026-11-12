using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

internal static class InvocationLifecycleBuilder
{
    internal const string ClearMethodName = "ClearInvocations";
    internal const string ResetMethodName = "Reset";

    internal static MethodDeclarationSyntax Clear(IEnumerable<StatementSyntax> statements) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, ClearMethodName)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(Block(statements))
            .Build();

    internal static StatementSyntax ClearChild(string fieldName) =>
        IdentifierName(fieldName).Dot(IdentifierName(ClearMethodName)).Call().ToStatementSyntax();

    internal static MethodDeclarationSyntax ClearChildren(params string?[] fieldNames) =>
        Clear(fieldNames.Where(name => name is not null).Select(name => ClearChild(name!)));

    internal static StatementSyntax ClearIfInitialized(string fieldName) =>
        ConditionalAccessExpression(
                IdentifierName(fieldName),
                MemberBindingExpression(IdentifierName("Clear")).Call()
            )
            .ToStatementSyntax();
}
