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

When you impersonate generic members, Imposter matches calls using both the concrete type arguments and the runtime argument values.

From a user perspective this means:

- Each distinct set of method type arguments has its own setups, return sequences, and verification. Calls made with `TArg = string` never satisfy setups or verifications declared for `TArg = int`, even when the method name is the same.
- For generic methods on a generic interface or class, all type arguments are part of the matching key. `IOpenGenericWithMethodGenerics<string>.DoSomething<string>(...)` and `IOpenGenericWithMethodGenerics<string>.DoSomething<int>(...)` are tracked independently, and they are also isolated from `IOpenGenericWithMethodGenerics<int>.DoSomething<string>(...)`.
- Argument matchers remain strongly typed. A setup that uses `Arg<string>` only participates in matching for calls where the argument is a `string`; a setup using `Arg<int>` never matches a `string` argument.
- Generic constraints (`where T : class`, `where TArg : IComparable<TArg>`, `where TStruct : struct`, `where TNew : new()`) only restrict which type arguments you are allowed to call the method with. Once the call compiles, matching still happens purely on the concrete type arguments and argument values.
- For params arrays and array-typed arguments, Imposter looks at the resulting array type. A setup that uses `Arg<IAnimal[]>` can match a `params` call whose element type is `Cat` when `Cat : IAnimal`, because the runtime argument is a `Cat[]` that is assignable to `IAnimal[]` (see `GivenGenericParamsParamMethodSetup_WhenMethodIsInvokedWithDerivedType_ShouldReturnValue` in `tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs`, line 212 — https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L212).
- When you configure multiple setups for a generic method with different type combinations (for example, `GenericOutParam<string, int>`, `GenericOutParam<int, string>`, and `GenericOutParam<double, int>`), each closed combination is matched independently. A call to `GenericOutParam<string, int>` only ever uses the `<string, int>` setup and ignores the others (see `GivenMultipleGenericOutParamSetups_WhenMethodIsInvokedWithMatchingType_ShouldInvokeMatchingSetup`, line 418 — https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L418).
- In more complex signatures that mix `out`, `ref`, `in`, `params`, and multiple generic parameters (such as `GenericAllRefKind<TOut, TRef, TIn, TParams, TResult>`), Imposter still matches based on the concrete generic arguments and argument types, not on parameter position alone. You can configure a setup where the `out` generic type is a more specific implementation and then call the method with a more general `out` type (for example, setup with `TOut = Cat` and call with `TOut = IAnimal`), and the setup will be used as long as the actual values are assignable and the call compiles (see `GivenGenericAllRefKindMethodSetupWithComplexDelegate_WhenMethodIsInvoked_ShouldModifyAllParameters`, line 475 — https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L475).
- Nullable generic arguments are matched using the same rules: a setup such as `GenericRefParam<string?, string?>(Arg<string?>.Any()).Returns((string?)null);` applies both to `null` and non-null string values as long as the static type is `string?` (see `GivenGenericRefParamMethodSetupWithNullValue_WhenMethodIsInvoked_ShouldReturnNull`, line 452 — https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L452).

You can see these rules in the examples below:

- In the `IOpenGenericWithMethodGenerics<T>.DoSomething<TArg>` example, calling `service.DoSomething("alpha")` followed by `service.DoSomething(42)` on `IOpenGenericWithMethodGenerics<string>` produces two separate tracks of calls. The verification `imposter.DoSomething<string>(Arg<string>.Any()).Called(Count.Once())` only counts the `string` call, and `imposter.DoSomething<int>(Arg<int>.Any()).Called(Count.Once())` only counts the `int` call. If you only called `DoSomething(42)` and then verified `DoSomething<string>(...)`, the verification would fail because those calls live in different generic buckets.
- In the `IHaveGenericMethods.AddItem<TItem>` example, `var stringVerifier = imposter.AddItem<string>(Arg<string>.Is(s => s == "alpha"));` only observes `AddItem("alpha")` calls, while `var intVerifier = imposter.AddItem<int>(Arg<int>.Is(i => i == 7));` only observes `AddItem(7)` calls. Each verifier is scoped to its own `TItem`, so string calls do not satisfy the `int` verifier and vice versa.
- In the `IGenericConstraintsTarget.CompareValues<TArg>` example, the verifier `imposter.CompareValues<ComparablePayload>(Arg<ComparablePayload>.Is(left => left.Score == 5), Arg<ComparablePayload>.Any())` is satisfied only by calls where both arguments are `ComparablePayload` and the first argument matches the predicate. A call that uses a different `TArg`, or a `ComparablePayload` where `Score != 5`, does not count toward this verifier.
- For methods with multiple type arguments (such as a `Map<TFrom, TTo>` pattern), each closed pair `<TFrom, TTo>` is treated as its own lane. A configuration for `<string, int>` affects only `Map<string, int>(...)` calls, and a configuration for `<int, string>` affects only `Map<int, string>(...)`; the two do not share setups, results, or verification.

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
