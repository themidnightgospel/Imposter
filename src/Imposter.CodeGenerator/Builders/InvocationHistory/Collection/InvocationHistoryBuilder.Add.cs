using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.InvocationHistory.Collection;

internal static partial class InvocationHistoryCollectionBuilder
{
    internal static MethodDeclarationSyntax BuildAddMethod(in ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.VoidKeyword)), "Add")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(method.InvocationHistory.Interface.Syntax, "invocationHistory"))
            .WithBody(
                Block(
                    IdentifierName(InvocationHistoryTypeMetadata.CollectionMetadata.InvocationHistoryCollectionFieldName)
                        .Dot(IdentifierName("Push"))
                        .Call(ArgumentListSyntax(Argument(IdentifierName("invocationHistory"))))
                        .ToStatementSyntax()
                )
            )
            .Build();
}