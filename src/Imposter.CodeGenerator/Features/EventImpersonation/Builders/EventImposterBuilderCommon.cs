using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterBuilderCommon
{
    internal static InvocationExpressionSyntax ThrowIfNull(string parameterName) =>
        WellKnownTypes
            .System.ArgumentNullException.Dot(IdentifierName("ThrowIfNull"))
            .Call(Argument(IdentifierName(parameterName)));

    internal static IdentifierNameSyntax FieldIdentifier(in FieldMetadata field) =>
        IdentifierName(field.Name);
}
