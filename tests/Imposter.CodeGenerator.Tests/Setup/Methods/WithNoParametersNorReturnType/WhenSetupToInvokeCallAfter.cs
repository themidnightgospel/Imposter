using Imposter.Abstractions;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.WithNoParametersNorReturnType;

[Subject("Setup.CallAfter")]
class WhenSetupToInvokeCallAfter
{
    static ISutForMethodSetupsImposter subject;
    private static bool _callAfterInvoked;

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .NoParamsNoReturn()
            .CallAfter(() => _callAfterInvoked = true);
    };

    Because HelloMethodWasInvoked = () => subject.Instance().NoParamsNoReturn();

    It CallAfterShouldBeInvoked = () => _callAfterInvoked.ShouldBeTrue();

    It ShouldBeAbleToVerifySingleInvocations = () => subject.NoParamsNoReturn().Called(Count.Once);
}