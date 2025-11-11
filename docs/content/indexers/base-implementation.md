# Indexer Base Implementation

Forward reads and writes to the base implementation for overridable class indexers.

```csharp
imposter[Arg<int>.Any()].Getter().UseBaseImplementation();
imposter[Arg<int>.Any()].Setter().UseBaseImplementation();
```

