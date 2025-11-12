# Protected Properties

Configure protected virtual properties on class targets and read/write them through public wrappers.

## Getter/Setter with wrapper

```csharp
[assembly: GenerateImposter(typeof(MyService))]

public class MyService
{
    protected virtual int ProtectedAge { get; set; } = 7;
    public virtual int ReadProtected() => ProtectedAge;
    public virtual void WriteProtected(int value) => ProtectedAge = value;
}

var imp = new MyServiceImposter();

// Arrange getter
imp.ProtectedAge.Getter().Returns(33);

// Forward wrappers to call the protected accessors
imp.ReadProtected().UseBaseImplementation();
imp.WriteProtected(Arg<int>.Any()).UseBaseImplementation();

var svc = imp.Instance();
svc.ReadProtected(); // 33

svc.WriteProtected(10);
imp.ProtectedAge.Setter(Arg<int>.Is(10)).Called(Count.Once());
```

Notes
- Class targets only; property must be overridable.
- Wrapper methods used to call the protected property must be `virtual` to be configurable on the imposter.
- Verify reads/writes with `Called(Count.*)` using wrapper or direct property setup.
