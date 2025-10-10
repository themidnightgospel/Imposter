using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static MemberDeclarationSyntax BuildAsMethodForGenericImposter(ImposterTargetMethodMetadata method)
    {
        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, "Target");

        var conditions = new List<ExpressionSyntax>();

        foreach (var p in method.Symbol.Parameters)
        {
            var pType = p.Type;
            if (!method.Symbol.TypeParameters.Any(tp => ContainsTypeParameter(pType, tp)))
            {
                continue;
            }

            var sourceTypeSyntax = SyntaxFactoryHelper.TypeSyntax(pType);
            var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

            var sourceTypeOf = TypeOfExpression(sourceTypeSyntax);
            var targetTypeOf = TypeOfExpression(targetTypeSyntax);

            switch (p.RefKind)
            {
                case RefKind.Ref:
                    conditions.Add(BinaryExpression(SyntaxKind.EqualsExpression, targetTypeOf, sourceTypeOf));
                    break;
                case RefKind.Out:
                    conditions.Add(IsAssignableTo(sourceTypeOf, targetTypeOf));
                    break;
                default: // In and None
                    conditions.Add(IsAssignableTo(targetTypeOf, sourceTypeOf));
                    break;
            }
        }

        if (method.HasReturnValue)
        {
            var returnType = method.Symbol.ReturnType;
            if (method.Symbol.TypeParameters.Any(tp => ContainsTypeParameter(returnType, tp)))
            {
                var sourceTypeSyntax = SyntaxFactoryHelper.TypeSyntax(returnType);
                var targetTypeSyntax = (TypeSyntax)typeParamRenamer.Visit(sourceTypeSyntax);

                var sourceTypeOf = TypeOfExpression(sourceTypeSyntax);
                var targetTypeOf = TypeOfExpression(targetTypeSyntax);

                conditions.Add(IsAssignableTo(sourceTypeOf, targetTypeOf));
            }
        }

        var condition = conditions.Any()
            ? conditions.Aggregate((current, next) => BinaryExpression(SyntaxKind.LogicalAndExpression, current, next))
            : LiteralExpression(SyntaxKind.TrueLiteralExpression);

        var asMethodTypeParams = method.Symbol.TypeParameters.Select(p => TypeParameter(Identifier(p.Name + "Target"))).ToArray();

        var genericImposterInterfaceWithTargets = GenericName(method.MethodImposter.Interface.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(method.TargetGenericTypeArguments)));

        return MethodDeclaration(
                NullableType(genericImposterInterfaceWithTargets),
                "As"
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax))
            .WithTypeParameterList(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(
                IfStatement(
                    condition,
                    Block(
                        ReturnStatement(
                            ObjectCreationExpression(
                                    GenericName("Adapter")
                                        .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(method.TargetGenericTypeArguments)))
                                )
                                .WithArgumentList(ArgumentList(SingletonSeparatedList(Argument(ThisExpression()))))
                        )
                    )
                ),
                ReturnStatement(LiteralExpression(SyntaxKind.NullLiteralExpression))
            ));
    }


    private static bool ContainsTypeParameter(ITypeSymbol typeSymbol, ITypeParameterSymbol typeParameter)
    {
        if (SymbolEqualityComparer.Default.Equals(typeSymbol, typeParameter))
        {
            return true;
        }

        if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
        {
            return namedTypeSymbol.TypeArguments.Any(typeArgument => ContainsTypeParameter(typeArgument, typeParameter));
        }

        if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            return ContainsTypeParameter(arrayTypeSymbol.ElementType, typeParameter);
        }

        return false;
    }

    private static ExpressionSyntax IsAssignableTo(ExpressionSyntax left, ExpressionSyntax right)
    {
        return InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                left,
                IdentifierName("IsAssignableTo")
            ),
            ArgumentList(SingletonSeparatedList(Argument(right)))
        );
    }

    private static MemberDeclarationSyntax BuildHasMatchingSetupMethod(ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "HasMatchingSetup")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameterIf(method.HasInputParameters, () => SyntaxFactoryHelper.ParameterSyntax(method.ArgumentsType.Syntax, "arguments"))
            .WithBody(Block(
                ReturnStatement(
                    IsPatternExpression(
                        InvocationExpression(
                            IdentifierName("FindMatchingSetup"),
                            ArgumentList(SingletonSeparatedList(Argument(IdentifierName("arguments"))))
                        ),
                        UnaryPattern(Token(SyntaxKind.NotKeyword), ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)))
                    )
                )
            ))
            .Build();
}