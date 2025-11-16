using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Builders.PropertyImposter;

internal static class DefaultPropertyBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(
        in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour
    ) =>
        new ClassDeclarationBuilder(defaultPropertyBehaviour.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SingleVariableField(
                    defaultPropertyBehaviour.IsOnField,
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.VolatileKeyword)),
                    True
                )
            )
            .AddMember(
                SingleVariableField(
                    defaultPropertyBehaviour.HasValueSetField,
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.VolatileKeyword)),
                    False
                )
            )
            .AddMember(
                SingleVariableField(
                    defaultPropertyBehaviour.BackingField,
                    SyntaxKind.InternalKeyword,
                    DefaultNonNullable
                )
            )
            .Build();
}
