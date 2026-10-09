using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

internal static class CollisionCompilation
{
    // Generates the imposter of a target declared in namespace Sample and compiles a snippet that uses it.
    internal static async Task AssertCompiles(
        string targetType,
        string targetDeclaration,
        string usage,
        string assemblyName
    )
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            $$"""
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof({{targetType}}))]

            namespace Sample
            {
                {{targetDeclaration}}
            }
            """,
            baseSourceFileName: $"{assemblyName}.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: assemblyName
        );

        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                /*lang=csharp*/
                $$"""
                using Imposter.Abstractions;

                public static class Usage
                {
                    public static void Run()
                    {
                        {{usage}}
                    }
                }
                """
            )
        );
    }
}
