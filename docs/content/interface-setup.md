# Interface-specific setup

When an interface hides a parent's member with `new`, use `For(default(TInterface))` to select which declaration to configure. Each view exposes the original member names, so you do not need to identify a member by a numbered suffix.

```csharp
public interface IBaseQueryNames
{
    int CallCount { get; }
}

public interface IHiddenQueryNames : IBaseQueryNames
{
    new int CallCount { get; }
    void Execute();
}
```

After generating an imposter for `IHiddenQueryNames`:

```csharp
var imposter = new IHiddenQueryNamesImposter();
var parent = imposter.For(default(IBaseQueryNames));
var child = imposter.For(default(IHiddenQueryNames));

parent.CallCount.Getter().Returns(1);
child.CallCount.Getter().Returns(2);

((IBaseQueryNames)imposter.Instance()).CallCount.ShouldBe(1);
imposter.Instance().CallCount.ShouldBe(2);
```

The argument selects an overload by its compile-time interface type. Its value is ignored. Use a typed expression such as `default(IBaseQueryNames)`; a bare `null` or `default` does not identify the intended interface. `For<TInterface>()` is not available because C# cannot select a different setup return type from that generic argument alone.

## Setup and verification

Views support method, property, event, and indexer setup using the existing builders. Parent views are inherited, so ordinary inherited members remain available through a child view. When inherited declarations are ambiguous, select their declaring interface explicitly.

```csharp
var execute = child.Execute();
imposter.Instance().Execute();
var before = execute.CallCount();

imposter.Instance().Execute();

(execute.CallCount() - before).ShouldBe(1);
execute.Called(Count.Exactly(2));
```

Selecting a view does not register a setup. Selecting a method through the view registers a setup just as selecting it directly on the imposter does. Retain the method builder for incremental count checks after configuring its behavior.

Views share the existing setup objects and invocation history. Existing members such as `CallCount_1` remain available for compatibility. Identical interface events continue to share their existing event builder.

## Generic interfaces and naming

The selector uses the complete interface type, including its generic arguments and namespace:

```csharp
imposter.For(default(IReader<int>)).Read().Returns(42);
imposter.For(default(IReader<string>)).Read().Returns("answer");
```

Only interfaces implemented by the generated interface target have selector overloads. Class targets do not expose these views. If `For` conflicts with an existing member or target type parameter, the generator allocates a unique selector name such as `For_1` to preserve the existing API. Generated setup type names may also need suffixes; callers can use `var` and the original member names shown above.

## Cost

The imposter implements the setup interfaces itself. Selecting a view returns the same object, without allocating a wrapper or performing reflection. The additional generated interfaces and forwarding members increase generated source size and can increase compilation time. Interface calls also add a dispatch step when selecting a setup; calls on `Instance()` continue using the existing invocation path.
