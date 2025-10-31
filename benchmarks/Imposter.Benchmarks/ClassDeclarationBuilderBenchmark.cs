using BenchmarkDotNet.Attributes;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.Benchmarks;

/*
BenchmarkDotNet v0.15.2, Windows 11 (10.0.26100.6584/24H2/2024Update/HudsonValley)
13th Gen Intel Core i9-13900HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2


| Method                        | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|------------------------------ |----------:|----------:|----------:|-------:|-------:|----------:|
| ClassDeclarationBuilder       |  1.287 us | 0.0254 us | 0.0734 us | 0.4997 | 0.0114 |   9.19 KB |
| SyntaxFactoryClassDeclaration | 15.504 us | 0.3041 us | 0.4361 us | 2.5635 | 0.0305 |  47.36 KB |
 */

[MemoryDiagnoser]
public class ClassDeclarationBuilderBenchmark
{
    TypeSyntax IntType = PredefinedType(Token(SyntaxKind.IntKeyword));

    [Benchmark]
    public void ClassDeclarationBuilder()
    {
        var builder = new ClassDeclarationBuilder("TestClass");

        for (var i = 0; i < 10; i++)
        {
            builder.AddMember(PropertyDeclaration(IntType, Identifier("Property" + i)));
        }

        for (var i = 0; i < 10; i++)
        {
            builder.AddMember(MethodDeclaration(IntType, Identifier("Method" + i)));
        }

        builder.Build();
    }

    [Benchmark]
    public void SyntaxFactoryClassDeclaration()
    {
        var classDeclaration = ClassDeclaration("TestClass");

        for (var i = 0; i < 10; i++)
        {
            classDeclaration = classDeclaration
                .AddMembers(PropertyDeclaration(IntType, Identifier("Property" + i)));
        }

        for (var i = 0; i < 10; i++)
        {
            classDeclaration = classDeclaration
                .AddMembers(MethodDeclaration(IntType, Identifier("Method" + i)));
        }
    }
}