using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Events;

// The event builder records raises and handler invocations in tuples whose elements are named after the delegate's
// parameters, next to a Handler element.
public class EventHistoryTupleNameCollisionTests
{
    [Fact]
    public async Task GivenDelegateParameterNamedHandler_WhenEventIsRaisedAndVerified_ShouldCompile()
    {
        await AssertEventCompiles(
            "int Handler, int other",
            "1, 2",
            "Arg<int>.Any(), Arg<int>.Any()"
        );
    }

    [Fact]
    public async Task GivenDelegateParameterNamedItem1_WhenEventIsRaisedAndVerified_ShouldCompile()
    {
        await AssertEventCompiles("int Item1, int other", "1, 2", "Arg<int>.Any(), Arg<int>.Any()");
    }

    [Fact]
    public async Task GivenDelegateParameterNamedLikeItsHistoryPositionButNotItsHandlerPosition_WhenEventIsRaisedAndVerified_ShouldCompile()
    {
        await AssertEventCompiles("int other, int Item2", "1, 2", "Arg<int>.Any(), Arg<int>.Any()");
    }

    [Fact]
    public async Task GivenSingleDelegateParameterNamedItem1_WhenEventIsRaisedAndVerified_ShouldCompile()
    {
        await AssertEventCompiles("int Item1", "1", "Arg<int>.Any()");
    }

    [Fact]
    public async Task GivenDelegateParametersNamedRestAndEquals_WhenEventIsRaisedAndVerified_ShouldCompile()
    {
        await AssertEventCompiles("int Rest, int Equals", "1, 2", "Arg<int>.Any(), Arg<int>.Any()");
    }

    private static Task AssertEventCompiles(
        string delegateParameters,
        string raiseArguments,
        string raisedCriteria
    ) =>
        AssertInterfaceCompiles(
            $"public delegate void Notify({delegateParameters}); public interface IService {{ event Notify Happened; }}",
            "imposter.Instance().Happened += delegate { }; "
                + $"imposter.Happened.Raise({raiseArguments}); "
                + $"imposter.Happened.Raised({raisedCriteria}, Count.Once()); "
                + "imposter.Happened.HandlerInvoked(Arg<Sample.Notify>.Any(), Count.Once());",
            nameof(EventHistoryTupleNameCollisionTests)
        );
}
