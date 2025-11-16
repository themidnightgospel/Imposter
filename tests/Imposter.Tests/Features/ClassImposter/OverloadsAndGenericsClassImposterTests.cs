using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImposter
{
    public class OverloadsAndGenericsClassImposterTests
    {
        [Fact]
        public void GivenOverloadedMethods_WhenConfigured_ShouldMatchEachOverloadIndependently()
        {
            var imposter = new ClassWithOverloadsAndGenericsImposter();

            var intFormat = imposter.Format(Arg<int>.Is(5));
            var stringFormat = imposter.Format(Arg<string>.Is("abc"), Arg<int>.Is(4));

            intFormat.Returns("five");
            stringFormat.Returns("padded");

            var instance = imposter.Instance();

            instance.FormatInt(5).ShouldBe("five");
            instance.FormatString("abc", 4).ShouldBe("padded");

            Should.NotThrow(() => intFormat.Called(Count.Once()));
            Should.NotThrow(() => stringFormat.Called(Count.Once()));
        }

        [Fact]
        public void GivenGenericMethod_WhenConfigured_ShouldTrackVerificationByTypeArgument()
        {
            var imposter = new ClassWithOverloadsAndGenericsImposter();

            var stringEcho = imposter.Echo<string>(Arg<string>.Any());
            var personEcho = imposter.Echo<Person>(Arg<Person>.Any());

            stringEcho.Returns("override");
            personEcho.Returns(person => new Person("Updated"));

            var instance = imposter.Instance();

            instance.EchoValue("original").ShouldBe("override");
            var person = instance.EchoValue(new Person("Original"));
            person.Name.ShouldBe("Updated");

            Should.NotThrow(() => stringEcho.Called(Count.Once()));
            Should.NotThrow(() => personEcho.Called(Count.Once()));
        }

        [Fact]
        public void GivenMultiGenericMethod_WhenConfigured_ShouldTrackEachTypeCombination()
        {
            var imposter = new ClassWithOverloadsAndGenericsImposter();

            var stringIntSelect = imposter.SelectFirst<string, int>(
                Arg<string>.Any(),
                Arg<int>.Any()
            );
            var personGuidSelect = imposter.SelectFirst<Person, Guid>(
                Arg<Person>.Any(),
                Arg<Guid>.Any()
            );

            stringIntSelect.Returns((string first, int number) => $"{first}:{number}");
            personGuidSelect.Returns(
                (Person person, Guid id) =>
                {
                    var suffix = id.ToString("N")[..4];
                    return new Person($"{person.Name}-{suffix}");
                }
            );

            var instance = imposter.Instance();

            instance.SelectFirstValue("alpha", 3).ShouldBe("alpha:3");

            var guid = Guid.NewGuid();
            var originalPerson = new Person("beta");
            var expectedSuffix = guid.ToString("N")[..4];
            var updatedPerson = instance.SelectFirstValue(originalPerson, guid);
            updatedPerson.Name.ShouldBe($"{originalPerson.Name}-{expectedSuffix}");

            Should.NotThrow(() => stringIntSelect.Called(Count.Once()));
            Should.NotThrow(() => personGuidSelect.Called(Count.Once()));
        }

        private class Person
        {
            public Person(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }
    }
}
