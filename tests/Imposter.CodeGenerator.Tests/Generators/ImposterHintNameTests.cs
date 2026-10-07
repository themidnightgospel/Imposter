using System.Linq;
using System.Threading.Tasks;
using Imposter.CodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Generators;

public class ImposterHintNameTests
{
    private const string SameNamedTargets = /*lang=csharp*/
        """
        namespace Sample.HintNames.A
        {
            public interface IFoo
            {
                void Run();
            }
        }

        namespace Sample.HintNames.B
        {
            public interface IFoo
            {
                int Count();
            }
        }
        """;

    private const string AttributesInOrderAB = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.A.IFoo))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.B.IFoo))]

        """;

    private const string AttributesInOrderBA = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.B.IFoo))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.A.IFoo))]

        """;

    private const string TargetsOfEachShape = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.IService), false)]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.IRepository<>))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.IPair<int, string>), false)]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Outer.IInner))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(IGlobalService))]

        public interface IGlobalService
        {
            void Run();
        }

        namespace Sample.HintNames
        {
            public interface IService
            {
                void Run();
            }

            public interface IRepository<TEntity>
            {
                TEntity Get(int id);
            }

            public interface IPair<TFirst, TSecond>
            {
                TFirst First(TSecond second);
            }

            public class Outer
            {
                public interface IInner
                {
                    void Run();
                }
            }
        }
        """;

    [Fact]
    public async Task GivenSameNamedTargetsInDifferentNamespaces_WhenGenerated_ShouldNameFilesAfterFullyQualifiedTargets()
    {
        var hintNames = await GenerateHintNames(
            AttributesInOrderAB + SameNamedTargets,
            "SameNamed"
        );

        hintNames.ShouldBe(
            ["Sample.HintNames.A.IFooImposter.g.cs", "Sample.HintNames.B.IFooImposter.g.cs"],
            ignoreOrder: true
        );
    }

    [Fact]
    public async Task GivenSameNamedTargets_WhenAttributeOrderIsReversed_ShouldKeepEachImposterInItsFile()
    {
        var inOrder = await GenerateNamespaceByHintName(
            AttributesInOrderAB + SameNamedTargets,
            "OrderAB"
        );
        var reversed = await GenerateNamespaceByHintName(
            AttributesInOrderBA + SameNamedTargets,
            "OrderBA"
        );

        reversed.ShouldBe(inOrder, ignoreOrder: true);
    }

    [Fact]
    public async Task GivenTargetsOfEachShape_WhenGenerated_ShouldNameFilesAfterFullyQualifiedTargets()
    {
        var hintNames = await GenerateHintNames(TargetsOfEachShape, "Shapes");

        hintNames.ShouldBe(
            [
                "Imposters.Sample.HintNames.IServiceImposter.g.cs",
                "Sample.HintNames.IRepository_TEntity_Imposter.6b1618b2.g.cs",
                "Imposters.Sample.HintNames.IPair_int__string_Imposter.7902b867.g.cs",
                "Sample.HintNames.Outer.IInnerImposter.g.cs",
                "IGlobalServiceImposter.g.cs",
            ],
            ignoreOrder: true
        );
    }

    // `IFoo<int>` and `IFoo_int_` both sanitize to `IFoo_int_`.
    private const string ClosedGenericAndUnderscoreNamedTargets = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Collisions.IFoo<int>))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Collisions.IFoo_int_))]

        namespace Sample.HintNames.Collisions
        {
            public interface IFoo<T>
            {
                T Get();
            }

            public interface IFoo_int_
            {
                void Run();
            }
        }
        """;

    // `IRepository<TEntity>` and `IRepository_TEntity_` both sanitize to `IRepository_TEntity_`.
    private const string OpenGenericAndUnderscoreNamedTargets = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Collisions.IRepository<>))]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Collisions.IRepository_TEntity_))]

        namespace Sample.HintNames.Collisions
        {
            public interface IRepository<TEntity>
            {
                TEntity Get(int id);
            }

            public interface IRepository_TEntity_
            {
                void Run();
            }
        }
        """;

    // In the dedicated namespace both imposters share `Imposters.Sample.HintNames.Collisions.IPair_int__string_`,
    // but their type names differ, so only the hint names collide.
    private const string DedicatedNamespaceClosedGenericAndUnderscoreNamedTargets = /*lang=csharp*/
        """
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Collisions.IPair<int, string>), false)]
        [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Collisions.IPair_int__string_), false)]

        namespace Sample.HintNames.Collisions
        {
            public interface IPair<TFirst, TSecond>
            {
                TFirst First(TSecond second);
            }

            public interface IPair_int__string_
            {
                void Run();
            }
        }
        """;

    [Fact]
    public async Task GivenClosedGenericTargetAndTargetNamedLikeItsSanitizedName_WhenGenerated_ShouldGenerateBoth()
    {
        var hintNames = await GenerateHintNames(
            ClosedGenericAndUnderscoreNamedTargets,
            "ClosedGenericCollision"
        );

        hintNames.Distinct().Count().ShouldBe(2);
    }

    [Fact]
    public async Task GivenOpenGenericTargetAndTargetNamedLikeItsSanitizedName_WhenGenerated_ShouldGenerateBoth()
    {
        var hintNames = await GenerateHintNames(
            OpenGenericAndUnderscoreNamedTargets,
            "OpenGenericCollision"
        );

        hintNames.Distinct().Count().ShouldBe(2);
    }

    [Fact]
    public async Task GivenDedicatedNamespaceTargetsWithSameSanitizedName_WhenGenerated_ShouldGenerateBoth()
    {
        var hintNames = await GenerateHintNames(
            DedicatedNamespaceClosedGenericAndUnderscoreNamedTargets,
            "DedicatedCollision"
        );

        hintNames.Distinct().Count().ShouldBe(2);
    }

    [Fact]
    public async Task GivenTargetWithUnderscoreInItsName_WhenGenerated_ShouldAppendHashOfTargetName()
    {
        var hintNames = await GenerateHintNames( /*lang=csharp*/
            """
            [assembly: Imposter.Abstractions.GenerateImposter(typeof(Sample.HintNames.Legacy_Service))]

            namespace Sample.HintNames
            {
                public interface Legacy_Service
                {
                    void Run();
                }
            }
            """,
            "Underscore"
        );

        hintNames.ShouldBe(["Sample.HintNames.Legacy_ServiceImposter.a5af509b.g.cs"]);
    }

    private static async Task<string[]> GenerateHintNames(string source, string name)
    {
        var context = await CreateContext(source, name);

        return context.RunGenerator().GeneratedSources.Select(source => source.HintName).ToArray();
    }

    private static async Task<string[]> GenerateNamespaceByHintName(string source, string name)
    {
        var context = await CreateContext(source, name);

        return context
            .RunGenerator()
            .GeneratedSources.Select(source =>
                $"{source.HintName}: {source.SourceText.Lines.Select(line => line.ToString()).First(line => line.StartsWith("namespace "))}"
            )
            .ToArray();
    }

    private static Task<GeneratorTestContext> CreateContext(string source, string name) =>
        GeneratorTestHelper.CreateContext(
            source,
            baseSourceFileName: $"HintNames.{name}.Source.cs",
            snippetFileName: $"HintNames.{name}.Snippet.cs",
            assemblyName: $"{nameof(ImposterHintNameTests)}{name}",
            languageVersion: LanguageVersion.CSharp9
        );
}
