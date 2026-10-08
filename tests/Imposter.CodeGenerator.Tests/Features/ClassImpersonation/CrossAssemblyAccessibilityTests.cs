using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.CodeGenerator;
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
        var consumer = await CreateConsumerOfExternalService(ServiceSource);

        GetErrorsAfterGeneration(consumer).ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenTargetInAssemblyGrantingInternalsToConsumer_WhenImposterIsGenerated_ShouldCompile()
    {
        var consumer = await CreateConsumerOfExternalService(
            $"[assembly: System.Runtime.CompilerServices.InternalsVisibleTo(\"{ConsumerAssemblyName}\")]\n"
                + ServiceSource
        );

        GetErrorsAfterGeneration(consumer).ShouldBeEmpty();
    }

    private static async Task<CSharpCompilation> CreateConsumerOfExternalService(
        string serviceSource
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

        return consumer.AddReferences(MetadataReference.CreateFromImage(serviceImage.ToArray()));
    }

    private static Diagnostic[] GetErrorsAfterGeneration(CSharpCompilation consumer)
    {
        CSharpGeneratorDriver
            .Create(new ImposterGenerator())
            .WithUpdatedParseOptions(new CSharpParseOptions(LanguageVersion.CSharp9))
            .RunGeneratorsAndUpdateCompilation(consumer, out var output, out _);

        return output
            .GetDiagnostics()
            .Where(it => it.Severity == DiagnosticSeverity.Error)
            .ToArray();
    }
}
