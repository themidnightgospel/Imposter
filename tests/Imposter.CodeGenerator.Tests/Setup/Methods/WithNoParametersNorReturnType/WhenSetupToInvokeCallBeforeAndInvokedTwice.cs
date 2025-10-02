using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.WithNoParametersNorReturnType;

[Subject("Setup.CallBefore")]
class WhenSetupToInvokeCallBeforeAndInvokedTwice
{
    static ISutForMethodSetupsImposter subject;
    private static int _callBeforeInvokedCount;

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .NoParamsNoReturn()
            .CallBefore(() => ++_callBeforeInvokedCount);
    };

    Because HelloMethodWasInvokedTwice = () =>
    {
        subject.Instance().NoParamsNoReturn();
        subject.Instance().NoParamsNoReturn();
    };

    It CallBeforeShouldBeInvokedTwice = () => _callBeforeInvokedCount.ShouldBe(2);

    It ShouldBeAbleToVerifySingleInvocations = () => subject.NoParamsNoReturn().Called(Count.Exactly(2));
}