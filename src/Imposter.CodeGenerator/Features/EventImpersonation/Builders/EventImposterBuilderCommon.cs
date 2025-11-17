using System;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Builders;

internal static class EventImposterBuilderCommon
{
    internal static MethodDeclarationBuilder ExplicitInterfaceMethod(
        NameSyntax interfaceType,
        TypeSyntax returnType,
        string name
    ) =>
        new MethodDeclarationBuilder(returnType, name).WithExplicitInterfaceSpecifier(
            ExplicitInterfaceSpecifier(interfaceType)
        );

    internal static InvocationExpressionSyntax ThrowIfNull(string parameterName) =>
        IdentifierName(nameof(ArgumentNullException))
            .Dot(IdentifierName("ThrowIfNull"))
            .Call(Argument(IdentifierName(parameterName)));

    internal static IdentifierNameSyntax FieldIdentifier(in FieldMetadata field) =>
        IdentifierName(field.Name);
}
