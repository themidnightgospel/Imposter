internal static class RenameProbes
{
    internal static IEnumerable<(string Name, string Source)> All
    {
        get
        {
            foreach (var keyword in new[] { "var", "_", "record", "partial" })
                yield return Probe(
                    $"keyword-{keyword}",
                    "Sample.IService<>",
                    $"public interface IService<@{keyword}> {{ @{keyword} Get(@{keyword} value); @{keyword} Current {{ get; set; }} event System.Action<@{keyword}> Changed; @{keyword} this[int key] {{ get; }} }}"
                );
            yield return Probe(
                "method-keywords",
                "Sample.IService",
                "public interface IService { @var Get<@var>(@var value); _ Put<_>(_ value); record Read<record>(record value); partial Write<partial>(partial value); }"
            );
            yield return Probe(
                "method-helper-members",
                "Sample.IService",
                "public interface IService { TResult Get<TResult>(TResult value); Arguments Convert<Arguments>(Arguments value); }"
            );
            yield return Probe(
                "suffix-collisions",
                "Sample.IService<,>",
                "public interface IService<@var, var_1> { @var Get(var_1 value, int var_2); int var_3 { get; } }"
            );
            yield return Probe(
                "imposter-class-name",
                "Sample.IService<>",
                "public interface IService<IServiceImposter> { IServiceImposter Get(IServiceImposter value); }"
            );
            yield return Probe(
                "containing-imposter-name",
                "Sample.Outer<>.IInner",
                "public class Outer<IInnerImposter> { public interface IInner { IInnerImposter Get(IInnerImposter value); } }"
            );
            yield return Probe(
                "nested-shadowing",
                "Sample.Outer<>.IInner<>",
                "public class Outer<T> { public interface IInner<T> { T Get(T value); } }"
            );
            yield return Probe(
                "constructed-containing-type",
                "Sample.Outer<string>.IInner<int>",
                "public class Outer<@var> { public interface IInner<@record> { @var Get(@record value); } }"
            );
            yield return Probe(
                "inheritance-and-constraints",
                "Sample.IService<,>",
                "public interface IBase<T> { T Get(T value); } public interface IService<IServiceImposter, U> : IBase<IServiceImposter> where IServiceImposter : class where U : IServiceImposter { V Convert<V>(U value) where V : IServiceImposter; }"
            );
            yield return Probe(
                "arrays-tuples-nullability",
                "Sample.IService<>",
                "#nullable enable\npublic interface IService<@var> where @var : class { System.Collections.Generic.List<(@var? Item, @var[,] Values)>[] Get(@var?[] value); }"
            );
            yield return Probe(
                "class-constructor-and-method",
                "Sample.Service<>",
                "public class Service<@var> { public Service(@var var_1) { } public virtual @var Get(@var value) => value; public virtual @record Convert<@record>(@var value) => default!; }"
            );
        }
    }

    private static (string Name, string Source) Probe(
        string name,
        string target,
        string declaration
    ) =>
        (
            name,
            $"using Imposter.Abstractions;\n[assembly: GenerateImposter(typeof({target}))]\nnamespace Sample {{\n{declaration}\n}}"
        );
}
