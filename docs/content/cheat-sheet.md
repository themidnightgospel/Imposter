# Cheat Sheet

Quick reference for common operations.

## Create Imposter

```csharp
// C# 14+
var imp = IMyService.Imposter();
var svc = imp.Instance();

// C# 8–13
var imp2 = new IMyServiceImposter();
var svc2 = imp2.Instance();
```

## Invocation Behavior

```csharp
var loose = new IMyServiceImposter(ImposterInvocationBehavior.Implicit);
var strict = new IMyServiceImposter(ImposterInvocationBehavior.Explicit);
```

## Methods

```csharp
// Return values
imp.GetNumber().Returns(42);
imp.GetNumber().Returns(() => 42);

// Sequencing
imp.GetNumber().Returns(1).Then().Returns(2).Then().Throws<InvalidOperationException>();

// Arguments
imp.Increment(Arg<int>.Any()).Returns(v => v + 1);
imp.Increment(Arg<int>.Is(x => x > 10)).Returns(100);
imp.Increment(5).Returns(50); // implicit Arg<int>

// Ref/Out/In
imp.GenericAllRefKind<int, string, double, bool, int>(OutArg<int>.Any(), Arg<string>.Any(), Arg<double>.Any(), Arg<bool[]>.Any())
   .Returns((out int o, ref string r, in double d, bool[] a) => { o = 5; return 99; })
   .Callback((out int o, ref string r, in double d, bool[] a) => { o = 5; });

// Async
imp.GetNumberAsync().ReturnsAsync(42);
imp.DoWorkAsync().Returns(Task.CompletedTask);

// Throw
imp.GetNumber().Throws<InvalidOperationException>();

// Verify
svc.Increment(1);
imp.Increment(Arg<int>.Any()).Called(Count.AtLeast(1));
```

## Properties

```csharp
imp.Age.Getter().Returns(33);
imp.Age.Setter(Arg<int>.Any()).Callback(v => { });
imp.Age.Setter(Arg<int>.Any()).Called(Count.Once());
```

## Indexers

```csharp
imp[Arg<int>.Is(k => k > 0)].Getter().Returns(10);
imp[Arg<int>.Any()].Setter().Callback((i, v) => { });
```

## Events

```csharp
EventHandler h = (s, e) => { };
svc.SomethingHappened += h;
imp.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Once());
imp.SomethingHappened.Raise(this, EventArgs.Empty);
```

## Base Implementation

```csharp
imp.DoWork(Arg<int>.Any()).UseBaseImplementation();
imp.Age.Getter().UseBaseImplementation();
```
