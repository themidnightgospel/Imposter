using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Tests.Features.MethodImposter;

public abstract class MethodImposterFluentApiTestsBase
{
    internal const string Source = /*lang=csharp*/
        """
        using System.Threading.Tasks;
        using Imposter.Abstractions;

        [assembly: GenerateImposter(typeof(Sample.IMethodInterface))]
        [assembly: GenerateImposter(typeof(Sample.ClassMethodTarget))]
        [assembly: GenerateImposter(typeof(Sample.SealedOverrideClass))]

        namespace Sample
        {
            public interface IMethodInterface
            {
                int GetNumber();
                void DoWork();
                Task<int> GetNumberAsync();
                Task DoWorkAsync();
                int Increment(int value);
                int RefOutParams(ref int seed, out int doubled, params string[] labels);
            }

            public class ClassMethodTarget
            {
                public virtual int VirtualCompute(int value) => value * 2;
                public virtual int VirtualAdd(int left, int right) => left + right;
                public virtual System.Threading.Tasks.Task<int> VirtualComputeAsync(int value) => System.Threading.Tasks.Task.FromResult(value * 2);
                public int NonVirtualCompute(int value) => value;
                public virtual void VirtualAction() { }
                public void NonVirtualAction() { }
                public virtual int VirtualRefOutParams(ref int seed, out int doubled, params string[] labels)
                {
                    doubled = seed * 2;
                    return doubled + labels.Length;
                }
            }

            public class SealedOverrideClass : ClassMethodTarget
            {
                public sealed override int VirtualCompute(int value) => base.VirtualCompute(value);
                public sealed override System.Threading.Tasks.Task<int> VirtualComputeAsync(int value) => base.VirtualComputeAsync(value);
                public sealed override void VirtualAction() => base.VirtualAction();
            }
        }
        """;

    internal const string BaseSourceFileName = "GeneratorInput.cs";
    internal const string SnippetFileName = "Snippet.cs";

    internal static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(MethodImposterFluentApiTestsBase)
        );

    internal static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    internal static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);

    internal static void AssertSingleDiagnostic(
        ImmutableArray<Diagnostic> diagnostics,
        string expectedId,
        int expectedLine
    ) =>
        GeneratorTestHelper.AssertSingleDiagnostic(
            diagnostics,
            expectedId,
            expectedLine,
            SnippetFileName
        );
}
