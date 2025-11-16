# Protected Indexers

Virtual and abstract protected indexers can be configured on the imposter.

## Getter/Setter with wrapper

!!! example
    ```csharp
    [assembly: GenerateImposter(typeof(MyService))]

    public class MyService
    {
        protected virtual int this[int index]
        {
            get => index;
            set { /* track */ }
        }

    }

    var imposter = new MyServiceImposter();
    imp[Arg<int>.Any()].Getter().Returns(i => i * 10);
    imp[Arg<int>.Any()].Getter().Callback((key, value) => { .. });
    ```