using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
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
                property.SetupName,
                IdentifierName(property.BuilderField.Name)
            )
        );

        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(property.BuilderField)
        );

        constructorBodyBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(property.BuilderField.Name))
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
