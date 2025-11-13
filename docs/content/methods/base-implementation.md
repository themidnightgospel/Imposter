# Base Implementation (Methods)

Forward method calls to the class’s own implementation when generating imposters for classes.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/BaseImplementationTests.cs#L7"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.MyService))]

    public class MyService
    {
        public virtual int Add(int a, int b) => a + b;
        public virtual System.Threading.Tasks.Task ProcessAsync(string s) => System.Threading.Tasks.Task.CompletedTask;
        public virtual int MightFail(int v) => throw new System.InvalidOperationException("fail");
    }
    ```

## When it applies

- Class targets only. Interfaces don’t have implementations to forward to.
- The member must be overridable (e.g., `virtual`/`protected virtual`). Sealed or non-virtual members can’t be forwarded.
- Works in both modes:
  - Implicit: the base method runs if you select `UseBaseImplementation()`.
  - Explicit: the base method runs and no exception is thrown for that call.

## Basic example

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/BaseImplementationTests.cs#L27"}
// Given a class target with an overridable method
public class MyService
{
    public virtual int Add(int a, int b) => a + b;
}

[assembly: GenerateImposter(typeof(MyService))]

var imp = new MyServiceImposter();
var svc = imp.Instance();

// Forward calls to the original implementation
imp.Add(Arg<int>.Any(), Arg<int>.Any()).UseBaseImplementation();
var sum = svc.Add(2, 5); // 7 (calls MyService.Add)
    ```

## With matchers and sequencing

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/BaseImplementationTests.cs#L38"}
// Otherwise return a specific value (fallback)
imp.Add(Arg<int>.Any(), Arg<int>.Any())
   .Returns(-1);

// Only forward when the first value is positive (more specific rule)
imp.Add(Arg<int>.Is(x => x > 0), Arg<int>.Any()).UseBaseImplementation();

svc.Add(2, 3);  // 5 (base)
svc.Add(-2, 3); // -1
    ```

Sequence with `Then()` if you need to call base once and then switch behavior:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/BaseImplementationTests.cs#L54"}
imp.Add(Arg<int>.Any(), Arg<int>.Any())
   .UseBaseImplementation()
   .Then()
   .Returns(100);

svc.Add(1, 1); // base -> 2
svc.Add(1, 1); // 100
    ```

## Async and ref/out/in methods

- Async: forwarding respects the original `async` method (await the returned `Task`/`ValueTask`).
- Ref/out/in: signatures must match; you can still use `Arg<T>` and `OutArg<T>.Any()` in your setup and call base.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/BaseImplementationTests.cs#L70"}
imp.ProcessAsync(Arg<string>.Any()).UseBaseImplementation();
await svc.ProcessAsync("x"); // runs class implementation
    ```

## Exceptions from base

If the base method throws, the exception flows to the caller. You can verify invocations using `Called(Count.*)` in either mode.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/Methods/BaseImplementationTests.cs#L74"}
imp.MightFail(Arg<int>.Any()).UseBaseImplementation();
Assert.Throws<InvalidOperationException>(() => svc.MightFail(5));
    ```

See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Features/ClassImposter/ClassMethodBaseImplementationTests.cs).
