using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static FieldDeclarationSyntax DefaultInstanceLazyInitializer(in ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper
            .SingleVariableField(
                method.InvocationSetup.Syntax,
                method.InvocationSetup.DefaultInvocationSetupField.Name,
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
        return new MethodDeclarationBuilder(method.InvocationSetup.DefaultResultGeneratorMethod.ReturnType, method.InvocationSetup.DefaultResultGeneratorMethod.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddModifierIf(method.IsAsync, () => Token(SyntaxKind.AsyncKeyword))
            .WithParameterList(method.Parameters.ParameterListSyntax)
            .WithBody(new BlockBuilder()
                .AddStatement(InvokeInitializeOutParametersWithDefaultValues(method))
                .AddStatement(method.HasReturnValue ? SyntaxFactoryHelper.ReturnDefault : null)
                .Build())
            .Build();

        static StatementSyntax? InvokeInitializeOutParametersWithDefaultValues(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasOutputParameters
                ? SyntaxFactoryHelper.InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters)
                : null;
    }
}