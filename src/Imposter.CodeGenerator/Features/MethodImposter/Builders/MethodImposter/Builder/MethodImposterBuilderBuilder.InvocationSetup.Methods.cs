using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static IdentifierNameSyntax CurrentInvocationImposterAccess(
        in ImposterTargetMethodMetadata method
    ) => IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name);

    private static MethodDeclarationSyntax BuildThrowsGenericImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var throwsMethod = method.MethodInvocationImposterGroup.ThrowsMethod;
        var builder = new MethodDeclarationBuilder(
            throwsMethod.ReturnType,
            throwsMethod.Name
        ).WithTypeParameters(throwsMethod.TypeParameterList);

        var throwGenericExceptionLambda = Lambda(
            method.Symbol.Parameters,
            Block(ThrowStatement(IdentifierName(throwsMethod.GenericTypeParameterName).New()))
        );

        var configureThrowsGenericCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ThrowsMethod.Name))
            .Call(Argument(throwGenericExceptionLambda).AsSingleArgumentListSyntax());

        var body = Block(
            configureThrowsGenericCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.MethodInvocationImposterGroup.ThrowsMethod.InterfaceSyntax
                )
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionInstanceImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var builder = new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType,
            method.MethodInvocationImposterGroup.ThrowsMethod.Name
        ).AddParameter(
            ParameterSyntax(method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionParameter)
        );

        var parameterName = method
            .MethodInvocationImposterGroup
            .ThrowsMethod
            .ExceptionParameter
            .Name;

        var throwProvidedExceptionLambda = Lambda(
            method.Symbol.Parameters,
            Block(ThrowStatement(IdentifierName(parameterName)))
        );

        var configureThrowsInstanceCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ThrowsMethod.Name))
            .Call(Argument(throwProvidedExceptionLambda).AsSingleArgumentListSyntax());

        var body = Block(
            configureThrowsInstanceCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.MethodInvocationImposterGroup.ThrowsMethod.InterfaceSyntax
                )
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionGeneratorImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var builder = new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType,
            method.MethodInvocationImposterGroup.ThrowsMethod.Name
        ).AddParameter(
            ParameterSyntax(
                method.MethodInvocationImposterGroup.ThrowsMethod.ExceptionGeneratorParameter
            )
        );

        var parameterName = method
            .MethodInvocationImposterGroup
            .ThrowsMethod
            .ExceptionGeneratorParameter
            .Name;

        var throwGeneratedExceptionLambda = Lambda(
            method.Symbol.Parameters,
            Block(
                ThrowStatement(
                    IdentifierName(parameterName)
                        .Dot(IdentifierName("Invoke"))
                        .Call(ArgumentListSyntax(method.Symbol.Parameters))
                )
            )
        );

        var configureThrowsGeneratorCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ThrowsMethod.Name))
            .Call(Argument(throwGeneratedExceptionLambda).AsSingleArgumentListSyntax());

        var body = Block(
            configureThrowsGeneratorCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.MethodInvocationImposterGroup.ThrowsMethod.InterfaceSyntax
                )
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildThrowsAsyncImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var throwsAsyncMethod = method.MethodInvocationImposterGroup.ThrowsAsyncMethod!.Value;
        var builder = new MethodDeclarationBuilder(
            throwsAsyncMethod.ReturnType,
            throwsAsyncMethod.Name
        ).AddParameter(ParameterSyntax(throwsAsyncMethod.ExceptionParameter));

        var parameterName = throwsAsyncMethod.ExceptionParameter.Name;

        var configureThrowsAsyncCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(throwsAsyncMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        var body = Block(
            configureThrowsAsyncCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(throwsAsyncMethod.InterfaceSyntax)
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildCallbackImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var builder = new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.CallbackMethod.ReturnType,
            method.MethodInvocationImposterGroup.CallbackMethod.Name
        ).AddParameter(
            ParameterSyntax(method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter)
        );

        var parameterName = method
            .MethodInvocationImposterGroup
            .CallbackMethod
            .CallbackParameter
            .Name;

        var configureCallbackCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.CallbackMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        var body = Block(
            configureCallbackCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.MethodInvocationImposterGroup.CallbackMethod.InterfaceSyntax
                )
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildReturnsDelegateImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var builder = new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType,
            method.MethodInvocationImposterGroup.ReturnsMethod.Name
        ).AddParameter(
            ParameterSyntax(
                method.MethodInvocationImposterGroup.ReturnsMethod.ResultGeneratorParameter
            )
        );

        var parameterName = method
            .MethodInvocationImposterGroup
            .ReturnsMethod
            .ResultGeneratorParameter
            .Name;

        var configureReturnsDelegateCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        var body = Block(
            configureReturnsDelegateCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.MethodInvocationImposterGroup.ReturnsMethod.InterfaceSyntax
                )
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildReturnsValueImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var builder = new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType,
            method.MethodInvocationImposterGroup.ReturnsMethod.Name
        ).AddParameter(
            ParameterSyntax(method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter)
        );

        var parameterName = method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter.Name;

        var configureReturnsValueCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.MethodInvocationImposterGroup.ReturnsMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        var body = Block(
            configureReturnsValueCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.MethodInvocationImposterGroup.ReturnsMethod.InterfaceSyntax
                )
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildReturnsAsyncImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var returnsAsyncMethod = method.MethodInvocationImposterGroup.ReturnsAsyncMethod!.Value;
        var builder = new MethodDeclarationBuilder(
            returnsAsyncMethod.ReturnType,
            returnsAsyncMethod.Name
        ).AddParameter(ParameterSyntax(returnsAsyncMethod.ValueParameter));

        var parameterName = returnsAsyncMethod.ValueParameter.Name;

        var configureReturnsAsyncCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(returnsAsyncMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        var body = Block(
            configureReturnsAsyncCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(returnsAsyncMethod.InterfaceSyntax)
            )
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildUseBaseImplementationImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var metadata = method.MethodInvocationImposterGroup.UseBaseImplementationMethod!.Value;
        var builder = new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name);

        var enableBaseImplementationCall = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName("UseBaseImplementation"))
            .Call();

        var body = Block(
            enableBaseImplementationCall.ToStatementSyntax(),
            ReturnStatement(ThisExpression())
        );

        return builder
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(metadata.InterfaceSyntax))
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildThenImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var builder = new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.ThenMethod.ReturnType,
            method.MethodInvocationImposterGroup.ThenMethod.Name
        );

        var assignment = ThisExpression()
            .Dot(IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name))
            .Assign(
                IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name)
                    .Dot(IdentifierName("AddInvocationImposter"))
                    .Call()
            );

        var body = Block(assignment.ToStatementSyntax(), ReturnStatement(ThisExpression()));

        return builder
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    method.MethodInvocationImposterGroup.ThenMethod.InterfaceSyntax
                )
            )
            .WithBody(body)
            .Build();
    }
}
