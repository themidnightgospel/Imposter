using System;
using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImpersonation.Suts;
using Shouldly;
using Xunit;

namespace Imposter.Tests.Features.ClassImpersonation
{
    public class MultiParameterIndexerClassImposterTests
    {
        [Fact]
        public void GivenMultiParameterIndexer_WhenConfigured_ShouldReturnConfiguredValue()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();

            imposter[Arg<int>.Is(2), Arg<string>.Is("alpha")].Getter().Returns(88);

            var instance = imposter.Instance();

            instance.ReadValue(2, "alpha").ShouldBe(88);
        }

        [Fact]
        public void GivenMultiParameterIndexer_WhenUsingBaseBehavior_ShouldStoreValues()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var instance = imposter.Instance();

            instance.WriteValue(5, "base", 123);

            instance.ReadValue(5, "base").ShouldBe(123);
        }

        [Fact]
        public void GivenMultiParameterIndexerUseBaseImplementation_WhenConfigured_ShouldInvokeBaseAccessors()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();

            imposter[Arg<int>.Is(1), Arg<string>.Is("call")].Getter().UseBaseImplementation();
            imposter[Arg<int>.Is(1), Arg<string>.Is("call")].Setter().UseBaseImplementation();

            var instance = imposter.Instance();

            instance.WriteValue(1, "call", 321);
            instance.ReadValue(1, "call").ShouldBe(321);
        }

        [Fact]
        public void GivenMultiParameterIndexerSetter_WhenInvoked_ShouldSucceedVerification()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var setter = imposter[Arg<int>.Is(7), Arg<string>.Is("verify")].Setter();
            var instance = imposter.Instance();

            instance.WriteValue(7, "verify", 77);

            Should.NotThrow(() => setter.Called(Count.Once()));
        }

        [Fact]
        public void GivenMultiParameterIndexerSetterUseBaseImplementation_WhenCriteriaDiffer_ShouldUseBaseOnlyForMatching()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var baseSetter = imposter[Arg<int>.Is(2), Arg<string>.Is("base")].Setter();
            baseSetter.UseBaseImplementation();

            int? recordedValue = null;
            var customSetter = imposter[Arg<int>.Is(3), Arg<string>.Is("custom")].Setter();
            customSetter.Callback((row, column, value) => recordedValue = value);

            var instance = imposter.Instance();

            instance.WriteValue(2, "base", 200);
            instance.ReadValue(2, "base").ShouldBe(200);

            instance.WriteValue(3, "custom", 300);
            recordedValue.ShouldBe(300);
            instance.ReadValue(3, "custom").ShouldBe(-1);
        }

        [Fact]
        public void GivenMultiParameterIndexerSetterUseBaseImplementation_WhenInvokedMultipleTimes_ShouldPersistAcrossCalls()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();

            imposter[Arg<int>.Is(5), Arg<string>.Is("persist")].Setter().UseBaseImplementation();
            imposter[Arg<int>.Any(), Arg<string>.Any()].Getter().UseBaseImplementation();

            var instance = imposter.Instance();

            instance.WriteValue(5, "persist", 50);
            instance.WriteValue(5, "persist", 60);
            instance.ReadValue(5, "persist").ShouldBe(60);

            instance.WriteValue(6, "other", 600);
            instance.ReadValue(6, "other").ShouldBe(-1);
        }

        [Fact]
        public void GivenMultiParameterIndexerSetterUseBaseImplementation_WhenCallbackRegistered_ShouldRunCallbackAndAllowVerification()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var setter = imposter[Arg<int>.Is(11), Arg<string>.Is("callback")].Setter();
            var callbackArguments = new List<(int Row, string Column, int Value)>();

            setter
                .UseBaseImplementation()
                .Callback((row, column, value) => callbackArguments.Add((row, column, value)));

            var instance = imposter.Instance();

            instance.WriteValue(11, "callback", 1111);
            instance.WriteValue(11, "callback", 2222);

            callbackArguments.ShouldBe(new[] { (11, "callback", 1111), (11, "callback", 2222) });

            Should.NotThrow(() => setter.Called(Count.Exactly(2)));
            instance.ReadValue(11, "callback").ShouldBe(2222);
        }

        [Fact]
        public void GivenMultiParameterIndexerSetterUseBaseImplementation_WhenMultipleCriteriaRegistered_ShouldRouteEachToBase()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            imposter[Arg<int>.Any(), Arg<string>.Any()].Getter().UseBaseImplementation();

            var alphaSetter = imposter[Arg<int>.Is(1), Arg<string>.Is("alpha")].Setter();
            var betaSetter = imposter[Arg<int>.Is(2), Arg<string>.Is("beta")].Setter();

            betaSetter.UseBaseImplementation();
            alphaSetter.UseBaseImplementation();

            var instance = imposter.Instance();

            instance.WriteValue(1, "alpha", 111);
            instance.WriteValue(2, "beta", 222);

            instance.ReadValue(1, "alpha").ShouldBe(111);
            instance.ReadValue(2, "beta").ShouldBe(222);

            instance.WriteValue(3, "none", 333);
            instance.ReadValue(3, "none").ShouldBe(-1);
        }

        [Fact]
        public void GivenMultiParameterIndexerGetter_WhenMixingOutcomesForSameCriteria_ShouldTreatLastCallAsWinning()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var getter = imposter[Arg<int>.Is(9), Arg<string>.Is("precedence")].Getter();
            // since we're configuring getter, that disabled the "default bindexer behaviour"
            imposter[Arg<int>.Is(9), Arg<string>.Is("precedence")]
                .Setter()
                .UseBaseImplementation();
            var instance = imposter.Instance();

            getter.Returns(100);
            instance.ReadValue(9, "precedence").ShouldBe(100);

            getter.Throws(new InvalidOperationException("fail"));
            Should.Throw<InvalidOperationException>(() => instance.ReadValue(9, "precedence"));

            getter.UseBaseImplementation();
            instance.WriteValue(9, "precedence", 4321);
            instance.ReadValue(9, "precedence").ShouldBe(4321);
        }

        [Fact]
        public void GivenMultiParameterIndexerGetterUseBaseImplementation_WhenCallbackRegistered_ShouldExecuteCallbackAndAllowVerification()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var getter = imposter[Arg<int>.Is(4), Arg<string>.Is("callback")].Getter();
            imposter[Arg<int>.Is(4), Arg<string>.Is("callback")].Setter().UseBaseImplementation();
            var callbackArguments = new List<(int Row, string Column)>();

            getter
                .UseBaseImplementation()
                .Callback((row, column) => callbackArguments.Add((row, column)));

            var instance = imposter.Instance();
            instance.WriteValue(4, "callback", 444);
            instance.ReadValue(4, "callback").ShouldBe(444);

            callbackArguments.ShouldBe(new[] { (4, "callback") });
            Should.NotThrow(() => getter.Called(Count.Once()));
        }

        [Fact]
        public void GivenMultiParameterIndexerGetter_WhenSequencingBaseReturnsBase_ShouldRunInvocationsInOrder()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            var getter = imposter[Arg<int>.Is(8), Arg<string>.Is("sequence")].Getter();
            imposter[Arg<int>.Is(8), Arg<string>.Is("sequence")].Setter().UseBaseImplementation();
            getter.UseBaseImplementation().Then().Returns(111).Then().UseBaseImplementation();

            var instance = imposter.Instance();

            instance.WriteValue(8, "sequence", 10);
            instance.ReadValue(8, "sequence").ShouldBe(10);

            instance.ReadValue(8, "sequence").ShouldBe(111);

            instance.WriteValue(8, "sequence", 30);
            instance.ReadValue(8, "sequence").ShouldBe(30);
        }

        [Fact]
        public void GivenMultiParameterIndexerGetterUseBaseImplementation_WhenCalledWithDifferentArguments_ShouldPassRuntimeIndicesToBase()
        {
            var imposter = new ClassWithMultiParameterIndexerImposter();
            imposter[Arg<int>.Any(), Arg<string>.Any()].Getter().UseBaseImplementation();
            imposter[Arg<int>.Any(), Arg<string>.Any()].Setter().UseBaseImplementation();

            var instance = imposter.Instance();
            instance.WriteValue(3, "first", 101);
            instance.WriteValue(10, "second", 202);
            instance.WriteValue(-5, "mixed", 305);

            instance.ReadValue(3, "first").ShouldBe(101);
            instance.ReadValue(10, "second").ShouldBe(202);
            instance.ReadValue(-5, "mixed").ShouldBe(305);
        }
    }
}
