using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(invocation));
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

        return BuildExplicitInterfaceImplementation(method, template, ExpressionStatement(assignment));
    }

    private static MethodDeclarationSyntax BuildExplicitInterfaceImplementation(
        in ImposterTargetMethodMetadata method,
        MethodDeclarationSyntax template,
        params StatementSyntax[] statements)
    {
        var bodyStatements = statements.Concat(new[] { ReturnStatement(ThisExpression()) });

        return template
            .WithModifiers(TokenList())
            .WithSemicolonToken(default)
            .WithConstraintClauses(default)
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.MethodInvocationImposterGroup.Interface.Syntax))
            .WithBody(Block(bodyStatements));
    }

    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var fields = GetFields(method).ToArray();

        var builderClass = new ClassDeclarationBuilder(method.MethodImposter.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.MethodImposter.BuilderInterface.Syntax))
            .AddMembers(fields)
            .AddMember(SinglePrivateReadonlyVariableField(method.MethodImposter.Builder.InvocationImposterGroupField.Type, method.MethodImposter.Builder.InvocationImposterGroupField.Name))
            .AddMember(SingleVariableField(
                method.MethodImposter.Builder.CurrentInvocationImposterField.Type,
                method.MethodImposter.Builder.CurrentInvocationImposterField.Name,
                TokenList(Token(SyntaxKind.PrivateKeyword))));

        var constructor = BuildConstructorAndInitializeMembers(method.MethodImposter.Builder.Name, fields);
        constructor = constructor.WithBody(
            constructor.Body!.AddStatements(BuildInvocationSetupInitializationStatements(method).ToArray()));

        return builderClass
            .AddMember(constructor)
            .AddMembers(ImplementInvocationSetupBuilderInterface(method))
            .AddMember(BuildCalledMethod(method))
            .Build();
    }

    private static List<StatementSyntax> BuildInvocationSetupInitializationStatements(in ImposterTargetMethodMetadata method)
    {
        var statements = new List<StatementSyntax>();

        var groupCreation = method.MethodInvocationImposterGroup.Syntax.New(
            method.Parameters.HasInputParameters
                ? Argument(IdentifierName(method.MethodImposter.Builder.ArgumentsCriteriaField.Name)).AsSingleArgumentListSyntax()
                : SyntaxFactoryHelper.EmptyArgumentListSyntax);

        statements.Add(
            ThisExpression()
                .Dot(IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name))
                .Assign(groupCreation)
                .ToStatementSyntax());

        ExpressionSyntax methodImposterAccess;

        if (method.Symbol.IsGenericMethod)
        {
            var addNewCall = IdentifierName(method.MethodImposter.Builder.ImposterCollectionField.Name)
                .Dot(GenericName(Identifier("AddNew"), method.GenericTypeArguments.AsTypeArguments()))
                .Call();

            statements.Add(
                LocalDeclarationStatement(
                    VariableDeclaration(method.MethodImposter.Syntax)
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier("methodImposter"))
                                    .WithInitializer(EqualsValueClause(addNewCall))
                            )
                        )
                ));

            methodImposterAccess = IdentifierName("methodImposter");
        }
        else
        {
            methodImposterAccess = IdentifierName(method.MethodImposter.Builder.MethodImposterField.Name);
        }

        statements.Add(
            methodImposterAccess
                .Dot(IdentifierName(method.MethodImposter.InvocationImpostersField.Name))
                .Dot(IdentifierName("Push"))
                .Call(Argument(IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name)).AsSingleArgumentListSyntax())
                .ToStatementSyntax());

        statements.Add(
            ThisExpression()
                .Dot(IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name))
                .Assign(
                    ThisExpression()
                        .Dot(IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name))
                        .Dot(IdentifierName("AddInvocationImposter"))
                        .Call()
                )
                .ToStatementSyntax());

        return statements;
    }
}
