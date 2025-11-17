using Imposter.CodeGenerator.Features.EventImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal readonly ref struct EventImposterMembersBuilder(
    in ClassDeclarationBuilder imposterBuilder,
    BlockBuilder constructorBodyBuilder,
    in ImposterInstanceBuilder imposterInstanceBuilder
)
{
    private readonly ClassDeclarationBuilder _imposterBuilder = imposterBuilder;
    private readonly ImposterInstanceBuilder _imposterInstanceBuilder = imposterInstanceBuilder;

    internal void AddEvent(in ImposterEventMetadata @event)
    {
        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.ReadOnlyPropertyDeclarationSyntax(
                @event.BuilderInterface.TypeSyntax,
                @event.Core.Name,
                IdentifierName(@event.BuilderField.Name)
            )
        );

        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                @event.Builder.TypeSyntax,
                @event.BuilderField.Name
            )
        );

        constructorBodyBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(@event.BuilderField.Name))
                .Assign(@event.Builder.TypeSyntax.New())
                .ToStatementSyntax()
        );

        _imposterInstanceBuilder.AddEvent(@event);
    }
}
