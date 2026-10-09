using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterBuilderCommon
{
    internal static IdentifierNameSyntax FieldIdentifier(in FieldMetadata field) =>
        IdentifierName(field.Name);

    // The Count every verification method takes.
    internal static ParameterSyntax CountParameter(in ImposterEventMetadata @event) =>
        SyntaxFactoryHelper.ParameterSyntax(@event.Builder.Methods.CountParameter);
}
