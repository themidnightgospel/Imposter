using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

public abstract class IndexerNamingCollisionPreventionTestsBase
{
    internal const string Source = /*lang=csharp*/"""
                                                 using System;
                                                 using System.Runtime.CompilerServices;
                                                 using Imposter.Abstractions;

                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IReturnsIndexerCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IThrowsIndexerCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.ICallbackIndexerCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IThenIndexerCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.ICalledIndexerCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.UseBaseImplementationIndexerCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.ThenVirtualIndexerCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerBuilderInterfaceCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterEntryCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerSetterEntryCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterOutcomeBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterContinuationBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterCallbackBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterVerifierCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterFluentBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerSetterBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerSetterCallbackBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerSetterContinuationBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerSetterVerifierCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerSetterFluentBuilderCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerValueDelegateCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerGetterCallbackDelegateCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerSetterCallbackDelegateCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerExceptionGeneratorDelegateCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerArgumentsCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerArgumentsCriteriaCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerMemberCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerParameterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IIndexerValueParameterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IndexerImposterFieldCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IMultiIndexerCollisionTarget))]

                                                 namespace Sample.NamingCollision
                                                 {
                                                     public interface IReturnsIndexerCollisionTarget
                                                     {
                                                         [IndexerName("Returns")]
                                                         int this[int number] { get; }
                                                     }

                                                     public interface IThrowsIndexerCollisionTarget
                                                     {
                                                         [IndexerName("Throws")]
                                                         int this[string label] { get; }
                                                     }

                                                     public interface ICallbackIndexerCollisionTarget
                                                     {
                                                         [IndexerName("Callback")]
                                                         int this[Guid identifier] { get; set; }
                                                     }

                                                     public interface IThenIndexerCollisionTarget
                                                     {
                                                         [IndexerName("Then")]
                                                         int this[bool flag] { get; set; }
                                                     }

                                                     public interface ICalledIndexerCollisionTarget
                                                     {
                                                         [IndexerName("Called")]
                                                         int this[long count] { get; set; }
                                                     }

                                                     public class UseBaseImplementationIndexerCollisionTarget
                                                     {
                                                         [IndexerName("UseBaseImplementation")]
                                                         public virtual int this[int number]
                                                         {
                                                             get => 0;
                                                             set { }
                                                         }
                                                     }

                                                     public class ThenVirtualIndexerCollisionTarget
                                                     {
                                                         [IndexerName("Then")]
                                                         public virtual int this[string key]
                                                         {
                                                             get => 0;
                                                             set { }
                                                         }
                                                     }

                                                     public interface IIndexerBuilderInterfaceCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerBuilder")]
                                                         int this[int seed] { get; }
                                                     }

                                                     public interface IIndexerGetterEntryCollisionTarget
                                                     {
                                                         [IndexerName("Getter")]
                                                         int this[string content] { get; }
                                                     }

                                                     public interface IIndexerSetterEntryCollisionTarget
                                                     {
                                                         [IndexerName("Setter")]
                                                         int this[Guid token] { get; set; }
                                                     }

                                                     public interface IIndexerGetterBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerGetterBuilder")]
                                                         int this[int amount] { get; }
                                                     }

                                                     public interface IIndexerGetterOutcomeBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerGetterOutcomeBuilder")]
                                                         int this[string outcome] { get; }
                                                     }

                                                     public interface IIndexerGetterContinuationBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerGetterContinuationBuilder")]
                                                         int this[Guid token] { get; }
                                                     }

                                                     public interface IIndexerGetterCallbackBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerGetterCallbackBuilder")]
                                                         int this[bool flag] { get; }
                                                     }

                                                     public interface IIndexerGetterVerifierCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerGetterVerifier")]
                                                         int this[long count] { get; }
                                                     }

                                                     public interface IIndexerGetterFluentBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerGetterFluentBuilder")]
                                                         int this[double amount] { get; }
                                                     }

                                                     public interface IIndexerSetterBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerSetterBuilder")]
                                                         int this[int amount] { get; set; }
                                                     }

                                                     public interface IIndexerSetterCallbackBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerSetterCallbackBuilder")]
                                                         int this[string payload] { get; set; }
                                                     }

                                                     public interface IIndexerSetterContinuationBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerSetterContinuationBuilder")]
                                                         int this[Guid token] { get; set; }
                                                     }

                                                     public interface IIndexerSetterVerifierCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerSetterVerifier")]
                                                         int this[bool flag] { get; set; }
                                                     }

                                                     public interface IIndexerSetterFluentBuilderCollisionTarget
                                                     {
                                                         [IndexerName("IWeirdIndexerSetterFluentBuilder")]
                                                         int this[long count] { get; set; }
                                                     }

                                                     public interface IIndexerValueDelegateCollisionTarget
                                                     {
                                                         [IndexerName("WeirdIndexerDelegate")]
                                                         int this[int amount] { get; }
                                                     }

                                                     public interface IIndexerGetterCallbackDelegateCollisionTarget
                                                     {
                                                         [IndexerName("WeirdIndexerGetterCallback")]
                                                         int this[string label] { get; }
                                                     }

                                                     public interface IIndexerSetterCallbackDelegateCollisionTarget
                                                     {
                                                         [IndexerName("WeirdIndexerSetterCallback")]
                                                         int this[Guid token] { get; set; }
                                                     }

                                                     public interface IIndexerExceptionGeneratorDelegateCollisionTarget
                                                     {
                                                         [IndexerName("WeirdIndexerExceptionGenerator")]
                                                         int this[bool flag] { get; }
                                                     }

                                                     public interface IIndexerArgumentsCollisionTarget
                                                     {
                                                         [IndexerName("WeirdIndexerArguments")]
                                                         int this[int left, int right] { get; }
                                                     }

                                                     public interface IIndexerArgumentsCriteriaCollisionTarget
                                                     {
                                                         [IndexerName("WeirdIndexerArgumentsCriteria")]
                                                         int this[int left, string right] { get; }
                                                     }

                                                     public interface IIndexerMemberCollisionTarget
                                                     {
                                                         int this[int key] { get; set; }
                                                         int this[string key, string secondary] { get; set; }
                                                         int this[Guid identifier] { get; set; }

                                                         int Indexer { get; set; }
                                                         int _IndexerIndexer { get; set; }
                                                         int _IndexerDefaultIndexerBehaviour { get; set; }
                                                         int Default { get; set; }
                                                         int Count { get; set; }
                                                         int _invocationBehavior { get; set; }
                                                         object ImposterTargetInstance { get; set; }
                                                         object _imposterInstance { get; set; }

                                                         object Instance();
                                                         void IndexerMethod();
                                                     }

                                                     public interface IIndexerParameterCollisionTarget
                                                     {
                                                         int this[int valueGenerator] { get; }
                                                         int this[string exceptionGenerator] { get; }
                                                         int this[Guid callback] { get; set; }
                                                         int this[long count] { get; set; }
                                                     }

                                                     public interface IIndexerValueParameterCollisionTarget
                                                     {
                                                         int this[int value] { get; set; }
                                                     }

                                                     public class IndexerImposterFieldCollisionTarget
                                                     {
                                                         public int _imposter { get; set; }

                                                         public virtual int this[int key]
                                                         {
                                                             get => 0;
                                                             set { }
                                                         }
                                                     }

                                                     public interface IMultiIndexerCollisionTarget
                                                     {
                                                         int this[int key] { get; set; }
                                                         int this[int first, string second] { get; set; }

                                                         int Default { get; set; }
                                                         int Count { get; set; }

                                                         void Indexer();
                                                     }
                                                 }
                                                 """;

    private const string BaseSourceFileName = "IndexerNamingCollisionGeneratorInput.cs";
    private const string SnippetFileName = "IndexerNamingCollisionSnippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(IndexerNamingCollisionPreventionTestsBase));

    protected static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    protected static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
}
