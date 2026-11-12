using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Builders.PropertyImposter.Setter;

internal static partial class SetterImposterBuilderBuilder
{
    private static MemberDeclarationSyntax[] BuildSharedVerificationMembers(
        in ImposterPropertyMetadata property
    )
    {
        var setter = IdentifierName(property.SetterImposter.Builder.SetterImposterField.Name);
        var criteria = IdentifierName(property.SetterImposter.Builder.RuntimeCriteriaField.Name);
        var record = property.SetterImposter.InvocationTypeSyntax;
        var arguments = property.Core.IsPassedThrough
            ? ArgumentList()
            : criteria.ToSingleArgumentList();
        return
        [
            new PropertyDeclarationBuilder(
                WellKnownTypes.System.Collections.Generic.IReadOnlyList(record),
                "Invocations"
            )
                .WithExplicitInterfaceSpecifier(
                    ExplicitInterfaceSpecifier(
                        property.SetterImposterBuilderInterface.VerificationInterfaceTypeSyntax
                    )
                )
                .WithGetterBody(
                    Block(
                        ReturnStatement(
                            setter
                                .Dot(
                                    GenericName(
                                            property.Core.IsPassedThrough
                                                ? "Snapshot"
                                                : "MatchingInvocations"
                                        )
                                        .WithTypeArgumentList(
                                            TypeArgumentList(
                                                SingletonSeparatedList<TypeSyntax>(record)
                                            )
                                        )
                                )
                                .Call(arguments)
                        )
                    )
                )
                .Build(),
            new MethodDeclarationBuilder(
                property.SetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax,
                VerificationSourceNames.LenientMethodName
            )
                .WithExplicitInterfaceSpecifier(
                    property.SetterImposterBuilderInterface.ContinuationInterfaceTypeSyntax
                )
                .WithBody(
                    Block(
                        IdentifierName(property.SetterImposter.Builder.SetupField.Name)
                            .Dot(IdentifierName(VerificationSourceNames.LenientMethodName))
                            .Call()
                            .ToStatementSyntax(),
                        ReturnThis
                    )
                )
                .Build(),
        ];
    }
}
