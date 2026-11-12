using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var fields = GetFields(method).ToArray();

        var builderClass = new ClassDeclarationBuilder(method.MethodImposter.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.MethodImposter.BuilderInterface.Syntax))
            .AddBaseType(
                SimpleBaseType(method.MethodInvocationImposterGroup.ContinuationInterface.Syntax)
            )
            .AddMembers(fields)
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Builder.InvocationImposterGroupField
                )
            )
            .AddMember(
                SingleVariableField(
                    method.MethodImposter.Builder.CurrentInvocationImposterField,
                    SyntaxKind.PrivateKeyword
                )
            );

        var constructor = new ConstructorWithFieldInitializationBuilder(
            method.MethodImposter.Builder.Name
        )
            .WithModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterWithoutField(method.MethodImposter.Builder.ImposterParameter)
            .AddParameters(fields)
            .AddStatements(BuildInvocationImposterGroupInitializationStatements(method))
            .Build();

        return builderClass
            .AddMember(constructor)
            .AddMembers(ImplementInvocationImposterGroupInterface(method))
            .AddMember(BuildCalledMethod(method))
            .AddMember(BuildCallCountMethod(method))
            .AddMembers(BuildOrderedStepMembers(method))
            .AddMember(BuildLenientMethod(method))
            .AddMember(
                new PropertyDeclarationBuilder(
                    WellKnownTypes.System.Collections.Generic.IReadOnlyList(
                        method.InvocationHistory.Syntax
                    ),
                    "Invocations"
                )
                    .WithExplicitInterfaceSpecifier(
                        ExplicitInterfaceSpecifier(method.InvocationVerifierInterface.Syntax)
                    )
                    .WithGetterBody(
                        Block(
                            ReturnStatement(
                                IdentifierName(method.InvocationHistory.Collection.AsField.Name)
                                    .Dot(method.WithGenericArguments("MatchingInvocations"))
                                    .Call(
                                        method.Parameters.HasInputParameters
                                            ? ArgumentListSyntax([
                                                Argument(
                                                    IdentifierName(
                                                        method
                                                            .MethodImposter
                                                            .Builder
                                                            .ArgumentsCriteriaField
                                                            .Name
                                                    )
                                                ),
                                            ])
                                            : EmptyArgumentListSyntax
                                    )
                            )
                        )
                    )
                    .Build()
            )
            .Build();
    }
}
