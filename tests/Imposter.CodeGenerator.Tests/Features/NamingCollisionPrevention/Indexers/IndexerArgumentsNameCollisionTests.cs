using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Indexers;

// The indexer's arguments class keeps each parameter in a field of the same name, next to the names its Equals and
// GetHashCode declare.
public class IndexerArgumentsNameCollisionTests
{
    [Fact]
    public async Task GivenIndexerParameterNamedOther_WhenIndexerIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[int other] { get; set; } }",
            "imposter[Arg<int>.Any()].Getter().Returns(1); var instance = imposter.Instance(); instance[1] = instance[2];",
            nameof(IndexerArgumentsNameCollisionTests)
        );
    }

    [Fact]
    public async Task GivenIndexerParameterNamedHash_WhenIndexerIsUsed_ShouldCompile()
    {
        await AssertInterfaceCompiles(
            "public interface IService { int this[string hash] { get; set; } }",
            "imposter[Arg<string>.Any()].Getter().Returns(1); var instance = imposter.Instance(); instance[\"a\"] = instance[\"b\"];",
            nameof(IndexerArgumentsNameCollisionTests)
        );
    }
}
