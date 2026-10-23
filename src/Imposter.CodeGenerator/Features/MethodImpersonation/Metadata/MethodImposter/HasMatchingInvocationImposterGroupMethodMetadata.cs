using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct HasMatchingInvocationImposterGroupMethodMetadata
{
    internal readonly TypeSyntax ReturnType;

    internal const string BaseName = "HasMatchingInvocationImposterGroup";

    internal readonly string Name;

    internal readonly string ArgumentsParameterName;

    public HasMatchingInvocationImposterGroupMethodMetadata(
        in ReservedParameterNames reservedParameterNames
    )
    {
        ReturnType = WellKnownTypes.Bool;
        Name = BaseName;
        var nameContext = reservedParameterNames.CreateNameSet();
        ArgumentsParameterName = nameContext.Use("arguments");
    }
}
