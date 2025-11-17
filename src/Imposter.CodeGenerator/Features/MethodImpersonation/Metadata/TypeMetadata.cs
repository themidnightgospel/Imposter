using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly record struct TypeMetadata(string Name, NameSyntax Syntax)
{
    public TypeMetadata(string Name)
        : this(Name, SyntaxFactory.IdentifierName(Name)) { }
}
