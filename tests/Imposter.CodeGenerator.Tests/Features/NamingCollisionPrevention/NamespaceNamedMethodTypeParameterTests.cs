using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

// A generic method's adapter and setup criteria write the method's types with their own type parameters in place of the
// method's, which must leave a namespace, or the global alias, of the same name as it is.
public class NamespaceNamedMethodTypeParameterTests
{
    [Theory]
    [InlineData("global")]
    [InlineData("System")]
    [InlineData("Collections")]
    [InlineData("Generic")]
    [InlineData("Threading")]
    [InlineData("Tasks")]
    [InlineData("Imposter")]
    [InlineData("Abstractions")]
    [InlineData("Sample")]
    public async Task GivenMethodTypeParameterNamedLikeAPartOfAQualifiedName_WhenImposterIsUsed_ShouldCompile(
        string name
    )
    {
        await AssertInterfaceCompiles(
            $"public sealed class Item {{ }} public interface IService {{ {name} Get<{name}>({name} value, global::System.Collections.Generic.List<{name}> items, global::Sample.Item item); global::System.Threading.Tasks.Task<{name}> GetAsync<{name}>({name} value); void Put<{name}>(ref {name} item, out {name} result); }}",
            "imposter.Get<string>(Arg<string>.Any(), Arg<System.Collections.Generic.List<string>>.Any(), Arg<Item>.Any()).Returns(\"a\"); _ = imposter.Instance().Get(\"b\", new System.Collections.Generic.List<string>(), new Item());",
            $"{nameof(GivenMethodTypeParameterNamedLikeAPartOfAQualifiedName_WhenImposterIsUsed_ShouldCompile)}_{name}"
        );
    }
}
