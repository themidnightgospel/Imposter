using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Helpers;
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
        if (!method.Model.IsGenericMethod)
        {
            return null;
        }
        var adapterNames = new AdapterNames(method);
        var adapterBaseType = SimpleBaseType(
            method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments
        );

        var adapterClass = new ClassDeclarationBuilder(
            MethodImposterMetadata.AdapterName,
            method.TargetGenericTypeParameterListSyntax
        )
            .WithTypeParameterConstraintClauses(method.TargetGenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddBaseType(adapterBaseType)
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Syntax,
                    adapterNames.TargetFieldName
                )
            )
            .AddMember(
                new ConstructorBuilder(MethodImposterMetadata.AdapterName)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .AddParameter(
                        ParameterSyntax(
                            method.MethodImposter.Syntax,
                            adapterNames.TargetConstructorParameterName
                        )
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
            method.Model.TypeParameters,
            method.TargetGenericTypeArguments
        );

        var parameterList = (ParameterListSyntax)
            typeParamRenamer.Visit(method.Parameters.ParameterListSyntaxIncludingNullable);

        var adaptedParameterNames = adapterNames.AdaptedParameterNames;
        foreach (var parameter in method.Parameters.AllParameterMetadata)
        {
            // A ref struct passes on as it is: its type doesn't use the method's type parameters (IMP009 otherwise).
            if (parameter.Model.IsPassedThrough)
            {
                invokeArguments.Add(ForwardingArgument(parameter.Name, parameter.Model.RefKind));
                continue;
            }

            var parameterType = parameter.NullableAwareTypeSyntax;
            var castArgument = parameter.IsSpan
                ? AdaptedSpan(
                    IdentifierName(parameter.Name),
                    (TypeSyntax)typeParamRenamer.Visit(parameter.NullableAwareStoredTypeSyntax),
                    parameter.NullableAwareStoredTypeSyntax
                )
                : TypeCasterSyntaxHelper.CastExpression(
                    parameter.Name,
                    (TypeSyntax)typeParamRenamer.Visit(parameterType),
                    parameterType
                );

            switch (parameter.Model.RefKind)
            {
                case RefKind.Ref:
                    invokeArguments.Add(PassThroughLocal(SyntaxKind.RefKeyword, castArgument));
                    postInvokeActions.Add(CastBackToCaller());
                    break;
                case RefKind.Out:
                    invokeArguments.Add(PassThroughLocal(SyntaxKind.OutKeyword, initializer: null));
                    postInvokeActions.Add(CastBackToCaller());
                    break;
                case RefKinds.RefReadOnlyParameter:
                    // A `ref readonly` parameter takes a variable, not the cast itself (CS9193).
                    invokeArguments.Add(PassThroughLocal(SyntaxKind.InKeyword, castArgument));
                    break;
                default:
                    invokeArguments.Add(Argument(castArgument));
                    break;
            }

            // The target takes the argument by reference, so it goes through a local of the target's type.
            ArgumentSyntax PassThroughLocal(SyntaxKind keyword, ExpressionSyntax? initializer)
            {
                var adaptedName = adaptedParameterNames[parameter.Name];
                body.Add(LocalVariableDeclarationSyntax(parameterType, adaptedName, initializer));
                return Argument(IdentifierName(adaptedName)).WithRefOrOutKeyword(Token(keyword));
            }

            StatementSyntax CastBackToCaller() =>
                IdentifierName(parameter.Name)
                    .Assign(
                        CastBack(parameter, adaptedParameterNames[parameter.Name], typeParamRenamer)
                    )
                    .ToStatementSyntax();
        }

        if (method.SupportsBaseImplementation)
        {
            // The adapter declares the base invocation in terms of its own type parameters.
            var baseInvocation = method.MethodImposter.InvokeMethod.BaseInvocationParameter;
            var adapterBaseInvocation = new ParameterMetadata(
                baseInvocation.Name,
                (TypeSyntax)typeParamRenamer.Visit(baseInvocation.Type),
                baseInvocation.DefaultValue
            );
            parameterList = parameterList.AddParameters(ParameterSyntax(adapterBaseInvocation));

            invokeArguments.Add(
                Argument(
                    TypeCasterSyntaxHelper.CastExpression(
                        baseInvocation.Name,
                        adapterBaseInvocation.Type,
                        method.Delegate.Syntax
                    )
                )
            );
        }

        var invokeExpression = IdentifierName(adapterNames.TargetFieldName)
            .Dot(IdentifierName(MethodImposterInvokeMethodMetadata.Name))
            .Call(ArgumentList(SeparatedList(invokeArguments)));

        if (method.HasReturnValue)
        {
            body.Add(
                LocalVariableDeclarationSyntax(
                    Var,
                    adapterNames.InvokeResultVariableName,
                    invokeExpression
                )
            );
            body.AddRange(postInvokeActions);

            body.Add(ReturnStatement(AdaptedResult(method, adapterNames, typeParamRenamer)));
        }
        else
        {
            body.Add(invokeExpression.ToStatementSyntax());
            body.AddRange(postInvokeActions);
        }

        return new MethodDeclarationBuilder(
            (TypeSyntax)typeParamRenamer.Visit(method.NullableAwareReturnTypeSyntax),
            MethodImposterInvokeMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(parameterList)
            .WithBody(Block(body))
            .Build();
    }

    // A ref or out argument goes back to the caller's type from the adapter's local.
    private static ExpressionSyntax CastBack(
        in MethodParameterMetadata parameter,
        string adaptedName,
        TypeParameterRenamer typeParamRenamer
    )
    {
        if (parameter.IsSpan)
        {
            var elementsType = parameter.NullableAwareStoredTypeSyntax;
            return AdaptedSpan(
                IdentifierName(adaptedName),
                elementsType,
                (TypeSyntax)typeParamRenamer.Visit(elementsType)
            );
        }

        var type = parameter.NullableAwareTypeSyntax;
        return TypeCasterSyntaxHelper.CastExpression(
            adaptedName,
            type,
            (TypeSyntax)typeParamRenamer.Visit(type)
        );
    }

    private static ExpressionSyntax AdaptedResult(
        in ImposterTargetMethodMetadata method,
        in AdapterNames adapterNames,
        TypeParameterRenamer typeParamRenamer
    )
    {
        if (method.ReturnType.IsSpan)
        {
            var result = IdentifierName(adapterNames.InvokeResultVariableName);
            var elementsType = method.ReturnType.ValueTypeSyntax;
            var targetElementsType = (TypeSyntax)typeParamRenamer.Visit(elementsType);

            // The result may refer to the locals the adapter passes by reference, so with those it can't pass back as
            // it is.
            return method.Parameters.HasByReferenceParameters
                ? SpanCopy(result, elementsType, targetElementsType)
                : AdaptedSpan(result, elementsType, targetElementsType);
        }

        // A ref struct result passes back as it is: its type doesn't use the method's type parameters (IMP009
        // otherwise).
        if (method.Model.ReturnType.IsPassedThrough)
        {
            return IdentifierName(adapterNames.InvokeResultVariableName);
        }

        var returnType = method.NullableAwareReturnTypeSyntax;
        return TypeCasterSyntaxHelper.CastExpression(
            adapterNames.InvokeResultVariableName,
            returnType,
            (TypeSyntax)typeParamRenamer.Visit(returnType)
        );
    }

    // A span argument or result can't go through TypeCaster. One whose type uses none of the method's type parameters
    // passes as it is, so writes to it reach the other side; any other passes as a cast copy of its elements, which
    // converts back to a span.
    private static ExpressionSyntax AdaptedSpan(
        ExpressionSyntax span,
        TypeSyntax fromElementsType,
        TypeSyntax toElementsType
    ) =>
        fromElementsType.IsEquivalentTo(toElementsType)
            ? span
            : SpanCopy(span, fromElementsType, toElementsType);

    private static ExpressionSyntax SpanCopy(
        ExpressionSyntax span,
        TypeSyntax fromElementsType,
        TypeSyntax toElementsType
    ) =>
        TypeCasterSyntaxHelper.CastExpression(
            SpanElementsCopy(span),
            fromElementsType,
            toElementsType
        );

    private static MethodDeclarationSyntax BuildAdapterHasMatchingInvocationImposterGroupMethod(
        in ImposterTargetMethodMetadata method,
        in AdapterNames adapterNames
    )
    {
        var typeParamRenamer = new TypeParameterRenamer(
            method.Model.TypeParameters,
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
                () => ParameterSyntax(argumentsTypeWithTarget, argumentsParameterName)
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
                                                    GenericName(
                                                        Identifier(method.ArgumentsAsMethodName),
                                                        TypeArguments(method.GenericTypeArguments)
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
            .Model.TypeParameters.Select(p => TypeParameter(p.Name + "Target1"))
            .ToArray();
        var targetTypeArgs = method.Model.TypeParameters.Select(p =>
            IdentifierName(p.Name + "Target1")
        );
        var genericImposterInterface = GenericName(
            Identifier(method.MethodImposter.Interface.Name),
            TypeArguments(targetTypeArgs)
        );

        return new MethodDeclarationBuilder(NullableType(genericImposterInterface), "As")
            .WithExplicitInterfaceSpecifier(method.MethodImposter.Interface.Syntax)
            .WithTypeParameters(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(ThrowStatement(WellKnownTypes.System.NotImplementedException.New())))
            .Build();
    }

    private readonly struct AdapterNames
    {
        internal readonly string TargetFieldName;
        internal readonly string TargetConstructorParameterName;
        internal readonly string InvokeResultVariableName;
        internal readonly string HasMatchingInvocationImposterGroupArgumentsParameterName;
        internal readonly Dictionary<string, string> AdaptedParameterNames;

        internal AdapterNames(in ImposterTargetMethodMetadata method)
        {
            var nameContext = method.ReservedParameterNames.CreateNameSet();
            TargetFieldName = nameContext.Use("_target");
            TargetConstructorParameterName = nameContext.Use("target");
            InvokeResultVariableName = nameContext.Use("result");
            HasMatchingInvocationImposterGroupArgumentsParameterName = nameContext.Use("arguments");
            AdaptedParameterNames = method
                .Parameters.AllParameterMetadata.Where(parameter =>
                    parameter.Model.RefKind
                        is RefKind.Ref
                            or RefKind.Out
                            or RefKinds.RefReadOnlyParameter
                )
                .ToDictionary(
                    parameter => parameter.Name,
                    parameter => nameContext.Use(parameter.Name + "Adapted")
                );
        }
    }
}
