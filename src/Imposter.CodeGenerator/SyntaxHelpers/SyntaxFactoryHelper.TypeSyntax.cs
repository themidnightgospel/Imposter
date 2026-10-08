using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeSyntax TypeSyntax(ITypeSymbol typeSymbol) =>
        ParseTypeName(typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

    internal static TypeSyntax TypeSyntax(TypeModel type) => ParseTypeName(type.FullyQualifiedName);

    internal static TypeSyntax TypeSyntaxIncludingNullable(ITypeSymbol typeSymbol) =>
        ParseTypeName(typeSymbol.ToDisplayString(TypeModel.FullyQualifiedFormatIncludingNullable));

    internal static TypeSyntax TypeSyntaxIncludingNullable(TypeModel type) =>
        ParseTypeName(type.FullyQualifiedNameIncludingNullable);

    internal static IEnumerable<TypeParameterSyntax> TypeParametersSyntax(
        IReadOnlyList<TypeParameterModel> typeParameters
    ) => typeParameters.Select(it => TypeParameter(EscapedIdentifier(it.Name)));

    internal static TypeParameterListSyntax? TypeParameterListSyntax(
        IReadOnlyList<TypeParameterModel> typeParameters
    ) =>
        typeParameters.Count > 0
            ? TypeParameterList(SeparatedList(TypeParametersSyntax(typeParameters)))
            : null;

    internal static SimpleNameSyntax WithMethodGenericArguments(
        string identifier,
        in ImposterTargetMethodMetadata method
    ) =>
        method.GenericTypeArgumentListSyntax is not null
            ? GenericName(Identifier(identifier), method.GenericTypeArgumentListSyntax)
            : IdentifierName(identifier);

    internal static NameSyntax WithMethodGenericArguments(
        IReadOnlyList<NameSyntax> genericArguments,
        string typeName
    )
    {
        if (genericArguments.Count > 0)
        {
            return GenericName(
                Identifier(typeName),
                TypeArgumentList(SeparatedList<TypeSyntax>(genericArguments))
            );
        }

        return IdentifierName(typeName);
    }

    internal static IReadOnlyList<TypeParameterConstraintClauseSyntax> TypeParameterConstraintClauses(
        IEnumerable<TypeParameterModel> typeParameters
    ) => EnumerateTypeParameterConstraintClauses(typeParameters).ToArray();

    private static IEnumerable<TypeParameterConstraintClauseSyntax> EnumerateTypeParameterConstraintClauses(
        IEnumerable<TypeParameterModel> typeParameters
    )
    {
        foreach (var typeParameter in typeParameters)
        {
            var constraintClause = TryBuildConstraintClause(typeParameter);
            if (constraintClause is not null)
            {
                yield return constraintClause;
            }
        }
    }

    private static TypeParameterConstraintClauseSyntax? TryBuildConstraintClause(
        TypeParameterModel typeParameter
    )
    {
        var constraints = new List<TypeParameterConstraintSyntax>();

        if (typeParameter.HasReferenceTypeConstraint)
        {
            var referenceConstraint = ClassOrStructConstraint(SyntaxKind.ClassConstraint);
            if (typeParameter.IsReferenceTypeConstraintNullable)
            {
                referenceConstraint = referenceConstraint.WithQuestionToken(
                    Token(SyntaxKind.QuestionToken)
                );
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
            typeParameter.ConstraintTypes.Select(constraintType =>
                TypeConstraint(TypeSyntax(constraintType))
            )
        );

        if (typeParameter.HasConstructorConstraint)
        {
            constraints.Add(ConstructorConstraint());
        }

        if (constraints.Count == 0)
        {
            return null;
        }

        return TypeParameterConstraintClause(IdentifierName(EscapedIdentifier(typeParameter.Name)))
            .WithConstraints(SeparatedList(constraints));
    }

    internal static SimpleNameSyntax AsSimpleName(NameSyntax nameSyntax) =>
        nameSyntax switch
        {
            SimpleNameSyntax simpleName => simpleName,
            QualifiedNameSyntax qualifiedName => qualifiedName.Right,
            _ => IdentifierName(nameSyntax.ToString()),
        };

    internal static NameSyntax GlobalQualifiedName(string? @namespace, string type)
    {
        return ParseName(
            string.IsNullOrWhiteSpace(@namespace)
                ? $"global::{type}"
                : $"global::{@namespace}.{type}"
        );
    }
}
