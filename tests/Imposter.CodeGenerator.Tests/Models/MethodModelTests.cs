using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Models;

public class MethodModelTests
{
    private const string Source = /*lang=csharp*/
        """
        #nullable enable
        using System.Collections.Generic;
        using System.Threading.Tasks;

        public abstract class Service<TKey>
        {
            protected internal abstract Task<IList<TValue>> LoadAsync<TValue>(TKey key, TValue? fallback = default)
                where TValue : class?, new();
        }
        """;

    [Fact]
    public void GivenSameSourceInTwoCompilations_WhenMethodModelsAreCreated_ShouldBeEqual()
    {
        var first = CreateMethodModel(Source);

        var second = CreateMethodModel(Source);

        second.ShouldBe(first);
    }

    [Fact]
    public void GivenChangedConstraint_WhenMethodModelsAreCreated_ShouldNotBeEqual()
    {
        var original = CreateMethodModel(Source);

        var changed = CreateMethodModel(Source.Replace("class?, new()", "class?"));

        changed.ShouldNotBe(original);
    }

    private static MethodModel CreateMethodModel(string source)
    {
        var compilation = CSharpCompilation.Create(
            "MethodModels",
            [CSharpSyntaxTree.ParseText(source)],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)]
        );
        var method = compilation
            .GetTypeByMetadataName("Service`1")!
            .GetMembers("LoadAsync")
            .OfType<IMethodSymbol>()
            .Single();

        return MethodModel.From(
            method,
            new MemberAccess(compilation.Assembly),
            hasRefKindOverload: false
        );
    }
}
