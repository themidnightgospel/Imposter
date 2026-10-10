using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using GetterReturnsMetadata = Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface.ReturnsMethodMetadata;
using GetterThrowsMetadata = Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface.ThrowsMethodMetadata;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

// The GetterImposter's nested Builder class, which sets up the getter invocation imposter for some criteria.
internal static partial class IndexerGetterBuilder
{
    private static ClassDeclarationSyntax BuildGetterBuilder(in ImposterIndexerMetadata indexer)
    {
        var builder = indexer.GetterImplementation.Builder;
        var returns = indexer.GetterBuilderInterface.ReturnsMethod;
        var throws = indexer.GetterBuilderInterface.ThrowsMethod;
        var imposterField = new FieldMetadata(
            builder.ImposterFieldName,
            indexer.GetterImplementation.TypeSyntax
        );
        var criteriaField = new FieldMetadata(
            builder.CriteriaFieldName,
            indexer.ArgumentsCriteria.TypeSyntax
        );

        return new ClassDeclarationBuilder(builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.TypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.FluentInterfaceTypeSyntax))
            .AddMember(SinglePrivateReadonlyVariableField(imposterField))
            .AddMember(SinglePrivateReadonlyVariableField(criteriaField))
            .AddMember(
                new ConstructorWithFieldInitializationBuilder(builder.Name)
                    .WithModifiers(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(imposterField)
                    .AddParameter(criteriaField)
                    .Build()
            )
            .AddMember(BuildGetterBuilderInvocationProperty(indexer.GetterImplementation))
            .AddMember(BuildGetterBuilderReturnsValueMethod(indexer, returns))
            .AddMember(BuildGetterBuilderReturnsFuncMethod(indexer, returns))
            .AddMember(BuildGetterBuilderReturnsDelegateMethod(indexer, returns))
            .AddMember(BuildGetterBuilderThrowsExceptionMethod(indexer, throws))
            .AddMember(BuildGetterBuilderThrowsGenericExceptionMethod(indexer, throws))
            .AddMember(BuildGetterBuilderThrowsDelegateMethod(indexer, throws))
            .AddMember(BuildGetterBuilderCallbackMethod(indexer))
            .AddMember(BuildGetterBuilderCalledMethod(indexer))
            .AddMember(BuildGetterBuilderThenMethod(indexer))
            .AddMember(
                indexer.GetterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildGetterBuilderUseBaseImplementationMethod(indexer)
                    : null
            )
            .Build();
    }

    private static PropertyDeclarationSyntax BuildGetterBuilderInvocationProperty(
        in IndexerGetterImposterMetadata getter
    ) =>
        new PropertyDeclarationBuilder(
            getter.Invocation.TypeSyntax,
            getter.Builder.InvocationImposterPropertyName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .Build()
            .WithAccessorList(null)
            .WithExpressionBody(
                ArrowExpressionClause(
                    IdentifierName(getter.Builder.ImposterFieldName)
                        .Dot(IdentifierName("GetOrCreate"))
                        .Call(Argument(IdentifierName(getter.Builder.CriteriaFieldName)))
                )
            )
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsValueMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returns
    ) =>
        new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
            .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
            .AddParameter(ParameterSyntax(returns.ValueParameter))
            .WithBody(
                AddReturnValueAndReturnThis(
                    indexer.GetterImplementation,
                    IdentifierName(returns.ValueParameter.Name)
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsFuncMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returns
    ) =>
        new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
            .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
            .AddParameter(ParameterSyntax(returns.FuncParameter))
            .WithBody(
                AddReturnValueAndReturnThis(
                    indexer.GetterImplementation,
                    IdentifierName(returns.FuncParameter.Name).Call()
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsDelegateMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returns
    ) =>
        new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
            .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
            .AddParameter(ParameterSyntax(returns.DelegateParameter))
            .WithBody(
                AddReturnValueAndReturnThis(
                    indexer.GetterImplementation,
                    IdentifierName(returns.DelegateParameter.Name).Call(DelegateArguments(indexer))
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsExceptionMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throws
    ) =>
        new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
            .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
            .AddParameter(ParameterSyntax(throws.ExceptionParameter))
            .WithBody(
                AddReturnValueAndReturnThis(
                    indexer.GetterImplementation,
                    ThrowExpression(IdentifierName(throws.ExceptionParameter.Name))
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsGenericExceptionMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throws
    ) =>
        new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
            .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
            .WithTypeParameters(throws.ExceptionTypeParameter.TypeParameterList)
            .WithBody(
                AddReturnValueAndReturnThis(
                    indexer.GetterImplementation,
                    ThrowExpression(IdentifierName(throws.ExceptionTypeParameter.Name).New())
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterBuilderThrowsDelegateMethod(
        in ImposterIndexerMetadata indexer,
        GetterThrowsMetadata throws
    ) =>
        new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
            .WithExplicitInterfaceSpecifier(throws.InterfaceSyntax)
            .AddParameter(ParameterSyntax(throws.DelegateParameter))
            .WithBody(
                AddReturnValueAndReturnThis(
                    indexer.GetterImplementation,
                    ThrowExpression(
                        IdentifierName(throws.DelegateParameter.Name)
                            .Call(DelegateArguments(indexer))
                    )
                )
            )
            .Build();

    // The arguments the user's Returns and Throws delegates get: the indexer's keys, read from the arguments.
    private static ArgumentListSyntax DelegateArguments(in ImposterIndexerMetadata indexer) =>
        BuildDelegateInvocationArguments(
            IdentifierName(indexer.GetterImplementation.ArgumentsVariableName),
            indexer
        );

    // Adds a return value to the invocation imposter: a generator that takes the arguments and yields outcome. Then
    // returns this for chaining.
    private static BlockSyntax AddReturnValueAndReturnThis(
        in IndexerGetterImposterMetadata getter,
        ExpressionSyntax outcome
    ) =>
        Block(
            IdentifierName(getter.Builder.InvocationImposterPropertyName)
                .Dot(IdentifierName("AddReturnValue"))
                .Call(
                    Argument(
                        SimpleLambdaExpression(
                            Parameter(Identifier(getter.ArgumentsVariableName)),
                            outcome
                        )
                    )
                )
                .ToStatementSyntax(),
            ReturnThis
        );

    private static MethodDeclarationSyntax BuildGetterBuilderCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var callback = indexer.GetterBuilderInterface.CallbackMethod;

        return new MethodDeclarationBuilder(callback.ReturnType, callback.Name)
            .WithExplicitInterfaceSpecifier(callback.InterfaceSyntax)
            .AddParameter(ParameterSyntax(callback.CallbackParameter))
            .WithBody(
                Block(
                    IdentifierName(
                            indexer.GetterImplementation.Builder.InvocationImposterPropertyName
                        )
                        .Dot(IdentifierName("AddCallback"))
                        .Call(Argument(IdentifierName(callback.CallbackParameter.Name)))
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builder = indexer.GetterImplementation.Builder;
        var called = indexer.GetterBuilderInterface.CalledMethod;

        return new MethodDeclarationBuilder(called.ReturnType, called.Name)
            .WithExplicitInterfaceSpecifier(
                indexer.GetterBuilderInterface.VerificationInterfaceTypeSyntax
            )
            .AddParameter(ParameterSyntax(called.CountParameter))
            .WithBody(
                Block(
                    IdentifierName(builder.ImposterFieldName)
                        .Dot(IdentifierName("Called"))
                        .Call([
                            Argument(IdentifierName(builder.CriteriaFieldName)),
                            Argument(IdentifierName(called.CountParameter.Name)),
                        ])
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderThenMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var then = indexer.GetterBuilderInterface.ThenMethod;

        return new MethodDeclarationBuilder(then.ReturnType, then.Name)
            .WithExplicitInterfaceSpecifier(then.InterfaceSyntax)
            .WithBody(Block(ReturnThis))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterBuilderUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var useBaseImplementation = indexer
            .GetterBuilderInterface
            .UseBaseImplementationMethod!
            .Value;

        return new MethodDeclarationBuilder(
            useBaseImplementation.ReturnType,
            useBaseImplementation.Name
        )
            .WithExplicitInterfaceSpecifier(useBaseImplementation.InterfaceSyntax)
            .WithBody(
                Block(
                    IdentifierName(
                            indexer.GetterImplementation.Builder.InvocationImposterPropertyName
                        )
                        .Dot(IdentifierName("UseBaseImplementation"))
                        .Call()
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }
}
