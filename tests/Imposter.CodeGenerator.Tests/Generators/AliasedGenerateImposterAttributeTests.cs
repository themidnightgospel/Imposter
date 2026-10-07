using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

// Every [GenerateImposter] in these sources goes through an alias, so discovery cannot rely on the attribute's
// name as written: a single plain [GenerateImposter] anywhere in the compilation would hide a name-based lookup.
public class AliasedGenerateImposterAttributeTests
{
    private const string UsingAliasSource = /*lang=csharp*/
        """
        using Generate = Imposter.Abstractions.GenerateImposterAttribute;

        [assembly: Generate(typeof(Sample.Aliases.IUsingAliasService))]

        namespace Sample.Aliases
        {
            public interface IUsingAliasService
            {
                int Get(int value);
            }
        }
        """;

    private const string GlobalUsingAliasSource = /*lang=csharp*/
        """
        global using Generate = Imposter.Abstractions.GenerateImposterAttribute;

        [assembly: Generate(typeof(Sample.Aliases.IGlobalUsingAliasService))]

        namespace Sample.Aliases
        {
            public interface IGlobalUsingAliasService
            {
                int Get(int value);
            }
        }
        """;

    [Fact]
    public async Task GivenAttributeAppliedThroughUsingAlias_WhenGenerated_ShouldGenerateImposter()
    {
        var context = await GeneratorTestHelper.CreateContext(
            UsingAliasSource,
            baseSourceFileName: "Aliases.UsingAlias.Source.cs",
            snippetFileName: "Aliases.UsingAlias.Snippet.cs",
            assemblyName: $"{nameof(AliasedGenerateImposterAttributeTests)}UsingAlias",
            languageVersion: LanguageVersion.CSharp9
        );

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            using Imposter.Abstractions;

            namespace Sample.Aliases
            {
                public static class Usage
                {
                    public static IUsingAliasService Create() => new IUsingAliasServiceImposter().Instance();
                }
            }
            """
        );

        diagnostics.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenAttributeAppliedThroughGlobalUsingAlias_WhenGenerated_ShouldGenerateImposter()
    {
        var context = await GeneratorTestHelper.CreateContext(
            GlobalUsingAliasSource,
            baseSourceFileName: "Aliases.GlobalUsingAlias.Source.cs",
            snippetFileName: "Aliases.GlobalUsingAlias.Snippet.cs",
            assemblyName: $"{nameof(AliasedGenerateImposterAttributeTests)}GlobalUsingAlias",
            languageVersion: LanguageVersion.CSharp10
        );

        var diagnostics = context.CompileSnippet( /*lang=csharp*/
            """
            using Imposter.Abstractions;

            namespace Sample.Aliases
            {
                public static class Usage
                {
                    public static IGlobalUsingAliasService Create() =>
                        new IGlobalUsingAliasServiceImposter().Instance();
                }
            }
            """
        );

        diagnostics.ShouldBeEmpty();
    }
}
