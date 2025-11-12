# Imposter Modes (Indexers)

How indexer reads and writes behave when you didn’t set them up.

## Implicit mode

Getter without a setup returns default values.

```csharp
var imposter = new IMyServiceImposter(ImposterMode.Implicit);
var service = imposter.Instance();

// Read without a setup => default(int) == 0
int v = service[123]; // 0

// Add a getter setup with a matcher
imposter[Arg<int>.Is(k => k > 0)].Getter().Returns(10);
v = service[123]; // 10

// Writes work without setups; verify writes
service[7] = 42;
imposter[Arg<int>.Is(7)].Setter().Called(Count.Once());
```

## Explicit mode

Getter without a setup throws so unintended reads are caught.

```csharp
var imposter = new IMyServiceImposter(ImposterMode.Explicit);
var service = imposter.Instance();

Assert.Throws<MissingImposterException>(() => { var _ = service[5]; });

imposter[Arg<int>.Any()].Getter().Returns(1);
var x = service[5]; // 1
```

## Calling real code on classes

Forward to the class’s own implementation for overridable indexers:

```csharp
imposter[Arg<int>.Any()].Getter().UseBaseImplementation();
imposter[Arg<int>.Any()].Setter().UseBaseImplementation();
```

See also: `tests/Imposter.Tests/Features/IndexerImposter/*`.
