using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImposter.Metadata;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.Imposter.ImposterInstance;

internal readonly ref struct ImposterInstanceBuilder
{
    // TODO this might collide. Move it to Metadata
    private const string ImposterFieldName = "_imposter";

    private readonly ClassDeclarationBuilder _imposterInstanceBuilder;

    private ImposterInstanceBuilder(ClassDeclarationBuilder imposterInstanceBuilder)
    {
        _imposterInstanceBuilder = imposterInstanceBuilder;
    }

    internal ImposterInstanceBuilder AddImposterProperty(in ImposterPropertyMetadata property)
    {
        var propertyBuilder = new PropertyDeclarationBuilder(property.Core.TypeSyntax, property.Core.Name)
            .AddModifiers(property.ImposterInstanceModifiers);

        if (property.Core.HasGetter)
        {
            var getterInvocation = IdentifierName("_imposter")
                .Dot(IdentifierName(property.AsField.Name))
                .Dot(IdentifierName("_getterImposterBuilder"))
                .Dot(IdentifierName("Get"));

            InvocationExpressionSyntax getterCall;

            if (property.Core.GetterSupportsBaseImplementation)
            {
                var baseGetterInvocation = BaseExpression().Dot(IdentifierName(property.Core.Name));
                getterCall = getterInvocation.Call(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                ParenthesizedLambdaExpression()
                                    .WithExpressionBody(baseGetterInvocation)))));
            }
            else
            {
                getterCall = getterInvocation.Call();
            }

            var getterBody = Block(
                ReturnStatement(getterCall));

            propertyBuilder = propertyBuilder.WithGetterBody(getterBody);
        }

        if (property.Core.HasSetter)
        {
            var setterInvocation = IdentifierName("_imposter")
                .Dot(IdentifierName(property.AsField.Name))
                .Dot(IdentifierName("_setterImposter"))
                .Dot(IdentifierName("Set"));

            var setterArguments = new List<ArgumentSyntax>
            {
                Argument(IdentifierName("value"))
            };

            if (property.Core.SetterSupportsBaseImplementation)
            {
                var baseAssignment = BaseExpression()
                    .Dot(IdentifierName(property.Core.Name))
                    .Assign(IdentifierName("value"));

                setterArguments.Add(
                    Argument(
                        ParenthesizedLambdaExpression()
                            .WithBlock(Block(baseAssignment.ToStatementSyntax()))));
            }

            var setterBody = Block(
            setterInvocation
                .Call(ArgumentListSyntax(setterArguments))
                .ToStatementSyntax());

            propertyBuilder = propertyBuilder.WithSetterBody(setterBody);
        }

        _imposterInstanceBuilder.AddMember(propertyBuilder.Build());
        return this;
    }

    internal ImposterInstanceBuilder AddIndexer(in ImposterIndexerMetadata indexer)
    {
        var parameters = indexer.Core.Parameters.Select(parameter => parameter.ParameterSyntax).ToArray();
        var parameterList = BracketedParameterList(SeparatedList(parameters));

        var accessors = new List<AccessorDeclarationSyntax>();

        if (indexer.Core.HasGetter)
        {
            var getterArguments = indexer.Core.Parameters
                .Select(parameter => Argument(IdentifierName(parameter.Name)))
                .ToList();

            if (indexer.Core.GetterSupportsBaseImplementation)
            {
                var baseInvocation = ElementAccessExpression(BaseExpression())
                    .WithArgumentList(
                        BracketedArgumentList(
                            SeparatedList(indexer.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))))));

                getterArguments.Add(
                    Argument(
                        ParenthesizedLambdaExpression()
                            .WithExpressionBody(baseInvocation)));
            }

            var getterCall = IdentifierName(ImposterFieldName)
                .Dot(IdentifierName(indexer.BuilderField.Name))
                .Dot(IdentifierName("Get"))
                .Call(ArgumentListSyntax(getterArguments));

            accessors.Add(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithBody(Block(ReturnStatement(getterCall))));
        }

        if (indexer.Core.HasSetter)
        {
            var setterArguments = indexer.Core.Parameters
                .Select(parameter => Argument(IdentifierName(parameter.Name)))
                .Concat([Argument(IdentifierName("value"))])
                .ToList();

            if (indexer.Core.SetterSupportsBaseImplementation)
            {
                var baseIndexerAccess = ElementAccessExpression(BaseExpression())
                    .WithArgumentList(
                        BracketedArgumentList(
                            SeparatedList(indexer.Core.Parameters.Select(parameter => Argument(IdentifierName(parameter.Name))))));

                var baseAssignment = baseIndexerAccess.Assign(IdentifierName("value"));

                setterArguments.Add(
                    Argument(
                        ParenthesizedLambdaExpression()
                            .WithBlock(
                                Block(
                                    baseAssignment.ToStatementSyntax()))));
            }

            var setterCall = IdentifierName(ImposterFieldName)
                .Dot(IdentifierName(indexer.BuilderField.Name))
                .Dot(IdentifierName("Set"))
                .Call(ArgumentListSyntax(setterArguments));

            accessors.Add(
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithBody(Block(setterCall.ToStatementSyntax())));
        }

        var indexerDeclaration = IndexerDeclaration(indexer.Core.TypeSyntax)
            .WithModifiers(indexer.ImposterInstanceModifiers)
            .WithParameterList(parameterList)
            .WithAccessorList(AccessorList(List(accessors)));

        _imposterInstanceBuilder.AddMember(indexerDeclaration);

        return this;
    }

    internal ImposterInstanceBuilder AddEvent(in ImposterEventMetadata @event)
    {
        var eventDeclaration = EventDeclaration(@event.Core.HandlerTypeSyntax, Identifier(@event.Core.Name))
            .WithModifiers(@event.ImposterInstanceModifiers)
            .WithAccessorList(
                AccessorList(
                    List([
                        AccessorDeclaration(SyntaxKind.AddAccessorDeclaration)
                            .WithBody(BuildEventAccessorBody(@event, isSubscribe: true)),
                        AccessorDeclaration(SyntaxKind.RemoveAccessorDeclaration)
                            .WithBody(BuildEventAccessorBody(@event, isSubscribe: false))
                    ])));

        _imposterInstanceBuilder.AddMember(eventDeclaration);

        return this;
    }
    internal ClassDeclarationSyntax Build() => _imposterInstanceBuilder.Build();

    internal static ImposterInstanceBuilder Create(in ImposterGenerationContext imposterGenerationContext, string name)
    {
        var fields = GetFields(imposterGenerationContext);

        var imposterClassBuilder = new ClassDeclarationBuilder(name)
            .AddBaseType(SimpleBaseType(TypeSyntax(imposterGenerationContext.TargetSymbol)))
            .AddMembers(fields);

        imposterClassBuilder = imposterGenerationContext.Imposter.IsClass
            ? imposterClassBuilder
                .AddMember(BuildInitializeImposterMethod(imposterGenerationContext.Imposter.Name))
                .AddMembers(BuildConstructorsForClassTarget(imposterGenerationContext, name))
            : imposterClassBuilder.AddMember(BuildConstructorAndInitializeMembers(name, fields));

        imposterClassBuilder = imposterClassBuilder
            .AddMembers(ImposterMethods(imposterGenerationContext));

        return new ImposterInstanceBuilder(imposterClassBuilder);
    }

    private static IReadOnlyList<FieldDeclarationSyntax> GetFields(ImposterGenerationContext imposterGenerationContext) =>
    [
        SingleVariableField(IdentifierName(imposterGenerationContext.Imposter.Name), ImposterFieldName)
    ];

    private static MethodDeclarationSyntax BuildInitializeImposterMethod(string imposterName)
    {
        var assignment = IdentifierName(ImposterFieldName).Assign(IdentifierName("imposter"));

        return new MethodDeclarationBuilder(
                WellKnownTypes.Void,
                "InitializeImposter")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                Parameter(Identifier("imposter"))
                    .WithType(IdentifierName(imposterName)))
            .WithBody(Block(assignment.ToStatementSyntax()))
            .Build();
    }

    private static List<ConstructorDeclarationSyntax> BuildConstructorsForClassTarget(
        in ImposterGenerationContext imposterGenerationContext,
        string name)
    {
        if (!imposterGenerationContext.Imposter.IsClass)
        {
            return [];
        }

        return imposterGenerationContext
            .Imposter
            .AccessibleConstructors
            .Select(constructorMetadata =>
            {
                var parameterList = ParameterListSyntax(constructorMetadata.Parameters, includeRefKind: true);
                var argumentList = ArgumentListSyntax(constructorMetadata.Parameters, includeRefKind: true);

                return new ConstructorBuilder(name)
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .WithParameterList(parameterList)
                    .AddInitializer(
                        ConstructorInitializer(
                            SyntaxKind.BaseConstructorInitializer,
                            argumentList))
                    .Build();

            }).ToList();
    }

    private static IEnumerable<MethodDeclarationSyntax> ImposterMethods(in ImposterGenerationContext imposterGenerationContext)
    {
        return imposterGenerationContext.Imposter.Methods.Select(imposterMethod =>
        {
            var invokeArguments = new List<ArgumentSyntax>(ArgumentListSyntax(imposterMethod.Symbol.Parameters, includeRefKind: true).Arguments);

            if (imposterMethod.SupportsBaseImplementation)
            {
                var baseMethodExpression = BaseExpression()
                    .Dot(IdentifierName(imposterMethod.Symbol.Name));
                invokeArguments.Add(Argument(baseMethodExpression));
            }

            var invokeMethodInvocationExpression = GetImposterWithMatchingSetupExpression(imposterMethod)
                .Dot(IdentifierName("Invoke"))
                .Call(ArgumentList(SeparatedList(invokeArguments)));

            var methodBuilder = new MethodDeclarationBuilder(TypeSyntax(imposterMethod.Symbol.ReturnType), imposterMethod.Symbol.Name)
                .AddTypeParameters(TypeParametersSyntax(imposterMethod.Symbol))
                .AddParameters(ParameterSyntaxes(imposterMethod.Symbol.Parameters))
                .WithBody(Block(
                    imposterMethod.HasReturnValue
                        ? ReturnStatement(invokeMethodInvocationExpression)
                        : invokeMethodInvocationExpression.ToStatementSyntax())
                )
                .AddModifiers(imposterMethod.ImposterInstanceMethodModifiers);

            foreach (var constraintClause in SyntaxFactoryHelper.TypeParameterConstraintClauses(imposterMethod.Symbol))
            {
                methodBuilder.AddConstraintClause(constraintClause);
            }

            return methodBuilder.Build();
        });

        ExpressionSyntax GetImposterWithMatchingSetupExpression(in ImposterTargetMethodMetadata method)
        {
            if (method.Symbol.IsGenericMethod)
            {
                return IdentifierName("_imposter")
                    .Dot(IdentifierName(method.MethodImposter.Collection.AsField.Name))
                    .Dot(GenericName(Identifier("GetImposterWithMatchingSetup"), method.GenericTypeArguments.ToTypeArguments()))
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
                                .New(ArgumentListSyntax(method.Parameters.InputParameters, includeRefKind: false))
                        )
                        .AsSingleArgumentListSyntax();
                }

                return default;
            }
        }
    }

    private static BlockSyntax BuildEventAccessorBody(in ImposterEventMetadata @event, bool isSubscribe)
    {
        var builderAccess = IdentifierName(ImposterFieldName)
            .Dot(IdentifierName(@event.BuilderField.Name));
        var arguments = new List<ArgumentSyntax>
        {
            Argument(IdentifierName("value"))
        };

        if (@event.Core.SupportsBaseImplementation)
        {
            arguments.Add(Argument(BuildBaseEventAccessorLambda(@event, isSubscribe)));
        }

        return Block(
        IdentifierName(nameof(ArgumentNullException))
        .Dot(IdentifierName("ThrowIfNull"))
        .Call(Argument(IdentifierName("value")))
        .ToStatementSyntax(),
        builderAccess
            .Dot(IdentifierName(isSubscribe ? "Subscribe" : "Unsubscribe"))
            .Call(arguments)
            .ToStatementSyntax());
    }

    private static ParenthesizedLambdaExpressionSyntax BuildBaseEventAccessorLambda(
        in ImposterEventMetadata @event,
        bool isSubscribe)
    {
        var assignmentExpression = AssignmentExpression(
            isSubscribe ? SyntaxKind.AddAssignmentExpression : SyntaxKind.SubtractAssignmentExpression,
            BaseExpression().Dot(IdentifierName(@event.Core.Name)),
            IdentifierName("value"));

        return ParenthesizedLambdaExpression()
            .WithBlock(Block(assignmentExpression.ToStatementSyntax()));
    }
}

