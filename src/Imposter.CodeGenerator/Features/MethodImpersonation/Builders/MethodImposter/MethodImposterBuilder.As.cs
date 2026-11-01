using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
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
            .WithBody(method.Model.HasAdapter ? AdapterBody(method) : SameTypeArgumentsBody(method))
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
        var conditions = TypeCompatibilityConditions(method);

        var returnThisWhenSameTypeArguments =
            GenericArgumentsMatcherBuilder.ReturnThisWhenSameTypeArguments(
                method,
                method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments
            );
        var returnAdapter = ReturnStatement(
            GenericName(
                    Identifier(method.MethodImposter.AdapterName),
                    TypeArguments(method.TargetGenericTypeArguments)
                )
                .New(Argument(ThisExpression()).ToSingleArgumentList())
        );

        return Block(
            returnThisWhenSameTypeArguments,
            IfStatement(
                conditions.Aggregate((current, next) => current.And(next)),
                Block(returnAdapter)
            ),
            ReturnStatement(Null)
        );
    }

    // The checks that the target type arguments fit each type using the method's type parameters: an input parameter
    // takes the target type, an output or the result gives the source type, and a ref parameter needs the same type. A
    // type parameter neither uses needs the same type argument. Every type parameter adds a check, so a generic method
    // always has one.
    private static List<ExpressionSyntax> TypeCompatibilityConditions(
        in ImposterTargetMethodMetadata method
    )
    {
        var conditions = new List<ExpressionSyntax>();

        foreach (
            var parameter in method.Parameters.AllParameterMetadata.Where(it =>
                it.Model.ReferencesMethodTypeParameter
            )
        )
        {
            var sourceTypeOf = RuntimeTypeOf(parameter.TypeSyntax, parameter.Model.Type);
            var targetTypeOf = RuntimeTypeOf(
                method.InTargetTypeArguments(parameter.TypeSyntax),
                parameter.Model.Type
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
            var returnType = method.Model.ReturnType.Type;
            var sourceTypeOf = RuntimeTypeOf(method.ReturnTypeSyntax, returnType);
            var targetTypeOf = RuntimeTypeOf(
                method.InTargetTypeArguments(method.ReturnTypeSyntax),
                returnType
            );

            conditions.Add(sourceTypeOf.IsAssignableTo(targetTypeOf));
        }

        foreach (var ordinal in method.Model.TypeParameterOrdinalsOutsideSignature)
        {
            conditions.Add(
                GenericArgumentsMatcherBuilder.SameType(
                    method.TargetGenericTypeArguments[ordinal],
                    method.GenericTypeArguments[ordinal]
                )
            );
        }

        return conditions;
    }
}
