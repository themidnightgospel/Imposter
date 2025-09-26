using Imposter.Abstractions;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.WithNoParametersNorReturnType;

[Subject("Setup.CallAfter")]
class WhenSetupToInvokeCallAfterAndInvokedTwice
{
    static ISutForMethodSetupsImposter subject;
    private static int _callAfterInvokedCount;

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .NoParamsNoReturn()
            .CallAfter(() => ++_callAfterInvokedCount);
    };

    Because HelloMethodWasInvokedTwice = () =>
    {
        subject.Instance().NoParamsNoReturn();
        subject.Instance().NoParamsNoReturn();
    };

    It CallAfterShouldBeInvoked = () => _callAfterInvokedCount.ShouldBe(2);

    It ShouldBeAbleToVerifyTwoInvocations = () => subject.NoParamsNoReturn().Called(Count.Exactly(2));
}