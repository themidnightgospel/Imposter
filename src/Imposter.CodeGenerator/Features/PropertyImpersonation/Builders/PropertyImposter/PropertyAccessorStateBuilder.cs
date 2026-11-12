using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter;

// The state the getter and the setter declare alike when they don't derive from the shared runtime types.
internal static class PropertyAccessorStateBuilder
{
    internal static IEnumerable<FieldDeclarationSyntax> Fields(
        in ImposterPropertyMetadata property,
        in PropertyAccessorStateMetadata state
    )
    {
        List<FieldDeclarationSyntax> fields = [];
        if (property.Core.KeepsValue)
        {
            fields.Add(SinglePrivateReadonlyVariableField(state.DefaultPropertyBehaviourField));
        }

        fields.Add(
            SinglePrivateReadonlyVariableField(
                PropertyAccessorStateMetadata.InvocationBehaviorField
            )
        );
        fields.Add(
            SinglePrivateReadonlyVariableField(
                PropertyAccessorStateMetadata.PropertyDisplayNameField
            )
        );
        fields.Add(SingleVariableField(state.HasConfiguredField, SyntaxKind.PrivateKeyword));
        return fields;
    }

    internal static ConstructorDeclarationSyntax Constructor(
        string className,
        in ImposterPropertyMetadata property,
        in PropertyAccessorStateMetadata state
    )
    {
        var constructor = new ConstructorWithFieldInitializationBuilder(className).WithModifiers(
            Token(SyntaxKind.InternalKeyword)
        );

        if (property.Core.KeepsValue)
        {
            constructor.AddParameter(state.DefaultPropertyBehaviourField);
        }

        return constructor
            .AddParameter(
                PropertyAccessorStateMetadata.InvocationBehaviorParameter,
                PropertyAccessorStateMetadata.InvocationBehaviorField.Name
            )
            .AddParameter(
                PropertyAccessorStateMetadata.PropertyDisplayNameParameter,
                PropertyAccessorStateMetadata.PropertyDisplayNameField.Name
            )
            .AddParameter(
                new ParameterMetadata(
                    property.Core.Locals["owner"],
                    WellKnownTypes.Imposter.Abstractions.IImposter
                ),
                InvocationStepNames.OwnerPropertyName
            )
            .Build();
    }

    internal static MethodDeclarationSyntax EnsureConfiguredMethod(
        in PropertyAccessorStateMetadata state
    ) =>
        MissingImposterBuilder.EnsureConfiguredMethod(
            state.EnsureConfiguredMethodName,
            PropertyAccessorStateMetadata.InvocationBehaviorField.Name,
            IdentifierName(state.HasConfiguredField.Name),
            ThrowMissingImposter(state)
        );

    internal static ThrowStatementSyntax ThrowMissingImposter(
        in PropertyAccessorStateMetadata state
    ) =>
        ThrowStatement(
            WellKnownTypes.Imposter.Abstractions.MissingImposterException.New(
                ArgumentListSyntax([
                    Argument(
                        IdentifierName(PropertyAccessorStateMetadata.PropertyDisplayNameField.Name)
                            .Add(state.MissingImposterSuffix.StringLiteral())
                    ),
                    Argument(
                        "get "
                            .StringLiteral()
                            .Add(
                                IdentifierName(
                                    PropertyAccessorStateMetadata.PropertyDisplayNameField.Name
                                )
                            )
                    ),
                    Argument(
                        WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String).New()
                    ),
                ])
            )
        );
}
