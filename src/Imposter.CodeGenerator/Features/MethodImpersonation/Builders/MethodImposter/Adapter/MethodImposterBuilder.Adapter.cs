using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Adapter;

internal static class MethodImposterAdapterBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Symbol.IsGenericMethod)
        {
            return null;
        }
        var adapterNames = new AdapterNames(method);
        var adapterBaseType = SimpleBaseType(
            GenericName(method.MethodImposter.Interface.Name)
                .WithTypeArgumentList(
                    TypeArgumentList(SeparatedList<TypeSyntax>(method.TargetGenericTypeArguments))
                )
        );

        var adapterClass = new ClassDeclarationBuilder(
            "Adapter",
            TypeParameterListSyntax(method.TargetGenericTypeArguments)
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddBaseType(adapterBaseType)
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Syntax,
                    adapterNames.TargetFieldName
                )
            )
            .AddMember(
                new ConstructorBuilder("Adapter")
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .AddParameter(
                        Parameter(Identifier(adapterNames.TargetConstructorParameterName))
                            .WithType(method.MethodImposter.Syntax)
                    )
                    .WithBody(
                        Block(
                            IdentifierName(adapterNames.TargetFieldName)
                                .Assign(IdentifierName(adapterNames.TargetConstructorParameterName))
                                .ToStatementSyntax()
                        )
                    )
                    .Build()
            )
            .AddMember(BuildAdapterInvokeMethod(method, adapterNames))
            .AddMember(BuildAdapterHasMatchingInvocationImposterGroupMethod(method, adapterNames))
            .AddMember(BuildAdapterAsMethod(method))
            .Build();

        return adapterClass;
    }

    private static MethodDeclarationSyntax BuildAdapterInvokeMethod(
        in ImposterTargetMethodMetadata method,
        in AdapterNames adapterNames
    )
    {
        var body = new List<StatementSyntax>();
        var invokeArguments = new List<ArgumentSyntax>();
        var postInvokeActions = new List<StatementSyntax>();

        var typeParamRenamer = new TypeParameterRenamer(
            method.Symbol.TypeParameters,
            method.TargetGenericTypeArguments
        );

        var parameterList = ParameterListSyntax(method.Symbol.Parameters, true);
        parameterList = (ParameterListSyntax)typeParamRenamer.Visit(parameterList);

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
                            TypeCasterSyntaxHelper.CastExpression(
                                p.Name,
                                (TypeSyntax)pTargetType,
                                pType
                            )
                        )
                    );
                    invokeArguments.Add(
                        Argument(IdentifierName(pAdaptedName))
                            .WithRefOrOutKeyword(Token(SyntaxKind.RefKeyword))
                    );
                    postInvokeActions.Add(
                        IdentifierName(p.Name)
                            .Assign(
                                TypeCasterSyntaxHelper.CastExpression(
                                    pAdaptedName.Text,
                                    pType,
                                    (TypeSyntax)pTargetType
                                )
                            )
                            .ToStatementSyntax()
                    );
                    break;
                case RefKind.Out:
                    body.Add(LocalVariableDeclarationSyntax(pType, pAdaptedName.Text));
                    invokeArguments.Add(
                        Argument(IdentifierName(pAdaptedName))
                            .WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword))
                    );
                    postInvokeActions.Add(
                        IdentifierName(p.Name)
                            .Assign(
                                TypeCasterSyntaxHelper.CastExpression(
                                    pAdaptedName.Text,
                                    pType,
                                    (TypeSyntax)pTargetType
                                )
                            )
                            .ToStatementSyntax()
                    );
                    break;
                default:
                    invokeArguments.Add(
                        Argument(
                            TypeCasterSyntaxHelper.CastExpression(
                                p.Name,
                                (TypeSyntax)pTargetType,
                                pType
                            )
                        )
                    );
                    break;
            }
        }

        if (method.SupportsBaseImplementation)
        {
            var baseImplementationParameterType = (TypeSyntax)
                typeParamRenamer.Visit(method.Delegate.Syntax);
            var baseImplementationParameterTypeNullable =
                baseImplementationParameterType.ToNullableType();
            parameterList = parameterList.AddParameters(
                Parameter(
                        Identifier(method.MethodImposter.InvokeMethod.BaseInvocationParameterName)
                    )
                    .WithType(baseImplementationParameterTypeNullable)
                    .WithDefault(EqualsValueClause(Null))
            );

            invokeArguments.Add(
                Argument(
                    TypeCasterSyntaxHelper.CastExpression(
                        method.MethodImposter.InvokeMethod.BaseInvocationParameterName,
                        baseImplementationParameterTypeNullable,
                        method.Delegate.Syntax
                    )
                )
            );
        }

        var invokeExpression = IdentifierName(adapterNames.TargetFieldName)
            .Dot(IdentifierName("Invoke"))
            .Call(ArgumentList(SeparatedList(invokeArguments)));

        if (method.HasReturnValue)
        {
            body.Add(
                LocalVariableDeclarationSyntax(
                    IdentifierName("var"),
                    adapterNames.InvokeResultVariableName,
                    invokeExpression
                )
            );
            body.AddRange(postInvokeActions);

            var returnType = TypeSyntax(method.Symbol.ReturnType);
            var returnTargetType = typeParamRenamer.Visit(returnType);
            body.Add(
                ReturnStatement(
                    TypeCasterSyntaxHelper.CastExpression(
                        adapterNames.InvokeResultVariableName,
                        returnType,
                        (TypeSyntax)returnTargetType
                    )
                )
            );
        }
        else
        {
            body.Add(invokeExpression.ToStatementSyntax());
            body.AddRange(postInvokeActions);
        }

        return new MethodDeclarationBuilder(
            (TypeSyntax)typeParamRenamer.Visit(TypeSyntax(method.Symbol.ReturnType)),
            "Invoke"
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(parameterList)
            .WithBody(Block(body))
            .Build();
    }

    private static MethodDeclarationSyntax BuildAdapterHasMatchingInvocationImposterGroupMethod(
        in ImposterTargetMethodMetadata method,
        in AdapterNames adapterNames
    )
    {
        var typeParamRenamer = new TypeParameterRenamer(
            method.Symbol.TypeParameters,
            method.TargetGenericTypeArguments
        );
        var hasMatchingMethod = method.MethodImposter.HasMatchingInvocationImposterGroupMethod;
        var argumentsTypeWithTarget = (TypeSyntax)typeParamRenamer.Visit(method.Arguments.Syntax);
        var argumentsParameterName =
            adapterNames.HasMatchingInvocationImposterGroupArgumentsParameterName;

        return new MethodDeclarationBuilder(hasMatchingMethod.ReturnType, hasMatchingMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameterIf(
                method.Parameters.HasInputParameters,
                () =>
                    Parameter(Identifier(argumentsParameterName)).WithType(argumentsTypeWithTarget)
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(adapterNames.TargetFieldName)
                            .Dot(IdentifierName(hasMatchingMethod.Name))
                            .Call(
                                method.Parameters.HasInputParameters
                                    ? Argument(
                                            IdentifierName(argumentsParameterName)
                                                .Dot(
                                                    GenericName("As")
                                                        .WithTypeArgumentList(
                                                            TypeArgumentList(
                                                                SeparatedList(
                                                                    method.GenericTypeArguments.Cast<TypeSyntax>()
                                                                )
                                                            )
                                                        )
                                                )
                                                .Call()
                                        )
                                        .ToSingleArgumentList()
                                    : ArgumentList()
                            )
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildAdapterAsMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var asMethodTypeParams = method
            .Symbol.TypeParameters.Select(p => TypeParameter(p.Name + "Target1"))
            .ToArray();
        var targetTypeArgs = method
            .Symbol.TypeParameters.Select(p => IdentifierName(p.Name + "Target1"))
            .Cast<TypeSyntax>()
            .ToArray();
        var genericImposterInterface = GenericName(method.MethodImposter.Interface.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList(targetTypeArgs)));

        return new MethodDeclarationBuilder(NullableType(genericImposterInterface), "As")
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax)
            )
            .WithTypeParameters(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(
                Block(
                    ThrowStatement(
                        ObjectCreationExpression(IdentifierName("NotImplementedException"))
                            .WithArgumentList(ArgumentList())
                    )
                )
            )
            .Build();
    }

    private readonly struct AdapterNames
    {
        internal readonly string TargetFieldName;
        internal readonly string TargetConstructorParameterName;
        internal readonly string InvokeResultVariableName;
        internal readonly string HasMatchingInvocationImposterGroupArgumentsParameterName;

        internal AdapterNames(in ImposterTargetMethodMetadata method)
        {
            var nameContext = method.CreateParameterNameContext();
            TargetFieldName = nameContext.Use("_target");
            TargetConstructorParameterName = nameContext.Use("target");
            InvokeResultVariableName = nameContext.Use("result");
            HasMatchingInvocationImposterGroupArgumentsParameterName = nameContext.Use("arguments");
        }
    }
}
