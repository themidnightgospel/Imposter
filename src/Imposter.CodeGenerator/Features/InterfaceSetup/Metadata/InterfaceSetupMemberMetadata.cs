using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;

internal readonly struct InterfaceSetupMemberMetadata
{
    internal readonly ISymbol Symbol;
    internal readonly string SetupName;
    internal readonly TypeSyntax ReturnType;
    internal readonly bool HidesInheritedMember;
    internal readonly IReadOnlyList<TypeParameterConstraintClauseSyntax> ImplementationConstraints;

    internal InterfaceSetupMemberMetadata(ISymbol symbol, string setupName, TypeSyntax returnType)
    {
        Symbol = symbol;
        SetupName = setupName;
        ReturnType = returnType;
        HidesInheritedMember = symbol
            .ContainingType.AllInterfaces.SelectMany(parent => parent.GetMembers(symbol.Name))
            .Any(parentMember => HidesMember(symbol, parentMember));
        ImplementationConstraints = symbol is IMethodSymbol method
            ? GetImplementationConstraints(method)
            : [];
    }

    internal InterfaceSetupMemberMetadata(in ImposterTargetMethodMetadata method)
        : this(
            method.Symbol,
            method.RequiresExplicitInterfaceImplementation ? method.UniqueName : method.Symbol.Name,
            method.MethodImposter.BuilderInterface.Syntax
        ) { }

    private static bool HidesMember(ISymbol member, ISymbol inherited)
    {
        return (member, inherited) switch
        {
            (IMethodSymbol method, IMethodSymbol parent) => method.Arity == parent.Arity
                && method.Parameters.Length == parent.Parameters.Length
                && method
                    .Parameters.Zip(parent.Parameters, SameArgumentType)
                    .All(matches => matches),
            (
                IPropertySymbol { IsIndexer: true } indexer,
                IPropertySymbol { IsIndexer: true } parent
            ) => indexer.Parameters.Length == parent.Parameters.Length
                && indexer
                    .Parameters.Zip(parent.Parameters, SameArgumentType)
                    .All(matches => matches),
            (IMethodSymbol, _) or (IPropertySymbol { IsIndexer: true }, _) => false,
            _ => true,
        };
    }

    private static bool SameArgumentType(IParameterSymbol left, IParameterSymbol right) =>
        (left.RefKind == RefKind.Out) == (right.RefKind == RefKind.Out)
        && (
            SymbolEqualityComparer.Default.Equals(left.Type, right.Type)
            || left.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                == right.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
        );

    private static List<TypeParameterConstraintClauseSyntax> GetImplementationConstraints(
        IMethodSymbol method
    )
    {
        var result = new List<TypeParameterConstraintClauseSyntax>();
        var nullableParameters = SyntaxFactoryHelper
            .ArgParameters(method.Parameters)
            .DescendantNodes()
            .OfType<NullableTypeSyntax>()
            .Select(nullable => nullable.ElementType)
            .OfType<IdentifierNameSyntax>()
            .Select(identifier => identifier.Identifier.ValueText)
            .ToArray();

        foreach (var parameter in method.TypeParameters)
        {
            TypeParameterConstraintSyntax? constraint = null;
            if (parameter.HasReferenceTypeConstraint)
            {
                // Explicit implementations may repeat class, but not class?. The actual
                // nullability constraint is inherited from the setup interface.
                constraint = ClassOrStructConstraint(SyntaxKind.ClassConstraint);
            }
            else if (parameter.HasValueTypeConstraint)
            {
                constraint = ClassOrStructConstraint(SyntaxKind.StructConstraint);
            }
            else if (nullableParameters.Contains(parameter.Name))
            {
                constraint = DefaultConstraint();
            }

            if (constraint is not null)
            {
                result.Add(
                    TypeParameterConstraintClause(SyntaxFactoryHelper.EscapeKeyword(parameter.Name))
                        .AddConstraints(constraint)
                );
            }
        }
        return result;
    }
}
