using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static MemberDeclarationSyntax? BuildAsMethodForGenericImposter(
        in ImposterTargetMethodMetadata method
    )
    {
        if (!method.Model.IsGenericMethod)
        {
            return null;
        }

        var typeParamRenamer = new TypeParameterRenamer(
            method.Model.TypeParameters,
            method.TargetGenericTypeArguments
        );

        var conditions = new List<ExpressionSyntax>();

        foreach (var parameter in method.Parameters.AllParameterMetadata)
        {
            if (!parameter.Model.ReferencesMethodTypeParameter)
            {
                continue;
            }

            var sourceTypeSyntax = parameter.TypeSyntax;
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            var sourceTypeOf = TypeOfExpression(sourceTypeSyntax);
            var targetTypeOf = TypeOfExpression(targetTypeSyntax);

            switch (parameter.Model.RefKind)
            {
                case RefKind.Ref:
                    conditions.Add(
                        BinaryExpression(SyntaxKind.EqualsExpression, targetTypeOf, sourceTypeOf)
                    );
                    break;
                case RefKind.Out:
                    conditions.Add(sourceTypeOf.IsAssignableTo(targetTypeOf));
                    break;
                default: // In and None
                    conditions.Add(targetTypeOf.IsAssignableTo(sourceTypeOf));
                    break;
            }
        }

        if (method.HasReturnValue)
        {
            if (method.Model.ReturnType.ReferencesMethodTypeParameter)
            {
                var sourceTypeSyntax = method.ReturnTypeSyntax;
                var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

                var sourceTypeOf = TypeOfExpression(sourceTypeSyntax);
                var targetTypeOf = TypeOfExpression(targetTypeSyntax);

                conditions.Add(sourceTypeOf.IsAssignableTo(targetTypeOf));
            }
        }

        var returnAdapter = ReturnStatement(
            GenericName(MethodImposterMetadata.AdapterName)
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(method.TargetGenericTypeArguments))
                )
                .New(Argument(ThisExpression()).ToSingleArgumentList())
        );

        // Without a type to check, the adapter always applies, and an if (true) would leave the
        // trailing return null unreachable.
        var body =
            conditions.Count > 0
                ? Block(
                    IfStatement(
                        conditions.Aggregate((current, next) => current.And(next)),
                        Block(returnAdapter)
                    ),
                    ReturnStatement(Null)
                )
                : Block(returnAdapter);

        return new MethodDeclarationBuilder(
            NullableType(method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments),
            "As"
        )
            .WithExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax)
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .WithBody(body)
            .Build();
    }
}
