using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.ImposterInstance;

internal readonly ref struct ImposterInstanceBuilder
{
    private readonly ClassDeclarationBuilder _imposterInstanceBuilder;
    private readonly string _imposterFieldName;
    private readonly bool _isClass;

    private ImposterInstanceBuilder(
        ClassDeclarationBuilder imposterInstanceBuilder,
        string imposterFieldName,
        bool isClass
    )
    {
        _imposterInstanceBuilder = imposterInstanceBuilder;
        _imposterFieldName = imposterFieldName;
        _isClass = isClass;
    }

    internal ImposterInstanceBuilder AddImposterProperty(in ImposterPropertyMetadata property)
    {
        var propertyBuilder = new PropertyDeclarationBuilder(
            property.Core.NullableAwareTypeSyntax,
            property.Core.Name
        )
            .AddModifiers(property.ImposterInstanceModifiers)
            .WithExplicitInterfaceSpecifier(property.ExplicitInterfaceSpecifier);

        if (property.Core.HasGetter)
        {
            var getterInvocation = IdentifierName(_imposterFieldName)
                .Dot(IdentifierName(property.AsField.Name))
                .Dot(IdentifierName("_getterImposterBuilder"))
                .Dot(IdentifierName("Get"));

            InvocationExpressionSyntax getterCall;
            ExpressionSyntax? baseGetterInvocation = property.Core.GetterSupportsBaseImplementation
                ? BaseExpression().Dot(IdentifierName(property.Core.Name))
                : null;

            if (baseGetterInvocation is not null)
            {
                getterCall = getterInvocation.Call(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(EmptyParametersGoesTo(baseGetterInvocation))
                        )
                    )
                );
            }
            else
            {
                getterCall = getterInvocation.Call();
            }

            var getterBody = Block(ReturnStatement(getterCall));
            getterBody = WithConstructorFallback(
                getterBody,
                Block(ReturnStatement(baseGetterInvocation ?? DefaultNonNullable))
            );

            propertyBuilder = propertyBuilder.WithGetterBody(
                getterBody,
                property.Core.GetterModifiers
            );
        }

        if (property.Core.HasSetter)
        {
            var setterInvocation = IdentifierName(_imposterFieldName)
                .Dot(IdentifierName(property.AsField.Name))
                .Dot(IdentifierName("_setterImposter"))
                .Dot(IdentifierName("Set"));

            var setterArguments = new List<ArgumentSyntax> { Argument(IdentifierName("value")) };
            var basePropertyAccess = property.Core.SetterSupportsBaseImplementation
                ? BaseExpression().Dot(IdentifierName(property.Core.Name))
                : null;

            if (basePropertyAccess is not null && !property.Core.SetterRequiresDirectBaseAssignment)
            {
                const string BaseSetterValueParameterName = "baseSetterValue";
                var baseSetterValueIdentifier = IdentifierName(BaseSetterValueParameterName);
                var baseAssignment = basePropertyAccess.Assign(baseSetterValueIdentifier);

                setterArguments.Add(
                    Argument(
                        ParenthesizedLambdaExpression()
                            .WithParameterList(
                                ParameterList(
                                    SingletonSeparatedList(
                                        Parameter(Identifier(BaseSetterValueParameterName))
                                    )
                                )
                            )
                            .WithBlock(Block(baseAssignment.ToStatementSyntax()))
                    )
                );
            }

            var setterCall = setterInvocation.Call(ArgumentListSyntax(setterArguments));
            var setterBody = property.Core.SetterRequiresDirectBaseAssignment
                ? Block(
                    IfStatement(
                        setterCall,
                        Block(
                            basePropertyAccess!.Assign(IdentifierName("value")).ToStatementSyntax()
                        )
                    )
                )
                : Block(setterCall.ToStatementSyntax());
            setterBody = WithConstructorFallback(
                setterBody,
                ConstructorDispatchBuilder.SetterFallback(
                    basePropertyAccess?.Assign(IdentifierName("value"))
                )
            );

            propertyBuilder = property.Core.IsInitOnly
                ? propertyBuilder.WithInitBody(setterBody, property.Core.SetterModifiers)
                : propertyBuilder.WithSetterBody(setterBody, property.Core.SetterModifiers);
        }

        _imposterInstanceBuilder.AddMember(propertyBuilder.Build());
        return this;
    }

    internal ImposterInstanceBuilder AddIndexer(in ImposterIndexerMetadata indexer)
    {
        var parameters = indexer
            .Core.Parameters.Select(parameter => ParameterSyntaxIncludingNullable(parameter.Model))
            .ToArray();
        var parameterList = BracketedParameterList(SeparatedList(parameters));
        var (parameterCopies, lambdaArguments) = CopyReadOnlyReferenceParametersForLambdas(indexer);

        var accessors = new List<AccessorDeclarationSyntax>();

        if (indexer.Core.HasGetter)
        {
            var getterArguments = new List<ArgumentSyntax>(indexer.Core.ParameterArguments);
            var getterStatements = new List<StatementSyntax>();

            ExpressionSyntax? baseInvocation = indexer.Core.GetterSupportsBaseImplementation
                ? BaseIndexerAccess(indexer.Core.ParameterArguments)
                : null;

            if (baseInvocation is not null)
            {
                getterStatements.AddRange(parameterCopies);
                getterArguments.Add(
                    Argument(EmptyParametersGoesTo(BaseIndexerAccess(lambdaArguments)))
                );
            }

            var getterCall = IdentifierName(_imposterFieldName)
                .Dot(IdentifierName(indexer.BuilderField.Name))
                .Dot(IdentifierName("Get"))
                .Call(ArgumentListSyntax(getterArguments));
            getterStatements.Add(ReturnStatement(getterCall));

            accessors.Add(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithModifiers(indexer.Core.GetterModifiers)
                    .WithBody(
                        WithConstructorFallback(
                            Block(getterStatements),
                            Block(ReturnStatement(baseInvocation ?? DefaultNonNullable))
                        )
                    )
            );
        }

        if (indexer.Core.HasSetter)
        {
            var setterArguments = new List<ArgumentSyntax>(indexer.Core.ParameterArguments)
            {
                Argument(IdentifierName("value")),
            };
            var setterStatements = new List<StatementSyntax>();

            var baseAssignment = indexer.Core.SetterSupportsBaseImplementation
                ? BaseIndexerAssignment(indexer.Core.ParameterArguments)
                : null;

            if (baseAssignment is not null)
            {
                setterStatements.AddRange(parameterCopies);
                setterArguments.Add(
                    Argument(
                        EmptyParametersGoesTo(
                            Block(BaseIndexerAssignment(lambdaArguments).ToStatementSyntax())
                        )
                    )
                );
            }

            var setterCall = IdentifierName(_imposterFieldName)
                .Dot(IdentifierName(indexer.BuilderField.Name))
                .Dot(IdentifierName("Set"))
                .Call(ArgumentListSyntax(setterArguments));
            setterStatements.Add(setterCall.ToStatementSyntax());

            accessors.Add(
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithModifiers(indexer.Core.SetterModifiers)
                    .WithBody(
                        WithConstructorFallback(
                            Block(setterStatements),
                            ConstructorDispatchBuilder.SetterFallback(baseAssignment)
                        )
                    )
            );
        }

        var indexerDeclaration = IndexerDeclaration(indexer.Core.NullableAwareTypeSyntax)
            .WithModifiers(indexer.ImposterInstanceModifiers)
            .WithExplicitInterfaceSpecifier(indexer.ExplicitInterfaceSpecifier)
            .WithParameterList(parameterList)
            .WithAccessorList(AccessorList(List(accessors)));

        _imposterInstanceBuilder.AddMember(indexerDeclaration);

        return this;
    }

    // A lambda cannot capture an `in` or `ref readonly` parameter, so the base-call lambdas read local copies of
    // those parameters.
    private static (
        IReadOnlyList<StatementSyntax> Copies,
        IReadOnlyList<ArgumentSyntax> LambdaArguments
    ) CopyReadOnlyReferenceParametersForLambdas(in ImposterIndexerMetadata indexer)
    {
        var localNames = new NameSet(
            indexer.Core.Parameters.Select(parameter => parameter.Name).Append("value")
        );
        var copies = new List<StatementSyntax>();
        var lambdaArguments = new List<ArgumentSyntax>();

        foreach (var parameter in indexer.Core.Parameters)
        {
            var argumentName = parameter.Name;
            if (parameter.Model.RefKind is RefKind.In or RefKinds.RefReadOnlyParameter)
            {
                argumentName = localNames.Use($"{parameter.Name}Copy");
                copies.Add(
                    LocalVariableDeclarationSyntax(
                        Var,
                        argumentName,
                        IdentifierName(parameter.Name)
                    )
                );
            }

            lambdaArguments.Add(parameter.ForwardingArgument(argumentName));
        }

        return (copies, lambdaArguments);
    }

    private static ElementAccessExpressionSyntax BaseIndexerAccess(
        IEnumerable<ArgumentSyntax> arguments
    ) =>
        ElementAccessExpression(BaseExpression())
            .WithArgumentList(BracketedArgumentList(SeparatedList(arguments)));

    private static AssignmentExpressionSyntax BaseIndexerAssignment(
        IEnumerable<ArgumentSyntax> arguments
    ) => BaseIndexerAccess(arguments).Assign(IdentifierName("value"));

    internal ImposterInstanceBuilder AddEvent(in ImposterEventMetadata @event)
    {
        var baseSubscribeAssignment = BuildBaseEventAccessorAssignment(@event, isSubscribe: true);
        var baseUnsubscribeAssignment = BuildBaseEventAccessorAssignment(
            @event,
            isSubscribe: false
        );
        var eventDeclaration = EventDeclaration(
                @event.Core.NullableAwareHandlerTypeSyntax,
                Identifier(@event.Core.Name)
            )
            .WithModifiers(@event.ImposterInstanceModifiers)
            .WithExplicitInterfaceSpecifier(@event.ExplicitInterfaceSpecifier)
            .WithAccessorList(
                AccessorList(
                    List([
                        AccessorDeclaration(SyntaxKind.AddAccessorDeclaration)
                            .WithBody(
                                WithConstructorFallback(
                                    BuildEventAccessorBody(
                                        @event,
                                        isSubscribe: true,
                                        _imposterFieldName,
                                        baseSubscribeAssignment
                                    ),
                                    ConstructorDispatchBuilder.SetterFallback(
                                        baseSubscribeAssignment
                                    )
                                )
                            ),
                        AccessorDeclaration(SyntaxKind.RemoveAccessorDeclaration)
                            .WithBody(
                                WithConstructorFallback(
                                    BuildEventAccessorBody(
                                        @event,
                                        isSubscribe: false,
                                        _imposterFieldName,
                                        baseUnsubscribeAssignment
                                    ),
                                    ConstructorDispatchBuilder.SetterFallback(
                                        baseUnsubscribeAssignment
                                    )
                                )
                            ),
                    ])
                )
            );

        _imposterInstanceBuilder.AddMember(eventDeclaration);

        return this;
    }

    internal ClassDeclarationSyntax Build() => _imposterInstanceBuilder.Build();

    private BlockSyntax WithConstructorFallback(BlockSyntax body, BlockSyntax fallback) =>
        _isClass
            ? ConstructorDispatchBuilder.WithFallback(body, fallback, _imposterFieldName)
            : body;

    internal static ImposterInstanceBuilder Create(
        in ImposterGenerationContext imposterGenerationContext,
        string name
    )
    {
        var imposterFieldName = CreateImposterFieldName(imposterGenerationContext);
        var fields = GetFields(imposterGenerationContext, imposterFieldName);

        var imposterClassBuilder = new ClassDeclarationBuilder(name)
            .AddBaseType(SimpleBaseType(imposterGenerationContext.Imposter.TargetTypeSyntax))
            .AddMembers(fields);

        imposterClassBuilder = imposterGenerationContext.Imposter.IsClass
            ? imposterClassBuilder.AddMembers(
                BuildConstructorsForClassTarget(imposterGenerationContext, name, imposterFieldName)
            )
            : imposterClassBuilder.AddMember(BuildConstructorAndInitializeMembers(name, fields));

        imposterClassBuilder = imposterClassBuilder.AddMembers(
            ImposterMethods(imposterGenerationContext, imposterFieldName)
        );

        return new ImposterInstanceBuilder(
            imposterClassBuilder,
            imposterFieldName,
            imposterGenerationContext.Imposter.IsClass
        );
    }

    private static IReadOnlyList<FieldDeclarationSyntax> GetFields(
        in ImposterGenerationContext imposterGenerationContext,
        string imposterFieldName
    ) =>
        [
            SinglePrivateReadonlyVariableField(
                imposterGenerationContext.Imposter.ImposterTypeSyntax,
                imposterFieldName
            ),
        ];

    // The instance's members reach the field by its bare name, so a parameter or method type parameter with that name
    // would shadow it.
    private static string CreateImposterFieldName(
        in ImposterGenerationContext imposterGenerationContext
    )
    {
        var target = imposterGenerationContext.Target;
        var methodScopeNames = target.Methods.SelectMany(method =>
            method
                .Member.Parameters.Select(parameter => parameter.Name)
                .Concat(method.Member.TypeParameters.Select(typeParameter => typeParameter.Name))
        );
        var indexerScopeNames = target.Indexers.SelectMany(indexer =>
            indexer.Member.Parameters.Select(parameter => parameter.Name)
        );

        var nameSet = new NameSet(
            target.MemberNames.Concat(methodScopeNames).Concat(indexerScopeNames)
        );
        return nameSet.Use("_imposter");
    }

    private static List<ConstructorDeclarationSyntax> BuildConstructorsForClassTarget(
        in ImposterGenerationContext imposterGenerationContext,
        string name,
        string imposterFieldName
    )
    {
        var imposterTypeSyntax = imposterGenerationContext.Imposter.ImposterTypeSyntax;
        var accessibleConstructors = imposterGenerationContext.Imposter.AccessibleConstructors;

        return accessibleConstructors
            .Select(constructorMetadata =>
            {
                var constructorParameters = new List<ParameterSyntax>(
                    constructorMetadata.Parameters.Length + 1
                )
                {
                    ParameterSyntax(imposterTypeSyntax, imposterFieldName),
                };
                constructorParameters.AddRange(
                    constructorMetadata.Parameters.Select(parameter =>
                        ParameterSyntax(parameter, includeRefKind: true)
                    )
                );

                var baseArgumentList = ArgumentListSyntax(
                    constructorMetadata.Parameters,
                    includeRefKind: true
                );

                var constructorBody = Block(
                    ThisExpression()
                        .Dot(IdentifierName(imposterFieldName))
                        .Assign(IdentifierName(imposterFieldName))
                        .ToStatementSyntax()
                );

                return new ConstructorBuilder(name)
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .WithParameterList(ParameterListSyntax(constructorParameters))
                    .AddInitializer(
                        ConstructorInitializer(
                            SyntaxKind.BaseConstructorInitializer,
                            baseArgumentList
                        )
                    )
                    .WithBody(constructorBody)
                    .Build();
            })
            .ToList();
    }

    private static IEnumerable<MethodDeclarationSyntax> ImposterMethods(
        in ImposterGenerationContext imposterGenerationContext,
        string imposterFieldName
    )
    {
        var isClass = imposterGenerationContext.Imposter.IsClass;
        return imposterGenerationContext.Imposter.Methods.Select(imposterMethod =>
        {
            var invokeArguments = new List<ArgumentSyntax>(
                ArgumentListSyntax(
                    imposterMethod.Parameters.AllParameters,
                    includeRefKind: true
                ).Arguments
            );

            if (imposterMethod.SupportsBaseImplementation)
            {
                // Type arguments are explicit because a type parameter that only appears in the
                // return type, or not in the signature at all, cannot be inferred.
                var baseMethodExpression = BaseExpression()
                    .Dot(WithMethodGenericArguments(imposterMethod.Model.Name, imposterMethod));
                invokeArguments.Add(Argument(baseMethodExpression));
            }

            var invokeMethodInvocationExpression =
                GetImposterWithMatchingInvocationImposterGroupExpression(imposterMethod)
                    .Dot(IdentifierName("Invoke"))
                    .Call(ArgumentList(SeparatedList(invokeArguments)));

            var body = Block(
                imposterMethod.HasReturnValue
                    ? ReturnStatement(invokeMethodInvocationExpression)
                    : invokeMethodInvocationExpression.ToStatementSyntax()
            );
            if (isClass)
            {
                body = ConstructorDispatchBuilder.WithFallback(
                    body,
                    ConstructorDispatchBuilder.MethodFallback(imposterMethod),
                    imposterFieldName
                );
            }

            var methodBuilder = new MethodDeclarationBuilder(
                TypeSyntaxIncludingNullable(imposterMethod.Model.ReturnType.Type),
                imposterMethod.Model.Name
            )
                .AddTypeParameters(TypeParametersSyntax(imposterMethod.Model.TypeParameters))
                .AddParameters(
                    imposterMethod.Parameters.AllParameterMetadata.Select(p =>
                        ParameterSyntaxWithoutDefaultValue(p)
                    )
                )
                .WithBody(body)
                .AddModifiers(imposterMethod.ImposterInstanceMethodModifiers)
                .WithExplicitInterfaceSpecifier(imposterMethod.ExplicitInterfaceSpecifier);

            foreach (var constraintClause in imposterMethod.ImposterInstanceMethodConstraintClauses)
            {
                methodBuilder.AddConstraintClause(constraintClause);
            }

            return methodBuilder.Build();
        });

        ExpressionSyntax GetImposterWithMatchingInvocationImposterGroupExpression(
            in ImposterTargetMethodMetadata method
        )
        {
            if (method.Model.IsGenericMethod)
            {
                return IdentifierName(imposterFieldName)
                    .Dot(IdentifierName(method.MethodImposter.Collection.AsField.Name))
                    .Dot(
                        GenericName(
                            Identifier("GetImposterWithMatchingInvocationImposterGroup"),
                            method.GenericTypeArguments.ToTypeArguments()
                        )
                    )
                    .Call(GetGetImposterWithMatchingInvocationImposterGroupArguments(method));
            }

            return IdentifierName(imposterFieldName)
                .Dot(IdentifierName(method.MethodImposter.AsField.Name));

            static ArgumentListSyntax? GetGetImposterWithMatchingInvocationImposterGroupArguments(
                in ImposterTargetMethodMetadata method
            )
            {
                if (method.Parameters.HasInputParameters)
                {
                    return Argument(
                            method.Arguments.Syntax.New(
                                ArgumentListSyntax(
                                    method.Parameters.InputParameters,
                                    includeRefKind: false
                                )
                            )
                        )
                        .AsSingleArgumentListSyntax();
                }

                return default;
            }
        }
    }

    private static BlockSyntax BuildEventAccessorBody(
        in ImposterEventMetadata @event,
        bool isSubscribe,
        string imposterFieldName,
        ExpressionSyntax? baseAssignment
    )
    {
        var builderAccess = IdentifierName(imposterFieldName)
            .Dot(IdentifierName(@event.BuilderField.Name));
        var arguments = new List<ArgumentSyntax> { Argument(IdentifierName("value")) };

        if (baseAssignment is not null)
        {
            arguments.Add(
                Argument(EmptyParametersGoesTo(Block(baseAssignment.ToStatementSyntax())))
            );
        }

        return Block(
            ThrowIfNull("value"),
            builderAccess
                .Dot(IdentifierName(isSubscribe ? "Subscribe" : "Unsubscribe"))
                .Call(arguments)
                .ToStatementSyntax()
        );
    }

    private static AssignmentExpressionSyntax? BuildBaseEventAccessorAssignment(
        in ImposterEventMetadata @event,
        bool isSubscribe
    ) =>
        @event.Core.SupportsBaseImplementation
            ? AssignmentExpression(
                isSubscribe
                    ? SyntaxKind.AddAssignmentExpression
                    : SyntaxKind.SubtractAssignmentExpression,
                BaseExpression().Dot(IdentifierName(@event.Core.Name)),
                IdentifierName("value")
            )
            : null;
}
