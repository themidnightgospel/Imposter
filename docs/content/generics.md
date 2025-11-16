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

Type arguments are part of the matching key. Imposter keeps setups and verification separate for each closed generic method or type parameter combination, even when the underlying method name is the same.

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

When you register open generic targets with `[assembly: GenerateImposter(typeof(...<>))]`, Imposter generates imposters for each closed combination you use. Call tracking is isolated per closed type so one `T` never satisfies verification for another.

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
