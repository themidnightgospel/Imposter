using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;

internal readonly record struct InvocationHistoryCollectionMetadata
{
    internal const string InvocationHistoryCollectionFieldName = "_invocationHistory";

    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly FieldDeclarationMetadata AsField;

    public InvocationHistoryCollectionMetadata(string name, NameSet methodNames)
    {
        Name = name;
        Syntax = SyntaxFactory.IdentifierName(Name);
        AsField = new FieldDeclarationMetadata(Name, methodNames);
    }
}
