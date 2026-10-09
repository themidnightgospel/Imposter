using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Arguments;

internal static class ArgumentsBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        var inputParameters = method.Parameters.InputParameterMetadata;

        if (inputParameters.Count <= 0)
        {
            return null;
        }

        var argumentsClassBuilder = new ClassDeclarationBuilder(
            method.Arguments.Name,
            method.GenericTypeParameterListSyntax
        )
            .WithTypeParameterConstraintClauses(method.GenericTypeConstraintClauses)
            .AddPublicModifier()
            .AddMembers(inputParameters.Select(ArgumentField))
            .AddMember(
                new ConstructorBuilder(method.Arguments.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
                    .WithParameterList(
                        method.Parameters.InputParameterWithoutRefKindListSyntaxIncludingNullable
                    )
                    .WithBody(
                        Block(
                            method.Parameters.InputParameterMetadata.Select(parameter =>
                                ThisExpression()
                                    .Dot(IdentifierName(parameter.Name))
                                    .Assign(parameter.StoredValue)
                                    .ToStatementSyntax()
                            )
                        )
                    )
                    .Build()
            );

        if (method.Model.IsGenericMethod)
        {
            argumentsClassBuilder.AddMember(BuildArgumentsAsMethod(method));
        }

        return argumentsClassBuilder.Build();
    }

    // A public field that keeps an argument, or the array a span argument is copied into.
    private static FieldDeclarationSyntax ArgumentField(MethodParameterMetadata parameter) =>
        SyntaxFactoryHelper.SingleVariableField(
            parameter.NullableAwareStoredTypeSyntax,
            parameter.Name,
            TokenList(Token(SyntaxKind.PublicKeyword))
        );

    private static MethodDeclarationSyntax BuildArgumentsAsMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var returnType = SyntaxFactoryHelper.WithMethodGenericArguments(
            method.TargetGenericTypeArguments,
            method.Arguments.Name
        );
        var renamer = new TypeParameterRenamer(
            method.Model.TypeParameters,
            method.TargetGenericTypeArguments
        );
        var constructorArgs = method.Parameters.InputParameterMetadata.Select(p =>
        {
            var sourceType = p.NullableAwareStoredTypeSyntax;
            var targetType = (TypeSyntax)renamer.Visit(sourceType);

            return Argument(TypeCasterSyntaxHelper.CastExpression(p.Name, sourceType, targetType));
        });

        return new MethodDeclarationBuilder(returnType, method.ArgumentsAsMethodName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddConstraintClauses(method.TargetGenericTypeConstraintClauses)
            .WithBody(
                Block(ReturnStatement(returnType.New(ArgumentList(SeparatedList(constructorArgs)))))
            )
            .Build();
    }
}
