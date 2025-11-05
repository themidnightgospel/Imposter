using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

using GetterReturnsMetadata = Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface.ReturnsMethodMetadata;
using GetterThrowsMetadata = Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface.ThrowsMethodMetadata;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerImposterBuilderBuilder
{
    private const string DefaultBehaviourParameterName = "defaultBehaviour";
    private const string InvocationBehaviorParameterName = "invocationBehavior";
    private const string PropertyDisplayNameParameterName = "propertyDisplayName";

    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var classBuilder = new ClassDeclarationBuilder(indexer.Builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.Builder.DefaultBehaviourField, indexer.DefaultIndexerBehaviour.TypeSyntax.New()))
            .AddMember(indexer.Core.HasGetter ? SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.Builder.GetterImposterField) : null)
            .AddMember(indexer.Core.HasSetter ? SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.Builder.SetterImposterField) : null)
            .AddMember(DefaultIndexerBehaviourBuilder.Build(indexer))
            .AddMember(BuildConstructor(indexer))
            .AddMember(indexer.Core.HasGetter ? BuildCreateGetterMethod(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildCreateSetterMethod(indexer) : null)
            .AddMember(BuildInvocationBuilder(indexer))
            .AddMember(indexer.Core.HasGetter ? BuildGetForwarder(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildSetForwarder(indexer) : null)
            .AddMember(indexer.Core.HasGetter ? BuildGetterImposter(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildSetterImposter(indexer) : null);

        return classBuilder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterIndexerMetadata indexer)
    {
        var invocationBehaviorParameter = Parameter(Identifier("invocationBehavior"))
            .WithType(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior);
        var propertyDisplayNameParameter = Parameter(Identifier("propertyDisplayName"))
            .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)));

        var getterInitialization = indexer.Core.HasGetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.GetterImposterField.Name))
                .Assign(
                    IdentifierName("GetterImposter")
                        .New(
                            SyntaxFactoryHelper.ArgumentListSyntax(
                                new[]
                                {
                                    Argument(IdentifierName(indexer.Builder.DefaultBehaviourField.Name)),
                                    Argument(IdentifierName(invocationBehaviorParameter.Identifier.Text)),
                                    Argument(IdentifierName(propertyDisplayNameParameter.Identifier.Text))
                                })))
                .ToStatementSyntax()
            : null;

        var setterInitialization = indexer.Core.HasSetter
            ? ThisExpression()
                .Dot(IdentifierName(indexer.Builder.SetterImposterField.Name))
                .Assign(
                    IdentifierName("SetterImposter")
                        .New(
                            SyntaxFactoryHelper.ArgumentListSyntax(
                                new[]
                                {
                                    Argument(IdentifierName(indexer.Builder.DefaultBehaviourField.Name)),
                                    Argument(IdentifierName(invocationBehaviorParameter.Identifier.Text)),
                                    Argument(IdentifierName(propertyDisplayNameParameter.Identifier.Text))
                                })))
                .ToStatementSyntax()
            : null;

        var bodyBuilder = new BlockBuilder()
            .AddStatement(getterInitialization)
            .AddStatement(setterInitialization);

        return new ConstructorBuilder(indexer.Builder.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(invocationBehaviorParameter)
            .AddParameter(propertyDisplayNameParameter)
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildCreateGetterMethod(in ImposterIndexerMetadata indexer)
    {
        var body = Block(
            ReturnStatement(
                QualifiedName(IdentifierName("GetterImposter"), IdentifierName("Builder"))
                    .New(
                        SyntaxFactoryHelper.ArgumentListSyntax(
                            new[]
                            {
                                Argument(IdentifierName(indexer.Builder.GetterImposterField.Name)),
                                Argument(IdentifierName("criteria"))
                            }))));

        return new MethodDeclarationBuilder(indexer.GetterBuilderInterface.TypeSyntax, "CreateGetter")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(Parameter(Identifier("criteria")).WithType(indexer.ArgumentsCriteria.TypeSyntax))
            .WithBody(body)
            .Build();
    }

    private static MethodDeclarationSyntax BuildCreateSetterMethod(in ImposterIndexerMetadata indexer)
    {
        var bodyBuilder = new BlockBuilder();

        if (indexer.Core.HasSetter)
        {
            bodyBuilder.AddStatement(
                IdentifierName(indexer.Builder.SetterImposterField.Name)
                    .Dot(IdentifierName("MarkConfigured"))
                    .Call()
                    .ToStatementSyntax());
        }

        bodyBuilder.AddStatement(
            ReturnStatement(
                QualifiedName(IdentifierName("SetterImposter"), IdentifierName("Builder"))
                    .New(
                        SyntaxFactoryHelper.ArgumentListSyntax(
                            new[]
                            {
                                Argument(IdentifierName(indexer.Builder.SetterImposterField.Name)),
                                Argument(IdentifierName("criteria"))
                            }))));

        return new MethodDeclarationBuilder(indexer.SetterBuilderInterface.TypeSyntax, "CreateSetter")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(Parameter(Identifier("criteria")).WithType(indexer.ArgumentsCriteria.TypeSyntax))
            .WithBody(bodyBuilder.Build())
            .Build();
    }

    private static ClassDeclarationSyntax BuildInvocationBuilder(in ImposterIndexerMetadata indexer)
    {
        var invocationBuilder = new ClassDeclarationBuilder("InvocationBuilder")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.BuilderInterface.TypeSyntax))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.Builder.TypeSyntax, "_builder"))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.ArgumentsCriteria.TypeSyntax, "_criteria"))
            .AddMember(new ConstructorBuilder("InvocationBuilder")
                .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                .AddParameter(Parameter(Identifier("builder")).WithType(indexer.Builder.TypeSyntax))
                .AddParameter(Parameter(Identifier("criteria")).WithType(indexer.ArgumentsCriteria.TypeSyntax))
                .WithBody(new BlockBuilder()
                    .AddStatement(ThisExpression().Dot(IdentifierName("_builder")).Assign(IdentifierName("builder")).ToStatementSyntax())
                    .AddStatement(ThisExpression().Dot(IdentifierName("_criteria")).Assign(IdentifierName("criteria")).ToStatementSyntax())
                    .Build())
                .Build());

        if (indexer.Core.HasGetter)
        {
            invocationBuilder = invocationBuilder.AddMember(
                new MethodDeclarationBuilder(indexer.GetterBuilderInterface.TypeSyntax, indexer.BuilderInterface.GetterMethod.Name)
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithBody(Block(ReturnStatement(
                        IdentifierName("_builder")
                            .Dot(IdentifierName("CreateGetter"))
                            .Call(Argument(IdentifierName("_criteria"))))))
                    .Build());
        }

        if (indexer.Core.HasSetter)
        {
            invocationBuilder = invocationBuilder.AddMember(
                new MethodDeclarationBuilder(indexer.SetterBuilderInterface.TypeSyntax, indexer.BuilderInterface.SetterMethod.Name)
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithBody(Block(ReturnStatement(
                        IdentifierName("_builder")
                            .Dot(IdentifierName("CreateSetter"))
                            .Call(Argument(IdentifierName("_criteria"))))))
                    .Build());
        }

        return invocationBuilder.Build();
    }

    private static MethodDeclarationSyntax BuildGetForwarder(in ImposterIndexerMetadata indexer)
        => new MethodDeclarationBuilder(indexer.Core.TypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(indexer.Core.Parameters.Select(parameter => parameter.ParameterSyntax).ToArray())
            .WithBody(Block(ReturnStatement(
                IdentifierName(indexer.Builder.GetterImposterField.Name)
                    .Dot(IdentifierName("Get"))
                    .Call(SyntaxFactoryHelper.ArgumentListSyntax(
                        indexer.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))))))))
            .Build();

    private static MethodDeclarationSyntax BuildSetForwarder(in ImposterIndexerMetadata indexer)
    {
        var parameters = indexer.Core.Parameters
            .Select(parameter => parameter.ParameterSyntax)
            .Concat(new[] { Parameter(Identifier("value")).WithType(indexer.Core.TypeSyntax) })
            .ToArray();

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Set")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters)
            .WithBody(Block(
                IdentifierName(indexer.Builder.SetterImposterField.Name)
                    .Dot(IdentifierName("Set"))
                    .Call(SyntaxFactoryHelper.ArgumentListSyntax(
                        indexer.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))).Concat(new[] { Argument(IdentifierName("value")) })))
                    .ToStatementSyntax()))
            .Build();
    }

    private static ClassDeclarationSyntax BuildGetterImposter(in ImposterIndexerMetadata indexer)
    {
        var getterInvocationType = IdentifierName(indexer.GetterImplementation.Invocation.Name);
        var getterImposterBuilder = new ClassDeclarationBuilder("GetterImposter")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.DefaultIndexerBehaviour.TypeSyntax, indexer.GetterImplementation.DefaultBehaviourField.Name))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(getterInvocationType),
                indexer.GetterImplementation.SetupsField.Name,
                WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(getterInvocationType).New()))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(indexer.ArgumentsCriteria.TypeSyntax, getterInvocationType),
                indexer.GetterImplementation.SetupLookupField.Name,
                WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(indexer.ArgumentsCriteria.TypeSyntax, getterInvocationType).New()))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(indexer.Arguments.TypeSyntax),
                indexer.GetterImplementation.InvocationHistoryField.Name,
                WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(indexer.Arguments.TypeSyntax).New()))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior, indexer.GetterImplementation.InvocationBehaviorField.Name))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(PredefinedType(Token(SyntaxKind.StringKeyword)), indexer.GetterImplementation.PropertyDisplayNameField.Name))
            .AddMember(SyntaxFactoryHelper.SingleVariableField(WellKnownTypes.Bool, indexer.GetterImplementation.HasConfiguredReturnField.Name, SyntaxKind.PrivateKeyword))
            .AddMember(BuildGetterConstructor(indexer))
            .AddMember(BuildGetterGetMethod(indexer))
            .AddMember(BuildFindMatchingSetupMethod(indexer))
            .AddMember(BuildGetOrCreateMethod(indexer))
            .AddMember(BuildGetterCalledMethod(indexer))
            .AddMember(PropertyDeclaration(PredefinedType(Token(SyntaxKind.StringKeyword)), Identifier("PropertyDisplayName"))
                .AddModifiers(Token(SyntaxKind.InternalKeyword))
                .WithExpressionBody(ArrowExpressionClause(IdentifierName(indexer.GetterImplementation.PropertyDisplayNameField.Name)))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
            .AddMember(BuildMarkReturnConfiguredMethod(indexer))
            .AddMember(BuildEnsureGetterConfiguredMethod(indexer))
            .AddMember(BuildGetterBuilder(indexer))
            .AddMember(BuildGetterInvocationImposter(indexer));

        return getterImposterBuilder.Build();
    }

    private static ClassDeclarationSyntax BuildGetterBuilder(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.GetterImplementation.Builder;
        var returnsMetadata = indexer.GetterBuilderInterface.ReturnsMethod;
        var throwsMetadata = indexer.GetterBuilderInterface.ThrowsMethod;

        return new ClassDeclarationBuilder(builderMetadata.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.TypeSyntax))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.GetterImplementation.TypeSyntax, builderMetadata.ImposterFieldName))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.ArgumentsCriteria.TypeSyntax, builderMetadata.CriteriaFieldName))
            .AddMember(new ConstructorBuilder(builderMetadata.Name)
                .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                .AddParameter(Parameter(Identifier(builderMetadata.ImposterFieldName)).WithType(indexer.GetterImplementation.TypeSyntax))
                .AddParameter(Parameter(Identifier(builderMetadata.CriteriaFieldName)).WithType(indexer.ArgumentsCriteria.TypeSyntax))
                .WithBody(new BlockBuilder()
                    .AddStatement(ThisExpression().Dot(IdentifierName(builderMetadata.ImposterFieldName)).Assign(IdentifierName(builderMetadata.ImposterFieldName)).ToStatementSyntax())
                    .AddStatement(ThisExpression().Dot(IdentifierName(builderMetadata.CriteriaFieldName)).Assign(IdentifierName(builderMetadata.CriteriaFieldName)).ToStatementSyntax())
                    .Build())
                .Build())
            .AddMember(BuildGetterBuilderInvocationProperty(indexer))
            .AddMember(BuildGetterBuilderReturnsValueMethod(indexer, returnsMetadata))
            .AddMember(BuildGetterBuilderReturnsFuncMethod(indexer, returnsMetadata))
            .AddMember(BuildGetterBuilderReturnsDelegateMethod(indexer, returnsMetadata))
            .AddMember(BuildGetterBuilderThrowsExceptionMethod(indexer, throwsMetadata))
            .AddMember(BuildGetterBuilderThrowsGenericExceptionMethod(indexer, throwsMetadata))
            .AddMember(BuildGetterBuilderThrowsDelegateMethod(indexer, throwsMetadata))
            .AddMember(BuildGetterBuilderCallbackMethod(indexer))
            .AddMember(BuildGetterBuilderCalledMethod(indexer))
            .AddMember(BuildGetterBuilderThenMethod(indexer))
            .Build();
    }

    private static PropertyDeclarationSyntax BuildGetterBuilderInvocationProperty(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.GetterImplementation.Builder;
        return PropertyDeclaration(indexer.GetterImplementation.Invocation.TypeSyntax, Identifier(builderMetadata.InvocationImposterPropertyName))
            .AddModifiers(Token(SyntaxKind.PrivateKeyword))
            .WithExpressionBody(
                ArrowExpressionClause(
                    InvocationExpression(
                        IdentifierName(builderMetadata.ImposterFieldName).Dot(IdentifierName("GetOrCreate")),
                        ArgumentList(SingletonSeparatedList(Argument(IdentifierName(builderMetadata.CriteriaFieldName)))))))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsValueMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returnsMetadata)
    {
        var parameter = SyntaxFactoryHelper.ParameterSyntax(returnsMetadata.ValueParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            IdentifierName(parameter.Identifier));

        return new MethodDeclarationBuilder(returnsMetadata.ReturnType, returnsMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(BuildFluentBodyReturningThis(
                InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsFuncMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returnsMetadata)
    {
        var parameter = SyntaxFactoryHelper.ParameterSyntax(returnsMetadata.FuncParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            InvocationExpression(IdentifierName(parameter.Identifier)));

        return new MethodDeclarationBuilder(returnsMetadata.ReturnType, returnsMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(BuildFluentBodyReturningThis(
                InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsDelegateMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returnsMetadata)
    {
        var parameter = SyntaxFactoryHelper.ParameterSyntax(returnsMetadata.DelegateParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            InvocationExpression(IdentifierName(parameter.Identifier))
                .WithArgumentList(
                    BuildDelegateInvocationArguments(
                        IdentifierName(indexer.GetterImplementation.ArgumentsVariableName),
                        indexer,
                        fromArguments: true)));

        return new MethodDeclarationBuilder(returnsMetadata.ReturnType, returnsMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(BuildFluentBodyReturningThis(
                InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsExceptionMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throwsMetadata)
    {
        var parameter = SyntaxFactoryHelper.ParameterSyntax(throwsMetadata.ExceptionParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            ThrowExpression(IdentifierName(parameter.Identifier)));

        return new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(BuildFluentBodyReturningThis(
                InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsGenericExceptionMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throwsMetadata)
        => new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
            .AddConstraintClause(TypeParameterConstraintClause("TException").AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
            .WithBody(BuildFluentBodyReturningThis(
                InvocationImposterAddReturnValue(
                    indexer.GetterImplementation.Builder,
                    SimpleLambdaExpression(
                        Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
                        ThrowExpression(ObjectCreationExpression(IdentifierName("TException")).WithArgumentList(ArgumentList()))))))
            .Build();

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsDelegateMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throwsMetadata)
    {
        var parameter = SyntaxFactoryHelper.ParameterSyntax(throwsMetadata.DelegateParameter);
        var lambda = SimpleLambdaExpression(
            Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)),
            ThrowExpression(
                InvocationExpression(IdentifierName(parameter.Identifier))
                    .WithArgumentList(
                        BuildDelegateInvocationArguments(
                            IdentifierName(indexer.GetterImplementation.ArgumentsVariableName),
                            indexer,
                            fromArguments: true))));

        return new MethodDeclarationBuilder(throwsMetadata.ReturnType, throwsMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(BuildFluentBodyReturningThis(
                InvocationImposterAddReturnValue(indexer.GetterImplementation.Builder, lambda)))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderCallbackMethod(in ImposterIndexerMetadata indexer)
    {
        var parameter = SyntaxFactoryHelper.ParameterSyntax(indexer.GetterBuilderInterface.CallbackMethod.CallbackParameter);
        return new MethodDeclarationBuilder(indexer.GetterBuilderInterface.CallbackMethod.ReturnType, indexer.GetterBuilderInterface.CallbackMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(BuildFluentBodyReturningThis(
                InvocationImposterAddCallback(indexer.GetterImplementation.Builder, IdentifierName(parameter.Identifier))))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderCalledMethod(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.GetterImplementation.Builder;
        var parameter = SyntaxFactoryHelper.ParameterSyntax(indexer.GetterBuilderInterface.CalledMethod.CountParameter);

        return new MethodDeclarationBuilder(indexer.GetterBuilderInterface.CalledMethod.ReturnType, indexer.GetterBuilderInterface.CalledMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName("Called"))
                        .Call(new[]
                        {
                            Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                            Argument(IdentifierName(parameter.Identifier))
                        })
                        .ToStatementSyntax()))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThenMethod(in ImposterIndexerMetadata indexer)
    {
        var thenMetadata = indexer.GetterBuilderInterface.ThenMethod;
        return new MethodDeclarationBuilder(thenMetadata.ReturnType, thenMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();
    }

    private static BlockSyntax BuildFluentBodyReturningThis(params StatementSyntax[] statements)
    {
        var fluentStatements = statements.Concat(new[] { ReturnStatement(ThisExpression()) }).ToArray();
        return Block(fluentStatements);
    }

    private static ExpressionStatementSyntax InvocationImposterAddReturnValue(IndexerGetterImposterMetadata.GetterBuilderMetadata builderMetadata, ExpressionSyntax lambda)
        => InvocationExpression(
                IdentifierName(builderMetadata.InvocationImposterPropertyName).Dot(IdentifierName("AddReturnValue")),
                ArgumentList(SingletonSeparatedList(Argument(lambda))))
            .ToStatementSyntax();

    private static ExpressionStatementSyntax InvocationImposterAddCallback(IndexerGetterImposterMetadata.GetterBuilderMetadata builderMetadata, ExpressionSyntax callback)
        => InvocationExpression(
                IdentifierName(builderMetadata.InvocationImposterPropertyName).Dot(IdentifierName("AddCallback")),
                ArgumentList(SingletonSeparatedList(Argument(callback))))
            .ToStatementSyntax();

    private static ClassDeclarationSyntax BuildGetterInvocationImposter(in ImposterIndexerMetadata indexer)
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;

        return new ClassDeclarationBuilder(invocationMetadata.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(invocationMetadata.ParentField))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(invocationMetadata.DefaultBehaviourField))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(invocationMetadata.ReturnValuesField, invocationMetadata.ReturnValuesField.Type.New()))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(invocationMetadata.CallbacksField, invocationMetadata.CallbacksField.Type.New()))
            .AddMember(SyntaxFactoryHelper.SingleVariableField(invocationMetadata.LastReturnValueField.Type, invocationMetadata.LastReturnValueField.Name, SyntaxKind.PrivateKeyword))
            .AddMember(SyntaxFactoryHelper.SingleVariableField(invocationMetadata.InvocationCountField.Type, invocationMetadata.InvocationCountField.Name, SyntaxKind.PrivateKeyword))
            .AddMember(SyntaxFactoryHelper.SingleVariableField(invocationMetadata.PropertyDisplayNameField.Type, invocationMetadata.PropertyDisplayNameField.Name, SyntaxKind.PrivateKeyword))
            .AddMember(PropertyDeclaration(indexer.ArgumentsCriteria.TypeSyntax, Identifier(invocationMetadata.CriteriaField.Name))
                .AddModifiers(Token(SyntaxKind.InternalKeyword))
                .WithAccessorList(
                    AccessorList(
                        List(new[]
                        {
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                            AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                .AddModifiers(Token(SyntaxKind.PrivateKeyword))
                                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        }))))
            .AddMember(PropertyDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)), Identifier("InvocationCount"))
                .AddModifiers(Token(SyntaxKind.InternalKeyword))
                .WithExpressionBody(
                    ArrowExpressionClause(
                        WellKnownTypes.System.Threading.Volatile
                            .Dot(IdentifierName("Read"))
                            .Call(Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(invocationMetadata.InvocationCountField.Name)))))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
            .AddMember(BuildGetterInvocationConstructor(indexer))
            .AddMember(BuildGetterInvocationAddReturnValueMethod(indexer))
            .AddMember(BuildGetterInvocationAddCallbackMethod(indexer))
            .AddMember(BuildGetterInvocationInvokeMethod(indexer))
            .AddMember(BuildGetterInvocationResolveNextGeneratorMethod(indexer))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildGetterInvocationConstructor(in ImposterIndexerMetadata indexer)
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;

        return new ConstructorBuilder(invocationMetadata.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(Parameter(Identifier("parent")).WithType(indexer.GetterImplementation.TypeSyntax))
            .AddParameter(Parameter(Identifier(DefaultBehaviourParameterName)).WithType(indexer.DefaultIndexerBehaviour.TypeSyntax))
            .AddParameter(Parameter(Identifier(indexer.GetterImplementation.CriteriaParameterName)).WithType(indexer.ArgumentsCriteria.TypeSyntax))
            .WithBody(new BlockBuilder()
                .AddStatement(ThisExpression().Dot(IdentifierName(invocationMetadata.ParentField.Name)).Assign(IdentifierName("parent")).ToStatementSyntax())
                .AddStatement(ThisExpression().Dot(IdentifierName(invocationMetadata.DefaultBehaviourField.Name)).Assign(IdentifierName(DefaultBehaviourParameterName)).ToStatementSyntax())
                .AddStatement(ThisExpression().Dot(IdentifierName(invocationMetadata.PropertyDisplayNameField.Name)).Assign(IdentifierName("parent").Dot(IdentifierName("PropertyDisplayName"))).ToStatementSyntax())
                .AddStatement(ThisExpression().Dot(IdentifierName(invocationMetadata.CriteriaField.Name)).Assign(IdentifierName(indexer.GetterImplementation.CriteriaParameterName)).ToStatementSyntax())
                .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationAddReturnValueMethod(in ImposterIndexerMetadata indexer)
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var builderMetadata = indexer.GetterImplementation.Builder;
        var parameter = Parameter(Identifier("generator")).WithType(builderMetadata.ReturnGeneratorType);

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "AddReturnValue")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(parameter)
            .WithBody(Block(
                IdentifierName(invocationMetadata.DefaultBehaviourField.Name)
                    .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName))
                    .Assign(LiteralExpression(SyntaxKind.FalseLiteralExpression))
                    .ToStatementSyntax(),
                IdentifierName(invocationMetadata.ReturnValuesField.Name)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(IdentifierName(parameter.Identifier)))
                    .ToStatementSyntax(),
                IdentifierName(invocationMetadata.ParentField.Name)
                    .Dot(IdentifierName("MarkReturnConfigured"))
                    .Call()
                    .ToStatementSyntax()))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationAddCallbackMethod(in ImposterIndexerMetadata indexer)
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var parameter = SyntaxFactoryHelper.ParameterSyntax(indexer.GetterBuilderInterface.CallbackMethod.CallbackParameter);

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "AddCallback")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(parameter)
            .WithBody(Block(
                IdentifierName(invocationMetadata.CallbacksField.Name)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(IdentifierName(parameter.Identifier)))
                    .ToStatementSyntax()))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationInvokeMethod(in ImposterIndexerMetadata indexer)
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var builderMetadata = indexer.GetterImplementation.Builder;
        var argumentsParameterName = indexer.GetterImplementation.ArgumentsVariableName;
        var argumentsParameter = Parameter(Identifier(argumentsParameterName)).WithType(indexer.Arguments.TypeSyntax);

        var incrementInvocation = WellKnownTypes.System.Threading.Interlocked
            .Dot(IdentifierName("Increment"))
            .Call(Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(invocationMetadata.InvocationCountField.Name)))
            .ToStatementSyntax();

        var foreachCallbacks = ForEachStatement(
            IdentifierName("var"),
            Identifier("callback"),
            IdentifierName(invocationMetadata.CallbacksField.Name),
            Block(
                InvocationExpression(IdentifierName("callback"))
                    .WithArgumentList(
                        BuildDelegateInvocationArguments(
                            IdentifierName(argumentsParameterName),
                            indexer,
                            fromArguments: true))
                    .ToStatementSyntax()));

        var generatorDeclaration = LocalDeclarationStatement(
            VariableDeclaration(builderMetadata.ReturnGeneratorType)
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("generator"))
                            .WithInitializer(
                                EqualsValueClause(
                                    InvocationExpression(IdentifierName("ResolveNextGenerator"))
                                        .WithArgumentList(
                                            ArgumentList(SingletonSeparatedList(Argument(IdentifierName(argumentsParameterName))))))))));

        var returnStatement = ReturnStatement(
            InvocationExpression(IdentifierName("generator"))
                .WithArgumentList(
                    ArgumentList(SingletonSeparatedList(Argument(IdentifierName(argumentsParameterName))))));

        return new MethodDeclarationBuilder(indexer.Core.TypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParameter)
            .WithBody(Block(
                incrementInvocation,
                foreachCallbacks,
                generatorDeclaration,
                returnStatement))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationResolveNextGeneratorMethod(in ImposterIndexerMetadata indexer)
    {
        var invocationMetadata = indexer.GetterImplementation.Invocation;
        var builderMetadata = indexer.GetterImplementation.Builder;
        var argumentsParameterName = indexer.GetterImplementation.ArgumentsVariableName;
        var argumentsParameter = Parameter(Identifier(argumentsParameterName)).WithType(indexer.Arguments.TypeSyntax);

        var defaultBehaviourCheck = IfStatement(
            IdentifierName(invocationMetadata.DefaultBehaviourField.Name)
                .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)),
            Block(
                ReturnStatement(
                    SimpleLambdaExpression(
                        Parameter(Identifier(argumentsParameterName)),
                        IdentifierName(invocationMetadata.DefaultBehaviourField.Name)
                            .Dot(IdentifierName("Get"))
                            .Call(Argument(IdentifierName(argumentsParameterName)))))));

        var tryDequeue = IfStatement(
            IdentifierName(invocationMetadata.ReturnValuesField.Name)
                .Dot(IdentifierName("TryDequeue"))
                .Call(
                    Argument(
                        null,
                        Token(SyntaxKind.OutKeyword),
                        DeclarationExpression(
                            builderMetadata.ReturnGeneratorType,
                            SingleVariableDesignation(Identifier("returnValue"))))),
            Block(
                IdentifierName(invocationMetadata.LastReturnValueField.Name)
                    .Assign(IdentifierName("returnValue"))
                    .ToStatementSyntax()));

        var throwIfMissing = IfStatement(
            BinaryExpression(
                SyntaxKind.EqualsExpression,
                IdentifierName(invocationMetadata.LastReturnValueField.Name),
                LiteralExpression(SyntaxKind.NullLiteralExpression)),
            Block(BuildMissingImposterThrow(indexer, indexer.GetterImplementation.GetterSuffix)));

        return new MethodDeclarationBuilder(builderMetadata.ReturnGeneratorType, "ResolveNextGenerator")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(argumentsParameter)
            .WithBody(Block(
                defaultBehaviourCheck,
                tryDequeue,
                throwIfMissing,
                ReturnStatement(IdentifierName(invocationMetadata.LastReturnValueField.Name))))
            .Build();
    }
    private static ClassDeclarationSyntax BuildSetterImposter(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;

        return new ClassDeclarationBuilder(setter.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(setter.CallbacksField, setter.CallbacksField.Type.New()))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(setter.InvocationHistoryField, setter.InvocationHistoryField.Type.New()))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(setter.DefaultBehaviourField))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(setter.InvocationBehaviorField))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(setter.PropertyDisplayNameField))
            .AddMember(SyntaxFactoryHelper.SingleVariableField(setter.HasConfiguredSetterField.Type, setter.HasConfiguredSetterField.Name, SyntaxKind.PrivateKeyword))
            .AddMember(BuildSetterConstructor(indexer))
            .AddMember(BuildSetterCallbackMethod(indexer))
            .AddMember(BuildSetterCalledMethod(indexer))
            .AddMember(BuildSetterSetMethod(indexer))
            .AddMember(BuildEnsureSetterConfiguredMethod(indexer))
            .AddMember(BuildMarkSetterConfiguredMethod(indexer))
            .AddMember(BuildSetterBuilder(indexer))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildSetterConstructor(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;

        return new ConstructorBuilder(setter.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(Parameter(Identifier(DefaultBehaviourParameterName)).WithType(indexer.DefaultIndexerBehaviour.TypeSyntax))
            .AddParameter(Parameter(Identifier(InvocationBehaviorParameterName)).WithType(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior))
            .AddParameter(Parameter(Identifier(PropertyDisplayNameParameterName)).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))))
            .WithBody(new BlockBuilder()
                .AddStatement(ThisExpression().Dot(IdentifierName(setter.DefaultBehaviourField.Name)).Assign(IdentifierName(DefaultBehaviourParameterName)).ToStatementSyntax())
                .AddStatement(ThisExpression().Dot(IdentifierName(setter.InvocationBehaviorField.Name)).Assign(IdentifierName(InvocationBehaviorParameterName)).ToStatementSyntax())
                .AddStatement(ThisExpression().Dot(IdentifierName(setter.PropertyDisplayNameField.Name)).Assign(IdentifierName(PropertyDisplayNameParameterName)).ToStatementSyntax())
                .Build())
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterCallbackMethod(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;
        var callbackParameter = SyntaxFactoryHelper.ParameterSyntax(indexer.SetterBuilderInterface.CallbackMethod.CallbackParameter);

        var tupleExpression = TupleExpression(
            SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
            {
                Argument(IdentifierName(setter.CriteriaParameterName)),
                Token(SyntaxKind.CommaToken),
                Argument(IdentifierName(callbackParameter.Identifier))
            }));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Callback")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(Parameter(Identifier(setter.CriteriaParameterName)).WithType(indexer.ArgumentsCriteria.TypeSyntax))
            .AddParameter(callbackParameter)
            .WithBody(Block(
                IdentifierName(setter.CallbacksField.Name)
                    .Dot(IdentifierName("Enqueue"))
                    .Call(Argument(tupleExpression))
                    .ToStatementSyntax()))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterCalledMethod(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;
        var countParameter = SyntaxFactoryHelper.ParameterSyntax(indexer.SetterBuilderInterface.CalledMethod.CountParameter);

        var invocationCountDeclaration = LocalDeclarationStatement(
            VariableDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)))
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("invocationCount"))
                            .WithInitializer(
                                EqualsValueClause(
                                    IdentifierName(setter.InvocationHistoryField.Name)
                                        .Dot(IdentifierName("Count"))
                                        .Call(Argument(IdentifierName(setter.CriteriaParameterName).Dot(IdentifierName("Matches")))))))));

        var condition = PrefixUnaryExpression(
            SyntaxKind.LogicalNotExpression,
            IdentifierName(countParameter.Identifier)
                .Dot(IdentifierName("Matches"))
                .Call(Argument(IdentifierName("invocationCount"))));

        var throwStatement = ThrowStatement(
            ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.VerificationFailedException)
                .WithArgumentList(
                    ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                    {
                        Argument(IdentifierName(countParameter.Identifier)),
                        Token(SyntaxKind.CommaToken),
                        Argument(IdentifierName("invocationCount"))
                    }))));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Called")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(Parameter(Identifier(setter.CriteriaParameterName)).WithType(indexer.ArgumentsCriteria.TypeSyntax))
            .AddParameter(countParameter)
            .WithBody(Block(invocationCountDeclaration, IfStatement(condition, Block(throwStatement))))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterSetMethod(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;
        var argumentsVariable = IdentifierName(indexer.GetterImplementation.ArgumentsVariableName);
        var parameters = indexer.Core.Parameters
            .Select(parameter => parameter.ParameterSyntax)
            .Concat(new[] { Parameter(Identifier(setter.ValueParameterName)).WithType(indexer.Core.TypeSyntax) })
            .ToArray();

        var foreachStatement = ForEachStatement(
            IdentifierName("var"),
            Identifier("registration"),
            IdentifierName(setter.CallbacksField.Name),
            Block(
                IfStatement(
                    IdentifierName("registration")
                        .Dot(IdentifierName("Criteria"))
                        .Dot(IdentifierName("Matches"))
                        .Call(Argument(argumentsVariable)),
                    Block(
                        InvocationExpression(
                                IdentifierName("registration").Dot(IdentifierName("Callback")))
                            .WithArgumentList(
                                BuildDelegateInvocationArgumentsWithValue(
                                    argumentsVariable,
                                    indexer,
                                    fromArguments: true,
                                    IdentifierName(setter.ValueParameterName)))
                            .ToStatementSyntax()))));

        var defaultBehaviourBlock = IfStatement(
            IdentifierName(setter.DefaultBehaviourField.Name)
                .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)),
            Block(
                IdentifierName(setter.DefaultBehaviourField.Name)
                    .Dot(IdentifierName("Set"))
                    .Call(
                        SyntaxFactoryHelper.ArgumentListSyntax(new[]
                        {
                            Argument(argumentsVariable),
                            Argument(IdentifierName(setter.ValueParameterName))
                        }))
                    .ToStatementSyntax()));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Set")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters)
            .WithBody(Block(
                IdentifierName("EnsureSetterConfigured").Call().ToStatementSyntax(),
                CreateArgumentsDeclaration(indexer),
                IdentifierName(setter.InvocationHistoryField.Name)
                    .Dot(IdentifierName("Add"))
                    .Call(Argument(argumentsVariable))
                    .ToStatementSyntax(),
                foreachStatement,
                defaultBehaviourBlock))
            .Build();
    }

    private static MethodDeclarationSyntax BuildEnsureSetterConfiguredMethod(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;

        var explicitCheck = BinaryExpression(
            SyntaxKind.EqualsExpression,
            IdentifierName(setter.InvocationBehaviorField.Name),
            WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior.Dot(IdentifierName("Explicit")));

        var configuredCheck = WellKnownTypes.System.Threading.Volatile
            .Dot(IdentifierName("Read"))
            .Call(Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(setter.HasConfiguredSetterField.Name)));

        var condition = BinaryExpression(
            SyntaxKind.LogicalAndExpression,
            explicitCheck,
            PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, configuredCheck));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "EnsureSetterConfigured")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(condition, Block(BuildMissingImposterThrow(indexer, setter.SetterSuffix)))))
            .Build();
    }

    private static MethodDeclarationSyntax BuildMarkSetterConfiguredMethod(in ImposterIndexerMetadata indexer)
    {
        var setter = indexer.SetterImplementation;

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "MarkConfigured")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    WellKnownTypes.System.Threading.Volatile
                        .Dot(IdentifierName("Write"))
                        .Call(ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                        {
                            Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(setter.HasConfiguredSetterField.Name)),
                            Token(SyntaxKind.CommaToken),
                            Argument(LiteralExpression(SyntaxKind.TrueLiteralExpression))
                        })))
                        .ToStatementSyntax()))
            .Build();
    }

    private static ClassDeclarationSyntax BuildSetterBuilder(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.SetterImplementation.Builder;

        return new ClassDeclarationBuilder(builderMetadata.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.TypeSyntax))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.SetterImplementation.TypeSyntax, builderMetadata.ImposterFieldName))
            .AddMember(SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(indexer.ArgumentsCriteria.TypeSyntax, builderMetadata.CriteriaFieldName))
            .AddMember(new ConstructorBuilder(builderMetadata.Name)
                .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                .AddParameter(Parameter(Identifier(builderMetadata.ImposterFieldName)).WithType(indexer.SetterImplementation.TypeSyntax))
                .AddParameter(Parameter(Identifier(builderMetadata.CriteriaFieldName)).WithType(indexer.ArgumentsCriteria.TypeSyntax))
                .WithBody(new BlockBuilder()
                    .AddStatement(ThisExpression().Dot(IdentifierName(builderMetadata.ImposterFieldName)).Assign(IdentifierName(builderMetadata.ImposterFieldName)).ToStatementSyntax())
                    .AddStatement(ThisExpression().Dot(IdentifierName(builderMetadata.CriteriaFieldName)).Assign(IdentifierName(builderMetadata.CriteriaFieldName)).ToStatementSyntax())
                    .Build())
                .Build())
            .AddMember(BuildSetterBuilderCallbackMethod(indexer))
            .AddMember(BuildSetterBuilderCalledMethod(indexer))
            .AddMember(BuildSetterBuilderThenMethod(indexer))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderCallbackMethod(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var callbackMetadata = indexer.SetterBuilderInterface.CallbackMethod;
        var parameter = SyntaxFactoryHelper.ParameterSyntax(callbackMetadata.CallbackParameter);

        return new MethodDeclarationBuilder(callbackMetadata.ReturnType, callbackMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(Block(
                IdentifierName(builderMetadata.ImposterFieldName)
                    .Dot(IdentifierName("Callback"))
                    .Call(new[]
                    {
                        Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                        Argument(IdentifierName(parameter.Identifier))
                    })
                    .ToStatementSyntax(),
                ReturnStatement(ThisExpression())))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderCalledMethod(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var calledMetadata = indexer.SetterBuilderInterface.CalledMethod;
        var parameter = SyntaxFactoryHelper.ParameterSyntax(calledMetadata.CountParameter);

        return new MethodDeclarationBuilder(calledMetadata.ReturnType, calledMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(parameter)
            .WithBody(Block(
                IdentifierName(builderMetadata.ImposterFieldName)
                    .Dot(IdentifierName("Called"))
                    .Call(new[]
                    {
                        Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                        Argument(IdentifierName(parameter.Identifier))
                    })
                    .ToStatementSyntax()))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderThenMethod(in ImposterIndexerMetadata indexer)
    {
        var thenMetadata = indexer.SetterBuilderInterface.ThenMethod;

        return new MethodDeclarationBuilder(thenMetadata.ReturnType, thenMetadata.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();
    }

    private static MethodDeclarationSyntax BuildFluentNoOpMethod(TypeSyntax returnType, string name, ParameterMetadata parameter)
    {
        var method = new MethodDeclarationBuilder(returnType, name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(parameter));

        if (returnType == WellKnownTypes.Void)
        {
            return method.WithBody(Block()).Build();
        }

        return method.WithBody(Block(ReturnStatement(ThisExpression()))).Build();
    }

    private static MethodDeclarationSyntax BuildCalledMethod(TypeSyntax returnType, string name, ParameterMetadata countParameter)
        => new MethodDeclarationBuilder(returnType, name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(countParameter))
            .WithBody(Block())
            .Build();

    private static MethodDeclarationSyntax BuildThenMethod(TypeSyntax returnType, string name)
        => new MethodDeclarationBuilder(returnType, name)
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();

    private static MethodDeclarationSyntax BuildGenericThrowsMethod(in ImposterIndexerMetadata indexer)
        => new MethodDeclarationBuilder(indexer.GetterBuilderInterface.ThrowsMethod.ReturnType, indexer.GetterBuilderInterface.ThrowsMethod.Name)
            .WithTypeParameters(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
            .AddConstraintClause(TypeParameterConstraintClause("TException").AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
            .WithBody(Block(ReturnStatement(ThisExpression())))
            .Build();

    private static ArgumentSyntax BuildArgument(IParameterSymbol parameter, ExpressionSyntax expression)
    {
        var modifier = parameter.RefKind switch
        {
            RefKind.Ref => Token(SyntaxKind.RefKeyword),
            RefKind.Out => Token(SyntaxKind.OutKeyword),
            RefKind.In => Token(SyntaxKind.InKeyword),
            _ => default(SyntaxToken)
        };

        return Argument(null, modifier, expression);
    }

    private static ArgumentListSyntax BuildIndexerArgumentsArgumentList(in ImposterIndexerMetadata indexer)
        => ArgumentList(SeparatedList(indexer.Core.Parameters.Select(parameter => SyntaxFactoryHelper.ArgumentSyntax(parameter.Symbol))));

    private static LocalDeclarationStatementSyntax CreateArgumentsDeclaration(in ImposterIndexerMetadata indexer)
        => LocalDeclarationStatement(
            VariableDeclaration(indexer.Arguments.TypeSyntax)
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier(indexer.GetterImplementation.ArgumentsVariableName))
                            .WithInitializer(
                                EqualsValueClause(
                                    indexer.Arguments.TypeSyntax.New(BuildIndexerArgumentsArgumentList(indexer)))))));

    private static ArgumentListSyntax BuildDelegateInvocationArguments(ExpressionSyntax source, in ImposterIndexerMetadata indexer, bool fromArguments)
    {
        var arguments = indexer.Core.Parameters.Select(parameter =>
        {
            ExpressionSyntax argumentExpression = fromArguments
                ? source.Dot(IdentifierName(parameter.Name))
                : IdentifierName(parameter.Name);

            return BuildArgument(parameter.Symbol, argumentExpression);
        });

        return ArgumentList(SeparatedList(arguments));
    }

    private static ArgumentListSyntax BuildDelegateInvocationArgumentsWithValue(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        bool fromArguments,
        ExpressionSyntax valueExpression)
    {
        var arguments = BuildDelegateInvocationArguments(source, indexer, fromArguments)
            .Arguments
            .Add(Argument(valueExpression));

        return ArgumentList(arguments);
    }

    private static TupleTypeSyntax BuildSetterCallbackRegistrationType(in ImposterIndexerMetadata indexer)
        => TupleType(SeparatedList<TupleElementSyntax>(new SyntaxNodeOrToken[]
            {
                TupleElement(indexer.ArgumentsCriteria.TypeSyntax, Identifier("Criteria")),
                Token(SyntaxKind.CommaToken),
                TupleElement(indexer.Delegates.SetterCallbackDelegateType, Identifier("Callback"))
            }));

    private static BinaryExpressionSyntax BuildMissingImposterMessage(in ImposterIndexerMetadata indexer, string suffix)
        => BinaryExpression(
            SyntaxKind.AddExpression,
            IdentifierName(indexer.GetterImplementation.PropertyDisplayNameField.Name),
            LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(suffix)));

    private static ThrowStatementSyntax BuildMissingImposterThrow(in ImposterIndexerMetadata indexer, string suffix)
        => ThrowStatement(
            ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.MissingImposterException)
                .WithArgumentList(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(BuildMissingImposterMessage(indexer, suffix))))));

    private static ConstructorDeclarationSyntax BuildGetterConstructor(in ImposterIndexerMetadata indexer)
        => new ConstructorBuilder("GetterImposter")
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(Parameter(Identifier(DefaultBehaviourParameterName)).WithType(indexer.DefaultIndexerBehaviour.TypeSyntax))
            .AddParameter(Parameter(Identifier(InvocationBehaviorParameterName)).WithType(WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior))
            .AddParameter(Parameter(Identifier(PropertyDisplayNameParameterName)).WithType(PredefinedType(Token(SyntaxKind.StringKeyword))))
            .WithBody(new BlockBuilder()
                .AddStatement(ThisExpression().Dot(IdentifierName(indexer.GetterImplementation.DefaultBehaviourField.Name)).Assign(IdentifierName(DefaultBehaviourParameterName)).ToStatementSyntax())
                .AddStatement(ThisExpression().Dot(IdentifierName(indexer.GetterImplementation.InvocationBehaviorField.Name)).Assign(IdentifierName(InvocationBehaviorParameterName)).ToStatementSyntax())
                .AddStatement(ThisExpression().Dot(IdentifierName(indexer.GetterImplementation.PropertyDisplayNameField.Name)).Assign(IdentifierName(PropertyDisplayNameParameterName)).ToStatementSyntax())
                .Build())
            .Build();
    private static MethodDeclarationSyntax BuildGetterGetMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsIdentifier = IdentifierName(indexer.GetterImplementation.ArgumentsVariableName);

        var tryStatements = new List<StatementSyntax>
        {
            LocalDeclarationStatement(
                VariableDeclaration(IdentifierName("var"))
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier(indexer.GetterImplementation.SetupVariableName))
                                .WithInitializer(
                                    EqualsValueClause(
                                        IdentifierName("FindMatchingSetup")
                                            .Call(ArgumentList(SingletonSeparatedList(Argument(argumentsIdentifier))))))))),
            IfStatement(
                IsPatternExpression(IdentifierName(indexer.GetterImplementation.SetupVariableName), ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression))),
                Block(
                    IdentifierName("EnsureGetterConfigured").Call().ToStatementSyntax(),
                    IfStatement(
                        IdentifierName(indexer.GetterImplementation.DefaultBehaviourField.Name).Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)),
                        ReturnStatement(
                            IdentifierName(indexer.GetterImplementation.DefaultBehaviourField.Name)
                                .Dot(IdentifierName("Get"))
                                .Call(ArgumentList(SingletonSeparatedList(Argument(argumentsIdentifier)))))),
                    BuildMissingImposterThrow(indexer, indexer.GetterImplementation.GetterSuffix))),
            ReturnStatement(
                IdentifierName(indexer.GetterImplementation.SetupVariableName)
                    .Dot(IdentifierName("Invoke"))
                    .Call(ArgumentList(SingletonSeparatedList(Argument(argumentsIdentifier)))))
        };

        var finallyClause = FinallyClause(
            Block(
                IdentifierName(indexer.GetterImplementation.InvocationHistoryField.Name)
                    .Dot(IdentifierName("Add"))
                    .Call(ArgumentList(SingletonSeparatedList(Argument(argumentsIdentifier))))
                    .ToStatementSyntax()));

        var body = Block(
            CreateArgumentsDeclaration(indexer),
            TryStatement(Block(tryStatements), default, finallyClause));

        return new MethodDeclarationBuilder(indexer.Core.TypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(indexer.Core.Parameters.Select(parameter => parameter.ParameterSyntax).ToArray())
            .WithBody(body)
            .Build();
    }
    private static MethodDeclarationSyntax BuildFindMatchingSetupMethod(in ImposterIndexerMetadata indexer)
    {
        var getterInvocationType = IdentifierName(indexer.GetterImplementation.Invocation.Name);

        return new MethodDeclarationBuilder(getterInvocationType, "FindMatchingSetup")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(Parameter(Identifier(indexer.GetterImplementation.ArgumentsVariableName)).WithType(indexer.Arguments.TypeSyntax))
            .WithBody(
                Block(
                    ForEachStatement(
                        IdentifierName("var"),
                        Identifier(indexer.GetterImplementation.SetupVariableName),
                        IdentifierName(indexer.GetterImplementation.SetupsField.Name),
                        Block(
                            IfStatement(
                                IdentifierName(indexer.GetterImplementation.SetupVariableName)
                                    .Dot(IdentifierName("Criteria"))
                                    .Dot(IdentifierName("Matches"))
                                    .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName(indexer.GetterImplementation.ArgumentsVariableName))))),
                                Block(ReturnStatement(IdentifierName(indexer.GetterImplementation.SetupVariableName)))))),
                    ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression))))
            .Build();
    }
    private static MethodDeclarationSyntax BuildGetOrCreateMethod(in ImposterIndexerMetadata indexer)
    {
        var getterInvocationType = IdentifierName(indexer.GetterImplementation.Invocation.Name);

        return new MethodDeclarationBuilder(getterInvocationType, "GetOrCreate")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(Parameter(Identifier(indexer.GetterImplementation.CriteriaParameterName)).WithType(indexer.ArgumentsCriteria.TypeSyntax))
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(indexer.GetterImplementation.SetupLookupField.Name)
                            .Dot(IdentifierName("GetOrAdd"))
                            .Call(ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                            {
                                Argument(IdentifierName(indexer.GetterImplementation.CriteriaParameterName)),
                                Token(SyntaxKind.CommaToken),
                                Argument(IdentifierName("CreateSetup"))
                            })))),
                    LocalFunctionStatement(getterInvocationType, Identifier("CreateSetup"))
                        .AddParameterListParameters(Parameter(Identifier("key")).WithType(indexer.ArgumentsCriteria.TypeSyntax))
                        .WithBody(
                            Block(
                                LocalDeclarationStatement(
                                    VariableDeclaration(getterInvocationType)
                                        .WithVariables(
                                            SingletonSeparatedList(
                                                VariableDeclarator(Identifier(indexer.GetterImplementation.SetupVariableName))
                                                    .WithInitializer(
                                                        EqualsValueClause(
                                                            getterInvocationType.New(
                                                                SyntaxFactoryHelper.ArgumentListSyntax(new[]
                                                                {
                                                                    Argument(ThisExpression()),
                                                                    Argument(IdentifierName(indexer.GetterImplementation.DefaultBehaviourField.Name)),
                                                                    Argument(IdentifierName("key"))
                                                                }))))))),
                                IdentifierName(indexer.GetterImplementation.SetupsField.Name)
                                    .Dot(IdentifierName("Push"))
                                    .Call(Argument(IdentifierName(indexer.GetterImplementation.SetupVariableName)))
                                    .ToStatementSyntax(),
                                ReturnStatement(IdentifierName(indexer.GetterImplementation.SetupVariableName))))))
            .Build();
    }
    private static MethodDeclarationSyntax BuildGetterCalledMethod(in ImposterIndexerMetadata indexer)
    {
        var invocationCountDeclaration = LocalDeclarationStatement(
            VariableDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)))
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("invocationCount"))
                            .WithInitializer(
                                EqualsValueClause(
                                    IdentifierName(indexer.GetterImplementation.InvocationHistoryField.Name)
                                        .Dot(IdentifierName("Count"))
                                        .Call(ArgumentList(SingletonSeparatedList(
                                            Argument(IdentifierName(indexer.GetterImplementation.CriteriaParameterName).Dot(IdentifierName("Matches")))))))))));

        var condition = PrefixUnaryExpression(
            SyntaxKind.LogicalNotExpression,
            IdentifierName(indexer.GetterImplementation.CountParameterName)
                .Dot(IdentifierName("Matches"))
                .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("invocationCount"))))));

        var throwStatement = ThrowStatement(
            ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.VerificationFailedException)
                .WithArgumentList(
                    ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                    {
                        Argument(IdentifierName(indexer.GetterImplementation.CountParameterName)),
                        Token(SyntaxKind.CommaToken),
                        Argument(IdentifierName("invocationCount"))
                    }))));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Called")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(Parameter(Identifier(indexer.GetterImplementation.CriteriaParameterName)).WithType(indexer.ArgumentsCriteria.TypeSyntax))
            .AddParameter(Parameter(Identifier(indexer.GetterImplementation.CountParameterName)).WithType(WellKnownTypes.Imposter.Abstractions.Count))
            .WithBody(
                Block(
                    invocationCountDeclaration,
                    IfStatement(condition, Block(throwStatement))))
            .Build();
    }
    private static MethodDeclarationSyntax BuildMarkReturnConfiguredMethod(in ImposterIndexerMetadata indexer)
        => new MethodDeclarationBuilder(WellKnownTypes.Void, "MarkReturnConfigured")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    WellKnownTypes.System.Threading.Volatile
                        .Dot(IdentifierName("Write"))
                        .Call(ArgumentList(SeparatedList<ArgumentSyntax>(new SyntaxNodeOrToken[]
                        {
                            Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(indexer.GetterImplementation.HasConfiguredReturnField.Name)),
                            Token(SyntaxKind.CommaToken),
                            Argument(LiteralExpression(SyntaxKind.TrueLiteralExpression))
                        })))
                        .ToStatementSyntax()))
            .Build();
    private static MethodDeclarationSyntax BuildEnsureGetterConfiguredMethod(in ImposterIndexerMetadata indexer)
    {
        var explicitCheck = BinaryExpression(
            SyntaxKind.EqualsExpression,
            IdentifierName(indexer.GetterImplementation.InvocationBehaviorField.Name),
            WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior.Dot(IdentifierName("Explicit")));

        var hasReturnConfigured = IdentifierName(indexer.GetterImplementation.HasConfiguredReturnField.Name);
        var missingReturnCheck = PrefixUnaryExpression(
            SyntaxKind.LogicalNotExpression,
            WellKnownTypes.System.Threading.Volatile
                .Dot(IdentifierName("Read"))
                .Call(ArgumentList(SingletonSeparatedList(Argument(null, Token(SyntaxKind.RefKeyword), hasReturnConfigured)))));

        var condition = BinaryExpression(SyntaxKind.LogicalAndExpression, explicitCheck, missingReturnCheck);

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "EnsureGetterConfigured")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(condition, Block(BuildMissingImposterThrow(indexer, indexer.GetterImplementation.GetterSuffix)))))
            .Build();
    }

}


