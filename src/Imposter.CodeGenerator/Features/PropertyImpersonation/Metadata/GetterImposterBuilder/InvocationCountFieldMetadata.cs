using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;

internal readonly struct InvocationCountFieldMetadata
{
    internal readonly string Name = "_invocationCount";

    internal readonly TypeSyntax TypeSyntax = WellKnownTypes.Int;

    public InvocationCountFieldMetadata() { }
}
