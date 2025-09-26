using System.Collections.Generic;
using Imposter.Abstractions;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods;

[Subject("Setup")]
public class WhenSetupMultipleInvocations
{
    static ISutForMethodSetupsImposter subject;
    static List<int> _invocationResults = new();
    static List<string> _calledMethodNames = new();
    static void Called(string name) => _calledMethodNames.Add(name);

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .ParamsReturn(Arg<int>.Any)
            .CallBefore(_ => Called("CallBefore"))
            .Returns(_ =>
            {
                Called("Returns");
                return 10;
            })
            .CallAfter(_ => Called("CallAfter"))
            .CallBefore(_ => Called("CallBefore2"))
            .Returns(_ =>
            {
                Called("Returns2");
                return 11;
            })
            .CallAfter(_ => Called("CallAfter2"))
            .CallBefore(_ => Called("CallBefore3"))
            .Returns(_ =>
            {
                Called("Returns3");
                return 12;
            })
            .CallAfter(_ => Called("CallAfter3"))
            ;
    };

    private Because PrintMethodInvokedFourTimes = () =>
    {
        _invocationResults.Add(subject.Instance().ParamsReturn(1));
        _invocationResults.Add(subject.Instance().ParamsReturn(2));
        _invocationResults.Add(subject.Instance().ParamsReturn(3));
        _invocationResults.Add(subject.Instance().ParamsReturn(4));
    };

    It FirstInvocationShouldInvokeFirstSetup = () =>
    {
        _calledMethodNames[0].ShouldBe("CallBefore");
        _calledMethodNames[1].ShouldBe("Returns");
        _calledMethodNames[2].ShouldBe("CallAfter");
    };
    
    It FirstInvocationShouldReturnFirstSetup = () =>
    {
        _invocationResults[0].ShouldBe(10);
    };

    It SecondInvocationShouldInvokeFirstSetup = () =>
    {
        _calledMethodNames[3].ShouldBe("CallBefore2");
        _calledMethodNames[4].ShouldBe("Returns2");
        _calledMethodNames[5].ShouldBe("CallAfter2");
    };
    
    It SecondInvocationShouldReturnFirstSetup = () =>
    {
        _invocationResults[1].ShouldBe(11);
    };

    It ThirdInvocationShouldInvokeFirstSetup = () =>
    {
        _calledMethodNames[6].ShouldBe("CallBefore3");
        _calledMethodNames[7].ShouldBe("Returns3");
        _calledMethodNames[8].ShouldBe("CallAfter3");
    };
    
    It ThirdInvocationShouldReturnFirstSetup = () =>
    {
        _invocationResults[2].ShouldBe(12);
    };

    It ShouldBeAbleToVerifySingleInvocations = () => subject.ParamsReturn(Arg<int>.Any).Called(Count.Exactly(4));
}