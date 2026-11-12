using System;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

internal static class InvocationStepMembersBuilder
{
    internal static MemberDeclarationSyntax[] Build(
        ExpressionSyntax owner,
        ExpressionSyntax description,
        NameSet names,
        Func<IdentifierNameSyntax, IdentifierNameSyntax, ExpressionSyntax> findAfter
    )
    {
        var sequence = IdentifierName(names.Use("sequence"));
        var match = IdentifierName(names.Use("match"));
        return
        [
            Property(
                WellKnownTypes.Imposter.Abstractions.IImposter,
                InvocationStepNames.ImposterPropertyName,
                owner
            ),
            Property(
                WellKnownTypes.String,
                InvocationStepNames.DescriptionPropertyName,
                description
            ),
            new MethodDeclarationBuilder(
                WellKnownTypes.Bool,
                InvocationStepNames.TryFindAfterMethodName
            )
                .WithExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IInvocationStep
                )
                .AddParameter(ParameterSyntax(WellKnownTypes.Long, sequence.Identifier.Text))
                .AddParameter(
                    ParameterSyntax(
                            WellKnownTypes.Imposter.Abstractions.InvocationMatch,
                            match.Identifier.Text
                        )
                        .WithModifiers(TokenList(Token(SyntaxKind.OutKeyword)))
                )
                .WithBody(Block(ReturnStatement(findAfter(sequence, match))))
                .Build(),
        ];
    }

    private static PropertyDeclarationSyntax Property(
        TypeSyntax type,
        string name,
        ExpressionSyntax value
    ) =>
        new PropertyDeclarationBuilder(type, name)
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(WellKnownTypes.Imposter.Abstractions.IInvocationStep)
            )
            .WithGetterBody(Block(ReturnStatement(value)))
            .Build();
}
