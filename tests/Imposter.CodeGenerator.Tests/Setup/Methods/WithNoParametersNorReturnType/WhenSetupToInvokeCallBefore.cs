using Imposter.Abstractions;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.WithNoParametersNorReturnType;

[Subject("Setup.CallBefore")]
class WhenSetupToInvokeCallBefore
{
    static ISutForMethodSetupsImposter subject;
    private static bool _callBeforeInvoked;

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .NoParamsNoReturn()
            .CallBefore(() => _callBeforeInvoked = true);
    };

    Because HelloMethodWasInvoked = () => subject.Instance().NoParamsNoReturn();

    It CallBeforeShouldBeInvoked = () => _callBeforeInvoked.ShouldBeTrue();

    It ShouldBeAbleToVerifySingleInvocations = () => subject.NoParamsNoReturn().Called(Count.Once);
}