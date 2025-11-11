# Property Sequential Returns

Return a sequence of values on successive reads.

```csharp
imposter.Age.Getter()
    .Returns(10)
    .Then().Returns(20)
    .Then().Returns(30);

var a = service.Age; // 10
var b = service.Age; // 20
var c = service.Age; // 30
```

Combine sequencing with callbacks if you need to record read order.

