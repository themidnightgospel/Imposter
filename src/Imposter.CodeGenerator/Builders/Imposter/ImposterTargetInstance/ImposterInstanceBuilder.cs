using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Imposter.ImposterTargetInstance;

internal static class ImposterTargetInstanceBuilder
{
    private const string ImposterFieldName = "_imposter";

    internal static ClassDeclarationSyntax Build(ImposterGenerationContext imposterGenerationContext, string name)
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
                IdentifierName(imposterGenerationContext.ImposterType.Name),
                SingletonSeparatedList(
                    VariableDeclarator(Identifier(ImposterFieldName))
                )
            ))
    ];

    private static IEnumerable<MethodDeclarationSyntax> ImposterTargetMethods(ImposterGenerationContext imposterGenerationContext)
    {
        return imposterGenerationContext.Methods.Select(imposterMethod =>
        {
            var invokeMethodInvocationExpression = InvocationExpression(
                IdentifierName($"{ImposterFieldName}.{imposterMethod.MethodImposter.AsField.Name}.Invoke"),
                SyntaxFactoryHelper.ArgumentSyntaxList(imposterMethod.Symbol.Parameters)
            );

            return MethodDeclaration(
                    SyntaxFactoryHelper.TypeSyntax(imposterMethod.Symbol.ReturnType),
                    Identifier(imposterMethod.Symbol.Name)
                )
                .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(imposterMethod.Symbol.Parameters))
                .WithBody(Block(
                    imposterMethod.HasReturnValue
                        ? ReturnStatement(invokeMethodInvocationExpression)
                        : ExpressionStatement(invokeMethodInvocationExpression))
                )
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
        });
    }
}