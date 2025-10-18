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
