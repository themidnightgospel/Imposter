using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal class BlockBuilder
{
    private readonly List<StatementSyntax> _statements = [];

    internal BlockBuilder AddStatements(IEnumerable<StatementSyntax> statementSyntaxes)
    {
        _statements.AddRange(statementSyntaxes);
        return this;
    }

    internal BlockBuilder AddStatement(StatementSyntax? statementSyntax)
    {
        if (statementSyntax is not null)
        {
            _statements.Add(statementSyntax);
        }

        return this;
    }

    internal BlockBuilder AddExpression(ExpressionSyntax? statementSyntax)
    {
        if (statementSyntax is not null)
        {
            _statements.Add(statementSyntax.ToStatementSyntax());
        }
        return this;
    }

    internal BlockBuilder AddStatementsIf(bool condition, Func<StatementSyntax> statementGenerator)
    {
        return condition ? AddStatement(statementGenerator()) : this;
    }

    internal BlockSyntax Build()
    {
        return SyntaxFactory.Block(_statements);
    }
}
