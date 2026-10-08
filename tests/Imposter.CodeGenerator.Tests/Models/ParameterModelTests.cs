using System.Linq;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Models;

public class ParameterModelTests
{
    private const string Source = /*lang=csharp*/
        """
        #nullable enable
        public enum Level { Below = -1, High = 2 }

        public interface IService
        {
            void Run<T>(
                ref int count,
                out string? name,
                in T item,
                decimal? amount = 1.5m,
                Level level = Level.Below,
                double ratio = double.NaN,
                string? label = null
            );
        }
        """;

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenParameterModelsAreCreated_ShouldBeEqual()
    {
        var first = CreateParameterModels(Source);

        var second = CreateParameterModels(Source);

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenChangedDefaultValue_WhenParameterModelsAreCreated_ShouldNotBeEqual()
    {
        var original = CreateParameterModels(Source);

        var changed = CreateParameterModels(Source.Replace("1.5m", "2.5m"));

        changed.ShouldNotBe(original);
    }

    private static ParameterModel[] CreateParameterModels(string source)
    {
        var compilation = CSharpCompilation.Create(
            "ParameterModels",
            [CSharpSyntaxTree.ParseText(source)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );
        var method = compilation
            .GetTypeByMetadataName("IService")!
            .GetMembers("Run")
            .OfType<IMethodSymbol>()
            .Single();

        return method.Parameters.Select(ParameterModel.From).ToArray();
    }
}
