using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static MemberDeclarationSyntax? BuildAsMethodForGenericImposter(
        in ImposterTargetMethodMetadata method
    )
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }

        var typeParamRenamer = new TypeParameterRenamer(
            method.Symbol.TypeParameters,
            method.TargetGenericTypeArguments
        );

        var conditions = new List<ExpressionSyntax>();

        foreach (var parameterSymbol in method.Symbol.Parameters)
        {
            var parameterSymbolType = parameterSymbol.Type;
            if (
                !method.Symbol.TypeParameters.Any(tp =>
                    ContainsTypeParameter(parameterSymbolType, tp)
                )
            )
            {
                continue;
            }

            var sourceTypeSyntax = TypeSyntax(parameterSymbolType);
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            var sourceTypeOf = TypeOfExpression(sourceTypeSyntax);
            var targetTypeOf = TypeOfExpression(targetTypeSyntax);

            switch (parameterSymbol.RefKind)
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
            var returnType = method.Symbol.ReturnType;
            if (method.Symbol.TypeParameters.Any(tp => ContainsTypeParameter(returnType, tp)))
            {
                var sourceTypeSyntax = TypeSyntax(returnType);
                var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

                var sourceTypeOf = TypeOfExpression(sourceTypeSyntax);
                var targetTypeOf = TypeOfExpression(targetTypeSyntax);

                conditions.Add(sourceTypeOf.IsAssignableTo(targetTypeOf));
            }
        }

        var condition =
            conditions.Count > 0
                ? conditions.Aggregate((current, next) => current.And(next))
                : True;

        var asMethodTypeParams =
            method.TargetGenericTypeParameterListSyntax
            ?? TypeParameterList(
                SeparatedList(
                    method.Symbol.TypeParameters.Select(p =>
                        TypeParameter(Identifier(p.Name + "Target"))
                    )
                )
            );

        var genericImposterInterfaceWithTargets = GenericName(method.MethodImposter.Interface.Name)
            .WithTypeArgumentList(
                TypeArgumentList(SeparatedList<TypeSyntax>(method.TargetGenericTypeArguments))
            );

        return new MethodDeclarationBuilder(NullableType(genericImposterInterfaceWithTargets), "As")
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax)
            )
            .WithTypeParameters(asMethodTypeParams)
            .WithBody(
                Block(
                    IfStatement(
                        condition,
                        Block(
                            ReturnStatement(
                                ObjectCreationExpression(
                                        GenericName("Adapter")
                                            .WithTypeArgumentList(
                                                TypeArgumentList(
                                                    SeparatedList<TypeSyntax>(
                                                        method.TargetGenericTypeArguments
                                                    )
                                                )
                                            )
                                    )
                                    .WithArgumentList(
                                        ArgumentList(
                                            SingletonSeparatedList(Argument(ThisExpression()))
                                        )
                                    )
                            )
                        )
                    ),
                    ReturnStatement(Null)
                )
            )
            .Build();
    }

    private static bool ContainsTypeParameter(
        ITypeSymbol typeSymbol,
        ITypeParameterSymbol typeParameter
    )
    {
        if (SymbolEqualityComparer.Default.Equals(typeSymbol, typeParameter))
        {
            return true;
        }

        if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
        {
            return namedTypeSymbol.TypeArguments.Any(typeArgument =>
                ContainsTypeParameter(typeArgument, typeParameter)
            );
        }

        if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            return ContainsTypeParameter(arrayTypeSymbol.ElementType, typeParameter);
        }

        return false;
    }
}
