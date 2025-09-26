using Imposter.Abstractions;
using Imposters.Imposter.CodeGenerator.Tests.Setup.Methods.ISutForMethodSetups;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.WithSingleParameterAndNoReturnType;

[Subject("Setup.CallAfter")]
class WhenSetupForAnyParameterToInvokeCallBefore
{
    static ISutForMethodSetupsImposter subject;
    private static bool _callBeforeInvoked;

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .ParamsNoReturn(Arg<int>.Any)
            .CallBefore(_ => _callBeforeInvoked = true);
    };

    Because PrintMethodInvoked = () => subject.Instance().ParamsNoReturn(22);

    It CallAfterShouldBeInvoked = () => _callBeforeInvoked.ShouldBeTrue();

    It ShouldBeAbleToVerifySingleInvocations = () => subject.ParamsNoReturn(Arg<int>.Any).Called(Count.Once);
}