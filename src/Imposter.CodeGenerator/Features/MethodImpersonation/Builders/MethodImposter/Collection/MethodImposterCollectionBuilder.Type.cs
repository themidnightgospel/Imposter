using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Collection;

internal static partial class MethodImposterCollectionBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Model.IsGenericMethod)
        {
            return null;
        }

        return new ClassDeclarationBuilder(method.MethodImposter.Collection.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(
                SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource)
            )
            .AddMembers(
                MethodImposterStateBuilder.BuildMembers(
                    method,
                    method.MethodImposter.Collection.Name
                )
            )
            .AddMember(BuildImpostersField(method))
            .AddMember(
                method.Parameters.InputParameterMetadata.Any(parameter => parameter.IsSpan)
                    ? SyntaxFactoryHelper.SingleVariableField(
                        WellKnownTypes.Bool,
                        MethodImposterCollectionMetadata.CanReuseCapturedSpansFieldName,
                        TokenList(
                            Token(SyntaxKind.InternalKeyword),
                            Token(SyntaxKind.VolatileKeyword)
                        ),
                        LiteralExpression(SyntaxKind.TrueLiteralExpression)
                    )
                    : null
            )
            .AddMember(BuildAddNewMethod(method))
            .AddMember(BuildGetImposterWithMatchingInvocationImposterGroup(method))
            .AddMembers(MethodVerificationSourceBuilder.Build(method, true))
            .Build();
    }

    private static FieldDeclarationSyntax BuildImpostersField(
        in ImposterTargetMethodMetadata method
    )
    {
        var impostersFieldType = WellKnownTypes.Imposter.Abstractions.SetupCollection(
            method.MethodImposter.Interface.Syntax
        );

        return SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
            impostersFieldType,
            MethodImposterCollectionMetadata.ImpostersFieldName,
            impostersFieldType.New()
        );
    }
}
