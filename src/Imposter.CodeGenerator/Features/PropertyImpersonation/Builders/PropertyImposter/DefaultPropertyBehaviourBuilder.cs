using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

internal static class DefaultPropertyBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(
        in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour
    ) =>
        new ClassDeclarationBuilder(defaultPropertyBehaviour.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(SingleVariableField(defaultPropertyBehaviour.IsOnField))
            .AddMember(SingleVariableField(defaultPropertyBehaviour.HasValueSetField))
            .AddMember(
                SingleVariableField(
                    defaultPropertyBehaviour.BackingField,
                    SyntaxKind.InternalKeyword,
                    DefaultNonNullable
                )
            )
            .Build();
}
