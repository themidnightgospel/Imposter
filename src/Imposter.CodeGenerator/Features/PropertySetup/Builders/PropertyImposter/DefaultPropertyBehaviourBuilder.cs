using Imposter.CodeGenerator.Features.PropertySetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertySetup.Builders.PropertyImposter;

internal static class DefaultPropertyBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(in DefaultPropertyBehaviourMetadata defaultPropertyBehaviour) =>
        new ClassDeclarationBuilder(defaultPropertyBehaviour.Name)
            .AddMember(SinglePrivateReadonlyVariableField(defaultPropertyBehaviour.IsOnField, True))
            .AddMember(SinglePrivateReadonlyVariableField(defaultPropertyBehaviour.BackingField, Default))
            .Build();
}