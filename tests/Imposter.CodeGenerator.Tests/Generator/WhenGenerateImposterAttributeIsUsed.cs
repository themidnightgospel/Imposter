/*using Imposter.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generator;

public class WhenGenerateImposterAttributeIsUsed
{
    [Fact]
    public void OnClassLevel_ShouldGenerateImposter()
    {
        var imposterTargetSourceCode = /*lang=c##1#
            """
            namespace Test;

            using Imposter.Abstractions;

            public interface ISut
            {
            }

            [GenerateImposter(typeof(ISut))]
            public class TestClass
            {
                public void TestMethod()
                {
                }
            }

            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(imposterTargetSourceCode);


        // Reference core libraries (mscorlib, System.Runtime, etc.)
        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(GenerateImposterAttribute).Assembly.Location),
        }
            /*
            .Concat(typeof(GenerateImposterAttribute)
            .Assembly
            .GetReferencedAssemblies()
            .Select(it => MetadataReference.CreateFromFile(Assembly.Load(it.Name).Location)))
            #1#
            ;

        var compilation = CSharpCompilation.Create(
            typeof(WhenGenerateImposterAttributeIsUsed).Assembly.FullName,
            [syntaxTree],
            references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // TODO reports diag
        /*
        var diag = compilation.GetDiagnostics();

        var generator = new ImposterGenerator();
        var generatorResult = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation).GetRunResult();
    #1#
    }
}*/