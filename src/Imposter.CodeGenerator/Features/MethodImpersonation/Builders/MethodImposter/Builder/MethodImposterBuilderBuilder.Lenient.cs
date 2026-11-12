using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static MethodDeclarationSyntax BuildLenientMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.ContinuationInterface.Syntax,
            VerificationSourceNames.LenientMethodName
        )
            .WithExplicitInterfaceSpecifier(
                method.MethodInvocationImposterGroup.ContinuationInterface.Syntax
            )
            .WithBody(
                Block(
                    IdentifierName(method.MethodImposter.Builder.InvocationImposterGroupField.Name)
                        .Dot(
                            IdentifierName(method.MethodInvocationImposterGroup.IsLenientFieldName)
                        )
                        .Assign(True)
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
}
