using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.PropertyImpersonation
{
    public class RefStructPropertyTests
    {
        private readonly IRefStructPropertySutImposter _sut = new IRefStructPropertySutImposter();

        [Fact]
        public void GivenReturnsDelegate_WhenRead_ShouldReturnWhatItProduces()
        {
            _sut.Current.Getter().Returns(() => new Bookmark(3));

            _sut.Instance().Current.Page.ShouldBe(3);
        }

        [Fact]
        public void GivenNoSetup_WhenRead_ShouldReturnTheDefault()
        {
            _sut.Instance().Current.Page.ShouldBe(0);
        }

        // The imposter can't keep the value, so a set doesn't change what the getter returns.
        [Fact]
        public void GivenValueSetWithoutSetup_WhenRead_ShouldReturnTheDefault()
        {
            _sut.Instance().Current = new Bookmark(5);

            _sut.Instance().Current.Page.ShouldBe(0);
        }

        [Fact]
        public void GivenReturnsDelegatesInSequence_WhenReadTwice_ShouldReturnEachInOrder()
        {
            _sut.Current.Getter()
                .Returns(() => new Bookmark(1))
                .Then()
                .Returns(() => new Bookmark(2));
            _ = _sut.Instance().Current.Page;

            _sut.Instance().Current.Page.ShouldBe(2);
        }

        [Fact]
        public void GivenThrows_WhenRead_ShouldThrow()
        {
            _sut.Current.Getter().Throws<InvalidOperationException>();

            Should.Throw<InvalidOperationException>(() => _sut.Instance().Current.Page);
        }

        [Fact]
        public void GivenReads_WhenGetterIsVerified_ShouldCountThem()
        {
            _ = _sut.Instance().Current.Page;
            _ = _sut.Instance().Current.Page;

            _sut.Current.Getter().Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenSetterCallback_WhenSet_ShouldGetTheValue()
        {
            var pages = new List<int>();
            _sut.Current.Setter().Callback(value => pages.Add(value.Page));

            _sut.Instance().Current = new Bookmark(4);

            pages.ShouldBe(new[] { 4 });
        }

        [Fact]
        public void GivenSets_WhenSetterIsVerified_ShouldCountEverySet()
        {
            _sut.Instance().Current = new Bookmark(1);
            _sut.Instance().Current = new Bookmark(2);

            _sut.Current.Setter().Called(Count.Exactly(2));
        }

        [Fact]
        public void GivenNoSet_WhenSetterIsVerifiedOnce_ShouldThrow()
        {
            Should.Throw<VerificationFailedException>(() =>
                _sut.Current.Setter().Called(Count.Once())
            );
        }

        [Fact]
        public void GivenExplicitModeWithoutSetup_WhenRead_ShouldThrow()
        {
            var imposter = new IRefStructPropertySutImposter(ImposterMode.Explicit);

            Should.Throw<MissingImposterException>(() => imposter.Instance().Current.Page);
        }

        [Fact]
        public void GivenExplicitModeWithSetterSetUp_WhenSet_ShouldCountIt()
        {
            var imposter = new IRefStructPropertySutImposter(ImposterMode.Explicit);
            imposter.Current.Setter();

            imposter.Instance().Current = new Bookmark(1);

            imposter.Current.Setter().Called(Count.Once());
        }

        [Fact]
        public void GivenNoSetup_WhenClassPropertyIsRead_ShouldReturnTheDefault()
        {
            var imposter = new RefStructPropertyClassImposter();

            imposter.Instance().Current.Page.ShouldBe(0);
        }

        [Fact]
        public void GivenGetterUseBaseImplementation_WhenClassPropertyIsRead_ShouldReturnTheBaseValue()
        {
            var imposter = new RefStructPropertyClassImposter();
            imposter.Current.Getter().UseBaseImplementation();

            imposter.Instance().Current.Page.ShouldBe(7);
        }

        [Fact]
        public void GivenSetterUseBaseImplementation_WhenClassPropertyIsSet_ShouldSetTheBaseValue()
        {
            var imposter = new RefStructPropertyClassImposter();
            imposter.Current.Setter().UseBaseImplementation();

            imposter.Instance().Current = new Bookmark(9);

            imposter.Instance().BasePage.ShouldBe(9);
        }

        [Fact]
        public void GivenPropertyUseBaseImplementation_WhenClassPropertyIsSetAndRead_ShouldUseTheBase()
        {
            var imposter = new RefStructPropertyClassImposter();
            imposter.Current.UseBaseImplementation();

            imposter.Instance().Current = new Bookmark(8);

            imposter.Instance().Current.Page.ShouldBe(8);
        }
    }
}
