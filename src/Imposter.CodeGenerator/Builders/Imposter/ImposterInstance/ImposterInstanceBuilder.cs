using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Imposter.ImposterInstance;

internal static class ImposterTargetInstanceBuilder
{
    private const string ImposterFieldName = "_imposter";

    internal static ClassDeclarationSyntax Build(in ImposterGenerationContext imposterGenerationContext, string name)
    {
        var fields = GetFields(imposterGenerationContext);

        return new ClassDeclarationBuilder(name)
            .AddBaseType(SimpleBaseType(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol)))
            .AddMembers(fields)
            .AddMember(SyntaxFactoryHelper.DeclareConstructorAndInitializeMembers(name, fields))
            .AddMembers(ImposterTargetMethods(imposterGenerationContext))
            .Build();
    }

    private static IReadOnlyList<FieldDeclarationSyntax> GetFields(ImposterGenerationContext imposterGenerationContext) =>
    [
        FieldDeclaration(
            VariableDeclaration(
                IdentifierName(imposterGenerationContext.Imposter.Name),
                SingletonSeparatedList(
                    VariableDeclarator(Identifier(ImposterFieldName))
                )
            ))
    ];

    private static IEnumerable<MethodDeclarationSyntax> ImposterTargetMethods(in ImposterGenerationContext imposterGenerationContext)
    {
        return imposterGenerationContext.Imposter.Methods.Select(imposterMethod =>
        {
            var invokeMethodInvocationExpression = GetImposterWithMatchingSetupExpression(imposterMethod)
                .Dot(IdentifierName("Invoke"))
                .Call(SyntaxFactoryHelper.ArgumenstListSyntax(imposterMethod.Symbol.Parameters, includeRefKind: true));

            return new MethodDeclarationBuilder(SyntaxFactoryHelper.TypeSyntax(imposterMethod.Symbol.ReturnType), imposterMethod.Symbol.Name)
                .AddTypeParameters(SyntaxFactoryHelper.TypeParametersSyntax(imposterMethod.Symbol))
                .AddParameters(SyntaxFactoryHelper.ParameterSyntaxes(imposterMethod.Symbol.Parameters))
                .WithBody(Block(
                    imposterMethod.HasReturnValue
                        ? ReturnStatement(invokeMethodInvocationExpression)
                        : ExpressionStatement(invokeMethodInvocationExpression))
                )
                .AddModifier(Token(SyntaxKind.PublicKeyword))
                .Build();
        });

        ExpressionSyntax GetImposterWithMatchingSetupExpression(in ImposterTargetMethodMetadata method)
        {
            if (method.Symbol.IsGenericMethod)
            {
                return IdentifierName("_imposter")
                    .Dot(IdentifierName(method.MethodImposter.Collection.AsField.Name))
                    .Dot(GenericName(Identifier("GetImposterWithMatchingSetup"), method.GenericTypeArguments.AsTypeArguments()))
                    .Call(GetGetImposterWithMatchingSetupArguments(method));
            }

            return IdentifierName("_imposter")
                .Dot(IdentifierName(method.MethodImposter.AsField.Name));

            static ArgumentListSyntax? GetGetImposterWithMatchingSetupArguments(in ImposterTargetMethodMetadata method)
            {
                if (method.Parameters.HasInputParameters)
                {
                    return Argument(
                        method.Arguments.Syntax
                            .New(SyntaxFactoryHelper.ArgumenstListSyntax(method.Parameters.InputParameters, includeRefKind: false))
                        )
                        .AsSingleArgumentListSyntax();
                }

                return default;
            }
        }
    }
}