using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly record struct MethodImposterCollectionMetadata(
    string Name,
    NameSyntax Syntax,
    FieldDeclarationMetadata AsField
)
{
    // A generic method's collection keeps a method imposter per type argument: AddNew adds one, and
    // GetImposterWithMatchingInvocationImposterGroup finds the one whose setups match a call.
    internal const string ImpostersFieldName = "_imposters";

    internal const string AddNewMethodName = "AddNew";

    internal const string GetImposterWithMatchingInvocationImposterGroupMethodName =
        "GetImposterWithMatchingInvocationImposterGroup";

    public MethodImposterCollectionMetadata(string name, NameSet fieldNames)
        : this(
            name,
            SyntaxFactory.IdentifierName(name),
            new FieldDeclarationMetadata(name, fieldNames)
        ) { }
}
