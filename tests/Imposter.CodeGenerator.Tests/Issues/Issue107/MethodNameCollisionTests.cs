using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Issues.Issue107;

public class MethodNameCollisionTests
{
    public static IEnumerable<object[]> CollisionCases()
    {
        (string Signature, string Body, bool SupportsVirtualImplementation)[] methods =
        [
            ("int Do(string methodDisplayName)", "return 0;", true),
            ("int Do(int invocationBehavior)", "return 0;", true),
            ("int Do(int invocationImposter)", "return 0;", true),
            ("void Do<T>(ref int value, int valueAdapted)", "", false),
            ("void Do<Called>(Called a)", "", true),
            (
                "int Do(int invocationBehavior, int invocationBehavior_1, string methodDisplayName, string methodDisplayName_1, int invocationImposter, int invocationImposter_1)",
                "return 0;",
                true
            ),
            ("T Do<T>(ref T value, T valueAdapted, T valueAdapted_1)", "return value;", true),
            (
                "T Do<T>(out T value, T valueAdapted, T valueAdapted_1)",
                "value = valueAdapted; return value;",
                true
            ),
            ("void Do<T>(ref T value, ref T valueAdapted, T valueAdaptedAdapted)", "", true),
            (
                "Called Do<Called, Called_1, CallCount, CallCount_1>(Called a, Called_1 b, CallCount c, CallCount_1 d)",
                "return a;",
                true
            ),
        ];

        foreach (var targetKind in new[] { "interface", "abstract class", "class" })
        {
            foreach (var method in methods)
            {
                // Unused generic type arguments have a separate virtual base-call limitation.
                if (targetKind == "class" && !method.SupportsVirtualImplementation)
                {
                    continue;
                }

                yield return [method.Signature, method.Body, targetKind];
            }
        }

        // Type-parameter constraints on class overrides have a separate generation limitation.
        yield return
        [
            "Called Do<Called, Called_1>(Called a) where Called : Called_1 where Called_1 : class",
            "",
            "interface",
        ];
    }

    [Theory]
    [MemberData(nameof(CollisionCases))]
    public async Task Given_CollidingMethodNames_When_ImposterIsGenerated_Should_Compile(
        string signature,
        string body,
        string targetKind
    )
    {
        var declaration = targetKind switch
        {
            "class" => $"public virtual {signature} {{ {body} }}",
            "abstract class" => $"public abstract {signature};",
            _ => signature + ";",
        };
        var source = $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.Service))]

            namespace Sample
            {
                public {{targetKind}} Service
                {
                    {{declaration}}
                }
            }
            """;

        var context = await GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: "MethodNameCollision.Source.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(MethodNameCollisionTests)
        );

        var diagnostics = context.CompileSnippet(
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
