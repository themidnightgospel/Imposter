using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MethodDeclarationSyntax BuildGetImposterWithMatchingInvocationImposterGroup(
        in ImposterTargetMethodMetadata method
    )
    {
        var hasMatchingMethod = method.MethodImposter.HasMatchingInvocationImposterGroupMethod;
        var parameterName = hasMatchingMethod.ArgumentsParameterName;

        var localNames = new NameSet(
            method
                .Model.TypeParameters.Select(typeParameter => typeParameter.Name)
                .Append(parameterName)
        );
        var storedImposterIdentifier = Identifier(localNames.Use("storedImposter"));
        var typedImposterName = IdentifierName(localNames.Use("typedImposter"));

        var methodBuilder = new MethodDeclarationBuilder(
            method.MethodImposter.GenericInterface.Syntax,
            MethodImposterCollectionMetadata.GetImposterWithMatchingInvocationImposterGroupMethodName
        )
            .AddParameter(GetParameter(method, parameterName))
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithTypeParameters(method.GenericTypeParameterListSyntax)
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .WithBody(
                Block(
                    ForEachStatement(
                        Var,
                        storedImposterIdentifier,
                        IdentifierName(MethodImposterCollectionMetadata.ImpostersFieldName),
                        Block(
                            LocalVariableDeclarationSyntax(
                                Var,
                                typedImposterName.Identifier.Text,
                                IdentifierName(storedImposterIdentifier)
                                    .Dot(
                                        GenericName(
                                            Identifier("As"),
                                            method.GenericTypeArguments.ToTypeArguments()
                                        )
                                    )
                                    .Call()
                            ),
                            IfStatement(
                                typedImposterName
                                    .IsNotNull()
                                    .And(
                                        typedImposterName
                                            .Dot(IdentifierName(hasMatchingMethod.Name))
                                            .Call(
                                                method.Parameters.HasInputParameters
                                                    ? Argument(IdentifierName(parameterName))
                                                        .AsSingleArgumentListSyntax()
                                                    : EmptyArgumentListSyntax
                                            )
                                    ),
                                ReturnStatement(typedImposterName)
                            )
                        )
                    ),
                    // Without a matching setup, the invocation is served by a transient imposter that
                    // records history but is not stored, so unconfigured invocations do not accumulate.
                    ReturnStatement(NewMethodImposterExpression(method))
                )
            );

        return methodBuilder.Build();

        static ParameterSyntax? GetParameter(
            in ImposterTargetMethodMetadata method,
            string parameterName
        ) =>
            method.Parameters.HasInputParameters
                ? ParameterSyntax(method.Arguments.Syntax, parameterName)
                : null;
    }
}
