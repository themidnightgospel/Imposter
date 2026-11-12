using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FluentInterfaceBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerGetterImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterIndexerMetadata indexer)
    {
        if (!indexer.Core.HasGetter)
        {
            return [];
        }

        return
        [
            BuildOutcomeInterface(indexer),
            CallbackInterface(
                indexer.GetterBuilderInterface.CallbackInterfaceName,
                indexer.GetterBuilderInterface.CallbackMethod
            ),
            ContinuationInterface(
                indexer.GetterBuilderInterface.ContinuationInterfaceName,
                indexer.GetterBuilderInterface.CallbackInterfaceTypeSyntax,
                indexer.GetterBuilderInterface.ThenMethod
            ),
            VerificationInterface(
                    indexer.GetterBuilderInterface.VerificationInterfaceName,
                    indexer.GetterBuilderInterface.CalledMethod
                )
                .AddMembers(IndexerHistoryBuilder.Invocations(indexer, true, false)),
            BuildFluentInterface(indexer),
            BuildBuilderInterface(indexer),
        ];
    }

    private static InterfaceDeclarationSyntax BuildBuilderInterface(
        in ImposterIndexerMetadata indexer
    ) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.OutcomeInterfaceTypeSyntax))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddBaseType(
                SimpleBaseType(indexer.GetterBuilderInterface.VerificationInterfaceTypeSyntax)
            )
            .Build();

    private static InterfaceDeclarationSyntax BuildFluentInterface(
        in ImposterIndexerMetadata indexer
    ) =>
        new InterfaceDeclarationBuilder(indexer.GetterBuilderInterface.FluentInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.GetterBuilderInterface.OutcomeInterfaceTypeSyntax))
            .AddBaseType(
                SimpleBaseType(indexer.GetterBuilderInterface.ContinuationInterfaceTypeSyntax)
            )
            .Build();

    private static InterfaceDeclarationSyntax BuildOutcomeInterface(
        in ImposterIndexerMetadata indexer
    )
    {
        var builder = new InterfaceDeclarationBuilder(
            indexer.GetterBuilderInterface.OutcomeInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(BuildReturnsMethods(indexer.GetterBuilderInterface))
            .AddMembers(BuildThrowsMethods(indexer.GetterBuilderInterface));

        if (indexer.GetterBuilderInterface.UseBaseImplementationMethod is { } useBaseImplementation)
        {
            builder.AddMember(
                InterfaceMethod(useBaseImplementation.ReturnType, useBaseImplementation.Name)
            );
        }

        return builder.Build();
    }

    private static MethodDeclarationSyntax[] BuildReturnsMethods(
        IndexerGetterImposterBuilderInterfaceMetadata getterInterface
    )
    {
        var returns = getterInterface.ReturnsMethod;
        ParameterMetadata?[] parameters =
        [
            returns.ValueParameter,
            returns.FuncParameter,
            returns.DelegateParameter,
        ];

        return
        [
            .. parameters
                .OfType<ParameterMetadata>()
                .Select(it => InterfaceMethod(returns.ReturnType, returns.Name, it)),
        ];
    }

    private static MethodDeclarationSyntax[] BuildThrowsMethods(
        IndexerGetterImposterBuilderInterfaceMetadata getterInterface
    )
    {
        var throws = getterInterface.ThrowsMethod;

        return
        [
            InterfaceMethod(throws.ReturnType, throws.Name, throws.ExceptionParameter),
            new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
                .WithTypeParameters(throws.ExceptionTypeParameter.TypeParameterList)
                .AddConstraintClause(throws.ExceptionTypeParameter.ConstraintClause)
                .WithSemicolon()
                .Build(),
            InterfaceMethod(throws.ReturnType, throws.Name, throws.DelegateParameter),
        ];
    }
}
