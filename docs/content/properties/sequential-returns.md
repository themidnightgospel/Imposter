# Property Sequential Returns

Return a sequence of values on successive reads.

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImposter/ReturnTests.cs#L72"}
    imposter.Age.Getter()
        .Returns(10)
        .Then().Returns(20)
        .Then().Returns(30);

    var a = service.Age; // 10
    var b = service.Age; // 20
    var c = service.Age; // 30
    var d = service.Age; // 30 - last setup repeats
    ```

Combine sequencing with callbacks if you need to record read order or side effects:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/PropertyImposter/CallbackTests.cs#L68"}
    var firstSeen = 0;
    var secondSeen = 0;

    imposter.Age.Getter()
        .Returns(10)
        .Then().Returns(20)
        .Then().Returns(30)
        .Callback(() => firstSeen++)
        .Then()
        .Callback(() => secondSeen++);

    var a = service.Age; // 10, firstSeen == 1, secondSeen == 0
    var b = service.Age; // 20, firstSeen == 1, secondSeen == 1
    var c = service.Age; // 30, firstSeen == 2, secondSeen == 1
    ```
