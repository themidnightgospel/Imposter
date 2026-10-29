using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.Properties;

public class SharedPropertyNameCollisionTests
{
    [Theory]
    [InlineData("PropertyValue")]
    [InlineData("PropertyGetter")]
    [InlineData("PropertySetter")]
    [InlineData("DefaultPropertyBehaviour")]
    [InlineData("SetForInit")]
    [InlineData("Returns")]
    [InlineData("Get")]
    [InlineData("Callback")]
    public async Task GivenTypeParameterNamedLikePropertyMachinery_WhenUsed_ShouldCompile(
        string name
    )
    {
        await AssertCompiles(
            "Sample.Target<>",
            $$"""
            public class Target<{{name}}>
            {
                public virtual {{name}} Value { get; set; }
                public virtual {{name}} Initial { get; init; }
            }
            """,
            """
            var imposter = new Sample.TargetImposter<int>();
            imposter.Value.Getter().Returns(1).Then().Returns(() => 2);
            imposter.Value.Setter(Arg<int>.Any()).Callback(_ => { });
            imposter.Initial.UseBaseImplementation();
            _ = imposter.Instance().Value;
            """,
            nameof(SharedPropertyNameCollisionTests) + name
        );
    }
}
