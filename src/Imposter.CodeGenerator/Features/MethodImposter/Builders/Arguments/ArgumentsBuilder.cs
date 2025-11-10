using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.Arguments;

internal static class ArgumentsBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        var inputParameters = method.Parameters.InputParameters;

        if (inputParameters.Count <= 0)
        {
            return null;
        }

        var argumentsClassBuilder = new ClassDeclarationBuilder(method.Arguments.Name, SyntaxFactoryHelper.TypeParameterListSyntax(method.GenericTypeArguments))
            .AddPublicModifier()
            .AddMembers(inputParameters.Select(SyntaxFactoryHelper.ParameterAsReadonlyField))
            .AddMember(new ConstructorBuilder(method.Arguments.Name)
                .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                .WithParameterList(method.Parameters.InputParameterWithoutRefKindListSyntax)
                .WithBody(Block(inputParameters
                        .Select(parameter =>
                            ThisExpression()
                                .Dot(IdentifierName(parameter.Name))
                                .Assign(IdentifierName(parameter.Name))
                                .ToStatementSyntax()
                        )
                    )
                )
                .Build()
            );

        if (method.Symbol.IsGenericMethod)
        {
            argumentsClassBuilder.AddMember(BuildArgumentsAsMethod(method));
        }

        return argumentsClassBuilder.Build();
    }

    // TODO refactor
    private static MethodDeclarationSyntax BuildArgumentsAsMethod(in ImposterTargetMethodMetadata method)
    {
        var typeParameters = method.Symbol.TypeParameters;
        var asMethodTypeParams = typeParameters.Select(p => TypeParameter(p.Name + "Target")).ToArray();
        var targetTypeArgs = typeParameters
            .Select(p => (TypeSyntax)IdentifierName(p.Name + "Target"))
            .ToArray();

        var returnType = GenericName(method.Arguments.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(targetTypeArgs)));

        var constructorArgs = method
            .Parameters
            .InputParameters.Select(p =>
            {
                var sourceType = SyntaxFactoryHelper.TypeSyntax(p.Type);
                var renamer = new TypeParameterRenamer(typeParameters, "Target");
                var targetType = (TypeSyntax)renamer.Visit(sourceType);

                return Argument(
                    IdentifierName("TypeCaster")
                        .Dot(
                            GenericName("Cast")
                                .WithTypeArgumentList(
                                    TypeArgumentList(SeparatedList<TypeSyntax>([sourceType, targetType]))
                                )
                        )
                        .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName(p.Name)))))
                );
            });

        return new MethodDeclarationBuilder(returnType, "As")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(
                ReturnStatement(
                    ObjectCreationExpression(returnType)
                        .WithArgumentList(ArgumentList(SeparatedList(constructorArgs)))
                )
            ))
            .Build();
    }
}
