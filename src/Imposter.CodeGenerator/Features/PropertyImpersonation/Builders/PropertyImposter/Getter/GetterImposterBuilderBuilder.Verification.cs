using System.Collections.Generic;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Getter;

internal static partial class GetterImposterBuilderBuilder
{
    private static MemberDeclarationSyntax[] BuildCounterVerificationMembers(
        in ImposterPropertyMetadata property
    )
    {
        var counter = IdentifierName(property.GetterImposterBuilder.VerificationField.Name);
        var description = IdentifierName(
                PropertyAccessorStateMetadata.PropertyDisplayNameField.Name
            )
            .Add(" (getter)".StringLiteral());
        var resultType = WellKnownTypes.System.Collections.Generic.IEnumerable(
            WellKnownTypes.String
        );
        var members = new List<MemberDeclarationSyntax>();
        foreach (
            var name in new[]
            {
                VerificationSourceNames.InteractionsMethodName,
                VerificationSourceNames.UncoveredInteractionsMethodName,
                VerificationSourceNames.UnusedSetupsMethodName,
            }
        )
            members.Add(
                new MethodDeclarationBuilder(resultType, name)
                    .WithExplicitInterfaceSpecifier(
                        WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource
                    )
                    .WithBody(
                        Block(
                            ReturnStatement(
                                counter.Dot(IdentifierName(name)).Call(Argument(description))
                            )
                        )
                    )
                    .Build()
            );
        members.Add(
            new MethodDeclarationBuilder(
                property.GetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax,
                VerificationSourceNames.LenientMethodName
            )
                .WithExplicitInterfaceSpecifier(
                    property.GetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax
                )
                .WithBody(
                    Block(
                        counter
                            .Dot(IdentifierName(VerificationSourceNames.LenientMethodName))
                            .Call()
                            .ToStatementSyntax(),
                        ReturnThis
                    )
                )
                .Build()
        );
        return members.ToArray();
    }
}
