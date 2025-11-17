using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal interface IParameterNameContextProvider
{
    NameSet CreateParameterNameContext();
}
