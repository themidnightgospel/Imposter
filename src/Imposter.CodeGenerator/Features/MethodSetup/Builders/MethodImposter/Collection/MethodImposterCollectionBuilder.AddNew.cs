using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    private static MethodDeclarationSyntax BuildAddNewMethod(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(method.MethodImposter.Syntax, "AddNew")
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddTypeParameterListParameters(TypeParametersSyntax(method.Symbol).ToArray())
            .WithBody(
                Block(
                    LocalDeclarationStatement(
                        VariableDeclarationSyntax(Var, "imposter",
                                method.MethodImposter.Syntax
                                    .New(Argument(IdentifierName(method.InvocationHistory.Collection.AsField.Name)).AsSingleArgumentListSyntax())
                            )
                    ),
                    IdentifierName("_imposters")
                        .Dot(IdentifierName("Push"))
                        .Call(Argument(IdentifierName("imposter")).AsSingleArgumentListSyntax())
                        .ToStatementSyntax(),
                    ReturnStatement(IdentifierName("imposter"))
                )
            );
    }
}