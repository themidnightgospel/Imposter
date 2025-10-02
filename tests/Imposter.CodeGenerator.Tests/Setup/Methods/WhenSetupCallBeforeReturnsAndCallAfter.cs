using System.Collections.Generic;
using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods;

[Subject("Setup")]
public class WhenSetupCallBeforeReturnsAndCallAfter
{
    static ISutForMethodSetupsImposter subject;
    static List<string> _calledMethodNames = new();
    static void Called(string name) => _calledMethodNames.Add(name);

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .ParamsReturn(Arg<int>.Is(it => it >= 10))
            .Returns(_ =>
            {
                Called("Returns");
                return 10;
            })
            .CallBefore(_ => Called("CallBefore"))
            .CallAfter(_ => Called("CallAfter"));
    };

    Because PrintMethodInvoked = () => subject.Instance().ParamsReturn(22);

    It FirstShouldBeCalledCallBefore = () => _calledMethodNames[0].ShouldBe("CallBefore");

    It SecondShouldBeCalledReturns = () => _calledMethodNames[1].ShouldBe("Returns");

    It ThirdShouldBeCalledCallAfter = () => _calledMethodNames[2].ShouldBe("CallAfter");

    It ShouldBeAbleToVerifySingleInvocations = () => subject.ParamsReturn(Arg<int>.Is(it => it == 22)).Called(Count.Once);
}