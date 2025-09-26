using Imposter.Abstractions;

namespace Imposter.CodeGenerator.Tests.Setup.Methods;

[GenerateImposter(typeof(ISutForMethodSetups))]
public interface ISutForMethodSetups
{
    void NoParamsNoReturn();

    void ParamsNoReturn(int value);

    int NoParamsReturn();

    int ParamsReturn(int value);
}