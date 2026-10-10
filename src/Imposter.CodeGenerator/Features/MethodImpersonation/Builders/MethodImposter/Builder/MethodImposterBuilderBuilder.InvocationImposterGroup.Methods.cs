using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MethodDeclarationSyntax BuildThrowsGenericImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var throws = method.MethodInvocationImposterGroup.ThrowsMethod;
        var throwNewException = Lambda(
            method.Parameters.ParameterListSyntaxIncludingNullable,
            Block(ThrowStatement(IdentifierName(throws.ExceptionTypeParameter.Name).New()))
        );

        return new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
            .WithTypeParameters(throws.ExceptionTypeParameter.TypeParameterList)
            .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(method, throws.Name, Argument(throwNewException))
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionInstanceImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var throws = method.MethodInvocationImposterGroup.ThrowsMethod;
        var throwException = Lambda(
            method.Parameters.ParameterListSyntaxIncludingNullable,
            Block(ThrowStatement(IdentifierName(throws.ExceptionParameter.Name)))
        );

        return new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
            .AddParameter(ParameterSyntax(throws.ExceptionParameter))
            .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(method, throws.Name, Argument(throwException))
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionGeneratorImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var throws = method.MethodInvocationImposterGroup.ThrowsMethod;
        var throwGeneratedException = Lambda(
            method.Parameters.ParameterListSyntaxIncludingNullable,
            Block(
                ThrowStatement(
                    IdentifierName(throws.ExceptionGeneratorParameter.Name)
                        .Dot(IdentifierName("Invoke"))
                        .Call(ArgumentListSyntax(method.Parameters.AllParameters))
                )
            )
        );

        return new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
            .AddParameter(ParameterSyntax(throws.ExceptionGeneratorParameter))
            .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(
                    method,
                    throws.Name,
                    Argument(throwGeneratedException)
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildThrowsAsyncImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var throwsAsync = method.MethodInvocationImposterGroup.ThrowsAsyncMethod!.Value;

        return new MethodDeclarationBuilder(throwsAsync.ReturnType, throwsAsync.Name)
            .AddParameter(ParameterSyntax(throwsAsync.ExceptionParameter))
            .WithExplicitInterfaceSpecifier(throwsAsync.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(
                    method,
                    throwsAsync.Name,
                    throwsAsync.ExceptionParameter.Name.ToArgument()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildCallbackImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var callback = method.MethodInvocationImposterGroup.CallbackMethod;

        return new MethodDeclarationBuilder(callback.ReturnType, callback.Name)
            .AddParameter(ParameterSyntax(callback.CallbackParameter))
            .WithExplicitInterfaceSpecifier(callback.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(
                    method,
                    callback.Name,
                    callback.CallbackParameter.Name.ToArgument()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildReturnsDelegateImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var returns = method.MethodInvocationImposterGroup.ReturnsMethod;

        return new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
            .AddParameter(ParameterSyntax(returns.ResultGeneratorParameter))
            .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(
                    method,
                    returns.Name,
                    returns.ResultGeneratorParameter.Name.ToArgument()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildReturnsValueImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var returns = method.MethodInvocationImposterGroup.ReturnsMethod;

        return new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
            .AddParameter(ParameterSyntax(returns.ValueParameter))
            .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(
                    method,
                    returns.Name,
                    Argument(
                        RuntimeValue(
                            IdentifierName(returns.ValueParameter.Name),
                            method.Model.ReturnType.Type
                        )
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildReturnsAsyncImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var returnsAsync = method.MethodInvocationImposterGroup.ReturnsAsyncMethod!.Value;

        return new MethodDeclarationBuilder(returnsAsync.ReturnType, returnsAsync.Name)
            .AddParameter(ParameterSyntax(returnsAsync.ValueParameter))
            .WithExplicitInterfaceSpecifier(returnsAsync.InterfaceSyntax)
            .WithBody(
                ForwardToCurrentInvocationImposter(
                    method,
                    returnsAsync.Name,
                    Argument(
                        RuntimeValue(
                            IdentifierName(returnsAsync.ValueParameter.Name),
                            method.Model.ReturnType.AwaitableResultType!
                        )
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildUseBaseImplementationImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var useBaseImplementation = method
            .MethodInvocationImposterGroup
            .UseBaseImplementationMethod!
            .Value;

        return new MethodDeclarationBuilder(
            useBaseImplementation.ReturnType,
            useBaseImplementation.Name
        )
            .WithExplicitInterfaceSpecifier(useBaseImplementation.InterfaceSyntax)
            .WithBody(ForwardToCurrentInvocationImposter(method, useBaseImplementation.Name))
            .Build();
    }

    // Passes the arguments on to the current invocation imposter's method of this name, then returns this for chaining.
    private static BlockSyntax ForwardToCurrentInvocationImposter(
        in ImposterTargetMethodMetadata method,
        string name,
        params ArgumentSyntax[] arguments
    ) =>
        Block(
            IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name)
                .Dot(IdentifierName(name))
                .Call(arguments)
                .ToStatementSyntax(),
            ReturnThis
        );

    private static MethodDeclarationSyntax BuildThenImplementation(
        in ImposterTargetMethodMetadata method
    )
    {
        var then = method.MethodInvocationImposterGroup.ThenMethod;
        var invocationImposterGroup = IdentifierName(
            method.MethodImposter.Builder.InvocationImposterGroupField.Name
        );

        return new MethodDeclarationBuilder(then.ReturnType, then.Name)
            .WithExplicitInterfaceSpecifier(then.InterfaceSyntax)
            .WithBody(
                Block(AdvanceToNewInvocationImposter(method, invocationImposterGroup), ReturnThis)
            )
            .Build();
    }

    // Adds a new invocation imposter to the group, which the setup methods configure from then on.
    private static ExpressionStatementSyntax AdvanceToNewInvocationImposter(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax invocationImposterGroup
    ) =>
        ThisExpression()
            .Dot(IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name))
            .Assign(
                invocationImposterGroup
                    .Dot(
                        IdentifierName(
                            MethodInvocationImposterGroupMetadata.AddInvocationImposterMethodName
                        )
                    )
                    .Call()
            )
            .ToStatementSyntax();
}
