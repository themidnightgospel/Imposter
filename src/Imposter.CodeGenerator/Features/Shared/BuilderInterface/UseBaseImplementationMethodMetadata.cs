using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared.BuilderInterface;

// A builder's UseBaseImplementation(), for a class member with a base implementation.
internal readonly struct UseBaseImplementationMethodMetadata
{
    internal const string BaseName = "UseBaseImplementation";

    internal readonly string Name = BaseName;

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal UseBaseImplementationMethodMetadata(TypeSyntax returnType, NameSyntax interfaceSyntax)
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
    }
}
