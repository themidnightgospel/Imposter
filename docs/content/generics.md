# Generics

Imposter supports generic methods, interfaces, classes, and fully open generic targets. Each closed generic combination gets its own setups and call history.

!!! note
    Generic support applies across methods, properties, indexers, and events. Verification and argument matching are always scoped to the concrete type arguments used at call site.

## Generic Methods

Generic methods (with their own `<T>` parameters) are treated like independent overloads per closed type argument combination. Each set of type arguments gets its own setup and verification.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/GenericMethodTargets.cs#L22"}
    using Imposter.Abstractions;
    using Imposter.Tests.Features.OpenGenericImposter;

    [assembly: GenerateImposter(typeof(IHaveGenericMethods))]

    namespace Imposter.Tests.Features.OpenGenericImposter
    {
        public interface IHaveGenericMethods
        {
            TResult GetValue<TResult>();
        }
    }
    ```

!!! example
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericGenericMethodImposterTests.cs#L113"}
        var imposter = IHaveGenericMethods.Imposter();
        imposter.GetValue<int>().Returns(5);
        imposter.GetValue<string>().Returns("value");

        var service = imposter.Instance();
        service.GetValue<int>().ShouldBe(5);
        service.GetValue<string>().ShouldBe("value");
        ```

    === "C# 9-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericGenericMethodImposterTests.cs#L113"}
        var imposter = new IHaveGenericMethodsImposter();
        imposter.GetValue<int>().Returns(5);
        imposter.GetValue<string>().Returns("value");

        var service = imposter.Instance();
        service.GetValue<int>().ShouldBe(5);
        service.GetValue<string>().ShouldBe("value");
        ```

## Generic Interfaces

Generic interfaces like `IOpenGenericMethodTarget<T>` get a distinct imposter per closed type argument. Each closed generic interface tracks its own setups, calls, and verification.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/IOpenGenericMethodTarget.cs#L1"}
    using Imposter.Abstractions;
    using Imposter.Tests.Features.OpenGenericImposter;

    [assembly: GenerateImposter(typeof(IOpenGenericMethodTarget<>))]

    namespace Imposter.Tests.Features.OpenGenericImposter
    {
        public interface IOpenGenericMethodTarget<T>
        {
            T GetNext();
        }
    }
    ```

!!! example
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericMethodImposterTests.cs#L60"}
        var imposter = IOpenGenericMethodTarget<string>.Imposter();
        imposter.GetNext().Returns("alpha").Then().Returns("beta");

        var service = imposter.Instance();
        service.GetNext().ShouldBe("alpha");
        service.GetNext().ShouldBe("beta");
        ```

    === "C# 9-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericMethodImposterTests.cs#L60"}
        var imposter = new IOpenGenericMethodTargetImposter<string>();
        imposter.GetNext().Returns("alpha").Then().Returns("beta");

        var service = imposter.Instance();
        service.GetNext().ShouldBe("alpha");
        service.GetNext().ShouldBe("beta");
        ```

## Generic Classes

Open generic classes with virtual members can be impersonated the same way as interfaces. You can configure methods, properties, indexers, and events on a closed generic imposter.



- For `ref` parameters, setups are matched by the exact static parameter type: a setup using a base type like `IAnimal` does not match a `ref` argument of a derived type like `Cat`, but a setup on `Dog` will match a `ref Dog` argument.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L235"}
    var animalCallback = false;

    _sut.GenericSingleRefParam<IAnimal>(Arg<IAnimal>.Any())
        .Callback((ref IAnimal _) => animalCallback = true);

    var cat = new Cat("mittens");

    _sut.Instance().GenericSingleRefParam(ref cat);

    animalCallback.ShouldBeFalse();
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L249"}
    var dogCallbackInvoked = false;

    _sut.GenericSingleRefParam<Dog>(Arg<Dog>.Any())
        .Callback((ref Dog _) => dogCallbackInvoked = true);

    var dog = new Dog("buddy");

    _sut.Instance().GenericSingleRefParam(ref dog);

    dogCallbackInvoked.ShouldBeTrue();
    ```

- Nullable generic arguments follow the same rules; a setup for `string?` can match both null and non-null values as long as the static type is `string?`.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L452"}
    _sut.GenericRefParam<string?, string?>(Arg<string?>.Any()).Returns((string?)null);

    var refValue = "test";

    var result = _sut.Instance().GenericRefParam<string?, string?>(ref refValue);

    result.ShouldBeNull();
    refValue.ShouldBe("test");
    ```

- Generic constraints (`where T : class`, `where TArg : IComparable<TArg>`, `where TStruct : struct`, `where TNew : new()`) only gate which type arguments compile; once they do, matching still follows the same rules.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/GenericMethodTargets.cs#L33"}
    using System;
    using Imposter.Abstractions;
    using Imposter.Tests.Features.OpenGenericImposter;

    [assembly: GenerateImposter(typeof(IGenericConstraintsTarget))]

    namespace Imposter.Tests.Features.OpenGenericImposter
    {
        public interface IGenericConstraintsTarget
        {
            void CompareValues<TArg>(TArg left, TArg right)
                where TArg : IComparable<TArg>;
        }
    }
    ```


## Open Generics

Open generics let you register a generic interface or class once (for example, `typeof(IAsyncObservable<>)`) and then create imposters for any concrete type you close it with at call site.

From a behaviour point of view:

- Each closed generic target gets its own imposter type and its own call tracking. An `IAsyncObservable<string>` imposter and an `IAsyncObservable<int>` imposter never share setups or call history.
- Verification is always scoped to both the concrete type argument and the imposter instance. `stringImposter.OnNext(Arg<string>.Any()).Called(...)` only counts calls made through instances created from `stringImposter`; calls made through `intImposter.Instance()` do not contribute.
- This isolation applies across all members on the open generic type: methods, properties, indexers, and events on `IAsyncObservable<string>` are independent from the same members on `IAsyncObservable<int>` (or any other `T`).
- If you only invoke `OnNext` on the `int` stream and then try to verify `OnNext` on the `string` imposter, verification will fail because there are no matching calls for that closed type.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/IAsyncObservable.cs#L1"}
    using Imposter.Abstractions;
    using Imposter.Tests.Features.OpenGenericImposter;

    [assembly: GenerateImposter(typeof(IAsyncObservable<>))]

    namespace Imposter.Tests.Features.OpenGenericImposter
    {
        public interface IAsyncObservable<T>
        {
            void OnNext(T item);
        }
    }
    ```

!!! example
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericMethodImposterTests.cs#L243"}
        var stringImposter = IAsyncObservable<string>.Imposter();
        var intImposter = IAsyncObservable<int>.Imposter();

        var stringStream = stringImposter.Instance();
        var intStream = intImposter.Instance();

        stringStream.OnNext("payload");
        stringStream.OnNext("another");
        intStream.OnNext(42);

        stringImposter.OnNext(Arg<string>.Any()).Called(Count.Exactly(2));
        intImposter.OnNext(Arg<int>.Any()).Called(Count.Once());
        ```

    === "C# 9-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericMethodImposterTests.cs#L243"}
        var stringImposter = new IAsyncObservableImposter<string>();
        var intImposter = new IAsyncObservableImposter<int>();

        var stringStream = stringImposter.Instance();
        var intStream = intImposter.Instance();

        stringStream.OnNext("payload");
        stringStream.OnNext("another");
        intStream.OnNext(42);

        stringImposter.OnNext(Arg<string>.Any()).Called(Count.Exactly(2));
        intImposter.OnNext(Arg<int>.Any()).Called(Count.Once());
        ```
