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

        var conditions = TypeCompatibilityConditions(
            method,
            new TypeParameterRenamer(method.Model.TypeParameters, method.TargetGenericTypeArguments)
        );

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

    // The checks that the target type arguments fit each type using the method's type parameters: an input parameter
    // takes the target type, an output or the result gives the source type, and a ref parameter needs the same type.
    private static List<ExpressionSyntax> TypeCompatibilityConditions(
        in ImposterTargetMethodMetadata method,
        TypeParameterRenamer typeParamRenamer
    )
    {
        var conditions = new List<ExpressionSyntax>();

        foreach (
            var parameter in method.Parameters.AllParameterMetadata.Where(it =>
                it.Model.ReferencesMethodTypeParameter
            )
        )
        {
            var sourceTypeOf = TypeOfExpression(parameter.TypeSyntax);
            var targetTypeOf = TypeOfExpression(
                (TypeSyntax)typeParamRenamer.Visit(parameter.TypeSyntax)
            );

            conditions.Add(
                parameter.Model.RefKind switch
                {
                    RefKind.Ref => BinaryExpression(
                        SyntaxKind.EqualsExpression,
                        targetTypeOf,
                        sourceTypeOf
                    ),
                    RefKind.Out => sourceTypeOf.IsAssignableTo(targetTypeOf),
                    _ => targetTypeOf.IsAssignableTo(sourceTypeOf),
                }
            );
        }

        if (method.HasReturnValue && method.Model.ReturnType.ReferencesMethodTypeParameter)
        {
            var sourceTypeOf = TypeOfExpression(method.ReturnTypeSyntax);
            var targetTypeOf = TypeOfExpression(
                (TypeSyntax)typeParamRenamer.Visit(method.ReturnTypeSyntax)
            );

            conditions.Add(sourceTypeOf.IsAssignableTo(targetTypeOf));
        }

        return conditions;
    }
}
