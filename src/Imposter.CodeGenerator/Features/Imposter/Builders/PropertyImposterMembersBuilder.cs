using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal readonly ref struct PropertyImposterMembersBuilder(
    in ClassDeclarationBuilder imposterBuilder,
    BlockBuilder memberInitializationBuilder,
    ExpressionSyntax invocationBehavior,
    in ImposterInstanceBuilder imposterInstanceBuilder
)
{
    private readonly ClassDeclarationBuilder _imposterBuilder = imposterBuilder;
    private readonly ImposterInstanceBuilder _imposterInstanceBuilder = imposterInstanceBuilder;

    internal void AddProperty(in ImposterPropertyMetadata property)
    {
        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.ReadOnlyPropertyDeclarationSyntax(
                property.ImposterBuilderInterface.Syntax,
                SyntaxFactoryHelper.EscapeKeyword(property.SetupName),
                IdentifierName(property.BuilderField.Name)
            )
        );

        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.SingleVariableField(
                property.BuilderField,
                SyntaxKind.PrivateKeyword
            )
        );

        memberInitializationBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(property.BuilderField.Name))
                .Assign(
                    property.ImposterBuilder.Syntax.New(
                        SyntaxFactoryHelper.ArgumentListSyntax([Argument(invocationBehavior)])
                    )
                )
                .ToStatementSyntax()
        );

        _imposterInstanceBuilder.AddImposterProperty(property);
    }
}
