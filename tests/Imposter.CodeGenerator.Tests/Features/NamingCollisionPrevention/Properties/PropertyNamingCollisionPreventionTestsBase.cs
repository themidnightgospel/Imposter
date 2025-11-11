using System.Collections.Immutable;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public abstract class PropertyNamingCollisionPreventionTestsBase
{
    internal const string Source = /*lang=csharp*/"""
                                                 using Imposter.Abstractions;

                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyBuilderOperationCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyBuilderOperationClassCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyImposterMemberCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyBackingFieldCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyGetterBuilderNameCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyGetterBuilderClassCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertySetterBuilderNameCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertySetterBuilderClassCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyBuilderParameterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyCommonNameCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyDuplicateAccessorCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.IPropertyDuplicateAccessorWithSetterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyUseBaseImplementationGetterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyUseBaseImplementationSetterCollisionTarget))]
                                                 [assembly: GenerateImposter(typeof(Sample.NamingCollision.PropertyUseBaseImplementationCombinedCollisionTarget))]

                                                 namespace Sample.NamingCollision
                                                 {
                                                     public interface IPropertyBuilderOperationCollisionTarget
                                                     {
                                                         int Returns { get; set; }
                                                         int Throws { get; set; }
                                                         int Callback { get; set; }
                                                         int Then { get; set; }
                                                         int Called { get; set; }
                                                     }

                                                     public class PropertyBuilderOperationClassCollisionTarget
                                                     {
                                                         public virtual int UseBaseImplementation { get; set; }
                                                     }

                                                     public interface IPropertyImposterMemberCollisionTarget
                                                     {
                                                         object Instance { get; set; }
                                                         object ImposterTargetInstance { get; set; }
                                                         object _imposterInstance { get; set; }
                                                         string _invocationBehavior { get; set; }
                                                         int InitializeOutParametersWithDefaultValues { get; set; }
                                                     }

                                                     public interface IPropertyBackingFieldCollisionTarget
                                                     {
                                                         int _defaultPropertyBehaviour { get; set; }
                                                         int _MyProperty { get; set; }
                                                         int Prop { get; set; }
                                                         int _Prop { get; set; }
                                                     }

                                                     public interface IPropertyGetterBuilderNameCollisionTarget
                                                     {
                                                         int IWeirdPropertyGetterBuilder { get; set; }
                                                         int IWeirdPropertyGetterOutcomeBuilder { get; set; }
                                                         int IWeirdPropertyGetterContinuationBuilder { get; set; }
                                                         int IWeirdPropertyGetterCallbackBuilder { get; set; }
                                                         int IWeirdPropertyGetterVerifier { get; set; }
                                                         int IWeirdPropertyGetterFluentBuilder { get; set; }
                                                     }

                                                     public class PropertyGetterBuilderClassCollisionTarget
                                                     {
                                                         public virtual int IWeirdPropertyGetterUseBaseImplementationBuilder { get; set; }
                                                     }

                                                     public interface IPropertySetterBuilderNameCollisionTarget
                                                     {
                                                         int IWeirdPropertySetterBuilder { get; set; }
                                                         int IWeirdPropertySetterFluentBuilder { get; set; }
                                                         int IWeirdPropertySetterCallbackBuilder { get; set; }
                                                         int IWeirdPropertySetterContinuationBuilder { get; set; }
                                                         int IWeirdPropertySetterVerifier { get; set; }
                                                     }

                                                     public class PropertySetterBuilderClassCollisionTarget
                                                     {
                                                         public virtual int IWeirdPropertySetterUseBaseImplementationBuilder { get; set; }
                                                     }

                                                     public interface IPropertyBuilderParameterCollisionTarget
                                                     {
                                                         int value { get; set; }
                                                         int valueGenerator { get; set; }
                                                         int exception { get; set; }
                                                         int callback { get; set; }
                                                     }

                                                     public interface IPropertyCommonNameCollisionTarget
                                                     {
                                                         int Count { get; set; }
                                                         int Default { get; set; }
                                                     }

                                                     public interface IPropertyDuplicateAccessorCollisionTarget
                                                     {
                                                         int Reused { get; }
                                                     }

                                                     public interface IPropertyDuplicateAccessorWithSetterCollisionTarget
                                                     {
                                                         int Reused { get; set; }
                                                     }

                                                     public class PropertyUseBaseImplementationGetterCollisionTarget
                                                     {
                                                         public virtual int UseBaseImplementation
                                                         {
                                                             get => 0;
                                                         }
                                                     }

                                                     public class PropertyUseBaseImplementationSetterCollisionTarget
                                                     {
                                                         public virtual int UseBaseImplementation { get; set; }
                                                     }

                                                     public class PropertyUseBaseImplementationCombinedCollisionTarget
                                                     {
                                                         public virtual int Then { get; set; }
                                                         public virtual int UseBaseImplementation { get; set; }
                                                     }
                                                 }
                                                 """;

    private const string BaseSourceFileName = "PropertyNamingCollisionGeneratorInput.cs";
    private const string SnippetFileName = "PropertyNamingCollisionSnippet.cs";

    private static readonly Task<GeneratorTestContext> TestContextTask =
        GeneratorTestHelper.CreateContext(
            Source,
            baseSourceFileName: BaseSourceFileName,
            snippetFileName: SnippetFileName,
            assemblyName: nameof(PropertyNamingCollisionPreventionTestsBase));

    protected static async Task<ImmutableArray<Diagnostic>> CompileSnippet(string snippet)
    {
        var context = await TestContextTask.ConfigureAwait(false);
        return context.CompileSnippet(snippet);
    }

    protected static void AssertNoDiagnostics(ImmutableArray<Diagnostic> diagnostics) =>
        GeneratorTestHelper.AssertNoDiagnostics(diagnostics);
}

