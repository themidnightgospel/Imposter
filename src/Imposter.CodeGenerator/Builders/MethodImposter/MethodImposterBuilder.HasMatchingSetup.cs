using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    private static MemberDeclarationSyntax BuildHasMatchingSetupMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "HasMatchingSetup")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameters(GetHasMatchingSetupParameters())
            .WithBody(Block(
                    ReturnStatement(
                        IdentifierName("FindMatchingSetup")
                            .Call(SyntaxFactoryHelper.ArgumentListSyntax(GetFindMatchingSetupArguments()))
                            .IsNotNull()
                    )
                )
            )
            .Build();

        IEnumerable<ParameterSyntax> GetHasMatchingSetupParameters()
        {
            if (method.HasInputParameters)
            {
                yield return SyntaxFactoryHelper.ParameterSyntax(method.Arguments.Syntax, "arguments");
            }
        }

        IEnumerable<ArgumentSyntax> GetFindMatchingSetupArguments()
        {

            if (method.HasInputParameters)
            {
                yield return Argument(IdentifierName("arguments"));
            }
        }
    }
}