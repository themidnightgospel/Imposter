using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.Testing;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

// Generated imposters compile in the consumer's project, so they may only use APIs of the frameworks consumers target.
public class GeneratedFrameworkCompatibilityTests
{
    private const string Source = /*lang=csharp*/
        """
        #nullable enable
        using System;
        using System.Collections.Generic;
        using System.Threading.Tasks;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IService))]
        [assembly: GenerateImposter(typeof(Sample.ServiceBase))]
        [assembly: GenerateImposter(typeof(Sample.IRepository<>))]

        namespace Sample
        {
            public interface IService
            {
                int Get(int id);
                void Run(string name, ref int counter, out string result);
                Task<int> GetAsync(int id);
                Task RunAsync();
                T Echo<T>(T value);
                TResult Convert<TInput, TResult>(TInput input) where TResult : class;
                int Value { get; set; }
                string this[int key] { get; set; }
                string this[int row, string column] { get; }
                event EventHandler Changed;
                event Func<object, EventArgs, Task> ChangedAsync;
            }

            public class ServiceBase
            {
                public virtual int Get(int id) => id;
                public virtual string? Name { get; set; }
                public virtual int this[string key] { get => 0; set { } }
                public virtual event EventHandler? Changed;
                protected virtual T Make<T>() where T : new() => new T();
            }

            public interface IRepository<TEntity>
            {
                TEntity Find(int id);
                IEnumerable<TEntity> All();
            }
        }
        """;

    [Fact]
    public async Task GivenNetStandard20References_WhenImpostersAreGenerated_ShouldCompile()
    {
        await AssertImpostersCompile(ReferenceAssemblies.NetStandard.NetStandard20);
    }

    [Fact]
    public async Task GivenNetStandard20ReferencesWithValueTaskPackage_WhenValueTaskImposterIsGenerated_ShouldCompile()
    {
        var context = await GeneratorTestHelper.CreateContext(
            /*lang=csharp*/
            """
            #nullable enable
            using System;
            using System.Threading.Tasks;
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IValueTaskService))]

            namespace Sample
            {
                public interface IValueTaskService
                {
                    ValueTask RunAsync();
                    ValueTask<int> GetAsync(int id);
                    event Func<object, EventArgs, ValueTask> Changed;
                }
            }
            """,
            baseSourceFileName: "FrameworkCompatibility.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(GeneratedFrameworkCompatibilityTests),
            ReferenceAssemblies.NetStandard.NetStandard20.AddPackages([
                new PackageIdentity("System.Threading.Tasks.Extensions", "4.5.4"),
            ])
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }

    [Fact]
    public async Task GivenNetFramework472References_WhenImpostersAreGenerated_ShouldCompile()
    {
        await AssertImpostersCompile(ReferenceAssemblies.NetFramework.Net472.Default);
    }

    [Fact]
    public async Task GivenNet90References_WhenImpostersAreGenerated_ShouldCompile()
    {
        await AssertImpostersCompile(ReferenceAssemblies.Net.Net90);
    }

    private static async Task AssertImpostersCompile(ReferenceAssemblies referenceAssemblies)
    {
        var context = await GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: "FrameworkCompatibility.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(GeneratedFrameworkCompatibilityTests),
            referenceAssemblies
        );

        GeneratorTestHelper.AssertNoDiagnostics(context.CompileSnippet(string.Empty));
    }
}
