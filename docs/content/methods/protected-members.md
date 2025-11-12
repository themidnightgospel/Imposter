# Protected Methods

Overrideable protected members of the class can be mocked just like other overridable class members.

## Example

```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Docs/MethodCodeSnippetsTests.cs#L19"}
// Class target with a protected virtual method and a public wrapper that calls it
[assembly: GenerateImposter(typeof(MyService))]

public class MyService
{
    protected virtual int ProtectedAdd(int value) => value * 2;
    public virtual int InvokeProtected(int value) => ProtectedAdd(value);
}

var imp = new MyServiceImposter();

// Arrange the protected method directly on the imposter
imp.ProtectedAdd(Arg<int>.Is(5)).Returns(42);

// Forward the public wrapper to the real implementation so it calls the protected member
imp.InvokeProtected(Arg<int>.Any()).UseBaseImplementation();

var svc = imp.Instance();
svc.InvokeProtected(5).ShouldBe(42);
```

See more examples on [GitHub](https://github.com/themidnightgospel/Imposter/blob/main/tests/Imposter.Tests/Features/ClassImposter/ProtectedOverrideableMembersClassImposterTests.cs).
