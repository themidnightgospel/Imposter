using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
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
            propertyBuilder = propertyBuilder.WithGetterBody(
                PropertyGetterBody(property),
                property.Core.GetterModifiers
            );
        }

        if (property.Core.HasSetter)
        {
            var setterBody = PropertySetterBody(property);
            propertyBuilder = property.Core.IsInitOnly
                ? propertyBuilder.WithInitBody(setterBody, property.Core.SetterModifiers)
                : propertyBuilder.WithSetterBody(setterBody, property.Core.SetterModifiers);
        }

        _imposterInstanceBuilder.AddMember(propertyBuilder.Build());
        return this;
    }

    // The getter asks the property's getter imposter for the value, passing the base getter when there is one.
    private BlockSyntax PropertyGetterBody(in ImposterPropertyMetadata property)
    {
        var getter = MemberBuilder(property.BuilderField.Name)
            .Dot(IdentifierName(property.ImposterBuilder.GetterImposterBuilderField.Name))
            .Dot(IdentifierName(property.GetterImposterBuilder.GetMethod.Name));
        ExpressionSyntax? baseGetter = property.Core.GetterSupportsBaseImplementation
            ? BaseExpression().Dot(IdentifierName(property.Core.Name))
            : null;
        var getterCall = baseGetter is null
            ? getter.Call()
            : getter.Call(Argument(EmptyParametersGoesTo(property.Core.StoredValue(baseGetter))));

        return WithConstructorFallback(
            Block(ReturnStatement(getterCall)),
            Block(ReturnStatement(baseGetter ?? DefaultNonNullable))
        );
    }

    // The setter passes the value to the property's setter imposter. When the base setter has to be assigned directly,
    // the imposter returns whether to, and the setter does it; otherwise the imposter gets a lambda that calls it.
    private BlockSyntax PropertySetterBody(in ImposterPropertyMetadata property)
    {
        var basePropertyAccess = property.Core.SetterSupportsBaseImplementation
            ? BaseExpression().Dot(IdentifierName(property.Core.Name))
            : null;
        var setterArguments = new List<ArgumentSyntax>
        {
            Argument(property.Core.StoredValue(IdentifierName("value"))),
        };
        if (basePropertyAccess is not null && !property.Core.SetterRequiresDirectBaseAssignment)
        {
            setterArguments.Add(Argument(BaseSetterLambda(basePropertyAccess)));
        }

        var setterCall = MemberBuilder(property.BuilderField.Name)
            .Dot(IdentifierName(property.ImposterBuilder.SetterImposterField.Name))
            .Dot(IdentifierName(property.SetterImposter.SetMethod.Name))
            .Call(ArgumentListSyntax(setterArguments));
        var setterBody = property.Core.SetterRequiresDirectBaseAssignment
            ? Block(
                IfStatement(
                    setterCall,
                    Block(basePropertyAccess!.Assign(IdentifierName("value")).ToStatementSyntax())
                )
            )
            : Block(setterCall.ToStatementSyntax());

        return WithConstructorFallback(
            setterBody,
            ConstructorDispatchBuilder.SetterFallback(
                basePropertyAccess?.Assign(IdentifierName("value"))
            )
        );
    }

    private static ParenthesizedLambdaExpressionSyntax BaseSetterLambda(
        ExpressionSyntax basePropertyAccess
    )
    {
        const string BaseSetterValueParameterName = "baseSetterValue";

        return ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(Parameter(Identifier(BaseSetterValueParameterName)))
                )
            )
            .WithBlock(
                Block(
                    basePropertyAccess
                        .Assign(IdentifierName(BaseSetterValueParameterName))
                        .ToStatementSyntax()
                )
            );
    }

    internal ImposterInstanceBuilder AddIndexer(in ImposterIndexerMetadata indexer)
    {
        var lambdaCopies = CopyForLambdas(indexer);
        var accessors = new List<AccessorDeclarationSyntax>();
        if (indexer.Core.HasGetter)
        {
            accessors.Add(IndexerGetter(indexer, lambdaCopies));
        }

        if (indexer.Core.HasSetter)
        {
            accessors.Add(IndexerSetter(indexer, lambdaCopies));
        }

        var parameters = indexer.Core.Parameters.Select(parameter =>
            ParameterSyntaxIncludingNullable(parameter.Model)
        );
        _imposterInstanceBuilder.AddMember(
            IndexerDeclaration(indexer.Core.NullableAwareTypeSyntax)
                .WithModifiers(indexer.ImposterInstanceModifiers)
                .WithExplicitInterfaceSpecifier(indexer.ExplicitInterfaceSpecifier)
                .WithParameterList(BracketedParameterList(SeparatedList(parameters)))
                .WithAccessorList(AccessorList(List(accessors)))
        );

        return this;
    }

    // The getter asks the indexer's builder for the value, passing the base getter when there is one.
    private AccessorDeclarationSyntax IndexerGetter(
        in ImposterIndexerMetadata indexer,
        LambdaCopies lambdaCopies
    )
    {
        var statements = new List<StatementSyntax>();
        var arguments = new List<ArgumentSyntax>(ImposterArguments(indexer));
        ExpressionSyntax? baseGetter = indexer.Core.GetterSupportsBaseImplementation
            ? BaseIndexerAccess(indexer.Core.ParameterArguments)
            : null;
        if (baseGetter is not null)
        {
            statements.AddRange(lambdaCopies.KeyCopies);
            arguments.Add(
                Argument(
                    EmptyParametersGoesTo(
                        indexer.Core.StoredValue(BaseIndexerAccess(lambdaCopies.LambdaArguments))
                    )
                )
            );
        }

        statements.Add(
            ReturnStatement(
                MemberBuilder(indexer.BuilderField.Name)
                    .Dot(IdentifierName("Get"))
                    .Call(ArgumentListSyntax(arguments))
            )
        );

        return AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
            .WithModifiers(indexer.Core.GetterModifiers)
            .WithBody(
                WithConstructorFallback(
                    Block(statements),
                    Block(ReturnStatement(baseGetter ?? DefaultNonNullable))
                )
            );
    }

    // The setter passes the value to the indexer's builder, with the base setter when there is one.
    private AccessorDeclarationSyntax IndexerSetter(
        in ImposterIndexerMetadata indexer,
        LambdaCopies lambdaCopies
    )
    {
        var statements = new List<StatementSyntax>();
        var arguments = new List<ArgumentSyntax>(ImposterArguments(indexer))
        {
            Argument(indexer.Core.StoredValue(IdentifierName("value"))),
        };
        var baseAssignment = indexer.Core.SetterSupportsBaseImplementation
            ? BaseIndexerAssignment(indexer.Core.ParameterArguments, IdentifierName("value"))
            : null;
        if (baseAssignment is not null)
        {
            statements.AddRange(lambdaCopies.KeyCopies);
            if (lambdaCopies.ValueCopy is { } valueCopy)
            {
                statements.Add(valueCopy);
            }

            arguments.Add(
                Argument(
                    EmptyParametersGoesTo(
                        Block(
                            BaseIndexerAssignment(
                                    lambdaCopies.LambdaArguments,
                                    lambdaCopies.LambdaValue
                                )
                                .ToStatementSyntax()
                        )
                    )
                )
            );
        }

        statements.Add(
            MemberBuilder(indexer.BuilderField.Name)
                .Dot(IdentifierName("Set"))
                .Call(ArgumentListSyntax(arguments))
                .ToStatementSyntax()
        );

        return AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
            .WithModifiers(indexer.Core.SetterModifiers)
            .WithBody(
                WithConstructorFallback(
                    Block(statements),
                    ConstructorDispatchBuilder.SetterFallback(baseAssignment)
                )
            );
    }

    private static IEnumerable<ArgumentSyntax> ImposterArguments(
        in ImposterIndexerMetadata indexer
    ) => indexer.Core.Parameters.Select(parameter => parameter.ImposterArgument);

    // A lambda cannot capture an `in` or `ref readonly` parameter or a span, so the base-call lambdas read local copies
    // of them. A span's copy is the array of its elements, which converts back to the span.
    private readonly record struct LambdaCopies(
        IReadOnlyList<StatementSyntax> KeyCopies,
        IReadOnlyList<ArgumentSyntax> LambdaArguments,
        StatementSyntax? ValueCopy,
        ExpressionSyntax LambdaValue
    );

    private static LambdaCopies CopyForLambdas(in ImposterIndexerMetadata indexer)
    {
        var localNames = new NameSet(
            indexer.Core.Parameters.Select(parameter => parameter.Name).Append("value")
        );
        var keyCopies = new List<StatementSyntax>();
        var lambdaArguments = new List<ArgumentSyntax>();

        foreach (var parameter in indexer.Core.Parameters)
        {
            var argumentName = parameter.Name;
            if (
                parameter.Model.Span is not null
                || parameter.Model.RefKind is RefKind.In or RefKinds.RefReadOnlyParameter
            )
            {
                argumentName = localNames.Use($"{parameter.Name}Copy");
                keyCopies.Add(
                    LocalVariableDeclarationSyntax(
                        Var,
                        argumentName,
                        parameter.Model.Span is null
                            ? IdentifierName(parameter.Name)
                            : SpanElementsCopy(IdentifierName(parameter.Name))
                    )
                );
            }

            lambdaArguments.Add(parameter.ForwardingArgument(argumentName));
        }

        if (!indexer.Core.HasSpanValue)
        {
            return new LambdaCopies(keyCopies, lambdaArguments, null, IdentifierName("value"));
        }

        var valueCopyName = localNames.Use("valueCopy");
        var valueCopy = LocalVariableDeclarationSyntax(
            Var,
            valueCopyName,
            SpanElementsCopy(IdentifierName("value"))
        );

        return new LambdaCopies(
            keyCopies,
            lambdaArguments,
            valueCopy,
            IdentifierName(valueCopyName)
        );
    }

    private static ElementAccessExpressionSyntax BaseIndexerAccess(
        IEnumerable<ArgumentSyntax> arguments
    ) =>
        ElementAccessExpression(BaseExpression())
            .WithArgumentList(BracketedArgumentList(SeparatedList(arguments)));

    private static AssignmentExpressionSyntax BaseIndexerAssignment(
        IEnumerable<ArgumentSyntax> arguments,
        ExpressionSyntax value
    ) => BaseIndexerAccess(arguments).Assign(value);

    internal ImposterInstanceBuilder AddEvent(in ImposterEventMetadata @event)
    {
        _imposterInstanceBuilder.AddMember(
            EventDeclaration(@event.Core.DeclaredTypeSyntax, Identifier(@event.Core.Name))
                .WithModifiers(@event.ImposterInstanceModifiers)
                .WithExplicitInterfaceSpecifier(@event.ExplicitInterfaceSpecifier)
                .WithAccessorList(
                    AccessorList(
                        List([
                            EventAccessor(@event, SyntaxKind.AddAccessorDeclaration),
                            EventAccessor(@event, SyntaxKind.RemoveAccessorDeclaration),
                        ])
                    )
                )
        );

        return this;
    }

    // The add and remove accessors subscribe and unsubscribe the handler through the event's builder, which also
    // passes it on to the base event when the target has one.
    private AccessorDeclarationSyntax EventAccessor(
        in ImposterEventMetadata @event,
        SyntaxKind accessorKind
    )
    {
        var isAdd = accessorKind == SyntaxKind.AddAccessorDeclaration;
        var baseAssignment = @event.Core.SupportsBaseImplementation
            ? AssignmentExpression(
                isAdd
                    ? SyntaxKind.AddAssignmentExpression
                    : SyntaxKind.SubtractAssignmentExpression,
                BaseExpression().Dot(IdentifierName(@event.Core.Name)),
                IdentifierName("value")
            )
            : null;
        var arguments = new List<ArgumentSyntax> { Argument(IdentifierName("value")) };
        if (baseAssignment is not null)
        {
            arguments.Add(
                Argument(EmptyParametersGoesTo(Block(baseAssignment.ToStatementSyntax())))
            );
        }

        var body = Block(
            ThrowIfNull("value"),
            MemberBuilder(@event.BuilderField.Name)
                .Dot(IdentifierName(isAdd ? "Subscribe" : "Unsubscribe"))
                .Call(arguments)
                .ToStatementSyntax()
        );

        return AccessorDeclaration(accessorKind)
            .WithBody(
                WithConstructorFallback(
                    body,
                    ConstructorDispatchBuilder.SetterFallback(baseAssignment)
                )
            );
    }

    internal ClassDeclarationSyntax Build() => _imposterInstanceBuilder.Build();

    // The imposter's builder of a member, which the instance's accessors forward to.
    private MemberAccessExpressionSyntax MemberBuilder(string builderFieldName) =>
        IdentifierName(_imposterFieldName).Dot(IdentifierName(builderFieldName));

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
    // would shadow it, and a constructor parameter would duplicate the constructor's imposter parameter.
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
        // The instance constructors take the imposter under the field's name next to the target's own parameters.
        var constructorParameterNames = target.AccessibleConstructors.SelectMany(constructor =>
            constructor.Parameters.Select(parameter => parameter.Name)
        );

        var nameSet = new NameSet(
            target
                .MemberNames.Concat(methodScopeNames)
                .Concat(indexerScopeNames)
                .Concat(constructorParameterNames)
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
        var hasRequiredMembers = imposterGenerationContext.Imposter.HasRequiredMembers;

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

                var constructor = new ConstructorBuilder(name)
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

                return hasRequiredMembers
                    ? LeavingRequiredMembersAtDefaults(constructor)
                    : constructor;
            })
            .ToList();
    }

    // The imposter creates its instance with new and leaves C# 11 required members at their defaults, which
    // [SetsRequiredMembers] allows. Nullable analysis still reports the non-nullable ones (CS8618).
    private static ConstructorDeclarationSyntax LeavingRequiredMembersAtDefaults(
        ConstructorDeclarationSyntax constructor
    ) =>
        constructor
            .AddAttributeLists(DefaultAttributes.SetsRequiredMembersAttribute)
            .WithLeadingTrivia(Trivia(DisableWarning("CS8618")))
            .WithTrailingTrivia(Trivia(RestoreWarning("CS8618")));

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
                    .Dot(imposterMethod.WithGenericArguments(imposterMethod.Model.Name));
                invokeArguments.Add(Argument(baseMethodExpression));
            }

            var invokeMethodInvocationExpression =
                GetImposterWithMatchingInvocationImposterGroupExpression(imposterMethod)
                    .Dot(IdentifierName(MethodImposterInvokeMethodMetadata.Name))
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
                    imposterMethod.Parameters.AllParameters.Select(it =>
                        ParameterSyntaxWithoutDefaultValue(it)
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
            var imposterField = IdentifierName(imposterFieldName)
                .Dot(IdentifierName(method.ImposterField.Name));

            if (method.Model.IsGenericMethod)
            {
                return imposterField
                    .Dot(
                        GenericName(
                            Identifier(
                                MethodImposterCollectionMetadata.GetImposterWithMatchingInvocationImposterGroupMethodName
                            ),
                            TypeArguments(method.GenericTypeArguments)
                        )
                    )
                    .Call(GetGetImposterWithMatchingInvocationImposterGroupArguments(method));
            }

            return imposterField;

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
                        .ToSingleArgumentList();
                }

                return default;
            }
        }
    }
}
