# Indexer Impersonation

## Creating an imposter

Use the generated imposter type for your indexer-bearing interface or class and obtain the configured instance:

!!! example
    ```csharp
    [assembly: GenerateImposter(typeof(IMyServiceSut))]

    public interface IMyServiceSut
    {
        int this[int key1] { get; set; }
    }
    ```

## Getter

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImposter/GetterTests.cs#L103"}
    var imposter = IMyServiceSut.Imposter();
    imposter[Arg<int>.Is(k => k > 0)].Getter().Returns(10);

    var service = imposter.Instance();
    var value = service[123]; // 10
    ```

## Setter

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/IndexerImposter/SetterTests.cs#L147"}
    // Observe writes
    imposter[Arg<int>.Any()].Setter().Callback((key, value) => 
            { 
                // value is 50
            });

    var service = imposter.Instance();
    service[42] = 50;
    ```
