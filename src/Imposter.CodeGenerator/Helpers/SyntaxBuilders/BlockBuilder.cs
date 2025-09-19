using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Helpers.SyntaxBuilders;

public class BlockBuilder
{
    private readonly List<StatementSyntax> _statements = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockBuilder AddStatement(StatementSyntax statementSyntax)
    {
        _statements.Add(statementSyntax);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockBuilder AddStatementsIf(bool condition, StatementSyntax statements)
    {
        return condition ? AddStatement(statements) : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BlockSyntax Build()
    {
        return SyntaxFactory.Block(_statements);
    }
}