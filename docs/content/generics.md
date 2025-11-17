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

## Generic types

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

## Type Matching


- For **input parameters**, setups declared on a generic argument type (like `Animal`) match calls made with either that type itself or any derived type (like `Cat`);

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L223"}
    public void GivenInputParameterSetup_WhenInvokedWithDerivedOrSameType_ShouldInvoke()
    {
        var capturedAnimals = new List<IAnimal>();

        _sut.GenericSingleParam<Animal>(Arg<Animal>.Any())
            .Callback(animal =>
            {
                capturedAnimals.Add(animal);
            });

        var animal = new Animal("mittens");
        _sut.Instance().GenericSingleParam(animal);
        
        var cat = new Cat("mittens");
        _sut.Instance().GenericSingleParam(cat);

        capturedAnimals.Count.ShouldBe(2);
        capturedAnimals[0].ShouldBe(animal);
        capturedAnimals[1].ShouldBe(cat);
    }
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L246"}
    public void GivenInputParameterSetup_WhenInvokedWithBase_ShouldNotInvoke()
    {
        Cat? capturedAnimal = null;

        _sut.GenericSingleParam<Cat>(Arg<Cat>.Any())
            .Callback(animal =>
            {
                capturedAnimal = animal;
            });

        var animal = new Animal("mittens");
        _sut.Instance().GenericSingleParam(animal);

        capturedAnimal.ShouldBeNull();
    }
    ```

- For **output parameters**, setups declared on a derived generic argument type (like `Cat`) can be invoked even when the method is called using a base type (like `IAnimal`) as the output parameter; the provided value is assigned and observed through the base-typed variable.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L263"}
    public void GivenOutputParameterSetupOnDerivedType_WhenMethodIsInvokedWithBaseType_ShouldInvoke()
    {
        Cat? providedCat = null;

        _sut.GenericSingleOutParam<Cat>(OutArg<Cat>.Any())
            .Callback((out Cat value) =>
            {
                providedCat = new Cat("mittens");
                value = providedCat;
            });

        _sut.Instance().GenericSingleOutParam<IAnimal>(out var impersonatedAnimal);

        providedCat.ShouldNotBeNull();
        impersonatedAnimal.ShouldBe(providedCat);
    }
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L281"}
    public void GivenOutputParameterSetupOnBase_WhenMethodIsInvokedWithDerivedType_ShouldNotInvoke()
    {
        var baseCallbackInvoked = false;

        _sut.GenericSingleOutParam<IAnimal>(OutArg<IAnimal>.Any())
            .Callback((out IAnimal value) =>
            {
                baseCallbackInvoked = true;
                value = new Animal("base");
            });

        _sut.Instance().GenericSingleOutParam<Cat>(out Cat? cat);

        baseCallbackInvoked.ShouldBeFalse();
        cat.ShouldBeNull();
    }
    ```

- For **ref parameters**, setups are matched by the exact static generic argument: a setup declared on a base type (like `IAnimal`) does not match when the method is invoked with a `ref` argument of a derived type (like `Cat`), but a setup declared on `Dog` matches a `ref Dog` argument.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L321"}
    public void GivenRefParameterSetupOnBase_WhenInvokedWithDerivedType_ShouldNotInvoke()
    {
        var animalCallback = false;

        _sut.GenericSingleRefParam<IAnimal>(Arg<IAnimal>.Any())
            .Callback((ref IAnimal _) => animalCallback = true);

        var cat = new Cat("mittens");
        _sut.Instance().GenericSingleRefParam(ref cat);

        animalCallback.ShouldBeFalse();
    }
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L335"}
    public void GivenRefParameterSetup_WhenInvokedWithSameType_ShouldInvoke()
    {
        var dogCallbackInvoked = false;

        _sut.GenericSingleRefParam<Dog>(Arg<Dog>.Any())
            .Callback((ref Dog _) => dogCallbackInvoked = true);

        var dog = new Dog("buddy");
        _sut.Instance().GenericSingleRefParam(ref dog);

        dogCallbackInvoked.ShouldBeTrue();
    }
    ```

- For **generic return types**, setups declared for a more specific derived type (like `Cat`) can be used when the method is invoked with a base return type (like `IAnimal`), but setups declared for a base type (like `IAnimal`) are not used when the method is invoked with a more specific derived return type (like `Cat`), so the call returns the default value instead.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L299"}
    public void GivenGenericReturnTypeSetupForDerived_WhenInvokedAsBase_ShouldReturnDerivedInstance()
    {
        var providedCat = new Cat("mittens");

        _sut.GenericReturnType<Cat>().Returns(providedCat);

        var impersonatedAnimal = _sut.Instance().GenericReturnType<IAnimal>();

        impersonatedAnimal.ShouldBe(providedCat);
    }
    ```

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/MethodImposter/ReturnValueSetupTests.cs#L311"}
    public void GivenGenericReturnTypeSetupForBase_WhenInvokedAsDerived_ShouldReturnDefault()
    {
        _sut.GenericReturnType<IAnimal>().Returns(new Animal("base"));

        var result = _sut.Instance().GenericReturnType<Cat>();

        result.ShouldBeNull();
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
