using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;

internal readonly record struct InvocationHistoryCollectionMetadata
{
    internal const string AddMethodName = "Add";

    internal const string RecordFailureMethodName = "RecordFailure";

    internal const string HeadFieldName = "_invocationHistory";

    internal const string NodeTypeName = "Node";

    internal const string NextFieldName = "Next";

    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly NameSyntax NodeSyntax;

    internal readonly FieldDeclarationMetadata AsField;

    public InvocationHistoryCollectionMetadata(string name, NameSet fieldNames)
    {
        Name = name;
        Syntax = SyntaxFactory.IdentifierName(Name);
        NodeSyntax = SyntaxFactory.QualifiedName(
            Syntax,
            SyntaxFactory.IdentifierName(NodeTypeName)
        );
        AsField = new FieldDeclarationMetadata(Name, fieldNames);
    }
}
