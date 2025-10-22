using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

public class BlockBuilder
{
    private readonly List<StatementSyntax> _statements = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockBuilder AddStatements(IEnumerable<StatementSyntax> statementSyntaxes)
    {
        _statements.AddRange(statementSyntaxes);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockBuilder AddStatement(StatementSyntax? statementSyntax)
    {
        if (statementSyntax is not null)
        {
            _statements.Add(statementSyntax);
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockBuilder AddExpression(ExpressionSyntax statementSyntax)
    {
        _statements.Add(statementSyntax.ToStatementSyntax());
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockBuilder AddStatementsIf(bool condition, Func<StatementSyntax> statementGenerator)
    {
        return condition ? AddStatement(statementGenerator()) : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockSyntax Build()
    {
        return SyntaxFactory.Block(_statements);
    }
}