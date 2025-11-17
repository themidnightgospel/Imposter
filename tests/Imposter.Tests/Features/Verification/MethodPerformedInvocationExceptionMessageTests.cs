using System;
using System.Linq;
using System.Text.RegularExpressions;
using Imposter.Abstractions;
using Shouldly;
using Xunit;
using static Imposter.Tests.Features.Verification.PerformedInvocationTestHelper;

namespace Imposter.Tests.Features.Verification
{
    public class MethodPerformedInvocationExceptionMessageTests
    {
        [Fact]
        public void GivenSimpleArgument_WhenVerificationFails_ShouldFormatValue()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var result = sut.Instance().IntSingleParam(42);

            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntSingleParam(Arg<int>.Any()).Called(Count.Never())
            );

            exception
                .ReadEntries()
                .ShouldBe(
                    new[]
                    {
                        MethodInvocation(
                            "IntSingleParam",
                            includeResult: true,
                            result,
                            ("age", 42)
                        ),
                    }
                );
        }

        [Fact]
        public void GivenNullArgument_WhenVerificationFails_ShouldIndicateNullValue()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var regex = new Regex("test");
            var result = sut.Instance().IntParams(20, null!, regex);

            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                    .Called(Count.Exactly(2))
            );

            exception
                .ReadEntries()
                .ShouldBe(
                    new[]
                    {
                        MethodInvocation(
                            "IntParams",
                            includeResult: true,
                            result,
                            ("age", 20),
                            ("name", null),
                            ("regex", regex)
                        ),
                    }
                );
        }

        [Fact]
        public void GivenStringArgumentWithSpecialCharacters_WhenVerificationFails_ShouldKeepRawValue()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var regex = new Regex("special");
            var result = sut.Instance().IntParams(10, "a b<c>", regex);

            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                    .Called(Count.Exactly(2))
            );

            exception
                .ReadEntries()
                .ShouldBe(
                    new[]
                    {
                        MethodInvocation(
                            "IntParams",
                            includeResult: true,
                            result,
                            ("age", 10),
                            ("name", "a b<c>"),
                            ("regex", regex)
                        ),
                    }
                );
        }

        [Fact]
        public void GivenComplexArgument_WhenVerificationFails_ShouldUseCustomToString()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var customValue = new CustomArgument(123);
            sut.Instance().GenericSingleParam(customValue);

            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.GenericSingleParam<CustomArgument>(Arg<CustomArgument>.Any())
                    .Called(Count.AtLeast(2))
            );

            exception
                .ReadEntries()
                .ShouldBe(new[] { MethodInvocation("GenericSingleParam", ("value", customValue)) });
        }

        [Fact]
        public void GivenParameterlessMethodWithTooFewCalls_WhenVerificationFails_ShouldListEachInvocation()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var firstResult = sut.Instance().IntNoParams();
            var secondResult = sut.Instance().IntNoParams();

            var expectedCount = Count.Exactly(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntNoParams().Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    MethodInvocation("IntNoParams", includeResult: true, secondResult),
                    MethodInvocation("IntNoParams", includeResult: true, firstResult),
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }

        [Fact]
        public void GivenMethodWithParameters_WhenAtMostVerificationFails_ShouldListAllArguments()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var firstResult = sut.Instance().IntSingleParam(1);
            var secondResult = sut.Instance().IntSingleParam(2);
            var thirdResult = sut.Instance().IntSingleParam(3);

            var expectedCount = Count.AtMost(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntSingleParam(Arg<int>.Any()).Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    MethodInvocation(
                        "IntSingleParam",
                        includeResult: true,
                        thirdResult,
                        ("age", 3)
                    ),
                    MethodInvocation(
                        "IntSingleParam",
                        includeResult: true,
                        secondResult,
                        ("age", 2)
                    ),
                    MethodInvocation(
                        "IntSingleParam",
                        includeResult: true,
                        firstResult,
                        ("age", 1)
                    ),
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 3);
        }

        [Fact]
        public void GivenPredicateVerification_WhenCountFails_ShouldListMatchingAndNonMatchingInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var firstResult = sut.Instance().IntSingleParam(5);
            var secondResult = sut.Instance().IntSingleParam(15);
            var thirdResult = sut.Instance().IntSingleParam(25);

            var expectedCount = Count.AtLeast(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntSingleParam(Arg<int>.Is(x => x > 10)).Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    MethodInvocation(
                        "IntSingleParam",
                        includeResult: true,
                        thirdResult,
                        ("age", 25)
                    ),
                    MethodInvocation(
                        "IntSingleParam",
                        includeResult: true,
                        secondResult,
                        ("age", 15)
                    ),
                    MethodInvocation(
                        "IntSingleParam",
                        includeResult: true,
                        firstResult,
                        ("age", 5)
                    ),
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }

        [Fact]
        public void GivenMethodWithMultipleParameters_WhenVerificationFails_ShouldCaptureEachParameter()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var firstRegex = new Regex("a");
            var firstResult = sut.Instance().IntParams(1, "alpha", firstRegex);
            var secondRegex = new Regex("b");
            var secondResult = sut.Instance().IntParams(2, "beta", secondRegex);

            var expectedCount = Count.Exactly(3);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntParams(Arg<int>.Any(), Arg<string>.Any(), Arg<Regex>.Any())
                    .Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    MethodInvocation(
                        "IntParams",
                        includeResult: true,
                        secondResult,
                        ("age", 2),
                        ("name", "beta"),
                        ("regex", secondRegex)
                    ),
                    MethodInvocation(
                        "IntParams",
                        includeResult: true,
                        firstResult,
                        ("age", 1),
                        ("name", "alpha"),
                        ("regex", firstRegex)
                    ),
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 2);
        }

        [Fact]
        public void GivenGenericMethodInvocations_WhenVerificationFails_ShouldDescribeEachTypeArgumentInvocation()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var intValue = 1;
            var stringValue = "value";
            var guidValue = Guid.Empty;
            sut.Instance().GenericSingleParam(intValue);
            sut.Instance().GenericSingleParam(stringValue);
            sut.Instance().GenericSingleParam(guidValue);

            var expectedCount = Count.Exactly(2);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.GenericSingleParam<int>(Arg<int>.Any()).Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            entries.ShouldBe(
                new[]
                {
                    MethodInvocation("GenericSingleParam", ("value", guidValue)),
                    MethodInvocation("GenericSingleParam", ("value", stringValue)),
                    MethodInvocation("GenericSingleParam", ("value", intValue)),
                }
            );
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }

        [Fact]
        public void GivenBetweenCountMismatch_WhenVerificationFails_ShouldKeepAllPerformedInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var result = sut.Instance().IntNoParams();

            var expectedCount = Count.Between(2, 4);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntNoParams().Called(expectedCount)
            );

            exception
                .ReadEntries()
                .ShouldBe(new[] { MethodInvocation("IntNoParams", includeResult: true, result) });
            exception.MessageShouldDescribeCounts(expectedCount, 1);
        }

        [Fact]
        public void GivenManyInvocations_WhenVerificationFails_ShouldListAllInvocations()
        {
            var sut = PerformedInvocationSutFactory.CreateMethodSut();
            var lastResult = 0;
            foreach (var _ in Enumerable.Range(0, 30))
            {
                lastResult = sut.Instance().IntNoParams();
            }

            var expectedCount = Count.Exactly(40);
            var exception = Should.Throw<VerificationFailedException>(() =>
                sut.IntNoParams().Called(expectedCount)
            );

            var entries = exception.ReadEntries();
            var expectedEntry = MethodInvocation("IntNoParams", includeResult: true, lastResult);
            entries.Length.ShouldBe(30);
            entries.ShouldAllBe(entry => entry == expectedEntry);
            exception.MessageShouldDescribeCounts(expectedCount, 30);
        }

        private static string MethodInvocation(
            string methodName,
            params (string Name, object? Value)[] args
        ) => MethodInvocation(methodName, includeResult: false, result: null, args);

        private static string MethodInvocation(
            string methodName,
            bool includeResult,
            object? result,
            params (string Name, object? Value)[] args
        )
        {
            var arguments =
                args.Length == 0
                    ? string.Empty
                    : string.Join(
                        ", ",
                        args.Select(arg => $"{arg.Name}: {FormatValue(arg.Value)}")
                    );
            var entry = $"{methodName}({arguments})";

            if (includeResult)
            {
                entry = $"{entry} => {FormatValue(result)}";
            }

            return entry;
        }

        private sealed class CustomArgument
        {
            private readonly int _value;

            public CustomArgument(int value)
            {
                _value = value;
            }

            public override string ToString() => $"MyType({_value})";
        }
    }
}
