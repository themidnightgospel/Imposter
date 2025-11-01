using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata;

internal interface IParameterNameContextProvider
{
    NameSet CreateParameterNameContext();
}