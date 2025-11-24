# Protected Methods

Overrideable protected members of the class can be impersonated just like other overridable class members.

Target type used in examples:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/ProtectedMethods/ProtectedMembersTests.cs#L5"}
    using Imposter.Abstractions;

    [assembly: GenerateImposter(typeof(Imposter.Tests.Docs.Methods.Protected.MyService))]

    namespace Imposter.Tests.Docs.Methods.Protected
    {
        public class MyService
        {
            protected virtual int ProtectedAdd(int value) => value * 2;
            public virtual int InvokeProtected(int value) => ProtectedAdd(value);
        }
    }
    ```

## Example

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/Docs/Methods/ProtectedMethods/ProtectedMembersTests.cs#L20"}
    // Class target with a protected virtual method and a public wrapper that calls it
    [assembly: GenerateImposter(typeof(MyService))]

    public class MyService
    {
        protected virtual int ProtectedAdd(int value) => value * 2;
        public virtual int InvokeProtected(int value) => ProtectedAdd(value);
    }

    var imposter = new MyServiceImposter();

    // Arrange the protected method directly on the imposter
    imposter.ProtectedAdd(Arg<int>.Is(5)).Returns(42);

    var service = imposter.Instance();
    service.InvokeProtected(5).ShouldBe(42);
    ```

See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/ClassImpersonation/ProtectedOverrideableMembersClassImposterTests.cs).
