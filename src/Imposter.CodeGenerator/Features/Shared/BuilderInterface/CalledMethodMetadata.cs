using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared.BuilderInterface;

// A builder's Called(count) verification.
internal readonly struct CalledMethodMetadata
{
    internal readonly string Name = "Called";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CountParameter;

    public CalledMethodMetadata(NameSet names)
    {
        ReturnType = WellKnownTypes.Void;
        CountParameter = new ParameterMetadata(
            names.Use("count"),
            WellKnownTypes.Imposter.Abstractions.Count
        );
    }
}
