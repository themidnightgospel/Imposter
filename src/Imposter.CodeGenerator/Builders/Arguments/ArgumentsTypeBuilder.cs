using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.Arguments;

internal static class ArgumentsTypeBuilder
{
    internal static ClassDeclarationSyntax? Build(ImposterTargetMethodMetadata method)
    {
        var parametersExceptOut = method.ParametersExceptOut;

        if (parametersExceptOut.Count <= 0)
        {
            return null;
        }

        var argumentsClass = ClassDeclaration(method.ArgumentsType.Name)
            .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterList(method.Symbol))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithMembers(List<MemberDeclarationSyntax>(parametersExceptOut.Select(SyntaxFactoryHelper.ParameterAsProperty)))
            .AddMembers(ConstructorDeclaration(method.ArgumentsType.Name)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(ParameterList(SeparatedList(parametersExceptOut.Select(parameter => SyntaxFactoryHelper.ParameterSyntax(parameter, false)))))
                .WithBody(Block(parametersExceptOut
                    .Select(parameter =>
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    ThisExpression(),
                                    IdentifierName(parameter.Name)
                                ),
                                IdentifierName(parameter.Name)
                            )
                        )
                    ))));
        if (method.Symbol.IsGenericMethod)
        {
            argumentsClass = argumentsClass.AddMembers(BuildArgumentsAsMethod(method));
        }

        return argumentsClass.WithTrailingTrivia(CarriageReturnLineFeed);
    }

    private static MemberDeclarationSyntax BuildArgumentsAsMethod(ImposterTargetMethodMetadata method)
    {
        var typeParameters = method.Symbol.TypeParameters;
        var asMethodTypeParams = typeParameters.Select(p => TypeParameter(p.Name + "Target")).ToArray();
        var targetTypeArgs = typeParameters.Select(p => IdentifierName(p.Name + "Target")).ToArray();

        var returnType = GenericName(method.ArgumentsType.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(targetTypeArgs)));

        var constructorArgs = method.ParametersExceptOut.Select(p =>
        {
            var sourceType = SyntaxFactoryHelper.TypeSyntax(p.Type);
            var renamer = new TypeParameterRenamer(typeParameters, "Target");
            var targetType = (TypeSyntax)renamer.Visit(sourceType);

            return Argument(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("TypeCaster"),
                        GenericName("Cast")
                            .WithTypeArgumentList(
                                TypeArgumentList(SeparatedList([sourceType, targetType]))
                            )
                    ),
                    ArgumentList(SingletonSeparatedList(Argument(IdentifierName(p.Name))))
                )
            );
        });

        return MethodDeclaration(returnType, "As")
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithTypeParameterList(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(
                ReturnStatement(
                    ObjectCreationExpression(returnType)
                        .WithArgumentList(ArgumentList(SeparatedList(constructorArgs)))
                )
            ));
    }
}