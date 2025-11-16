# Protected Properties

Virtual and abstract protected properties can be configured on the imposter.

## Getter/Setter with wrapper

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/Properties/ProtectedMembersTests.cs#L5"}
    [assembly: GenerateImposter(typeof(MyService))]

    public class MyService
    {
        protected virtual int ProtectedAge { get; set; } = 7;
        public virtual int ReadProtected() => ProtectedAge;
        public virtual void WriteProtected(int value) => ProtectedAge = value;
    }

    var imposter = new MyServiceImposter();

    // Arrange getter
    imposter.ProtectedAge.Getter().Returns(33);
    imposter.ProtectedAge.Setter(Arg<int>.Is(10)).Called(Count.Once());
    ```
