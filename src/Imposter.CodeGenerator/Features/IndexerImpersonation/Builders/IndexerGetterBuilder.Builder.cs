using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.IndexerGetterImposterMetadata.GetterInvocationMetadata;
using static Imposter.CodeGenerator.Features.Shared.Builders.ThenMethodBuilder;
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

        return AccessorBuilder(
                builder,
                indexer.GetterImplementation.TypeSyntax,
                indexer.ArgumentsCriteria.TypeSyntax,
                indexer.GetterBuilderInterface.TypeSyntax,
                indexer.GetterBuilderInterface.FluentInterfaceTypeSyntax
            )
            .AddMember(BuildGetterBuilderInvocationProperty(indexer.GetterImplementation))
            .AddMembers(IndexerVerificationBuilder.Step(indexer, true))
            .AddMember(IndexerVerificationBuilder.Lenient(indexer, true))
            .AddMember(IndexerHistoryBuilder.Invocations(indexer, true, true))
            .AddMember(BuildGetterBuilderReturnsValueMethod(indexer, returns))
            .AddMember(BuildGetterBuilderReturnsFuncMethod(indexer, returns))
            .AddMember(BuildGetterBuilderReturnsDelegateMethod(indexer, returns))
            .AddMember(BuildGetterBuilderThrowsExceptionMethod(indexer, throws))
            .AddMember(BuildGetterBuilderThrowsGenericExceptionMethod(indexer, throws))
            .AddMember(BuildGetterBuilderThrowsDelegateMethod(indexer, throws))
            .AddMember(BuildGetterBuilderCallbackMethod(indexer))
            .AddMember(
                ForwardCalled(
                    builder,
                    indexer.GetterBuilderInterface.VerificationInterfaceTypeSyntax,
                    indexer.GetterBuilderInterface.CalledMethod,
                    IndexerGetterImposterMetadata.CalledMethodName
                )
            )
            .AddMember(ThenReturningThis(indexer.GetterBuilderInterface.ThenMethod))
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
            IndexerGetterImposterMetadata.InvocationImposterPropertyName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .Build()
            .WithAccessorList(null)
            .WithExpressionBody(
                ArrowExpressionClause(
                    IdentifierName(getter.Builder.ImposterFieldName)
                        .Dot(IdentifierName(IndexerGetterImposterMetadata.GetOrCreateMethodName))
                        .Call(Argument(IdentifierName(getter.Builder.CriteriaFieldName)))
                )
            )
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

    private static MethodDeclarationSyntax? BuildGetterBuilderReturnsValueMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returns
    ) =>
        returns.ValueParameter is { } value
            ? new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
                .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
                .AddParameter(ParameterSyntax(value))
                .WithBody(AddReturnValueAndReturnThis(indexer, IdentifierName(value.Name)))
                .Build()
            : null;

    private static MethodDeclarationSyntax? BuildGetterBuilderReturnsFuncMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returns
    ) =>
        returns.FuncParameter is { } valueGenerator
            ? new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
                .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
                .AddParameter(ParameterSyntax(valueGenerator))
                .WithBody(
                    AddReturnValueAndReturnThis(indexer, IdentifierName(valueGenerator.Name).Call())
                )
                .Build()
            : null;

    private static MethodDeclarationSyntax BuildGetterBuilderReturnsDelegateMethod(
        in ImposterIndexerMetadata indexer,
        GetterReturnsMetadata returns
    ) =>
        new MethodDeclarationBuilder(returns.ReturnType, returns.Name)
            .WithExplicitInterfaceSpecifier(returns.InterfaceSyntax)
            .AddParameter(ParameterSyntax(returns.DelegateParameter))
            .WithBody(
                AddReturnValueAndReturnThis(
                    indexer,
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
                    indexer,
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
                    indexer,
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
                    indexer,
                    ThrowExpression(
                        IdentifierName(throws.DelegateParameter.Name)
                            .Call(DelegateArguments(indexer))
                    )
                )
            )
            .Build();

    // The arguments the user's Returns and Throws delegates get: the indexer's keys, read from the arguments, and the
    // keys passed through, which the generator takes beside them.
    private static ArgumentListSyntax DelegateArguments(in ImposterIndexerMetadata indexer) =>
        BuildDelegateInvocationArguments(
            IdentifierName(indexer.GetterImplementation.ArgumentsVariableName),
            indexer,
            indexer.GetterPassedThroughKeyNames
        );

    // Adds a return value to the invocation imposter: a generator that takes the arguments, and the keys passed
    // through, and yields outcome. Then returns this for chaining.
    private static BlockSyntax AddReturnValueAndReturnThis(
        in ImposterIndexerMetadata indexer,
        ExpressionSyntax outcome
    )
    {
        var getter = indexer.GetterImplementation;
        var arguments = Identifier(getter.ArgumentsVariableName);
        LambdaExpressionSyntax generator =
            indexer.Core.PassedThroughParameters.Length == 0
                ? arguments.Lambda(outcome)
                : ParenthesizedLambdaExpression(
                    ParameterList(
                        SeparatedList([
                            Parameter(arguments),
                            .. PassedThroughKeyLambdaParameters(
                                indexer.GetterPassedThroughKeyNames
                            ),
                        ])
                    ),
                    outcome
                );

        return CallInvocationImposter(indexer, AddReturnValueMethodName, Argument(generator));
    }

    private static MethodDeclarationSyntax BuildGetterBuilderCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var callback = indexer.GetterBuilderInterface.CallbackMethod;

        return new MethodDeclarationBuilder(callback.ReturnType, callback.Name)
            .WithExplicitInterfaceSpecifier(callback.InterfaceSyntax)
            .AddParameter(ParameterSyntax(callback.CallbackParameter))
            .WithBody(
                CallInvocationImposter(
                    indexer,
                    AddCallbackMethodName,
                    Argument(IdentifierName(callback.CallbackParameter.Name))
                )
            )
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
            .WithBody(CallInvocationImposter(indexer, UseBaseImplementationMethodName))
            .Build();
    }

    // The setup's invocation imposter does the work; the builder returns itself so setups chain.

    private static BlockSyntax CallInvocationImposter(
        in ImposterIndexerMetadata indexer,
        string methodName,
        params ArgumentSyntax[] arguments
    ) =>
        Block(
            IdentifierName(IndexerGetterImposterMetadata.InvocationImposterPropertyName)
                .Dot(IdentifierName(methodName))
                .Call(arguments)
                .ToStatementSyntax(),
            ReturnThis
        );
}
