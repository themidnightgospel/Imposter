using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.MethodImposter;

internal readonly record struct MethodImposterCollectionMetadata(string Name, NameSyntax Syntax, FieldDeclarationMetadata AsField)
{
    public MethodImposterCollectionMetadata(string name)
        : this(name, SyntaxFactory.IdentifierName(name), new FieldDeclarationMetadata(name))
    {
    }
}
