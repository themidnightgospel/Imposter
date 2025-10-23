using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.Adapter;

internal static class MethodImposterAdapterBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }
        
        var adapterClass = ClassDeclaration("Adapter")
            .AddModifiers(Token(SyntaxKind.PrivateKeyword))
            .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterListSyntax(method.TargetGenericTypeArguments))
            .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(
                SimpleBaseType(
                    GenericName(method.MethodImposter.Interface.Name)
                        .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(method.TargetGenericTypeArguments)))
                )
            )))
            .AddMembers(
                FieldDeclaration(
                        VariableDeclaration(method.MethodImposter.Syntax)
                            .AddVariables(VariableDeclarator("_target"))
                    )
                    .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
                ConstructorDeclaration("Adapter")
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .AddParameterListParameters(Parameter(Identifier("target")).WithType(method.MethodImposter.Syntax))
                    .WithBody(Block(
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName("_target"),
                                IdentifierName("target")
                            )
                        )
                    )),
                BuildAdapterInvokeMethod(method),
                BuildAdapterHasMatchingSetupMethod(method),
                BuildAdapterAsMethod(method)
            );

        return adapterClass;
    }

    private static MemberDeclarationSyntax BuildAdapterInvokeMethod(in ImposterTargetMethodMetadata method)
    {
        var body = new List<StatementSyntax>();
        var invokeArguments = new List<ArgumentSyntax>();
        var postInvokeActions = new List<StatementSyntax>();

        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, "Target");

        foreach (var p in method.Symbol.Parameters)
        {
            var pType = SyntaxFactoryHelper.TypeSyntax(p.Type);
            var pTargetType = typeParamRenamer.Visit(pType);
            var pAdaptedName = Identifier(p.Name + "Adapted");

            switch (p.RefKind)
            {
                case RefKind.Ref:
                    body.Add(LocalDeclarationStatement(VariableDeclaration(pType, SingletonSeparatedList(
                        VariableDeclarator(pAdaptedName)
                            .WithInitializer(EqualsValueClause(SyntaxFactoryHelper.CastExpressionSyntax(p.Name, (TypeSyntax)pTargetType, pType)))
                    ))));
                    invokeArguments.Add(Argument(IdentifierName(pAdaptedName)).WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword)));
                    postInvokeActions.Add(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName(p.Name),
                            SyntaxFactoryHelper.CastExpressionSyntax(pAdaptedName.Text, pType, (TypeSyntax)pTargetType)
                        )
                    ));
                    break;
                case RefKind.Out:
                    body.Add(LocalDeclarationStatement(VariableDeclaration(pType, SingletonSeparatedList(
                        VariableDeclarator(pAdaptedName)
                    ))));
                    invokeArguments.Add(Argument(IdentifierName(pAdaptedName)).WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword)));
                    postInvokeActions.Add(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName(p.Name),
                            SyntaxFactoryHelper.CastExpressionSyntax(pAdaptedName.Text, pType, (TypeSyntax)pTargetType)
                        )
                    ));
                    break;
                default:
                    invokeArguments.Add(Argument(SyntaxFactoryHelper.CastExpressionSyntax(p.Name, (TypeSyntax)pTargetType, pType)));
                    break;
            }
        }

        var invokeExpression = InvocationExpression(
            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("_target"), IdentifierName("Invoke")),
            ArgumentList(SeparatedList(invokeArguments))
        );

        if (method.HasReturnValue)
        {
            body.Add(
                LocalDeclarationStatement(VariableDeclaration(IdentifierName("var"), SingletonSeparatedList(
                    VariableDeclarator("result").WithInitializer(EqualsValueClause(invokeExpression))
                )))
            );
            body.AddRange(postInvokeActions);

            var returnType = SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType);
            var returnTargetType = typeParamRenamer.Visit(returnType);
            body.Add(ReturnStatement(SyntaxFactoryHelper.CastExpressionSyntax("result", returnType, (TypeSyntax)returnTargetType)));
        }
        else
        {
            body.Add(ExpressionStatement(invokeExpression));
            body.AddRange(postInvokeActions);
        }

        return MethodDeclaration(
                (TypeSyntax)typeParamRenamer.Visit(SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType)),
                "Invoke"
            )
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(SyntaxFactoryHelper.ToParameterListSyntax(method.Symbol.Parameters, true))
            .WithBody(Block(body));
    }

    private static MemberDeclarationSyntax BuildAdapterHasMatchingSetupMethod(in ImposterTargetMethodMetadata method)
    {
        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, "Target");
        var argumentsTypeWithTarget = (TypeSyntax)typeParamRenamer.Visit(method.Arguments.Syntax);

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "HasMatchingSetup")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameterIf(method.Parameters.HasInputParameters, () => Parameter(Identifier("arguments")).WithType(argumentsTypeWithTarget))
            .WithBody(Block(
                ReturnStatement(
                    IdentifierName("_target")
                        .Dot(IdentifierName("HasMatchingSetup"))
                        .Call(
                            method.Parameters.HasInputParameters
                                ? Argument(
                                        InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("arguments"),
                                                GenericName("As").WithTypeArgumentList(TypeArgumentList(SeparatedList(method.GenericTypeArguments.Cast<TypeSyntax>())))
                                            )
                                        )
                                    )
                                    .ToSingleArgumentList()
                                : ArgumentList()
                        )
                )
            ))
            .Build();
    }

    private static MemberDeclarationSyntax BuildAdapterAsMethod(in ImposterTargetMethodMetadata method)
    {
        var asMethodTypeParams = method.Symbol.TypeParameters.Select(p => TypeParameter(p.Name + "Target1")).ToArray();
        var targetTypeArgs = method.Symbol.TypeParameters.Select(p => IdentifierName(p.Name + "Target1")).Cast<TypeSyntax>().ToArray();
        var genericImposterInterface = GenericName(method.MethodImposter.Interface.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList(targetTypeArgs)));

        return MethodDeclaration(
                NullableType(genericImposterInterface),
                "As"
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax))
            .WithTypeParameterList(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(ThrowStatement(ObjectCreationExpression(IdentifierName("NotImplementedException")).WithArgumentList(ArgumentList()))));
    }
}