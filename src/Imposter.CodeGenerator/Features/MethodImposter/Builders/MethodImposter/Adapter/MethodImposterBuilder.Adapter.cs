using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Adapter;

internal static class MethodImposterAdapterBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }
        var adapterBaseType = SimpleBaseType(
            GenericName(method.MethodImposter.Interface.Name)
                .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(method.TargetGenericTypeArguments))));

        var adapterClass = new ClassDeclarationBuilder("Adapter", TypeParameterListSyntax(method.TargetGenericTypeArguments))
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddBaseType(adapterBaseType)
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Syntax,
                    "_target"))
            .AddMember(
                new ConstructorBuilder("Adapter")
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .AddParameter(Parameter(Identifier("target")).WithType(method.MethodImposter.Syntax))
                    .WithBody(Block(
                        IdentifierName("_target")
                            .Assign(IdentifierName("target"))
                            .ToStatementSyntax()
                    ))
                    .Build())
            .AddMember(BuildAdapterInvokeMethod(method))
            .AddMember(BuildAdapterHasMatchingSetupMethod(method))
            .AddMember(BuildAdapterAsMethod(method))
            .Build();

        return adapterClass;
    }

    private static MethodDeclarationSyntax BuildAdapterInvokeMethod(in ImposterTargetMethodMetadata method)
    {
        var body = new List<StatementSyntax>();
        var invokeArguments = new List<ArgumentSyntax>();
        var postInvokeActions = new List<StatementSyntax>();

        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, "Target");
        var parameterList = method.Symbol.Parameters.ToParameterListSyntax(true);

        foreach (var p in method.Symbol.Parameters)
        {
            var pType = TypeSyntax(p.Type);
            var pTargetType = typeParamRenamer.Visit(pType);
            var pAdaptedName = Identifier(p.Name + "Adapted");

            switch (p.RefKind)
            {
                case RefKind.Ref:
                    body.Add(
                        LocalVariableDeclarationSyntax(
                            pType,
                            pAdaptedName.Text,
                            TypeCasterSyntaxHelper.CastExpression(p.Name, (TypeSyntax)pTargetType, pType)));
                    invokeArguments.Add(Argument(IdentifierName(pAdaptedName)).WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword)));
                    postInvokeActions.Add(
                    IdentifierName(p.Name)
                        .Assign(
                            TypeCasterSyntaxHelper.CastExpression(pAdaptedName.Text, pType, (TypeSyntax)pTargetType)
                        )
                        .ToStatementSyntax());
                    break;
                case RefKind.Out:
                    body.Add(LocalVariableDeclarationSyntax(pType, pAdaptedName.Text));
                    invokeArguments.Add(Argument(IdentifierName(pAdaptedName)).WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword)));
                    postInvokeActions.Add(
                    IdentifierName(p.Name)
                        .Assign(
                            TypeCasterSyntaxHelper.CastExpression(pAdaptedName.Text, pType, (TypeSyntax)pTargetType)
                        )
                        .ToStatementSyntax());
                    break;
                default:
                    invokeArguments.Add(Argument(TypeCasterSyntaxHelper.CastExpression(p.Name, (TypeSyntax)pTargetType, pType)));
                    break;
            }
        }

        if (method.SupportsBaseImplementation)
        {
            var baseImplementationParameterType = (TypeSyntax)typeParamRenamer.Visit(method.Delegate.Syntax);
            parameterList = parameterList.AddParameters(
                Parameter(Identifier(method.MethodImposter.InvokeMethod.BaseInvocationParameterName))
                    .WithType(baseImplementationParameterType)
                    .WithDefault(EqualsValueClause(LiteralExpression(SyntaxKind.NullLiteralExpression))));

            invokeArguments.Add(
                Argument(
                    TypeCasterSyntaxHelper.CastExpression(
                        method.MethodImposter.InvokeMethod.BaseInvocationParameterName,
                        baseImplementationParameterType,
                        method.Delegate.Syntax)));
        }

        var invokeExpression = IdentifierName("_target")
            .Dot(IdentifierName("Invoke"))
            .Call(ArgumentList(SeparatedList(invokeArguments)));

        if (method.HasReturnValue)
        {
            body.Add(
                LocalVariableDeclarationSyntax(
                    IdentifierName("var"),
                    "result",
                    invokeExpression)
            );
            body.AddRange(postInvokeActions);

            var returnType = TypeSyntax(method.Symbol.ReturnType);
            var returnTargetType = typeParamRenamer.Visit(returnType);
            body.Add(ReturnStatement(TypeCasterSyntaxHelper.CastExpression("result", returnType, (TypeSyntax)returnTargetType)));
        }
        else
        {
            body.Add(invokeExpression.ToStatementSyntax());
            body.AddRange(postInvokeActions);
        }

        return new MethodDeclarationBuilder(
                (TypeSyntax)typeParamRenamer.Visit(TypeSyntax(method.Symbol.ReturnType)),
                "Invoke")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(parameterList)
            .WithBody(Block(body))
            .Build();
    }

    private static MethodDeclarationSyntax BuildAdapterHasMatchingSetupMethod(in ImposterTargetMethodMetadata method)
    {
        var typeParamRenamer = new TypeParameterRenamer(method.Symbol.TypeParameters, "Target");
        var argumentsTypeWithTarget = (TypeSyntax)typeParamRenamer.Visit(method.Arguments.Syntax);

        return new MethodDeclarationBuilder(WellKnownTypes.Bool, "HasMatchingSetup")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameterIf(method.Parameters.HasInputParameters, () => Parameter(Identifier("arguments")).WithType(argumentsTypeWithTarget))
            .WithBody(Block(
                ReturnStatement(
                    IdentifierName("_target")
                        .Dot(IdentifierName("HasMatchingSetup"))
                        .Call(
                            method.Parameters.HasInputParameters
                                ? Argument(
                                        IdentifierName("arguments")
                                            .Dot(GenericName("As").WithTypeArgumentList(TypeArgumentList(SeparatedList(method.GenericTypeArguments.Cast<TypeSyntax>()))))
                                            .Call()
                                    )
                                    .ToSingleArgumentList()
                                : ArgumentList()
                        )
                )
            ))
            .Build();
    }

    private static MethodDeclarationSyntax BuildAdapterAsMethod(in ImposterTargetMethodMetadata method)
    {
        var asMethodTypeParams = method.Symbol.TypeParameters.Select(p => TypeParameter(p.Name + "Target1")).ToArray();
        var targetTypeArgs = method.Symbol.TypeParameters.Select(p => IdentifierName(p.Name + "Target1")).Cast<TypeSyntax>().ToArray();
        var genericImposterInterface = GenericName(method.MethodImposter.Interface.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList(targetTypeArgs)));

        return new MethodDeclarationBuilder(
                NullableType(genericImposterInterface),
                "As")
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax))
            .WithTypeParameters(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(ThrowStatement(ObjectCreationExpression(IdentifierName("NotImplementedException")).WithArgumentList(ArgumentList()))))
            .Build();
    }
}
