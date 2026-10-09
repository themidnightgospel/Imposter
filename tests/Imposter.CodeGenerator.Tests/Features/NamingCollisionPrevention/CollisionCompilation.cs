using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

internal static class CollisionCompilation
{
    // Generates the imposter of a target declared in namespace Sample and compiles a snippet that uses it.
    internal static async Task AssertCompiles(
        string targetType,
        string targetDeclaration,
        string usage,
        string assemblyName,
        LanguageVersion languageVersion = LanguageVersion.CSharp9
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
            assemblyName: assemblyName,
            languageVersion
        );

        // The snippet imports Sample because the C# 14 Imposter() extension is only in scope through its namespace.
        GeneratorTestHelper.AssertNoDiagnostics(
            context.CompileSnippet(
                /*lang=csharp*/
                $$"""
                using Imposter.Abstractions;
                using Sample;

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
