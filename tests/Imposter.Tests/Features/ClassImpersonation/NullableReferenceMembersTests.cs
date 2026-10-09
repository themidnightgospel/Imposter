using System.Threading.Tasks;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class NullableReferenceMembersTests
    {
        // A typed null picks the value overload of Returns; a bare null would also fit the delegate overload.
        private const string? NoValue = null;

        private readonly NullableReferenceMembersClassImposter _sut =
            new NullableReferenceMembersClassImposter();

        [Fact]
        public void GivenNullReturnValue_WhenInvoked_ShouldReturnNull()
        {
            _sut.Describe(Arg<string?>.Any()).Returns(NoValue);

            _sut.Instance().Describe("value").ShouldBeNull();
        }

        [Fact]
        public void GivenReturnsDelegateReturningNull_WhenInvoked_ShouldReturnNull()
        {
            _sut.Describe(Arg<string?>.Any()).Returns(value => null);

            _sut.Instance().Describe("value").ShouldBeNull();
        }

        [Fact]
        public void GivenBaseImplementation_WhenInvokedWithNull_ShouldReturnNull()
        {
            _sut.Describe(Arg<string?>.Any()).UseBaseImplementation();

            _sut.Instance().Describe(null).ShouldBeNull();
        }

        [Fact]
        public void GivenIndexerGetterReturningNull_WhenReadWithNullKey_ShouldReturnNull()
        {
            _sut[Arg<string?>.Any()].Getter().Returns(NoValue);

            _sut.Instance()[null].ShouldBeNull();
        }

        [Fact]
        public void GivenIndexerGetterBaseImplementation_WhenReadWithNullKey_ShouldReturnNull()
        {
            _sut[Arg<string?>.Any()].Getter().UseBaseImplementation();

            _sut.Instance()[null].ShouldBeNull();
        }

        [Fact]
        public void GivenPropertyGetterReturningNull_WhenRead_ShouldReturnNull()
        {
            _sut.Name.Getter().Returns(NoValue);

            _sut.Instance().Name.ShouldBeNull();
        }

        [Fact]
        public void GivenPropertyGetterGeneratorReturningNull_WhenRead_ShouldReturnNull()
        {
            _sut.Name.Getter().Returns(() => null);

            _sut.Instance().Name.ShouldBeNull();
        }

        [Fact]
        public void GivenInterfaceMethodReturningNull_WhenInvokedWithNull_ShouldReturnNull()
        {
            var imposter = new INullableReferenceMembersImposter();
            imposter.Find(Arg<string?>.Is(key => key == null)).Returns(NoValue);

            imposter.Instance().Find(null).ShouldBeNull();
        }

        [Fact]
        public async Task GivenAsyncInterfaceMethodReturningNull_WhenAwaited_ShouldReturnNull()
        {
            var imposter = new INullableReferenceMembersImposter();
            imposter.FindAsync(Arg<string?>.Any()).ReturnsAsync(NoValue);

            (await imposter.Instance().FindAsync("key")).ShouldBeNull();
        }
    }
}
