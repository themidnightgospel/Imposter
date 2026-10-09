using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.InterfaceSetup.Metadata;
using Imposter.CodeGenerator.Models;
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
    private static readonly AccessorListSyntax GetOnlyAccessorList = AccessorList(
        SingletonList(
            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        )
    );

    internal static InterfaceDeclarationSyntax BuildInterface(in InterfaceSetupViewMetadata view)
    {
        var builder = new InterfaceDeclarationBuilder(view.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(view.Members.Select(member => BuildDeclaration(member)));
        foreach (var baseType in view.BaseTypes)
        {
            builder.AddBaseType(baseType);
        }
        return builder.Build();
    }

    internal static IEnumerable<MemberDeclarationSyntax> BuildImplementations(
        InterfaceSetupViewMetadata view
    )
    {
        var specifier = ExplicitInterfaceSpecifier(view.Syntax);

        return view.Members.Select(member => BuildImplementation(member, specifier));
    }

    internal static MethodDeclarationSyntax BuildSelector(
        in InterfaceSetupViewMetadata view,
        string selectorName
    ) =>
        new MethodDeclarationBuilder(view.Syntax, selectorName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(
                ParameterSyntax(view.SelectorType, InterfaceSetupViewMetadata.SelectorParameterName)
            )
            .WithBody(Block(ReturnThis))
            .Build();

    // The view declares each member with the setup's signature, and its getter for a property or an indexer.
    private static MemberDeclarationSyntax BuildDeclaration(
        in InterfaceSetupMemberMetadata member
    ) =>
        member.Model.Kind switch
        {
            InterfaceSetupMemberKind.Method => BuildMethodDeclaration(member),
            InterfaceSetupMemberKind.Indexer => BuildIndexerDeclaration(member),
            _ => BuildPropertyDeclaration(member),
        };

    // The imposter implements each member of the view explicitly, by forwarding it to the member's setup.
    private static MemberDeclarationSyntax BuildImplementation(
        in InterfaceSetupMemberMetadata member,
        ExplicitInterfaceSpecifierSyntax specifier
    ) =>
        member.Model.Kind switch
        {
            InterfaceSetupMemberKind.Method => BuildMethodImplementation(member, specifier),
            InterfaceSetupMemberKind.Indexer => BuildIndexerImplementation(member, specifier),
            _ => BuildPropertyImplementation(member, specifier),
        };

    private static MethodDeclarationSyntax BuildMethodDeclaration(
        in InterfaceSetupMemberMetadata member
    ) =>
        MethodSignature(member, ArgParameters(member.Model.Parameters))
            .AddModifiers(HidingModifiers(member))
            .AddConstraintClauses(TypeParameterConstraintClauses(member.Model.TypeParameters))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildMethodImplementation(
        in InterfaceSetupMemberMetadata member,
        ExplicitInterfaceSpecifierSyntax specifier
    )
    {
        var parameters = ArgParameters(member.Model.Parameters);
        var setup = ThisExpression()
            .Dot(
                (SimpleNameSyntax)WithMethodGenericArguments(
                    member
                        .Model.TypeParameters.Select(parameter =>
                            IdentifierName(EscapeKeyword(parameter.Name))
                        )
                        .ToArray(),
                    EscapeKeyword(member.SetupName)
                )
            )
            .Call(ArgumentList(ForwardedArguments(parameters)));

        // Explicit implementations inherit constraints, with annotations for nullable parameters.
        return MethodSignature(member, parameters)
            .WithExplicitInterfaceSpecifier(specifier)
            .AddConstraintClauses(member.ImplementationConstraints)
            .WithBody(Block(ReturnStatement(setup)))
            .Build();
    }

    private static MethodDeclarationBuilder MethodSignature(
        in InterfaceSetupMemberMetadata member,
        ParameterListSyntax parameters
    ) =>
        new MethodDeclarationBuilder(member.ReturnType, EscapeKeyword(member.ViewName))
            .WithTypeParameters(TypeParameterListSyntax(member.Model.TypeParameters))
            .WithParameterList(parameters);

    private static IndexerDeclarationSyntax BuildIndexerDeclaration(
        in InterfaceSetupMemberMetadata member
    ) =>
        IndexerSignature(member, ArgParameters(member.Model.Parameters))
            .WithModifiers(HidingModifiers(member))
            .WithAccessorList(GetOnlyAccessorList);

    private static IndexerDeclarationSyntax BuildIndexerImplementation(
        in InterfaceSetupMemberMetadata member,
        ExplicitInterfaceSpecifierSyntax specifier
    )
    {
        var parameters = ArgParameters(member.Model.Parameters);
        var arguments = ForwardedArguments(parameters);
        ExpressionSyntax setup = member.IsSetUpByMethod
            ? IdentifierName(member.SetupName).Call(ArgumentList(arguments))
            : ElementAccessExpression(ThisExpression())
                .WithArgumentList(BracketedArgumentList(arguments));

        return IndexerSignature(member, parameters)
            .WithExplicitInterfaceSpecifier(specifier)
            .WithExpressionBody(ArrowExpressionClause(setup))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    private static IndexerDeclarationSyntax IndexerSignature(
        in InterfaceSetupMemberMetadata member,
        ParameterListSyntax parameters
    ) =>
        IndexerDeclaration(member.ReturnType)
            .WithParameterList(BracketedParameterList(parameters.Parameters));

    private static PropertyDeclarationSyntax BuildPropertyDeclaration(
        in InterfaceSetupMemberMetadata member
    ) =>
        PropertySignature(member)
            .WithModifiers(HidingModifiers(member))
            .WithAccessorList(GetOnlyAccessorList);

    private static PropertyDeclarationSyntax BuildPropertyImplementation(
        in InterfaceSetupMemberMetadata member,
        ExplicitInterfaceSpecifierSyntax specifier
    ) =>
        PropertySignature(member)
            .WithExplicitInterfaceSpecifier(specifier)
            .WithExpressionBody(
                ArrowExpressionClause(
                    ThisExpression().Dot(IdentifierName(EscapeKeyword(member.SetupName)))
                )
            )
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

    private static PropertyDeclarationSyntax PropertySignature(
        in InterfaceSetupMemberMetadata member
    ) => PropertyDeclaration(member.ReturnType, EscapeKeyword(member.Model.Name));

    // A view member that hides one the view inherits declares itself `new`.
    private static SyntaxTokenList HidingModifiers(in InterfaceSetupMemberMetadata member) =>
        member.Model.HidesInheritedMember ? TokenList(Token(SyntaxKind.NewKeyword)) : default;

    private static SeparatedSyntaxList<ArgumentSyntax> ForwardedArguments(
        ParameterListSyntax parameters
    ) =>
        SeparatedList(
            parameters.Parameters.Select(parameter =>
                Argument(IdentifierName(parameter.Identifier))
            )
        );
}
