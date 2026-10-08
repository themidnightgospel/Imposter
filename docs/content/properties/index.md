# Property Impersonation

Configure getters and setters, verify writes, and forward to base implementations for class targets.

## Creating an imposter

Define the target interface and enable generation:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/IPropertySetupSut.cs#L1"}
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/ReturnTests.cs#L80"}
    imposter.Age.Getter().Returns(33);
    var value = service.Age; // 33

    // Sequencing
    imposter.Age.Getter().Returns(10).Then().Returns(20);
    var first = service.Age;  // 10
    var second = service.Age; // 20
    var third = service.Age;  // 20 (sequence exhausted)
    ```

## Setter

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/CallbackTests.cs#L17"}
    // Observe writes
    imposter.Age.Setter(Arg<int>.Any()).Callback(v => { /* side-effects */ });

    var service = imposter.Instance();
    service.Age = 10;
    service.Age = 11; // two writes in total

    // Verify writes
    imposter.Age.Setter(Arg<int>.Any()).Called(Count.AtLeast(2));
    imposter.Age.Setter(Arg<int>.Is(11)).Called(Count.Once());
    ```

## Init-only properties

Properties declared with `init` keep their init-only accessor in the generated implementation.
Configure reads with the usual getter API:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/InitOnlyPropertyTests.cs#L30"}
    imposter.Value.Getter().Returns(99);
    var value = imposter.Instance().Value; // 99
    ```

`Instance()` returns an already constructed object, so C# does not allow assigning its init-only
properties afterward. Getter setup is the way to provide a value for code that reads such a property;
it does not invoke the init accessor.

When an init accessor is invoked, it uses the same `Setter(...)` callbacks, verification, explicit-mode
checks, and default value storage as an ordinary setter. For virtual class properties,
`Setter(...).UseBaseImplementation()` forwards to the base init accessor, and the property-level
`UseBaseImplementation()` configures both available accessors. Callbacks run before base initialization;
if a callback throws, the base accessor is not invoked. Abstract and interface accessors do not expose
base delegation.

## Base Implementation

Forward to the base implementation for overridable class members:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/PropertyUseBaseImplementationTests.cs#L15"}
    imposter.Age.Getter().UseBaseImplementation();
    imposter.Age.Setter(Arg<int>.Any()).UseBaseImplementation();

    var service = imposter.Instance();
    var original = service.Age; // returns base value
    service.Age = 10;           // uses base setter
    ```
