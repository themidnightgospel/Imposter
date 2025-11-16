# Property Callbacks

Run side effects when a property is written or read.

## Setter callbacks

!!! example
    ```csharp
    imposter.Age.Setter(Arg<int>.Any()).Callback(v =>
    {
        // observe or react to writes
    });

    var service = imposter.Instance();
    service.Age = 10; // triggers callback
    ```

## Getter callbacks

!!! example
    ```csharp
    imposter.Age.Getter().Callback(() =>
    {
        // observe reads; combine with Returns/Then
    }).Returns(10);

    var service = imposter.Instance();
    var value = service.Age; // 10 and callback invoked
    ```

Tips
- Use `Arg<T>` to scope callbacks to specific values.
- Combine with `Called(Count.…)` for verification.
