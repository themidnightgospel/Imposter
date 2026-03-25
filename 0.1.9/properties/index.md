# Property Impersonation

Configure getters and setters, verify writes, and forward to base implementations for class targets.

## Creating an imposter

Define the target interface and enable generation:

Example

```
using Imposter.Abstractions;
using Imposter.Tests.Features.PropertyImpersonation;

[assembly: GenerateImposter(typeof(IPropertySetupSut))]

public interface IPropertySetupSut
{
    int Age { get; set; }

    int Name { get; }

    int LastName { set; }
}
```

## Getter

Example

```
imposter.Age.Getter().Returns(33);
var value = service.Age; // 33

// Sequencing
imposter.Age.Getter().Returns(10).Then().Returns(20);
var first = service.Age;  // 10
var second = service.Age; // 20
var third = service.Age;  // 20 (sequence exhausted)
```

## Setter

Example

```
// Observe writes
imposter.Age.Setter(Arg<int>.Any()).Callback(v => { /* side-effects */ });

var service = imposter.Instance();
service.Age = 10;
service.Age = 11; // two writes in total

// Verify writes
imposter.Age.Setter(Arg<int>.Any()).Called(Count.AtLeast(2));
imposter.Age.Setter(Arg<int>.Is(11)).Called(Count.Once());
```

## Base Implementation

Forward to the base implementation for overridable class members:

Example

```
imposter.Age.Getter().UseBaseImplementation();
imposter.Age.Setter(Arg<int>.Any()).UseBaseImplementation();

var service = imposter.Instance();
var original = service.Age; // returns base value
service.Age = 10;           // uses base setter
```
