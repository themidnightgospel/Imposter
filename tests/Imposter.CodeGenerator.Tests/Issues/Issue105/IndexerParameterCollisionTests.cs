using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue105;

public class IndexerParameterCollisionTests
{
    public static IEnumerable<object[]> CollisionCases()
    {
        string[] parameterNames =
        [
            "arguments",
            "baseImplementation",
            "invokedBaseImplementation",
            "matchedCallback",
            "registration",
            "getterInvocationImposter",
            "criteria",
        ];

        foreach (var isClass in new[] { false, true })
        {
            foreach (var name in parameterNames)
            {
                yield return [name, isClass, "both"];
            }

            var combinedNames = string.Join(
                ",",
                parameterNames.SelectMany(name => new[] { name, name + "_1" })
            );

            foreach (var accessors in new[] { "both", "get", "set" })
            {
                yield return [combinedNames, isClass, accessors];
            }
        }
    }

    [Theory]
    [MemberData(nameof(CollisionCases))]
    public async Task Given_CollidingIndexerParameters_When_ImposterIsGenerated_Should_Compile(
        string parameterNames,
        bool isClass,
        string accessors
    )
    {
        var parameters = string.Join(", ", parameterNames.Split(',').Select(name => "int " + name));
        var targetKind = isClass ? "class" : "interface";
        var modifiers = isClass ? "public virtual " : "";
        var getter =
            accessors == "set" ? ""
            : isClass ? "get => 0;"
            : "get;";
        var setter =
            accessors == "get" ? ""
            : isClass ? "set { }"
            : "set;";
        var source = $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Service))]

            namespace Sample
            {
                public {{targetKind}} Service
                {
                    {{modifiers}}int this[{{parameters}}] { {{getter}} {{setter}} }
                }
            }
            """;

        var context = await GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: "IndexerParameterCollision.Source.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(IndexerParameterCollisionTests)
        );

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            public static class Scenario
            {
                public static object Create() => new Sample.ServiceImposter();
            }
            """
        );

        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
    }
}
