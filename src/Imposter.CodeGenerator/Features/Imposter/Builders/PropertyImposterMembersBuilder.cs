using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal readonly ref struct PropertyImposterMembersBuilder(
    in ClassDeclarationBuilder imposterBuilder,
    BlockBuilder constructorBodyBuilder,
    string invocationBehaviorParameterName,
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
                property.Core.Name,
                IdentifierName(property.AsField.Name)
            )
        );

        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                property.ImposterBuilder.Syntax,
                property.AsField.Name
            )
        );

        constructorBodyBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(property.AsField.Name))
                .Assign(
                    property.ImposterBuilder.Syntax.New(
                        SyntaxFactoryHelper.ArgumentListSyntax([
                            Argument(IdentifierName(invocationBehaviorParameterName)),
                        ])
                    )
                )
                .ToStatementSyntax()
        );

        _imposterInstanceBuilder.AddImposterProperty(property);
    }
}
