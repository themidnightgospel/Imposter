using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.MethodImpersonation
{
    public class RefStructParameterTests
    {
        private readonly IRefStructParameterSutImposter _sut = new IRefStructParameterSutImposter();

        [Fact]
        public void GivenReturnsDelegate_WhenInvoked_ShouldPassItTheRefStruct()
        {
            _sut.Read(Arg<int>.Any()).Returns((id, cursor) => id + cursor.Position);

            _sut.Instance().Read(1, new Cursor(2)).ShouldBe(3);
        }

        [Fact]
        public void GivenSetupForTheOtherArguments_WhenInvoked_ShouldMatchWhateverTheRefStruct()
        {
            _sut.Read(Arg<int>.Is(1)).Returns(10);

            _sut.Instance().Read(1, new Cursor(5)).ShouldBe(10);
            _sut.Instance().Read(2, new Cursor(5)).ShouldBe(0);
        }

        [Fact]
        public void GivenCallback_WhenInvoked_ShouldPassItTheRefStruct()
        {
            var seen = 0;
            _sut.Read(Arg<int>.Any()).Callback((id, cursor) => seen = cursor.Position);

            _sut.Instance().Read(1, new Cursor(4));

            seen.ShouldBe(4);
        }

        [Fact]
        public void GivenInvocations_WhenVerified_ShouldCountThemByTheOtherArguments()
        {
            _sut.Instance().Read(1, new Cursor(1));
            _sut.Instance().Read(1, new Cursor(2));
            _sut.Instance().Read(2, new Cursor(1));

            _sut.Read(Arg<int>.Is(1)).Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenCallbackThatChangesTheRefStruct_WhenInvoked_ShouldChangeTheCallersValue()
        {
            _sut.Advance().Callback((ref Cursor cursor) => cursor.Position++);
            var cursor = new Cursor(1);

            _sut.Instance().Advance(ref cursor);

            cursor.Position.ShouldBe(2);
        }

        [Fact]
        public void GivenReturnsDelegateThatAssignsTheOutRefStruct_WhenInvoked_ShouldHandItToTheCaller()
        {
            _sut.TryOpen()
                .Returns(
                    (out Cursor cursor) =>
                    {
                        cursor = new Cursor(7);
                        return true;
                    }
                );

            _sut.Instance().TryOpen(out var cursor).ShouldBeTrue();

            cursor.Position.ShouldBe(7);
        }

        [Fact]
        public void GivenNoSetup_WhenInvokedWithAnOutRefStruct_ShouldAssignTheDefault()
        {
            _sut.Instance().TryOpen(out var cursor);

            cursor.Position.ShouldBe(0);
        }

        [Fact]
        public void GivenSetupsThroughEachInterfacesView_WhenEachOverloadIsInvoked_ShouldUseItsOwnSetup()
        {
            var imposter = new IRefStructOverloadDerivedImposter();
            imposter.For(default(IRefStructOverloadBase)!).Read(Arg<int>.Any()).Returns(1);
            imposter.For(default(IRefStructOverloadDerived)!).Read(Arg<int>.Any()).Returns(2);
            IRefStructOverloadDerived instance = imposter.Instance();

            ((IRefStructOverloadBase)instance).Read(5).ShouldBe(1);
            instance.Read(5, new Cursor(1)).ShouldBe(2);
        }

        [Fact]
        public void GivenUseBaseImplementation_WhenClassMethodIsInvoked_ShouldPassTheRefStructToIt()
        {
            var imposter = new RefStructParameterClassImposter();
            imposter.Read().UseBaseImplementation();

            imposter.Instance().Read(new Cursor(4)).ShouldBe(4);
        }
    }
}
