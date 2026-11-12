using System.Collections.Generic;
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
    internal static ClassDeclarationSyntax Build(in ImposterPropertyMetadata property)
    {
        if (!property.Core.IsPassedThrough)
            return BuildShared(property);
        var setter = property.SetterImposter;
        var setupType = WellKnownTypes.Imposter.Abstractions.SetterSetup;
        var tupleType = WellKnownTypes.System.Tuple(setupType, property.Core.SetterCallbackType);
        var callbacks = new FieldMetadata(
            setter.CallbacksField.Name,
            WellKnownTypes.Imposter.Abstractions.CallbackCollection(tupleType)
        );
        var mode = PropertyAccessorStateMetadata.InvocationBehaviorParameter;
        var displayName = PropertyAccessorStateMetadata.PropertyDisplayNameParameter;
        return new ClassDeclarationBuilder(setter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(
                SimpleBaseType(
                    WellKnownTypes.Imposter.Abstractions.PassThroughSetter(
                        setter.InvocationTypeSyntax
                    )
                )
            )
            .AddMember(LazyCollectionSyntaxHelper.Field(callbacks))
            .AddMember(
                new ConstructorBuilder(setter.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .AddParameter(ParameterSyntax(mode))
                    .AddParameter(ParameterSyntax(displayName))
                    .WithInitializer(
                        ConstructorInitializer(
                            SyntaxKind.BaseConstructorInitializer,
                            ArgumentListSyntax([
                                Argument(IdentifierName(mode.Name)),
                                Argument(IdentifierName(displayName.Name)),
                            ])
                        )
                    )
                    .Build()
            )
            .AddMember(
                new MethodDeclarationBuilder(setter.InvocationTypeSyntax, "CreateInvocation")
                    .AddModifier(Token(SyntaxKind.ProtectedKeyword))
                    .AddModifier(Token(SyntaxKind.OverrideKeyword))
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
            )
            .AddMember(
                new MethodDeclarationBuilder(WellKnownTypes.Void, setter.CallbackMethod.Name)
                    .AddModifier(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(ParameterSyntax(setupType, "setup"))
                    .AddParameter(ParameterSyntax(setter.CallbackMethod.CallbackParameter))
                    .WithBody(
                        Block(
                            IdentifierName("Register")
                                .Call(Argument(IdentifierName("setup")))
                                .ToStatementSyntax(),
                            LazyCollectionSyntaxHelper
                                .EnsureInitialized(callbacks.Name)
                                .Dot(IdentifierName("Add"))
                                .Call(
                                    Argument(
                                        tupleType.New(
                                            ArgumentListSyntax([
                                                Argument(IdentifierName("setup")),
                                                Argument(
                                                    IdentifierName(
                                                        setter.CallbackMethod.CallbackParameter.Name
                                                    )
                                                ),
                                            ])
                                        )
                                    )
                                )
                                .ToStatementSyntax()
                        )
                    )
                    .Build()
            )
            .AddMember(BuildPassThroughSet(property))
            .AddMember(SetterImposterBuilderBuilder.Build(property))
            .Build();
    }

    private static MethodDeclarationSyntax BuildPassThroughSet(in ImposterPropertyMetadata property)
    {
        var setter = property.SetterImposter;
        var value = IdentifierName(setter.SetMethod.ValueParameter.Name);
        var callback = IdentifierName(property.Core.Locals["setterRegistration"]);
        var statements = new List<StatementSyntax>
        {
            IdentifierName("EnsureConfigured").Call().ToStatementSyntax(),
            LazyCollectionSyntaxHelper.UnlessNull(
                ForEachStatement(
                    Var,
                    Identifier(property.Core.Locals["setterRegistration"]),
                    IdentifierName(setter.CallbacksField.Name),
                    Block(
                        callback
                            .Dot(IdentifierName("Item1"))
                            .Dot(IdentifierName("MarkUsed"))
                            .Call()
                            .ToStatementSyntax(),
                        callback
                            .Dot(IdentifierName("Item2"))
                            .Call(Argument(value))
                            .ToStatementSyntax()
                    )
                )
            ),
        };
        if (setter.SetMethod.RequiresDirectBaseAssignment)
            statements.Add(ReturnStatement(IdentifierName("ShouldUseBase").Call()));
        else if (property.Core.SetterSupportsBaseImplementation)
            statements.Add(
                IfStatement(
                    IdentifierName("ShouldUseBase").Call(),
                    Block(
                        PostfixUnaryExpression(
                                SyntaxKind.SuppressNullableWarningExpression,
                                IdentifierName(setter.SetMethod.BaseImplementationParameter.Name)
                            )
                            .Call(Argument(value))
                            .ToStatementSyntax()
                    )
                )
            );
        var builder = new MethodDeclarationBuilder(
            setter.SetMethod.ReturnType,
            setter.SetMethod.Name
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(setter.SetMethod.ValueParameter));
        if (!setter.SetMethod.RequiresDirectBaseAssignment)
            builder.AddParameter(ParameterSyntax(setter.SetMethod.BaseImplementationParameter));
        if (setter.SetMethod.RequiresDirectBaseAssignment)
            return builder.WithBody(Block(statements)).Build();
        return builder
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        Var,
                        property.Core.Locals["setterSequence"],
                        WellKnownTypes
                            .Imposter.Abstractions.InvocationSequence.Dot(IdentifierName("Next"))
                            .Call()
                    ),
                    LocalVariableDeclarationSyntax(
                        WellKnownTypes.System.Exception.ToNullableType(),
                        property.Core.Locals["setterFailure"],
                        Null
                    ),
                    TryStatement(
                        Block(statements),
                        SingletonList(
                            CatchClause()
                                .WithDeclaration(
                                    CatchDeclaration(
                                        WellKnownTypes.System.Exception,
                                        Identifier(property.Core.Locals["setterException"])
                                    )
                                )
                                .WithBlock(
                                    Block(
                                        IdentifierName(property.Core.Locals["setterFailure"])
                                            .Assign(
                                                IdentifierName(
                                                    property.Core.Locals["setterException"]
                                                )
                                            )
                                            .ToStatementSyntax(),
                                        ThrowStatement()
                                    )
                                )
                        ),
                        FinallyClause(
                            Block(
                                IdentifierName("RecordInvocation")
                                    .Call(
                                        ArgumentListSyntax([
                                            Argument(
                                                IdentifierName(
                                                    property.Core.Locals["setterSequence"]
                                                )
                                            ),
                                            Argument(
                                                IdentifierName(
                                                    property.Core.Locals["setterFailure"]
                                                )
                                            ),
                                        ])
                                    )
                                    .ToStatementSyntax()
                            )
                        )
                    )
                )
            )
            .Build();
    }

    internal static ClassDeclarationSyntax BuildPassThroughInvocationRecord(
        in ImposterPropertyMetadata property
    ) =>
        new ClassDeclarationBuilder(property.SetterImposter.InvocationTypeName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddBaseType(SimpleBaseType(WellKnownTypes.Imposter.Abstractions.InvocationRecord))
            .AddMember(
                new PropertyDeclarationBuilder(WellKnownTypes.String, "Member")
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .AddModifier(Token(SyntaxKind.OverrideKeyword))
                    .WithGetterBody(Block(ReturnStatement(property.Core.Name.StringLiteral())))
                    .Build()
            )
            .AddMember(
                new ConstructorBuilder(property.SetterImposter.InvocationTypeName)
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
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
                                Argument(IdentifierName(property.Core.Locals["setterSequence"])),
                                Argument(IdentifierName(property.Core.Locals["setterException"])),
                            ])
                        )
                    )
                    .Build()
            )
            .Build();
}
