using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Methods;

public class MethodInvocationPropertyCollisionTests
{
    [Theory]
    [InlineData("int")]
    [InlineData("System.Threading.Tasks.Task<int>")]
    [InlineData("System.Threading.Tasks.Task?")]
    [InlineData("System.Threading.Tasks.Task<int>?")]
    public async Task GivenArgumentsNamedLikeRecordMembers_WhenReadingTypedHistory_ShouldUseUnambiguousProperties(
        string resultType
    )
    {
        await AssertInterfaceCompiles(
            $$"""
            public interface IService
            {
                {{resultType}} Inspect(int toString, int equals, int getHashCode, int getType,
                    int referenceEquals, int memberwiseClone, int finalize, int formatValue,
                    int _result, int _exception, int _outcome, int _sequence);
            }
            """,
            """
            var invocation = imposter.Inspect(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12).Invocations[0];
            int a = invocation.ToString_1;
            int b = invocation.Equals_1;
            int c = invocation.GetHashCode_1;
            int d = invocation.GetType_1;
            int e = invocation.ReferenceEquals_1;
            int f = invocation.MemberwiseClone_1;
            int g = invocation.Finalize_1;
            int h = invocation.FormatValue_1;
            int i = invocation._result_1;
            int j = invocation._exception_1;
            int k = invocation._outcome_1;
            int l = invocation._sequence_1;
            _ = invocation.ToString();
            _ = invocation.GetHashCode();
            _ = invocation.GetType();
            _ = invocation.Equals(invocation);
            """,
            nameof(
                GivenArgumentsNamedLikeRecordMembers_WhenReadingTypedHistory_ShouldUseUnambiguousProperties
            ) + (resultType == "int" ? "Value" : "Task")
        );
    }
}
