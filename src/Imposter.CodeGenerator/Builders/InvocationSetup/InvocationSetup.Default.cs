using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static FieldDeclarationSyntax DefaultInstanceLazyInitializer(in ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper
            .SingleVariableField(
                method.InvocationSetup.Syntax,
                InvocationSetupMetadata.DefaultInvocationSetupMethod.Name,
                TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.StaticKeyword)),
                method.InvocationSetup.Syntax
                    .New(method.Parameters.HasInputParameters
                        ? Argument(ObjectCreationExpression(method.ArgumentsCriteria.Syntax)
                                .WithArgumentList(SyntaxFactoryHelper.ArgAnyArgumentList(method.Symbol.Parameters))
                            )
                            .ToSingleArgumentList()
                        : SyntaxFactoryHelper.EmptyArgumentListSyntax
                    )
            );

    internal static MethodDeclarationSyntax DefaultResultGenerator(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType),
                Identifier("DefaultResultGenerator")
            )
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.InternalKeyword),
                    Token(SyntaxKind.StaticKeyword)
                )
            )
            .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters))
            .WithBody(
                new BlockBuilder()
                    .AddStatement(InvokeInitializeOutParametersWithDefaultValues(method))
                    .AddStatement(ReturnDefault(method))
                    .Build()
            );

        static StatementSyntax? ReturnDefault(in ImposterTargetMethodMetadata method)
        {
            return method.HasReturnValue ? SyntaxFactoryHelper.ReturnDefault : null;
        }

        static StatementSyntax? InvokeInitializeOutParametersWithDefaultValues(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasOutputParameters
                ? SyntaxFactoryHelper.InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters)
                : null;
        }
    }
}