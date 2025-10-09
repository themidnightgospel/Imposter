using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.Benchmarks;

[MemoryDiagnoser]
public class SyntaxFactoryBenchmarks
{
    [Benchmark]
    public void Constructor()
    {
        var block = SyntaxFactory.Block(
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()),
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()),
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()),
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()),
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()),
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()),
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()),
            SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression())
        );
    }

    [Benchmark]
    public void BuilderMethod()
    {
        var block = SyntaxFactory.Block();
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
    }

    [Benchmark]
    public void BlockBuilder()
    {
        var block = new BlockBuilder();
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
        block = block.AddStatements(SyntaxFactory.ReturnStatement(SyntaxFactory.ThisExpression()));
    }
}

public class BlockBuilder
{
    public List<StatementSyntax> Statements { get; } = new();

    public BlockSyntax Build()
    {
        return SyntaxFactory.Block(Statements);
    }

    public BlockBuilder AddStatements(StatementSyntax statement)
    {
        Statements.Add(statement);
        return this;
    }
}
