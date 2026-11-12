using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.IndexerImpersonation;

public class IndexerInvocationNameCollisionTests
{
    [Fact]
    public async Task GivenOptionalIndexerKey_WhenGeneratingEntryConstructors_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService",
            """
            public interface IService
            {
                int this[int key = 0] { get; set; }
            }
            """,
            """
            var imposter = new IServiceImposter();
            var target = imposter.Instance();
            target[0] = 42;
            _ = target[0];
            var setter = imposter[0].Setter();
            var getter = imposter[0].Getter();
            _ = setter.Invocations;
            _ = getter.Invocations;
            setter.Called(Count.Once());
            getter.Called(Count.Once());
            """,
            nameof(GivenOptionalIndexerKey_WhenGeneratingEntryConstructors_ShouldCompile)
        );
    }

    [Fact]
    public async Task GivenKeysNamedLikeInvocationStorage_WhenGeneratingHistoryAndDefaultStorage_ShouldCompile()
    {
        await AssertCompiles(
            "Sample.IService<>",
            """
            public interface IService<StoredKey>
            {
                StoredKey this[StoredKey StoredKey_1, int CompleteInvocation, int RecordException,
                    int Arguments, int _value, int _value_1, int Sequence, int Next,
                    int Member, int Result, int Value, int Equals, int GetHashCode,
                    int toString, int GetType] { get; set; }
            }
            """,
            """
            var imposter = new IServiceImposter<int>();
            var target = imposter.Instance();
            target[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15] = 42;
            _ = target[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
            var setter = imposter[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15].Setter();
            var getter = imposter[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15].Getter();
            _ = setter.Invocations;
            _ = getter.Invocations;
            setter.Called(Count.Once());
            getter.Called(Count.Once());
            imposter.ClearInvocations();
            _ = target[1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
            """,
            nameof(
                GivenKeysNamedLikeInvocationStorage_WhenGeneratingHistoryAndDefaultStorage_ShouldCompile
            )
        );
    }
}
