using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    private static MethodDeclarationSyntax BuildClearMethod() =>
        new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            InvocationHistoryCollectionMetadata.ClearMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    InterlockedSyntaxHelper
                        .InterlockedExchange(
                            InvocationHistoryCollectionMetadata.HeadFieldName,
                            Null
                        )
                        .ToStatementSyntax()
                )
            )
            .Build();
}
