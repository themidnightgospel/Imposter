using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.InterfaceSetup.Builders;

internal static class InterfaceSetupViewBuilder
{
    internal static InterfaceDeclarationSyntax BuildInterface(in InterfaceSetupViewMetadata view)
    {
        var builder = new InterfaceDeclarationBuilder(view.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(view.Members.Select(member => BuildMember(member, null)));
        foreach (var baseType in view.BaseTypes)
        {
            builder.AddBaseType(baseType);
        }
        return builder.Build();
    }

    internal static IEnumerable<MemberDeclarationSyntax> BuildImplementations(
        InterfaceSetupViewMetadata view
    ) => view.Members.Select(member => BuildMember(member, view.Syntax));

    internal static MethodDeclarationSyntax BuildSelector(
        in InterfaceSetupViewMetadata view,
        string selectorName
    ) =>
        new MethodDeclarationBuilder(view.Syntax, selectorName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(
                ParameterSyntax(view.SelectorType, InterfaceSetupViewMetadata.SelectorParameterName)
            )
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();

    private static MemberDeclarationSyntax BuildMember(
        in InterfaceSetupMemberMetadata member,
        NameSyntax? viewType
    )
    {
        var specifier = viewType is null ? null : ExplicitInterfaceSpecifier(viewType);
        if (member.Symbol is IMethodSymbol method)
        {
            var parameters = ArgParameters(method.Parameters);
            var builder = new MethodDeclarationBuilder(
                member.ReturnType,
                EscapeKeyword(method.Name)
            )
                .WithTypeParameters(TypeParameterListSyntax(method))
                .WithParameterList(parameters)
                .WithExplicitInterfaceSpecifier(specifier);

            if (viewType is null)
            {
                if (member.HidesInheritedMember)
                {
                    builder.AddModifier(Token(SyntaxKind.NewKeyword));
                }
                return builder
                    .AddConstraintClauses(TypeParameterConstraintClauses(method.TypeParameters))
                    .WithSemicolon()
                    .Build();
            }

            // Explicit implementations inherit constraints, with annotations for nullable parameters.
            builder.AddConstraintClauses(member.ImplementationConstraints);

            var call = ThisExpression()
                .Dot(
                    (SimpleNameSyntax)WithMethodGenericArguments(
                        method
                            .TypeParameters.Select(parameter =>
                                IdentifierName(EscapeKeyword(parameter.Name))
                            )
                            .ToArray(),
                        EscapeKeyword(member.SetupName)
                    )
                )
                .Call(
                    ArgumentList(
                        SeparatedList(
                            parameters.Parameters.Select(parameter =>
                                Argument(IdentifierName(parameter.Identifier))
                            )
                        )
                    )
                );
            return builder.WithBody(Block(ReturnStatement(call))).Build();
        }

        var getter = AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        if (member.Symbol is IPropertySymbol { IsIndexer: true } indexer)
        {
            var parameters = ArgParameters(indexer.Parameters);
            var declaration = IndexerDeclaration(member.ReturnType)
                .WithParameterList(BracketedParameterList(parameters.Parameters))
                .WithExplicitInterfaceSpecifier(specifier);
            if (viewType is null && member.HidesInheritedMember)
            {
                declaration = declaration.AddModifiers(Token(SyntaxKind.NewKeyword));
            }
            return viewType is null
                ? declaration.WithAccessorList(AccessorList(SingletonList(getter)))
                : declaration
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            ElementAccessExpression(ThisExpression())
                                .WithArgumentList(
                                    BracketedArgumentList(
                                        SeparatedList(
                                            parameters.Parameters.Select(parameter =>
                                                Argument(IdentifierName(parameter.Identifier))
                                            )
                                        )
                                    )
                                )
                        )
                    )
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        var property = PropertyDeclaration(member.ReturnType, EscapeKeyword(member.Symbol.Name))
            .WithExplicitInterfaceSpecifier(specifier);
        if (viewType is null && member.HidesInheritedMember)
        {
            property = property.AddModifiers(Token(SyntaxKind.NewKeyword));
        }
        return viewType is null
            ? property.WithAccessorList(AccessorList(SingletonList(getter)))
            : property
                .WithExpressionBody(
                    ArrowExpressionClause(
                        ThisExpression().Dot(IdentifierName(EscapeKeyword(member.SetupName)))
                    )
                )
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }
}
