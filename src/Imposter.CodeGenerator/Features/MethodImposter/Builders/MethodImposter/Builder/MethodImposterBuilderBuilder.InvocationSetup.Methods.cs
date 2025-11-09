using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static IdentifierNameSyntax CurrentInvocationImposterAccess(in ImposterTargetMethodMetadata method) =>
        IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name);

    private static MethodDeclarationSyntax BuildThrowsGenericImplementation(in ImposterTargetMethodMetadata method)
    {
        var template = new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType, method.MethodInvocationImposterGroup.ThrowsMethod.Name)
            .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
            .AddConstraintClause(TypeParameterConstraintClause("TException").AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
            .Build();

        var lambda = SyntaxFactoryHelper.Lambda(
            method.Symbol.Parameters,
            Block(
                ThrowStatement(
                    ObjectCreationExpression(IdentifierName("TException"))
                        .WithArgumentList(ArgumentList())
                )));

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ThrowsMethod.Name))
            .Call(Argument(lambda).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            method.MethodInvocationImposterGroup.ThrowsMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionInstanceImplementation(in ImposterTargetMethodMetadata method)
    {
        var template = new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType, method.MethodInvocationImposterGroup.ThrowsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionParameter))
            .Build();

        var parameterName = method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionParameter.Name;

        var lambda = SyntaxFactoryHelper.Lambda(
            method.Symbol.Parameters,
            Block(ThrowStatement(IdentifierName(parameterName))));

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ThrowsMethod.Name))
            .Call(Argument(lambda).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            method.MethodInvocationImposterGroup.ThrowsMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionGeneratorImplementation(in ImposterTargetMethodMetadata method)
    {
        var template = new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType, method.MethodInvocationImposterGroup.ThrowsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionGeneratorParameter))
            .Build();

        var parameterName = method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionGeneratorParameter.Name;

        var lambda = SyntaxFactoryHelper.Lambda(
            method.Symbol.Parameters,
            Block(
                ThrowStatement(
                    IdentifierName(parameterName)
                        .Dot(IdentifierName("Invoke"))
                        .Call(SyntaxFactoryHelper.ArgumentListSyntax(method.Symbol.Parameters))
                )));

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ThrowsMethod.Name))
            .Call(Argument(lambda).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            method.MethodInvocationImposterGroup.ThrowsMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildThrowsAsyncImplementation(in ImposterTargetMethodMetadata method)
    {
        var throwsAsyncMethod = method.MethodInvocationImposterGroup.ThrowsAsyncMethod!.Value;
        var template = new MethodDeclarationBuilder(throwsAsyncMethod.ReturnType, throwsAsyncMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(throwsAsyncMethod.ExceptionParameter))
            .Build();

        var parameterName = throwsAsyncMethod.ExceptionParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(throwsAsyncMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            throwsAsyncMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildCallbackImplementation(in ImposterTargetMethodMetadata method)
    {
        var template = new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.CallbackMethod.ReturnType, method.MethodInvocationImposterGroup.CallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter))
            .Build();

        var parameterName = method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.CallbackMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            method.MethodInvocationImposterGroup.CallbackMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildReturnsDelegateImplementation(in ImposterTargetMethodMetadata method)
    {
        var template = new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType, method.MethodInvocationImposterGroup.ReturnsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ReturnsMethod.ResultGeneratorParameter))
            .Build();

        var parameterName = method.MethodInvocationImposterGroup.ReturnsMethod.ResultGeneratorParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            method.MethodInvocationImposterGroup.ReturnsMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildReturnsValueImplementation(in ImposterTargetMethodMetadata method)
    {
        var template = new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType, method.MethodInvocationImposterGroup.ReturnsMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter))
            .Build();

        var parameterName = method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            method.MethodInvocationImposterGroup.ReturnsMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildReturnsAsyncImplementation(in ImposterTargetMethodMetadata method)
    {
        var returnsAsyncMethod = method.MethodInvocationImposterGroup.ReturnsAsyncMethod!.Value;
        var template = new MethodDeclarationBuilder(returnsAsyncMethod.ReturnType, returnsAsyncMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(returnsAsyncMethod.ValueParameter))
            .Build();

        var parameterName = returnsAsyncMethod.ValueParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(returnsAsyncMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            returnsAsyncMethod.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildUseBaseImplementationImplementation(in ImposterTargetMethodMetadata method)
    {
        var metadata = method.MethodInvocationImposterGroup.UseBaseImplementationMethod!.Value;
        var template = new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name)
            .Build();

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName("UseBaseImplementation"))
            .Call();

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            metadata.InterfaceSyntax,
            ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildThenImplementation(in ImposterTargetMethodMetadata method)
    {
        var template = new MethodDeclarationBuilder(method.MethodInvocationImposterGroup.ThenMethod.ReturnType, method.MethodInvocationImposterGroup.ThenMethod.Name)
            .Build();

        var assignment = ThisExpression()
            .Dot(IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name))
            .Assign(
                IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name)
                    .Dot(IdentifierName("AddInvocationImposter"))
                    .Call());

        return BuildExplicitInterfaceImplementation(
            method,
            template,
            method.MethodInvocationImposterGroup.ThenMethod.InterfaceSyntax,
            ExpressionStatement(assignment));
    }
}
