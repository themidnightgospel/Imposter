using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter;

internal static class DefaultPropertyBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour) =>
        new ClassDeclarationBuilder(defaultPropertyBehaviour.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(SingleVariableField(defaultPropertyBehaviour.IsOnField, SyntaxKind.InternalKeyword, True))
            .AddMember(SingleVariableField(defaultPropertyBehaviour.BackingField, SyntaxKind.InternalKeyword, Default))
            .Build();
}