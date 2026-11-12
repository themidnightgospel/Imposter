using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;

internal static partial class SetterImposterBuilder
{
    private static ClassDeclarationSyntax BuildShared(in ImposterPropertyMetadata property)
    {
        var setter = property.SetterImposter;
        var builder = new ClassDeclarationBuilder(setter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(property.SharedRuntime.SetterType))
            .AddMember(SharedPropertyBuilder.BuildConstructor(property, setter.Name))
            .AddMember(
                new MethodDeclarationBuilder(
                    WellKnownTypes.Imposter.Abstractions.PropertySetterInvocation(
                        property.Core.NullableAwareValueTypeSyntax
                    ),
                    "CreateInvocation"
                )
                    .AddModifier(Token(SyntaxKind.ProtectedKeyword))
                    .AddModifier(Token(SyntaxKind.OverrideKeyword))
                    .AddParameter(
                        ParameterSyntax(
                            property.Core.NullableAwareValueTypeSyntax,
                            property.Core.Locals["setterValue"]
                        )
                    )
                    .AddParameter(
                        ParameterSyntax(WellKnownTypes.Long, property.Core.Locals["setterSequence"])
                    )
                    .AddParameter(
                        ParameterSyntax(
                            WellKnownTypes.System.Exception.ToNullableType(),
                            property.Core.Locals["setterException"]
                        )
                    )
                    .WithBody(
                        Block(
                            ReturnStatement(
                                setter.InvocationTypeSyntax.New(
                                    ArgumentListSyntax([
                                        Argument(
                                            CastExpression(
                                                property.Core.NullableAwareKeptTypeSyntax,
                                                IdentifierName(property.Core.Locals["setterValue"])
                                            )
                                        ),
                                        Argument(
                                            IdentifierName(property.Core.Locals["setterSequence"])
                                        ),
                                        Argument(
                                            IdentifierName(property.Core.Locals["setterException"])
                                        ),
                                    ])
                                )
                            )
                        )
                    )
                    .Build()
            );

        if (!property.Core.UsesSharedRuntime)
            builder.AddMember(
                new MethodDeclarationBuilder(
                    property.Core.NullableAwareValueTypeSyntax,
                    "CaptureInvocationValue"
                )
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .AddModifier(Token(SyntaxKind.OverrideKeyword))
                    .AddParameter(
                        ParameterSyntax(
                            property.Core.NullableAwareValueTypeSyntax,
                            property.Core.Locals["setterValue"]
                        )
                    )
                    .WithBody(
                        Block(
                            ReturnStatement(
                                CastExpression(
                                    property.Core.NullableAwareValueTypeSyntax,
                                    IdentifierName(property.Core.Locals["setterValue"])
                                        .Dot(IdentifierName("Clone"))
                                        .Call()
                                )
                            )
                        )
                    )
                    .Build()
            );

        if (setter.SetMethod.RequiresDirectBaseAssignment)
        {
            builder.AddMember(
                new MethodDeclarationBuilder(setter.SetMethod.ReturnType, setter.SetMethod.Name)
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(ParameterSyntax(setter.SetMethod.ValueParameter))
                    .WithBody(
                        Block(
                            ReturnStatement(
                                BaseExpression()
                                    .Dot(
                                        IdentifierName(
                                            SharedPropertyMetadata.RecordAndShouldCallBase
                                        )
                                    )
                                    .Call(
                                        Argument(
                                            IdentifierName(setter.SetMethod.ValueParameter.Name)
                                        )
                                    )
                            )
                        )
                    )
                    .Build()
            );
        }

        return builder.AddMember(SetterImposterBuilderBuilder.Build(property)).Build();
    }

    internal static ClassDeclarationSyntax BuildInvocationRecord(
        in ImposterPropertyMetadata property
    ) =>
        new ClassDeclarationBuilder(property.SetterImposter.InvocationTypeName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(
                SimpleBaseType(
                    WellKnownTypes.Imposter.Abstractions.PropertySetterInvocation(
                        property.Core.NullableAwareValueTypeSyntax
                    )
                )
            )
            .AddMember(
                new PropertyDeclarationBuilder(WellKnownTypes.String, "Member")
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .AddModifier(Token(SyntaxKind.OverrideKeyword))
                    .WithGetterBody(Block(ReturnStatement(property.Core.Name.StringLiteral())))
                    .Build()
            )
            .AddMember(
                new PropertyDeclarationBuilder(
                    property.Core.UsesSharedRuntime
                        ? property.Core.NullableAwareValueTypeSyntax
                        : WellKnownTypes.System.ReadOnlyMemory(
                            (
                                (ArrayTypeSyntax)property.Core.NullableAwareValueTypeSyntax
                            ).ElementType
                        ),
                    "Value"
                )
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithGetterBody(Block(ReturnStatement(IdentifierName("StoredValue"))))
                    .Build()
            )
            .AddMember(
                new ConstructorBuilder(property.SetterImposter.InvocationTypeName)
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .AddParameter(
                        ParameterSyntax(
                            property.Core.NullableAwareValueTypeSyntax,
                            property.Core.Locals["setterValue"]
                        )
                    )
                    .AddParameter(
                        ParameterSyntax(WellKnownTypes.Long, property.Core.Locals["setterSequence"])
                    )
                    .AddParameter(
                        ParameterSyntax(
                            WellKnownTypes.System.Exception.ToNullableType(),
                            property.Core.Locals["setterException"]
                        )
                    )
                    .WithInitializer(
                        ConstructorInitializer(
                            SyntaxKind.BaseConstructorInitializer,
                            ArgumentListSyntax([
                                Argument(
                                    CastExpression(
                                        property.Core.NullableAwareKeptTypeSyntax,
                                        IdentifierName(property.Core.Locals["setterValue"])
                                    )
                                ),
                                Argument(IdentifierName(property.Core.Locals["setterSequence"])),
                                Argument(IdentifierName(property.Core.Locals["setterException"])),
                            ])
                        )
                    )
                    .Build()
            )
            .Build();
}
