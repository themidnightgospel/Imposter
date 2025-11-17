using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal static class MethodImposterMembersBuilder
{
    internal static IEnumerable<FieldDeclarationSyntax> BuildMethodFields(
        in ImposterGenerationContext imposterGenerationContext
    ) =>
        imposterGenerationContext.Imposter.Methods.Select(method =>
            method.Symbol.IsGenericMethod
                ? SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Collection.Syntax,
                    method.MethodImposter.Collection.AsField.Name
                )
                : SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Syntax,
                    method.MethodImposter.AsField.Name
                )
        );

    internal static IEnumerable<FieldDeclarationSyntax> BuildInvocationHistoryFields(
        in ImposterGenerationContext imposterGenerationContext
    ) =>
        imposterGenerationContext.Imposter.Methods.Select(method =>
            SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                method.InvocationHistory.Collection.Syntax,
                method.InvocationHistory.Collection.AsField.Name,
                method.InvocationHistory.Collection.Syntax.New()
            )
        );

    internal static IEnumerable<MethodDeclarationSyntax> BuildMethodBuilders(
        in ImposterGenerationContext imposterGenerationContext
    )
    {
        return imposterGenerationContext.Imposter.Methods.Select(method =>
            new MethodDeclarationBuilder(
                method.MethodImposter.BuilderInterface.Syntax,
                method.Symbol.Name
            )
                .WithTypeParameters(SyntaxFactoryHelper.TypeParameterListSyntax(method.Symbol))
                .WithParameterList(SyntaxFactoryHelper.ArgParameters(method.Symbol.Parameters))
                .WithBody(
                    Block(
                        ReturnStatement(
                            method.MethodImposter.Builder.Syntax.New(
                                ArgumentList(SeparatedList(GetBuilderClassArguments(method)))
                            )
                        )
                    )
                )
                .AddModifier(Token(SyntaxKind.PublicKeyword))
                .Build()
        );

        List<ArgumentSyntax> GetBuilderClassArguments(in ImposterTargetMethodMetadata method)
        {
            var arguments = new List<ArgumentSyntax>();

            arguments.Add(
                Argument(
                    IdentifierName(
                        method.Symbol.IsGenericMethod
                            ? method.MethodImposter.Collection.AsField.Name
                            : method.MethodImposter.AsField.Name
                    )
                )
            );

            arguments.Add(
                Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name))
            );

            if (method.Parameters.HasInputParameters)
            {
                arguments.Add(Argument(SyntaxFactoryHelper.NewArgumentsCriteria(method)));
            }

            return arguments;
        }
    }
}
