using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// dynamic is object at runtime, so the imposter of members with dynamic types binds all its own calls when it compiles.
// A call the runtime binder resolves would need Microsoft.CSharp, and binds on its first use.
public class DynamicMemberBindingTests
{
    [Fact]
    public async Task GivenInterfaceMembersOfDynamicType_WhenImposterIsCompiled_ShouldNotUseTheRuntimeBinder()
    {
        var binderReferences = await RuntimeBinderReferences(
            "public interface IService { dynamic Echo(dynamic value); void Use<T>(T first, dynamic second); dynamic? Value { get; set; } dynamic this[dynamic key] { get; set; } event System.Action<dynamic> Raised; }",
            "Sample.IService"
        );

        binderReferences.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenClassMembersOfDynamicType_WhenImposterIsCompiled_ShouldNotUseTheRuntimeBinder()
    {
        var binderReferences = await RuntimeBinderReferences(
            "public class Service { public Service(dynamic value) { } public virtual dynamic Echo(dynamic value) => value; public virtual dynamic? Value { get; set; } public virtual dynamic this[dynamic key] { get => key; set { } } public virtual System.Threading.Tasks.Task<dynamic> GetAsync(dynamic value) => System.Threading.Tasks.Task.FromResult<dynamic>((object)value); }",
            "Sample.Service"
        );

        binderReferences.ShouldBeEmpty();
    }

    // The types of the runtime binder the emitted assembly refers to: the call sites it creates and the binder that
    // resolves them. The scan covers the target's own code too, so a target mustn't bind itself:
    // Task.FromResult<dynamic>(value) does, Task.FromResult<dynamic>((object)value) doesn't.
    private static async Task<IReadOnlyList<string>> RuntimeBinderReferences(
        string targetDeclaration,
        string targetType
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
            baseSourceFileName: $"{nameof(DynamicMemberBindingTests)}.cs",
            snippetFileName: "Snippet.cs",
            assemblyName: nameof(DynamicMemberBindingTests)
        );
        var parseOptions = context.Compilation.SyntaxTrees.First().Options;
        var compilation = context.Compilation.AddSyntaxTrees(
            context
                .RunGenerator()
                .GeneratedSources.Select(source =>
                    CSharpSyntaxTree.ParseText(source.SourceText, (CSharpParseOptions)parseOptions)
                )
        );

        using var stream = new MemoryStream();
        var emitResult = compilation.Emit(stream);
        emitResult.Diagnostics.Where(it => it.Severity == DiagnosticSeverity.Error).ShouldBeEmpty();
        stream.Position = 0;
        using var peReader = new PEReader(stream);
        var metadata = peReader.GetMetadataReader();

        return
        [
            .. metadata
                .TypeReferences.Select(metadata.GetTypeReference)
                .Select(reference =>
                    $"{metadata.GetString(reference.Namespace)}.{metadata.GetString(reference.Name)}"
                )
                .Where(name =>
                    name.StartsWith("System.Runtime.CompilerServices.CallSite")
                    || name.StartsWith("Microsoft.CSharp.RuntimeBinder.")
                ),
        ];
    }
}
