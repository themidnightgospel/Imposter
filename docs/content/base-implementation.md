# Base Implementation

Forward calls from imposters to the original class implementation instead of configuring custom behavior.

!!! note
    Base Implementation is available for overrideable and non-abstract class members only.

## Methods

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/UseBaseImplementation/UseBaseImplementationTests.cs#L8"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(MyService))]

    public class MyService
    {
        public virtual int Add(int a, int b)
            => a + b;

        public virtual System.Threading.Tasks.Task ProcessAsync(string s)
            => System.Threading.Tasks.Task.CompletedTask;

        public virtual int MightFail(int v)
            => throw new System.InvalidOperationException("fail");
    }
    ```

### Basic example

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/UseBaseImplementation/UseBaseImplementationTests.cs#L23"}
    var imposter = new MyServiceImposter();
    var service = imposter.Instance();

    // Forward calls to the original implementation
    imposter.Add(Arg<int>.Any(), Arg<int>.Any()).UseBaseImplementation();

    service.Add(2, 5); // 7 (calls MyService.Add)
    ```

### With matchers and sequencing

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/UseBaseImplementation/UseBaseImplementationTests.cs#L35"}
    var imposter = new MyServiceImposter();
    var service = imposter.Instance();

    // Otherwise return a specific value (fallback)
    imposter.Add(Arg<int>.Any(), Arg<int>.Any()).Returns(-1);

    // Only forward when the first value is positive (more specific rule)
    imposter.Add(Arg<int>.Is(x => x > 0), Arg<int>.Any()).UseBaseImplementation();

    service.Add(2, 3);  // 5 (base)
    service.Add(-2, 3); // -1 (fallback)
    ```

Sequence with `Then()` if you need to call base once and then switch behavior:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/UseBaseImplementation/UseBaseImplementationTests.cs#L51"}
    var imposter = new MyServiceImposter();
    var service = imposter.Instance();

    imposter.Add(Arg<int>.Any(), Arg<int>.Any())
       .UseBaseImplementation()
       .Then()
       .Returns(100);

    service.Add(1, 1); // base -> 2
    service.Add(1, 1); // 100
    ```

### Async and exceptions

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/UseBaseImplementation/UseBaseImplementationTests.cs#L64"}
    var imposter = new MyServiceImposter();
    var service = imposter.Instance();

    // Async forwarding
    imposter.ProcessAsync(Arg<string>.Any()).UseBaseImplementation();
    await service.ProcessAsync("x"); // runs class implementation

    // Exceptions from base flow to the caller
    imposter.MightFail(Arg<int>.Any()).UseBaseImplementation();
    Should.Throw<InvalidOperationException>(() => service.MightFail(5));
    ```

## Properties

For class targets with virtual properties, you can forward getters and setters to the base implementation.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/PropertyUseBaseImplementationTests.cs#L17"}
    var imposter = new ClassWithVirtualPropertyImposter();
    imposter.VirtualProperty.Getter().UseBaseImplementation();

    var service = imposter.Instance();
    var value = service.VirtualProperty; // uses base implementation
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/PropertyUseBaseImplementationTests.cs#L57"}
    var imposter = new ClassWithVirtualPropertyImposter();
    imposter.VirtualProperty.Setter(Arg<string>.Any()).UseBaseImplementation();

    var service = imposter.Instance();
    service.VirtualProperty = "value"; // uses base implementation
    ```

This is useful when you want imposters to preserve class invariants or initialization logic defined in virtual properties.

## Indexers

Class targets with virtual indexers also support `UseBaseImplementation()` on getter and setter builders.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/GetterTests.cs#L18"}
    var imposter = new IIndexerSetupSutImposter();
    imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Getter().UseBaseImplementation();

    var service = imposter.Instance();
    var result = service[1, "key", new object()]; // uses base implementation
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImpersonation/SetterTests.cs#L17"}
    var imposter = new IIndexerSetupSutImposter();
    imposter[Arg<int>.Any(), Arg<string>.Any(), Arg<object>.Any()].Setter().UseBaseImplementation();

    var service = imposter.Instance();
    service[1, "key", new object()] = 42; // uses base implementation
    ```

Use this when a virtual indexer encapsulates non-trivial logic (e.g., backing collections or validation) and you want imposters to reuse it rather than reimplement it in tests.
