using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax BuildHasMatchingInvocationImposterGroupMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "HasMatchingSetup")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(GetHasMatchingSetupParameter(method))
            .WithBody(Block(
                    ReturnStatement(
                        IdentifierName(method.MethodImposter.FindMatchingInvocationImposterGroupMethod.Name)
                            .Call(SyntaxFactoryHelper.ArgumentListSyntax(GetFindMatchingSetupArguments(method)))
                            .IsNotNull()
                    )
                )
            )
            .Build();

        static ParameterSyntax? GetHasMatchingSetupParameter(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasInputParameters
                ? SyntaxFactoryHelper.ParameterSyntax(method.Arguments.Syntax, "arguments")
                : null;
        }

        static ArgumentSyntax? GetFindMatchingSetupArguments(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasInputParameters ? Argument(IdentifierName("arguments")) : null;
        }
    }
}
