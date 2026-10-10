using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Features.Diagnostics;
using Shouldly;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.MethodImpersonation;

// dynamic is object at runtime. Generated code passes a dynamic argument or result between its own members as an
// object, so those calls bind when the imposter compiles, and writes typeof(object) for it.
public class DynamicMemberCompilationTests
{
    [Fact]
    public async Task GivenMethodReturningDynamic_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { dynamic Use(dynamic value); }",
            "imposter.Use(Arg<dynamic>.Any()).Returns(value => value).Then().Returns(2); _ = imposter.Instance().Use(1); imposter.Use(Arg<dynamic>.Any()).Called(Count.Once());",
            nameof(DynamicMemberCompilationTests)
        );
    }

    [Fact]
    public async Task GivenClassMethodReturningDynamic_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual dynamic Get(object value) => value; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Get(Arg<object>.Any()).UseBaseImplementation(); _ = imposter.Instance().Get(1);",
            nameof(DynamicMemberCompilationTests)
        );
    }

    [Fact]
    public async Task GivenClassMethodWithDynamicParameter_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual void Use(dynamic value) { } public virtual int Read(in dynamic value) => 0; }",
            "var imposter = new Sample.ServiceImposter(); imposter.Use(Arg<dynamic>.Any()).UseBaseImplementation(); imposter.Read(Arg<dynamic>.Any()).UseBaseImplementation(); imposter.Instance().Use(1); _ = imposter.Instance().Read(2);",
            nameof(DynamicMemberCompilationTests)
        );
    }

    [Fact]
    public async Task GivenGenericMethodWithDynamicParameter_WhenImposterIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { void Use<T>(T first, dynamic second); dynamic Get<T>(System.Collections.Generic.List<dynamic> values); }",
            "imposter.Use<int>(Arg<int>.Any(), Arg<dynamic>.Any()).Callback((first, second) => { }); imposter.Get<int>(Arg<System.Collections.Generic.List<dynamic>>.Any()).Returns(values => values.Count); imposter.Instance().Use(1, \"second\"); _ = imposter.Instance().Get<int>(new System.Collections.Generic.List<dynamic>());",
            nameof(DynamicMemberCompilationTests)
        );
    }

    // Written in syntax, a type parameter named dynamic looks like the keyword. Its type checks compare the type
    // arguments, so writing it as object would make the method's setups and verification ignore them.
    [Fact]
    public async Task GivenTypeParameterNamedDynamic_WhenGeneratorRuns_ShouldKeepItInTheTypeChecks()
    {
        var result = await TargetGeneratorRun.RunAsync(
            /*lang=csharp*/
            """
            using Imposter.Abstractions;

            [assembly: GenerateImposter(typeof(Sample.IService))]

            namespace Sample
            {
                public interface IService { int Tally<dynamic>(dynamic value); }
            }
            """,
            nameof(DynamicMemberCompilationTests)
        );

        var source = result.GeneratedSources.ShouldHaveSingleItem().SourceText.ToString();
        source.ShouldContain("typeof(dynamic)");
        source.ShouldNotContain("typeof(object)");
    }

    [Fact]
    public async Task GivenClassPropertyAndIndexerOfDynamicType_WhenBaseImplementationIsUsed_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.Service",
            "public class Service { public virtual dynamic Value { get; set; } public virtual dynamic this[dynamic key] { get => key; set { } } }",
            "var imposter = new Sample.ServiceImposter(); imposter.Value.Getter().UseBaseImplementation(); imposter.Value.Setter(Arg<dynamic>.Any()).UseBaseImplementation(); imposter[Arg<dynamic>.Any()].Getter().UseBaseImplementation(); imposter[Arg<dynamic>.Any()].Setter().UseBaseImplementation(); var service = imposter.Instance(); service.Value = 1; service[2] = service.Value; _ = service[3];",
            nameof(DynamicMemberCompilationTests)
        );
    }
}
