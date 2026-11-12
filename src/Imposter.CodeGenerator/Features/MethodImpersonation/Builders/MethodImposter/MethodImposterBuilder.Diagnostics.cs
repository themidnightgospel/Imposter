using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax BuildConfiguredSetupsMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var listType = WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String);
        var setups = IdentifierName(method.Locals["setups"]);
        var group = IdentifierName(method.Locals["group"]);
        return new MethodDeclarationBuilder(
            listType,
            method.MethodImposter.ConfiguredSetupsMethodName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(Var, method.Locals["setups"], listType.New()),
                    ForEachStatement(
                        Var,
                        group.Identifier,
                        IdentifierName(method.MethodImposter.InvocationImposterGroupsField.Name),
                        Block(
                            IfStatement(
                                group.Dot(
                                    IdentifierName(
                                        method
                                            .MethodInvocationImposterGroup
                                            .HasConfiguredOutcomeFieldName
                                    )
                                ),
                                setups
                                    .Dot(IdentifierName("Add"))
                                    .Call(Argument(group.Dot(IdentifierName("ToString")).Call()))
                                    .ToStatementSyntax()
                            )
                        )
                    ),
                    ReturnStatement(setups)
                )
            )
            .Build();
    }
}
