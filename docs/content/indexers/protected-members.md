# Protected Members (Indexers)

Configure protected virtual indexers on class targets and access them through public wrappers.

## Getter/Setter with wrapper

```csharp
[assembly: GenerateImposter(typeof(MyService))]

public class MyService
{
    protected virtual int this[int index]
    {
        get => index;
        set { /* track */ }
    }

    public virtual int ReadProtected(int i) => this[i];
    public virtual void WriteProtected(int i, int v) => this[i] = v;
}

var imp = new MyServiceImposter();

// Arrange getter via matcher
imp[Arg<int>.Any()].Getter().Returns(i => i * 10);

// Forward wrappers to base so they call the protected indexer
imp.ReadProtected(Arg<int>.Any()).UseBaseImplementation();
imp.WriteProtected(Arg<int>.Any(), Arg<int>.Any()).UseBaseImplementation();

var svc = imp.Instance();
svc.ReadProtected(3); // 30

svc.WriteProtected(7, 123);
imp[Arg<int>.Is(7)].Setter().Called(Count.Once());
```

Notes
- Class targets only; indexer must be overridable.
- Wrapper methods must be `virtual` to be configurable on the imposter.
