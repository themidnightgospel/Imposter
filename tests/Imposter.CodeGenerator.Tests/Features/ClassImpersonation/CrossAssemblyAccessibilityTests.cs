using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
using Imposter.CodeGenerator.CodeGenerator.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.ClassImpersonation.ClassImpersonationTestShared;

namespace Imposter.CodeGenerator.Tests.Features.ClassImpersonation;

public class CrossAssemblyAccessibilityTests
{
    private const string ConsumerAssemblyName = "CrossAssemblyConsumer";

    // Every kind of member the generator overrides, with each accessibility that has assembly-dependent rules.
    private const string ServiceSource = /*lang=csharp*/
        """
        public class Service
        {
            public Service() { }
            internal Service(int seed) { }
            protected internal virtual int ProtectedInternalMethod() => 1;
            protected internal virtual int ProtectedInternalProperty { get; set; }
            protected internal virtual int this[int index] { get => index; set { } }
            protected internal virtual event System.EventHandler ProtectedInternalEvent;
            internal virtual int InternalMethod() => 2;
            private protected virtual int PrivateProtectedMethod() => 3;
            public virtual int ProtectedInternalSetter { get; protected internal set; }
            public virtual int InternalSetter { get; internal set; }
            public virtual int this[string key] { get => 0; protected internal set { } }
        }
        """;

    private const string InternalConstructorServiceSource = /*lang=csharp*/
        """
        public class Service
        {
            internal Service() { }
            public virtual int Get() => 1;
        }
        """;

    private const string GenerateServiceImposter = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Service))]
        """;

    [Fact]
    public async Task GivenHttpMessageHandler_WhenImposterIsGenerated_ShouldCompile()
    {
        var consumer = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            /*lang=csharp*/"""
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(System.Net.Http.HttpMessageHandler))]
            """,
            ConsumerAssemblyName
        );

        GetErrorsAfterGeneration(consumer).ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenTargetInSameAssembly_WhenImposterIsGenerated_ShouldCompile()
    {
        var consumer = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            GenerateServiceImposter + ServiceSource,
            ConsumerAssemblyName
        );

        GetErrorsAfterGeneration(consumer).ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenTargetInAnotherAssembly_WhenImposterIsGenerated_ShouldCompile()
    {
        var consumer = await CreateConsumerOfExternalService(
            ServiceSource,
            MetadataImportOptions.All
        );

        GetErrorsAfterGeneration(consumer).ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenTargetInAssemblyGrantingInternalsToConsumer_WhenImposterIsGenerated_ShouldCompile()
    {
        var consumer = await CreateConsumerOfExternalService(
            $"[assembly: System.Runtime.CompilerServices.InternalsVisibleTo(\"{ConsumerAssemblyName}\")]\n"
                + ServiceSource,
            MetadataImportOptions.All
        );

        GetErrorsAfterGeneration(consumer).ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenOnlyInternalConstructorInAnotherAssemblyWithAllMetadata_WhenGeneratorRuns_ShouldReportIMP004()
    {
        var consumer = await CreateConsumerOfExternalService(
            InternalConstructorServiceSource,
            MetadataImportOptions.All
        );

        GetGeneratorDiagnosticIds(consumer)
            .ShouldBe([DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor.Id]);
    }

    [Fact]
    public async Task GivenOnlyInternalConstructorInAnotherAssemblyWithPublicMetadata_WhenGeneratorRuns_ShouldReportIMP004()
    {
        var consumer = await CreateConsumerOfExternalService(
            InternalConstructorServiceSource,
            MetadataImportOptions.Public
        );

        GetGeneratorDiagnosticIds(consumer)
            .ShouldBe([DiagnosticDescriptors.ImposterTargetMustHaveAccessibleConstructor.Id]);
    }

    // The IDE imports all metadata, so internal members of other assemblies are visible there; command-line builds
    // import only public and protected metadata.
    private static async Task<CSharpCompilation> CreateConsumerOfExternalService(
        string serviceSource,
        MetadataImportOptions metadataImportOptions
    )
    {
        var service = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            serviceSource,
            "ExternalServiceLibrary"
        );
        using var serviceImage = new MemoryStream();
        service.Emit(serviceImage).Success.ShouldBeTrue();

        var consumer = await CreateCompilationAsync(
            LanguageVersion.CSharp9,
            GenerateServiceImposter,
            ConsumerAssemblyName
        );

        return consumer
            .WithOptions(consumer.Options.WithMetadataImportOptions(metadataImportOptions))
            .AddReferences(MetadataReference.CreateFromImage(serviceImage.ToArray()));
    }

    private static Diagnostic[] GetErrorsAfterGeneration(CSharpCompilation consumer)
    {
        CreateDriver().RunGeneratorsAndUpdateCompilation(consumer, out var output, out _);

        return output
            .GetDiagnostics()
            .Where(it => it.Severity == DiagnosticSeverity.Error)
            .ToArray();
    }

    private static string[] GetGeneratorDiagnosticIds(CSharpCompilation consumer) =>
        CreateDriver()
            .RunGenerators(consumer)
            .GetRunResult()
            .Diagnostics.Select(it => it.Id)
            .ToArray();

    private static GeneratorDriver CreateDriver() =>
        CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .WithUpdatedParseOptions(new CSharpParseOptions(LanguageVersion.CSharp9));
}
