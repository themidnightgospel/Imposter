* FAQ

- Why should generic type arguments in callbacks/returns/throws have to be `object` ?

 ```csharp
      public interface IGenericInterface<T>
      {
          T Value { get; }
      }
 ```

     * then setup

```csharp
      imposter
          .Print<GenericInterfaceImpl<ArgType.Any>>(
              Arg<GenericInterfaceImpl<ArgType.Any>>.Any,
              Arg<int>.Any)
          .Returns((GenericInterfaceImpl<ArgType.Any> input, int age) =>
          {
              return 1;
          });
```

        then invoked
```csharp
     imposter.Instance().Print(new GenericInterfaceImpl<int> { Value = 21 }, 1).ShouldBe(1)
```
     
now in order us to call `Returns` and pass `GenericInterfaceImpl<ArgType.Any> input` to it, we need to convert `GenericInterfaceImpl<int>` to `GenericInterfaceImpl<ArgType.Any>`  which is no possible mi amigo 

UPDATE: We no longer support .Any, `object` can be used to support the behaviour.


- Why method imposter uses `Stack` to store `_invocationSetups`
   Because newly added setups override existing ones, so LIFO container fits well.


- Why generic method invocation verification matches exact type match.

  example:
```csharp
            _sut.Instance().GenericSinlgeParam<IMammal, int>(mammal);
            _sut.Instance().GenericSinlgeParam<ICat, int>(cat);
            _sut.Instance().GenericSinlgeParam<ITiger, int>(tiger);
```

  then
  
```csharp
            _sut.GenericOutParam<IAnimal, int>(OutArg<IAnimal>.Any()).Called(1);
```

this will not match `GenericOutParam<Cat, int>` call, even though `Cat` is assignable to `IAnimal` reason is that, if we  do that then the following will not work

```csharp
            _sut.GenericOutParam<ICat, int>(OutArg<IAnimal>.Any()).Called(1);
```

Here we want to assert that `ICat` invocation was made exactly once. If assert were to match `ITiger` call (because `ITiger : ICat`), then we wouldn't be able to make this assertion.
Only way then would be to pass custom criteria so like `Arg<ICat>.Is(it => it is ITiger)`.

The reason why we might consider changing this behaviour and actually matching based on "IsAssigneableTo" is that that is how method setups works. So method setup for Tiger will match invocation made on Cat.



