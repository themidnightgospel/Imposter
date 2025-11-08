using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static List<MemberDeclarationSyntax> ImplementInvocationSetupBuilderInterface(in ImposterTargetMethodMetadata method)
    {
        var implementations = new List<MemberDeclarationSyntax>
        {
            BuildThrowsGenericImplementation(method),
            BuildThrowsExceptionInstanceImplementation(method),
            BuildThrowsExceptionGeneratorImplementation(method),
            BuildCallbackImplementation(method),
        };

        if (method.HasReturnValue)
        {
            implementations.Add(BuildReturnsDelegateImplementation(method));
            implementations.Add(BuildReturnsValueImplementation(method));
        }

        if (method.MethodInvocationImposterGroup.ReturnsAsyncMethod is not null)
        {
            implementations.Add(BuildReturnsAsyncImplementation(method));
        }

        if (method.MethodInvocationImposterGroup.ThrowsAsyncMethod is not null)
        {
            implementations.Add(BuildThrowsAsyncImplementation(method));
        }

        implementations.Add(BuildThenImplementation(method));

        return implementations;
    }

    private static MethodDeclarationSyntax BuildExplicitInterfaceImplementation(
        in ImposterTargetMethodMetadata method,
        MethodDeclarationSyntax template,
        params StatementSyntax[] statements)
    {
        var bodyStatements = statements.Concat([ReturnStatement(ThisExpression())]);

        return template
            .WithModifiers(TokenList())
            .WithSemicolonToken(default)
            .WithConstraintClauses(default)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.MethodInvocationImposterGroup.Interface.Syntax))
            .WithBody(Block(bodyStatements));
    }
}
