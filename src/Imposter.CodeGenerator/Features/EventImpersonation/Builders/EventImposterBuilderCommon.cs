using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterBuilderCommon
{
    internal static IdentifierNameSyntax FieldIdentifier(in FieldMetadata field) =>
        IdentifierName(field.Name);
}
