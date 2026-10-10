using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Arguments;

public static class ArgumentsCriteriaBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Parameters.HasInputParameters)
        {
            return null;
        }

        var argumentsCriteriaClass = new ClassDeclarationBuilder(
            method.ArgumentsCriteria.Name,
            method.GenericTypeParameterListSyntax
        )
            .WithTypeParameterConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(method.Parameters.MatchedParameterMetadata.Select(ArgProperty))
            .AddMember(BuildConstructor(method))
            .AddMember(MatchesMethod(method));

        if (method.Model.IsGenericMethod)
        {
            argumentsCriteriaClass.AddMember(BuildAsMethod(method));
        }

        return argumentsCriteriaClass
            .Build()
#if DEBUG
            .WithLeadingTriviaComment(method.DisplayName)
#endif
            .WithTrailingTrivia(CarriageReturnLineFeed);
    }

    // The Arg<T> criterion for one of the method's parameters.
    private static PropertyDeclarationSyntax ArgProperty(MethodParameterMetadata parameter) =>
        new PropertyDeclarationBuilder(parameter.ArgTypeSyntax, parameter.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithGetter()
            .Build();

    private static ConstructorDeclarationSyntax BuildConstructor(
        in ImposterTargetMethodMetadata method
    )
    {
        var constructor = new ConstructorWithFieldInitializationBuilder(
            method.ArgumentsCriteria.Name
        ).WithModifiers(Token(SyntaxKind.PublicKeyword));

        foreach (var parameter in method.Parameters.MatchedParameterMetadata)
        {
            constructor.AddParameter(
                new ParameterMetadata(parameter.Name, parameter.ArgTypeSyntax),
                parameter.Name
            );
        }

        return constructor.Build();
    }

    private static MethodDeclarationSyntax BuildAsMethod(in ImposterTargetMethodMetadata method)
    {
        var returnType = method.ArgumentsCriteria.SyntaxWithTargetGenericTypeArguments;
        var typeParameterRenamer = new TypeParameterRenamer(
            method.Model.TypeParameters,
            method.TargetGenericTypeArguments
        );
        var constructorArgs = BuildConstructorArgs(method, typeParameterRenamer);

        return new MethodDeclarationBuilder(returnType, method.ArgumentsCriteria.AsMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddConstraintClauses(method.TargetGenericTypeConstraintClauses)
            .WithBody(Block(ReturnStatement(returnType.New(ArgumentList(constructorArgs)))))
            .Build();

        static SeparatedSyntaxList<ArgumentSyntax> BuildConstructorArgs(
            in ImposterTargetMethodMetadata metadata,
            TypeParameterRenamer renamer
        )
        {
            var matcherLambdaParameter = metadata.ArgumentsCriteria.AsMethod.MatcherLambdaParameter;

            return SeparatedList(
                metadata.Parameters.MatchedParameterMetadata.Select(parameter =>
                    BuildArgForParameter(parameter, renamer, matcherLambdaParameter)
                )
            );
        }

        static ArgumentSyntax BuildArgForParameter(
            MethodParameterMetadata parameter,
            TypeParameterRenamer renamer,
            IdentifierNameSyntax matcherLambdaParameter
        )
        {
            var targetMatcherType = (TypeSyntax)renamer.Visit(parameter.KeptArgTypeSyntax);

            if (parameter.Model.RefKind is RefKind.Out)
            {
                return Argument(targetMatcherType.Dot(IdentifierName("Any")).Call());
            }

            var sourceType = parameter.NullableAwareKeptTypeSyntax;
            var targetType = (TypeSyntax)renamer.Visit(sourceType);
            return Argument(
                targetMatcherType
                    .Dot(IdentifierName("Is"))
                    .Call(
                        Argument(
                            BuildTryCastAndMatchLambda(
                                parameter,
                                targetType,
                                sourceType,
                                matcherLambdaParameter
                            )
                        )
                    )
            );
        }

        static SimpleLambdaExpressionSyntax BuildTryCastAndMatchLambda(
            MethodParameterMetadata parameter,
            TypeSyntax targetType,
            TypeSyntax sourceType,
            IdentifierNameSyntax matcherLambdaParameter
        )
        {
            var tryCastVarIdentifier = Identifier(parameter.Name + "Target");
            var tryCastInvocation = WellKnownTypes
                .Imposter.Abstractions.TypeCaster.Dot(
                    GenericName(Identifier("TryCast"), TypeArguments([targetType, sourceType]))
                )
                .Call(
                    ArgumentList(
                        SeparatedList([
                            Argument(matcherLambdaParameter),
                            Argument(
                                    DeclarationExpression(
                                        sourceType,
                                        SingleVariableDesignation(tryCastVarIdentifier)
                                    )
                                )
                                .WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword)),
                        ])
                    )
                );

            var matchesCall = IdentifierName(parameter.Name)
                .Dot(IdentifierName("Matches"))
                .Call(Argument(IdentifierName(tryCastVarIdentifier)));

            return SimpleLambdaExpression(
                Parameter(matcherLambdaParameter.Identifier),
                tryCastInvocation.And(matchesCall)
            );
        }
    }

    private static MethodDeclarationSyntax MatchesMethod(in ImposterTargetMethodMetadata method)
    {
        var matchesParameterName = method.ArgumentsCriteria.MatchesMethod.ParameterName;
        var matchesParameterExpression = IdentifierName(matchesParameterName);

        return new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            method.ArgumentsCriteria.MatchesMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(ParameterSyntax(method.Arguments.Syntax, matchesParameterName))
            .WithBody(
                Block(
                    ReturnStatement(
                        method
                            .Parameters.InputParameterMetadata.Select(it =>
                                (ExpressionSyntax)InvokeMatches(it)
                            )
                            .Aggregate((left, right) => left.And(right))
                    )
                )
            )
            .Build();

        InvocationExpressionSyntax InvokeMatches(MethodParameterMetadata p) =>
            IdentifierName(p.Name)
                .Dot(IdentifierName("Matches"))
                .Call(Argument(matchesParameterExpression.Dot(IdentifierName(p.Name))));
    }
}
