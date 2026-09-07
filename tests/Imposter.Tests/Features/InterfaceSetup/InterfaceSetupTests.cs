using System;
using Imposter.Abstractions;
using Imposter.Tests.Features.InterfaceSetup;
using Shouldly;
using Xunit;

[assembly: GenerateImposter(typeof(ISetupChild))]
[assembly: GenerateImposter(typeof(IGenericSetupTarget))]

namespace Imposter.Tests.Features.InterfaceSetup
{
    public interface ISetupParent
    {
        int Value { get; set; }
        int Execute(int value);
        void Notify(int value);
        event Action Changed;
        event Action SharedChanged;
        string this[int index] { get; set; }
    }

    public interface ISetupChild : ISetupParent
    {
        new int Value { get; set; }
        new int Execute(int value);
        void Notify(string value);
        new event EventHandler Changed;
        new event Action SharedChanged;
        string this[string index] { get; set; }
        void NoOp();
        T Echo<T>(T value)
            where T : class;
    }

    public interface IGenericSetup<T>
    {
        T Read();
    }

    public interface IGenericSetupTarget : IGenericSetup<int>, IGenericSetup<string> { }

    public class InterfaceSetupTests
    {
        private readonly ISetupChildImposter _sut = new ISetupChildImposter();

        [Fact]
        public void Given_HiddenProperties_When_ConfiguringViews_Should_KeepValuesSeparate()
        {
            var parent = _sut.For(default(ISetupParent));
            var child = _sut.For(default(ISetupChild));
            parent.Value.Getter().Returns(10);
            child.Value.Getter().Returns(20);

            ((ISetupParent)_sut.Instance()).Value.ShouldBe(10);
            _sut.Instance().Value.ShouldBe(20);
            parent.Value.Getter().Called(Count.Once());
            child.Value.Getter().Called(Count.Once());
        }

        [Fact]
        public void Given_HiddenMethods_When_ConfiguringViews_Should_KeepBehaviorAndHistorySeparate()
        {
            var parent = _sut.For(default(ISetupParent)).Execute(Arg<int>.Any());
            var child = _sut.For(default(ISetupChild)).Execute(Arg<int>.Any());
            parent.Returns(10);
            child.Returns(20);

            ((ISetupParent)_sut.Instance()).Execute(1).ShouldBe(10);
            _sut.Instance().Execute(1).ShouldBe(20);
            _sut.Instance().Execute(2).ShouldBe(20);

            parent.CallCount().ShouldBe(1);
            child.CallCount().ShouldBe(2);
            parent.Called(Count.Once());
            child.Called(Count.Exactly(2));
        }

        [Fact]
        public void Given_ExistingMethodSetup_When_SelectingView_Should_ShareInvocationHistory()
        {
            var method = _sut.NoOp();
            _sut.Instance().NoOp();

            _sut.For(default(ISetupChild)).NoOp().CallCount().ShouldBe(1);
            method.CallCount().ShouldBe(1);
        }

        [Fact]
        public void Given_ExplicitMode_When_SelectingViews_Should_NotRegisterSetups()
        {
            var imposter = new ISetupChildImposter(ImposterMode.Explicit);
            var parent = imposter.For(default(ISetupParent));
            var child = imposter.For(default(ISetupChild));

            ReferenceEquals(parent, imposter).ShouldBeTrue();
            ReferenceEquals(child, imposter).ShouldBeTrue();
            Should.Throw<MissingImposterException>(() => imposter.Instance().NoOp());
        }

        [Fact]
        public void Given_ConflictingEvents_When_RaisingThroughViews_Should_UseMatchingHandlers()
        {
            var parentCalls = 0;
            var childCalls = 0;
            ((ISetupParent)_sut.Instance()).Changed += () => parentCalls++;
            _sut.Instance().Changed += (_, __) => childCalls++;

            _sut.For(default(ISetupParent)).Changed.Raise();
            parentCalls.ShouldBe(1);
            childCalls.ShouldBe(0);
            _sut.For(default(ISetupChild)).Changed.Raise(this, EventArgs.Empty);
            parentCalls.ShouldBe(1);
            childCalls.ShouldBe(1);
        }

        [Fact]
        public void Given_Indexer_When_ConfiguringDeclaringInterfaceView_Should_ShareSetup()
        {
            var parent = _sut.For(default(ISetupParent));
            parent[1].Getter().Returns("one");

            _sut.Instance()[1].ShouldBe("one");
            parent[1].Getter().Called(Count.Once());
            _sut[1].Getter().Called(Count.Once());
        }

        [Fact]
        public void Given_InheritedOverloads_When_UsingChildView_Should_ResolveBothSignatures()
        {
            var view = _sut.For(default(ISetupChild));
            var parent = view.Notify(42);
            var child = view.Notify("value");
            view[42].Getter().Returns("number");
            view["key"].Getter().Returns("text");

            _sut.Instance().Notify(42);
            _sut.Instance().Notify("value");
            parent.CallCount().ShouldBe(1);
            child.CallCount().ShouldBe(1);
            _sut.Instance()[42].ShouldBe("number");
            _sut.Instance()["key"].ShouldBe("text");
        }

        [Fact]
        public void Given_IdenticalInheritedEvents_When_SelectingViews_Should_ShareExistingEventBuilder()
        {
            var parent = _sut.For(default(ISetupParent));
            var child = _sut.For(default(ISetupChild));

            ReferenceEquals(parent.SharedChanged, child.SharedChanged).ShouldBeTrue();
            var calls = 0;
            ((ISetupParent)_sut.Instance()).SharedChanged += () => calls++;
            _sut.Instance().SharedChanged += () => calls++;

            parent.SharedChanged.Raise();
            calls.ShouldBe(2);
            child.SharedChanged.Raise();
            calls.ShouldBe(4);
        }

        [Fact]
        public void Given_GenericMethod_When_ConfiguringView_Should_PreserveConstraintsAndBehavior()
        {
            var method = _sut.For(default(ISetupChild)).Echo<string>(Arg<string>.Any());
            method.Returns("result");

            _sut.Instance().Echo("input").ShouldBe("result");
            method.CallCount().ShouldBe(1);
        }

        [Fact]
        public void Given_ClosedGenericInterfaces_When_SelectingViews_Should_UseExactInterface()
        {
            var imposter = new IGenericSetupTargetImposter();
            imposter.For(default(IGenericSetup<int>)).Read().Returns(42);
            imposter.For(default(IGenericSetup<string>)).Read().Returns("answer");

            ((IGenericSetup<int>)imposter.Instance()).Read().ShouldBe(42);
            ((IGenericSetup<string>)imposter.Instance()).Read().ShouldBe("answer");
        }
    }
}
