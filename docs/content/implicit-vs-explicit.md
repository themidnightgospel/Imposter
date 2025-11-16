# Implicit vs Explicit Modes

How `ImposterMode` affects methods, properties, and indexers when no setup exists.

## Methods

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/ImposterModes/ImposterModesTests.cs#L8"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(IMyService))]

    public interface IMyService
    {
        int GetNumber();
        System.Threading.Tasks.Task<int> GetNumberAsync();
    }
    ```

### Implicit mode (methods)

Methods without setups are implicitly stubbed and return `default(T)`.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/ImposterModes/ImposterModesTests.cs#L20"}
    var imposter = new IMyServiceImposter(ImposterMode.Implicit);
    var service = imposter.Instance();

    // Method without a setup => default(int) == 0
    int n = service.GetNumber(); // 0

    // Add a setup and the call returns your value
    imposter.GetNumber().Returns(42);
    service.GetNumber(); // 42

    // Async methods
    imposter.GetNumberAsync().ReturnsAsync(7);
    var v = await service.GetNumberAsync(); // 7
    ```

### Explicit mode (methods)

Missing setups throw an exception so unintended calls are caught.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/ImposterModes/ImposterModesTests.cs#L38"}
    var imposter = new IMyServiceImposter(ImposterMode.Explicit);
    var service = imposter.Instance();

    // No setup -> throws MissingImposterException
    Should.Throw<MissingImposterException>(() => service.GetNumber());

    // Add a setup -> call succeeds
    imposter.GetNumber().Returns(42);
    service.GetNumber(); // 42
    ```

!!! tip "Pro tip"
    Default to `Explicit` in unit tests to catch missing setups early. Use `Implicit` in spike/prototyping code where default results are acceptable.

## Properties

In Implicit mode, property getters without setups return `default(T)` and setters do nothing. In Explicit mode, missing setups throw `MissingImposterException`.

!!! example
    ```csharp
    var imposter = new IPropertySetupSutImposter(ImposterMode.Implicit);
    var sut = imposter.Instance();

    // No getter setup => default(int) == 0
    int n = sut.Age; // 0

    // No setter setup => call is ignored
    sut.Age = 10;
    ```

!!! example
    ```csharp
    var imposter = new IPropertySetupSutImposter(ImposterMode.Explicit);
    var sut = imposter.Instance();

    // No getter setup -> MissingImposterException
    // sut.Age; // throws

    // No setter setup -> MissingImposterException
    // sut.Age = 10; // throws
    ```

## Indexers

Indexers follow the same rules: Implicit mode returns defaults / does nothing; Explicit mode throws when no setup matches.

!!! example
    ```csharp
    var imposter = new IIndexerSetupSutImposter(ImposterMode.Implicit);
    var sut = imposter.Instance();

    // No setup => default(int) == 0
    int value = sut[1, "key"]; // 0

    // Setter without setup is ignored
    sut[1, "key"] = 10;
    ```

!!! example
    ```csharp
    var imposter = new IIndexerSetupSutImposter(ImposterMode.Explicit);
    var sut = imposter.Instance();

    // No getter setup -> MissingImposterException
    // var value = sut[1, "missing"]; // throws

    // No setter setup -> MissingImposterException
    // sut[1, "missing"] = 10; // throws
    ```

