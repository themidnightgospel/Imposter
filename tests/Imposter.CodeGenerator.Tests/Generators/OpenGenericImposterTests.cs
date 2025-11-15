using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class OpenGenericImposterTests
{
    private const string InterfaceSource =
        /*lang=csharp*/
        @"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.IAsyncObserver<>))]

namespace Sample;

public interface IAsyncObserver<TItem>
    where TItem : class, new()
{
    TItem Last { get; }
    void OnNext(TItem value);
}
";

    private const string ClassSource =
        /*lang=csharp*/
        @"using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(Sample.GenericProcessor<,>))]

namespace Sample;

public abstract class GenericProcessor<TKey, TValue>
    where TKey : struct
    where TValue : class, new()
{
    protected GenericProcessor() { }

    public abstract TValue Compute(TKey key, TValue current);

    public virtual TValue Identity(TValue current) => current;
}
";

    private static readonly Task<GeneratorTestContext> InterfaceContextTask =
        GeneratorTestHelper.CreateContext(
            InterfaceSource,
            baseSourceFileName: "OpenGenericInterface.cs",
            snippetFileName: "OpenGenericInterface.Snippet.cs",
            assemblyName: nameof(OpenGenericImposterTests),
            languageVersion: LanguageVersion.CSharp9
        );

    private static readonly Task<GeneratorTestContext> ClassContextTask =
        GeneratorTestHelper.CreateContext(
            ClassSource,
            baseSourceFileName: "OpenGenericClass.cs",
            snippetFileName: "OpenGenericClass.Snippet.cs",
            assemblyName: nameof(OpenGenericImposterTests),
            languageVersion: LanguageVersion.CSharp9
        );

    [Fact]
    public async Task GivenOpenGenericInterfaceTarget_WhenGeneratorRuns_ShouldEmitGenericImposter()
    {
        var testContext = await InterfaceContextTask.ConfigureAwait(false);
        var result = testContext.RunGenerator();

        var imposterSource = result
            .GeneratedSources.Single(source => source.HintName == "IAsyncObserverImposter.g.cs")
            .SourceText.ToString();

        imposterSource.ShouldContain("public sealed class IAsyncObserverImposter<TItem>");
        imposterSource.ShouldContain("where TItem : class, new()");
        imposterSource.ShouldContain("private readonly IAsyncObserverImposter<TItem> _imposter;");
        imposterSource.ShouldContain("class ImposterTargetInstance");
        imposterSource.ShouldContain(": global::Sample.IAsyncObserver<TItem>");
        imposterSource.ShouldContain(
            "global::Imposter.Abstractions.IHaveImposterInstance<global::Sample.IAsyncObserver<TItem>>"
        );
    }

    [Fact]
    public async Task GivenOpenGenericClassTarget_WhenGeneratorRuns_ShouldEmitGenericImposter()
    {
        var testContext = await ClassContextTask.ConfigureAwait(false);
        var result = testContext.RunGenerator();

        var imposterSource = result
            .GeneratedSources.Single(source => source.HintName == "GenericProcessorImposter.g.cs")
            .SourceText.ToString();

        imposterSource.ShouldContain("public sealed class GenericProcessorImposter<TKey, TValue>");
        imposterSource.ShouldContain("where TKey : struct");
        imposterSource.ShouldContain("where TValue : class, new()");
        imposterSource.ShouldContain(
            "private readonly GenericProcessorImposter<TKey, TValue> _imposter;"
        );
        imposterSource.ShouldContain("class ImposterTargetInstance");
        imposterSource.ShouldContain(": global::Sample.GenericProcessor<TKey, TValue>");
    }
}
