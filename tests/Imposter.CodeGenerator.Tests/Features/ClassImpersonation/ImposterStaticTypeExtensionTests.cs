using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;
using GeneratorArtifacts = Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared.GeneratorArtifacts;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class ImposterStaticTypeExtensionTests
{
    private const string InterfaceSource = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IOrderService))]

        namespace Sample;

        public interface IOrderService
        {
            int Sum(int left, int right);
        }
        """;
    private const string ClassSource = /*lang=csharp*/
        """
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.MultiCtorClass))]

        namespace Sample;

        public class MultiCtorClass
        {
            public MultiCtorClass()
            {
            }

            public MultiCtorClass(int value, string label)
            {
            }

            public MultiCtorClass(int value = 0)
            {
            }

            public MultiCtorClass(ref double value)
            {
            }

            public MultiCtorClass(out int value)
            {
                value = 0;
            }

            public MultiCtorClass(params int[] values)
            {
            }
        }
        """;

    [Fact]
    public async Task GivenClassWithMultipleConstructors_WhenUsingImposterExtension_ShouldExposeAllOverloads()
    {
        var artifacts = await RunGeneratorAsync(
            LanguageVersion.Preview,
            ClassSource,
            "ImposterGeneratorExtensionsTests"
        );

        const string snippet = /*lang=csharp*/
            """
            using Imposter.Abstractions;

            namespace Sample;

            public static class ClassUsage
            {
                public static void Call()
                {
                    // Parameterless extension (ImposterMode default + explicit)
                    // parameterless Imposter method will no longer be generated as there is no way to distinguish
                    //   between parameterless and 'public MultiCtorClass(int value = 0)'
                    // var defaultCtor = MultiCtorClass.Imposter();
                    var defaultCtorExplicit = MultiCtorClass.Imposter(ImposterMode.Explicit);

                    // (int value, string label) constructor
                    var overload = MultiCtorClass.Imposter(42, "label");
                    var overloadExplicit = MultiCtorClass.Imposter(42, "label", ImposterMode.Explicit);

                    // (int value = 0) constructor
                    var defaultValue = MultiCtorClass.Imposter(10);
                    var defaultValueExplicit = MultiCtorClass.Imposter(10, ImposterMode.Explicit);

                    // (ref double value) constructor
                    var refValue = 10.1;
                    var refImposter = MultiCtorClass.Imposter(ref refValue);

                    var explicitRefValue = 20.2;
                    var explicitRefImposter = MultiCtorClass.Imposter(ref explicitRefValue, ImposterMode.Explicit);

                    // (out int value) constructor
                    int outValue;
                    var outImposter = MultiCtorClass.Imposter(out outValue);

                    int explicitOutValue;
                    var explicitOutImposter = MultiCtorClass.Imposter(out explicitOutValue, ImposterMode.Explicit);

                    // (params int[] values) constructor
                    var paramsImposter = MultiCtorClass.Imposter([1, 2, 3]);
                    var paramsExplicitImposter = MultiCtorClass.Imposter([1, 2, 3], ImposterMode.Explicit);
                }
            }
            """;

        AssertSnippetCompiles(LanguageVersion.Preview, artifacts, snippet);
    }

    [Fact]
    public async Task GivenInterface_WhenUsingImposterExtension_ShouldExposeParameterlessAndImposterMode()
    {
        var artifacts = await RunGeneratorAsync(
            LanguageVersion.Preview,
            InterfaceSource,
            "ImposterGeneratorExtensionsTests"
        );

        const string snippet = /*lang=csharp*/
            """
            using Imposter.Abstractions;

            namespace Sample;

            public static class ClassUsage
            {
                public static void Call()
                {
                    var orderServiceImposter = IOrderService.Imposter();
                    var orderServiceExplicitImposter = IOrderService.Imposter(ImposterMode.Explicit);
                }
            }
            """;

        AssertSnippetCompiles(LanguageVersion.Preview, artifacts, snippet);
    }

    private static void AssertSnippetCompiles(
        LanguageVersion languageVersion,
        GeneratorArtifacts artifacts,
        string snippet
    )
    {
        var (compilation, parseOptions) = InitializeCompilation(languageVersion, artifacts);

        var snippetTree = CSharpSyntaxTree.ParseText(
            snippet,
            options: parseOptions,
            path: "Snippet.cs"
        );
        compilation = compilation.AddSyntaxTrees(snippetTree);

        var diagnostics = compilation
            .GetDiagnostics()
            .Where(static diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
            .ToArray();

        if (diagnostics.Length > 0)
        {
            diagnostics.ShouldBeEmpty(FormatDiagnostics(diagnostics));
        }
    }
}
