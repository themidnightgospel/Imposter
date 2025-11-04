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
    // TODO pass method as in parameter
    private static IEnumerable<MemberDeclarationSyntax> ImplementInvocationSetupBuilderInterface(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        foreach (var interfaceMethod in invocationSetupBuilderInterface.Members.OfType<MethodDeclarationSyntax>())
        {
            var identifier = interfaceMethod.Identifier.ValueText;
            MethodDeclarationSyntax? implementation = null;

            if (identifier == method.InvocationSetup.ThrowsMethod.Name)
            {
                if (interfaceMethod.TypeParameterList is { Parameters.Count: > 0 })
                {
                    implementation = BuildThrowsGenericImplementation(method, interfaceMethod);
                }
                else if (MatchesParameter(interfaceMethod, method.InvocationSetup.ThrowsMethod.ExceptionParameter.Name))
                {
                    implementation = BuildThrowsExceptionInstanceImplementation(method, interfaceMethod);
                }
                else if (MatchesParameter(interfaceMethod, method.InvocationSetup.ThrowsMethod.ExceptionGeneratorParameter.Name))
                {
                    implementation = BuildThrowsExceptionGeneratorImplementation(method, interfaceMethod);
                }
            }
            else if (identifier == method.InvocationSetup.CallbackMethod.Name)
            {
                implementation = BuildCallbackImplementation(method, interfaceMethod);
            }
            else if (method.HasReturnValue && identifier == method.InvocationSetup.ReturnsMethod.Name)
            {
                if (MatchesParameter(interfaceMethod, method.InvocationSetup.ReturnsMethod.ResultGeneratorParameter.Name))
                {
                    implementation = BuildReturnsDelegateImplementation(method, interfaceMethod);
                }
                else if (MatchesParameter(interfaceMethod, method.InvocationSetup.ReturnsMethod.ValueParameter.Name))
                {
                    implementation = BuildReturnsValueImplementation(method, interfaceMethod);
                }
            }
            else if (identifier == method.InvocationSetup.ThenMethod.Name)
            {
                implementation = BuildThenImplementation(method, interfaceMethod);
            }

            if (implementation != null)
            {
                yield return implementation;
            }
        }
    }

    private static bool MatchesParameter(MethodDeclarationSyntax methodDeclaration, string parameterName) =>
        methodDeclaration.ParameterList.Parameters.Any(p => p.Identifier.ValueText == parameterName);

    private static ExpressionSyntax CurrentInvocationImposterAccess(in ImposterTargetMethodMetadata method) =>
        IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name);

    private static MethodDeclarationSyntax BuildThrowsGenericImplementation(in ImposterTargetMethodMetadata method, MethodDeclarationSyntax interfaceMethod)
    {
        var lambda = SyntaxFactoryHelper.Lambda(
            method.Symbol.Parameters,
            Block(
                ThrowStatement(
                    ObjectCreationExpression(IdentifierName("TException"))
                        .WithArgumentList(ArgumentList())
                )));

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.InvocationSetup.ThrowsMethod.Name))
            .Call(Argument(lambda).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(method, interfaceMethod, ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionInstanceImplementation(in ImposterTargetMethodMetadata method, MethodDeclarationSyntax interfaceMethod)
    {
        var parameterName = method.InvocationSetup.ThrowsMethod.ExceptionParameter.Name;

        var lambda = SyntaxFactoryHelper.Lambda(
            method.Symbol.Parameters,
            Block(ThrowStatement(IdentifierName(parameterName))));

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.InvocationSetup.ThrowsMethod.Name))
            .Call(Argument(lambda).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(method, interfaceMethod, ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildThrowsExceptionGeneratorImplementation(in ImposterTargetMethodMetadata method, MethodDeclarationSyntax interfaceMethod)
    {
        var parameterName = method.InvocationSetup.ThrowsMethod.ExceptionGeneratorParameter.Name;

        var lambda = SyntaxFactoryHelper.Lambda(
            method.Symbol.Parameters,
            Block(
                ThrowStatement(
                    IdentifierName(parameterName)
                        .Dot(IdentifierName("Invoke"))
                        .Call(SyntaxFactoryHelper.ArgumentListSyntax(method.Symbol.Parameters))
                )));

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.InvocationSetup.ThrowsMethod.Name))
            .Call(Argument(lambda).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(method, interfaceMethod, ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildCallbackImplementation(in ImposterTargetMethodMetadata method, MethodDeclarationSyntax interfaceMethod)
    {
        var parameterName = method.InvocationSetup.CallbackMethod.CallbackParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.InvocationSetup.CallbackMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(method, interfaceMethod, ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildReturnsDelegateImplementation(in ImposterTargetMethodMetadata method, MethodDeclarationSyntax interfaceMethod)
    {
        var parameterName = method.InvocationSetup.ReturnsMethod.ResultGeneratorParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.InvocationSetup.ReturnsMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(method, interfaceMethod, ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildReturnsValueImplementation(in ImposterTargetMethodMetadata method, MethodDeclarationSyntax interfaceMethod)
    {
        var parameterName = method.InvocationSetup.ReturnsMethod.ValueParameter.Name;

        var invocation = CurrentInvocationImposterAccess(method)
            .Dot(IdentifierName(method.InvocationSetup.ReturnsMethod.Name))
            .Call(Argument(IdentifierName(parameterName)).AsSingleArgumentListSyntax());

        return BuildExplicitInterfaceImplementation(method, interfaceMethod, ExpressionStatement(invocation));
    }

    private static MethodDeclarationSyntax BuildThenImplementation(in ImposterTargetMethodMetadata method, MethodDeclarationSyntax interfaceMethod)
    {
        var assignment = ThisExpression()
            .Dot(IdentifierName(method.MethodImposter.Builder.CurrentInvocationImposterField.Name))
            .Assign(
                IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name)
                    .Dot(IdentifierName("AddInvocationImposter"))
                    .Call());

        return BuildExplicitInterfaceImplementation(method, interfaceMethod, ExpressionStatement(assignment));
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
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InvocationSetup.Interface.Syntax))
            .WithBody(Block(bodyStatements));
    }

    internal static ClassDeclarationSyntax Build(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
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

        var constructor = SyntaxFactoryHelper.BuildConstructorAndInitializeMembers(method.MethodImposter.Builder.Name, fields);
        constructor = constructor.WithBody(
            constructor.Body.AddStatements(BuildInvocationSetupInitializationStatements(method).ToArray()));

        return builderClass
            .AddMember(constructor)
            .AddMembers(ImplementInvocationSetupBuilderInterface(method, invocationSetupBuilderInterface))
            .AddMember(BuildCalledMethod(method))
            .Build();
    }

    private static IEnumerable<StatementSyntax> BuildInvocationSetupInitializationStatements(in ImposterTargetMethodMetadata method)
    {
        var statements = new List<StatementSyntax>();

        var groupCreation = method.InvocationSetup.Syntax.New(
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
