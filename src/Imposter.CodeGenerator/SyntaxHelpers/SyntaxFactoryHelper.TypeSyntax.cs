using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeSyntax TypeSyntax(ITypeSymbol typeSymbol) =>
        ParseTypeName(
            typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
        );

    internal static TypeParameterSyntax TypeParameterSyntax(ITypeParameterSymbol typeParameterSymbol)
        => TypeParameter(typeParameterSymbol.Name);

    internal static IEnumerable<TypeParameterSyntax> TypeParametersSyntax(IMethodSymbol method) =>
        method.TypeParameters.Length > 0
            ? method.TypeParameters.Select(TypeParameterSyntax)
            : [];

    internal static TypeParameterListSyntax? TypeParameterListSyntax(IMethodSymbol method) =>
        method.TypeParameters.Length > 0
            ? TypeParameterList(
                SeparatedList(
                    TypeParametersSyntax(method)
                )
            )
            : null;

    internal static SimpleNameSyntax WithMethodGenericArguments(string identifier, in ImposterTargetMethodMetadata method) =>
        method.GenericTypeArgumentListSyntax is not null
            ? GenericName(Identifier(identifier), method.GenericTypeArgumentListSyntax)
            : IdentifierName(identifier);

    internal static NameSyntax WithMethodGenericArguments(IReadOnlyList<NameSyntax> genericArguments, string typeName)
    {
        if (genericArguments.Count > 0)
        {
            return GenericName(
                Identifier(typeName),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(genericArguments)
                )
            );
        }

        return IdentifierName(typeName);
    }

    internal static IEnumerable<TypeParameterConstraintClauseSyntax> TypeParameterConstraintClauses(IMethodSymbol method)
    {
        foreach (var typeParameter in method.TypeParameters)
        {
            var constraints = new List<TypeParameterConstraintSyntax>();

            if (typeParameter.HasReferenceTypeConstraint)
            {
                var referenceConstraint = ClassOrStructConstraint(SyntaxKind.ClassConstraint);
                if (typeParameter.ReferenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated)
                {
                    referenceConstraint = referenceConstraint.WithQuestionToken(Token(SyntaxKind.QuestionToken));
                }

                constraints.Add(referenceConstraint);
            }

            if (typeParameter.HasUnmanagedTypeConstraint)
            {
                constraints.Add(TypeConstraint(IdentifierName("unmanaged")));
            }
            else if (typeParameter.HasValueTypeConstraint)
            {
                constraints.Add(ClassOrStructConstraint(SyntaxKind.StructConstraint));
            }

            if (typeParameter.HasNotNullConstraint)
            {
                constraints.Add(TypeConstraint(IdentifierName("notnull")));
            }

            constraints.AddRange(
                typeParameter.ConstraintTypes.Select(constraintType => TypeConstraint(TypeSyntax(constraintType))));

            if (typeParameter.HasConstructorConstraint)
            {
                constraints.Add(ConstructorConstraint());
            }

            if (constraints.Count > 0)
            {
                yield return TypeParameterConstraintClause(typeParameter.Name)
                    .WithConstraints(SeparatedList(constraints));
            }
        }
    }

    internal static SimpleNameSyntax AsSimpleName(NameSyntax nameSyntax) =>
        nameSyntax switch
        {
            SimpleNameSyntax simpleName => simpleName,
            QualifiedNameSyntax qualifiedName => qualifiedName.Right,
            _ => IdentifierName(nameSyntax.ToString())
        };

    internal static NameSyntax GlobalQualifiedName(string @namespace, string type)
    {
        return ParseName(
            string.IsNullOrWhiteSpace(@namespace)
                ? $"global::{type}"
                : $"global::{@namespace}.{type}"
        );
    }
}