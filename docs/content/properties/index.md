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

## Accessors with their own accessibility

A class property or indexer can restrict one accessor, as in `{ get; protected set; }`. The imposter
overrides each accessor with the accessibility it declares, so a `protected` setter is still impersonated
when the class itself assigns the property.

A `private` accessor can't be overridden, and neither can an `internal` one from an assembly that
doesn't grant yours `InternalsVisibleTo`. The imposter leaves such an accessor to the class: for
`{ get; private set; }` you can configure `Getter()`, but there is no `Setter(...)`.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/ClassImpersonation/RestrictedAccessorsClassImposterTests.cs#L16"}
    imposter.PrivateSetter.Getter().Returns(5);
    var value = imposter.Instance().PrivateSetter; // 5
    ```

## Span properties

A property of type `Span<T>` or `ReadOnlySpan<T>` can be impersonated. A span can't be kept, so the imposter keeps the elements in an array:

- `Getter().Returns` takes the array the returned span covers. Every read returns a span over that same array, so writes to a returned `Span<T>` land in it. A delegate passed to `Returns` returns an array too.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/SpanPropertyTests.cs#L23"}
    // Span<byte> Buffer { get; set; }
    var buffer = new byte[1];
    imposter.Buffer.Getter().Returns(buffer);

    imposter.Instance().Buffer[0] = 7; // buffer[0] is now 7
    ```

- `Setter(...)` matches the elements with `SpanArg<T>` or `ReadOnlySpanArg<T>` (see [Span parameters](../arguments-matching.md#span-parameters)), and callbacks get a copy of them as an array.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImpersonation/SpanPropertyTests.cs#L67"}
    byte[]? received = null;
    imposter.Buffer.Setter(SpanArg<byte>.Is(1, 2)).Callback(value => received = value);

    imposter.Instance().Buffer = new byte[] { 1, 2 }; // received is a copy: { 1, 2 }
    ```

- Without a setup, the property keeps a copy of the elements it's set to, and a read returns a span over that copy.
- With `UseBaseImplementation()`, the getter returns a copy of the base property's span, so writes to it don't reach the base class's memory.

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
