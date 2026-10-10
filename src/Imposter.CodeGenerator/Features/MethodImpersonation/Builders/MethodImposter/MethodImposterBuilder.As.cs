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

        return new MethodDeclarationBuilder(
            NullableType(method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments),
            "As"
        )
            .WithExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax)
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .WithBody(method.HasAdapter ? AdapterBody(method) : SameTypeArgumentsBody(method))
            .Build();
    }

    // Without an adapter, the imposter serves calls with its own type arguments only: as its generic interface over
    // the target type arguments, which it implements only when they're the same.
    private static BlockSyntax SameTypeArgumentsBody(in ImposterTargetMethodMetadata method) =>
        Block(
            ReturnStatement(
                BinaryExpression(
                    SyntaxKind.AsExpression,
                    ThisExpression(),
                    method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments
                )
            )
        );

    private static BlockSyntax AdapterBody(in ImposterTargetMethodMetadata method)
    {
        var conditions = TypeCompatibilityConditions(
            method,
            new TypeParameterRenamer(method.Model.TypeParameters, method.TargetGenericTypeArguments)
        );

        var returnAdapter = ReturnStatement(
            GenericName(
                    Identifier(MethodImposterMetadata.AdapterName),
                    TypeArguments(method.TargetGenericTypeArguments)
                )
                .New(Argument(ThisExpression()).ToSingleArgumentList())
        );

        // Without a type to check, the adapter always applies, and an if (true) would leave the
        // trailing return null unreachable.
        return conditions.Count > 0
            ? Block(
                IfStatement(
                    conditions.Aggregate((current, next) => current.And(next)),
                    Block(returnAdapter)
                ),
                ReturnStatement(Null)
            )
            : Block(returnAdapter);
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
