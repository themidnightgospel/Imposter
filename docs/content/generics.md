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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericClassTargets.cs#L1"}
    using Imposter.Abstractions;
    using Imposter.Tests.Features.OpenGenericImposter;

    [assembly: GenerateImposter(typeof(OpenGenericClass<>))]

    namespace Imposter.Tests.Features.OpenGenericImposter
    {
        public class OpenGenericClass<T>
            where T : class
        {
            public virtual T Echo(T value) => value;
        }
    }
    ```

!!! example
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericClassImposterTests.cs#L29"}
        var imposter = OpenGenericClass<string>.Imposter();
        imposter.Echo(Arg<string>.Any()).Returns("configured");

        var service = imposter.Instance();
        service.Echo("input").ShouldBe("configured");
        ```

    === "C# 9-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericClassImposterTests.cs#L29"}
        var imposter = new OpenGenericClassImposter<string>();
        imposter.Echo(Arg<string>.Any()).Returns("configured");

        var service = imposter.Instance();
        service.Echo("input").ShouldBe("configured");
        ```

## Type Matching

When you impersonate generic members, Imposter matches calls using both the concrete type arguments and the runtime argument values. In practice:

- Each closed set of method type arguments gets its own setups, return sequence, and verification; calls with `TArg = string` never satisfy setups for `TArg = int`.

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

- For generic methods on generic interfaces/classes, all type arguments are part of the key, so different `<TArg>` or `<TFrom, TTo>` combinations are tracked independently and never cross-satisfy.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/GenericMethodTargets.cs#L13"}
    using Imposter.Abstractions;
    using Imposter.Tests.Features.OpenGenericImposter;

    [assembly: GenerateImposter(typeof(IOpenGenericWithMethodGenerics<>))]

    namespace Imposter.Tests.Features.OpenGenericImposter
    {
        public interface IOpenGenericWithMethodGenerics<T>
        {
            void DoSomething<TArg>(TArg arg);
        }
    }
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericGenericMethodImposterTests.cs#L44"}
    var imposter = IOpenGenericWithMethodGenerics<string>.Imposter();
    var service = imposter.Instance();

    service.DoSomething("alpha");
    service.DoSomething(42);

    imposter.DoSomething<string>(Arg<string>.Any()).Called(Count.Once());
    imposter.DoSomething<int>(Arg<int>.Any()).Called(Count.Once());
    ```

- Argument matchers remain strongly typed; a setup using `Arg<string>` only matches `string` arguments, and `Arg<int>` never matches a `string`.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/GenericMethodTargets.cs#L22"}
    using Imposter.Abstractions;
    using Imposter.Tests.Features.OpenGenericImposter;

    [assembly: GenerateImposter(typeof(IHaveGenericMethods))]

    namespace Imposter.Tests.Features.OpenGenericImposter
    {
        public interface IHaveGenericMethods
        {
            void AddItem<TItem>(TItem item);
        }
    }
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericGenericMethodImposterTests.cs#L142"}
    var imposter = IHaveGenericMethods.Imposter();
    var stringVerifier = imposter.AddItem<string>(Arg<string>.Is(s => s == "alpha"));
    var intVerifier = imposter.AddItem<int>(Arg<int>.Is(i => i == 7));

    var service = imposter.Instance();
    service.AddItem("alpha");
    service.AddItem(7);

    stringVerifier.Called(Count.Once());
    intVerifier.Called(Count.Once());
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

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/OpenGenericImposter/OpenGenericGenericMethodImposterTests.cs#L193"}
    var imposter = IGenericConstraintsTarget.Imposter();
    var verifier = imposter.CompareValues<ComparablePayload>(
        Arg<ComparablePayload>.Is(left => left.Score == 5),
        Arg<ComparablePayload>.Any());

    var service = imposter.Instance();
    service.CompareValues(new ComparablePayload(5), new ComparablePayload(10));

    verifier.Called(Count.Once());
    ```

- Imposter uses normal C# assignability (inheritance and variance) when comparing setup parameter types to argument types.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L210"}
    _sut.GenericParamsParam<IAnimal, bool>(Arg<IAnimal[]>.Any()).Returns(true);

    var cats = new[] { new Cat("cat1"), new Cat("cat2") };

    var result = _sut.Instance().GenericParamsParam<Cat, bool>(cats);

    result.ShouldBe(true);
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L595"}
    _sut.GenericAllRefKind<Cat, Dog, IAnimal, IAnimal, bool>(
            OutArg<Cat>.Any(),
            Arg<Dog>.Any(),
            Arg<IAnimal>.Any(),
            Arg<IAnimal[]>.Any())
        .Returns(true);

    var refAnimal = new Dog("ref-dog");
    var inAnimal = new Tiger("in-tiger");
    var paramsAnimals = new Tiger[] { new Tiger("tiger1") };

    var result = _sut.Instance().GenericAllRefKind(ref refAnimal, inAnimal, paramsAnimals);

    result.ShouldBe(true);
    ```

- Multiple setups for different generic combinations on the same method are matched independently; a call only ever uses the setup for its exact closed generic pair.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L418"}
    _sut.GenericOutParam<string, int>(OutArg<string>.Any()).Returns(123);
    _sut.GenericOutParam<int, string>(OutArg<int>.Any()).Returns("hello");
    _sut.GenericOutParam<double, int>(OutArg<double>.Any()).Returns(321);

    var result = _sut.Instance().GenericOutParam<string, int>(out var outValue);

    result.ShouldBe(123);
    outValue.ShouldBe(default);
    ```

- Complex signatures that mix `out`, `ref`, `in`, `params`, and multiple generic parameters still match purely on the concrete generic arguments and argument types, not parameter position alone.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L475"}
    _sut.GenericAllRefKind<Cat, IAnimal, Dog, IAnimal, string>(
            OutArg<Cat>.Any(),
            Arg<IAnimal>.Any(),
            Arg<Dog>.Any(),
            Arg<IAnimal[]>.Any())
        .Returns(
            (out Cat outValue,
             ref IAnimal refValue,
             in Dog inValue,
             IAnimal[] paramsValues) =>
            {
                outValue = new Cat("out-cat");
                refValue = new Dog("ref-dog");
                paramsValues[0] = new Tiger("tiger-from-params");
                return "updated";
            });

    var refAnimal = (IAnimal)new Dog("initial-ref");
    var inAnimal = new Dog("in-dog");
    var paramsAnimals = new IAnimal[] { new Tiger("initial-tiger") };

    var result = _sut.Instance().GenericAllRefKind(ref refAnimal, inAnimal, paramsAnimals);

    result.ShouldBe("updated");
    ```

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
