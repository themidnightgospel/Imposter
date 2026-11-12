using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

internal static class SharedPropertyBuilder
{
    internal static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterPropertyMetadata property,
        string className,
        bool withOwner = false
    )
    {
        var mode = PropertyAccessorStateMetadata.InvocationBehaviorParameter;
        var displayName = PropertyAccessorStateMetadata.PropertyDisplayNameParameter;
        var constructor = new ConstructorBuilder(className).WithModifiers(
            TokenList(Token(SyntaxKind.InternalKeyword))
        );
        var arguments = new List<ArgumentSyntax>();
        if (property.Core.KeepsValue)
        {
            var field = property.ImposterBuilder.DefaultPropertyBehaviourField;
            constructor.AddParameter(ParameterSyntax(field.Type, field.Name));
            arguments.Add(Argument(IdentifierName(field.Name)));
        }
        else
        {
            arguments.Add(Argument(Null));
        }

        constructor.AddParameter(ParameterSyntax(mode)).AddParameter(ParameterSyntax(displayName));
        arguments.Add(Argument(IdentifierName(mode.Name)));
        arguments.Add(Argument(IdentifierName(displayName.Name)));

        if (withOwner)
        {
            constructor.AddParameter(
                ParameterSyntax(
                    WellKnownTypes.Imposter.Abstractions.IImposter,
                    property.Core.Locals["owner"]
                )
            );
            arguments.Add(Argument(IdentifierName(property.Core.Locals["owner"])));
        }

        return constructor
            .WithInitializer(
                ConstructorInitializer(
                    SyntaxKind.BaseConstructorInitializer,
                    ArgumentListSyntax(arguments)
                )
            )
            .Build();
    }
}
