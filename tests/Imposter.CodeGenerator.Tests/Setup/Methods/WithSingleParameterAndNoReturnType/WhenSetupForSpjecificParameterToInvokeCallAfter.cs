using Imposter.Abstractions;
using Machine.Specifications;
using Shouldly;

namespace Imposter.CodeGenerator.Tests.Setup.Methods.WithSingleParameterAndNoReturnType;

[Subject("Setup.CallAfter")]
class WhenSetupForSpjecificParameterToInvokeCallAfter
{
    static ISutForMethodSetupsImposter subject;
    private static bool _callAfterInvoked;

    Establish Context = () =>
    {
        subject = new ISutForMethodSetupsImposter();
        subject
            .ParamsNoReturn(Arg<int>.Is(it => it >= 10))
            .CallAfter(_ => _callAfterInvoked = true);
    };

    Because PrintMethodInvokedNonMatchingParameter = () => subject.Instance().ParamsNoReturn(9);

    It CallAfterShouldNotBeInvoked = () => _callAfterInvoked.ShouldBeFalse();

    It ShouldBeAbleToVerifySingleInvocations = () => subject.ParamsNoReturn(Arg<int>.Is(it => it == 9)).Called(Count.Once);
}