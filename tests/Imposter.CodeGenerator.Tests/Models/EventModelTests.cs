using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Models;

public class EventModelTests
{
    private const string Source = /*lang=csharp*/
        """
        #nullable enable
        using System.Threading.Tasks;

        public delegate ValueTask Changed(object? sender, in int value);

        public abstract class Service
        {
            public abstract event Changed? ValueChanged;
        }
        """;

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenEventModelsAreCreated_ShouldBeEqual()
    {
        var first = CreateEventModel(Source);

        var second = CreateEventModel(Source);

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenChangedDelegateParameter_WhenEventModelsAreCreated_ShouldNotBeEqual()
    {
        var original = CreateEventModel(Source);

        var changed = CreateEventModel(Source.Replace("in int value", "long value"));

        changed.ShouldNotBe(original);
    }

    private static EventModel CreateEventModel(string source)
    {
        var compilation = CSharpCompilation.Create(
            "EventModels",
            [CSharpSyntaxTree.ParseText(source)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );
        var @event = compilation
            .GetTypeByMetadataName("Service")!
            .GetMembers("ValueChanged")
            .OfType<IEventSymbol>()
            .Single();

        return EventModel.From(@event, new MemberAccess(compilation.Assembly));
    }
}
