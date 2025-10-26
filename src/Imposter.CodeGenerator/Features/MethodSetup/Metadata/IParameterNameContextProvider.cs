using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

internal interface IParameterNameContextProvider
{
    NameSet CreateParameterNameContext();
}