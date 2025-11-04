using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationHistory;

internal readonly record struct InvocationHistoryCollectionMetadata
{
    internal const string InvocationHistoryCollectionFieldName = "_invocationHistory";

    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly FieldDeclarationMetadata AsField;

    internal readonly InvocationHistoryCollectionCountMethodMetadata CountMethod;

    public InvocationHistoryCollectionMetadata(string name)
    {
        Name = name;
        Syntax = SyntaxFactory.IdentifierName(Name);
        AsField = new FieldDeclarationMetadata(Name);
        CountMethod = new InvocationHistoryCollectionCountMethodMetadata();
    }
}
