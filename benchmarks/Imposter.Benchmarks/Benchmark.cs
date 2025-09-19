using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
/*
using Imposter.Abstractions;
using Imposters;
*/
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using NSubstitute;

BenchmarkRunner.Run<SyntaxFactoryBenchmarks>();

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

/*[GenerateImposter(typeof(ICalculator))]
[MemoryDiagnoser]
public class Benchy
{
    [Benchmark]
    public void Mock()
    {
        var calculatorMock = new Mock<ICalculator>();
        calculatorMock
            .Setup(it => it.Add(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(1);

        calculatorMock.Object.Add(1, 1);
    }

    [Benchmark]
    public void NSub()
    {
        var calculatorMock = Substitute.For<ICalculator>();

        calculatorMock.Add(Arg.Any<int>(), Arg.Any<int>()).Returns(1);

        calculatorMock.Add(1, 1);
    }

    [Benchmark]
    public void Imposter()
    {
        var imposter = new ICalculatorImposter();

        /* TODO
        imposter
            .Add(Arg<int>.Any, Arg<int>.Any)
            .Returns((left, right) => left * right);
            #1#

        imposter.Instance().Add(10, 9);
    }

    public interface ICalculator
    {
        int Add(int left, int right);
    }
}*/