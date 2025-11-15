using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MethodDeclarationSyntax BuildAddMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, "Add")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(method.InvocationHistory.Interface.Syntax, "invocationHistory")
            )
            .WithBody(
                Block(
                    IdentifierName(
                            InvocationHistoryCollectionMetadata.InvocationHistoryCollectionFieldName
                        )
                        .Dot(IdentifierName("Push"))
                        .Call(ArgumentListSyntax(Argument(IdentifierName("invocationHistory"))))
                        .ToStatementSyntax()
                )
            )
            .Build();
}
