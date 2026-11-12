using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
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
    private static readonly string[] VerificationMemberNames =
    [
        VerificationSourceNames.InteractionsMethodName,
        VerificationSourceNames.UncoveredInteractionsMethodName,
        VerificationSourceNames.UnusedSetupsMethodName,
    ];

    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Model.HasAdapter)
        {
            return null;
        }
        var adapterNames = new AdapterNames(method);
        var adapterBaseType = SimpleBaseType(
            method.MethodImposter.GenericInterface.SyntaxWithTargetGenericArguments
        );

        var adapterClass = new ClassDeclarationBuilder(
            method.MethodImposter.AdapterName,
            method.TargetGenericTypeParameterListSyntax
        )
            .AddConstraintClauses(method.TargetGenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddBaseType(adapterBaseType)
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Syntax,
                    adapterNames.TargetFieldName
                )
            )
            .AddMember(
                new ConstructorBuilder(method.MethodImposter.AdapterName)
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
            .AddMember(
                method.CanReuseCapturedSpansAcrossAdapters
                    ? BuildAdapterInvokeMethod(method, adapterNames, reuseCapturedSpans: true)
                    : null
            )
            .AddMember(BuildAdapterHasMatchingInvocationImposterGroupMethod(method, adapterNames))
            .AddMember(BuildAdapterAsMethod(method))
            .AddMember(
                new MethodDeclarationBuilder(
                    WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String),
                    method.MethodImposter.ConfiguredSetupsMethodName
                )
                    .AddModifier(Token(SyntaxKind.PublicKeyword))
                    .WithBody(
                        Block(
                            ReturnStatement(
                                IdentifierName(adapterNames.TargetFieldName)
                                    .Dot(
                                        IdentifierName(
                                            method.MethodImposter.ConfiguredSetupsMethodName
                                        )
                                    )
                                    .Call()
                            )
                        )
                    )
                    .Build()
            )
            .AddMembers(BuildVerificationForwarders(adapterNames.TargetFieldName))
            .Build();

        return adapterClass;
    }

    private static MemberDeclarationSyntax[] BuildVerificationForwarders(string targetFieldName) =>
        VerificationMemberNames
            .Select(name =>
                (MemberDeclarationSyntax)
                    new MethodDeclarationBuilder(
                        WellKnownTypes.System.Collections.Generic.IEnumerable(
                            WellKnownTypes.String
                        ),
                        name
                    )
                        .WithExplicitInterfaceSpecifier(
                            WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource
                        )
                        .WithBody(
                            Block(
                                ReturnStatement(
                                    ParenthesizedExpression(
                                            CastExpression(
                                                WellKnownTypes
                                                    .Imposter
                                                    .Abstractions
                                                    .IImposterVerificationSource,
                                                IdentifierName(targetFieldName)
                                            )
                                        )
                                        .Dot(IdentifierName(name))
                                        .Call()
                                )
                            )
                        )
                        .Build()
            )
            .ToArray();

    private static MethodDeclarationSyntax BuildAdapterInvokeMethod(
        in ImposterTargetMethodMetadata method,
        in AdapterNames adapterNames,
        bool reuseCapturedSpans = false
    )
    {
        var body = new List<StatementSyntax>();
        var invokeArguments = new List<ArgumentSyntax>();
        var postInvokeActions = new List<StatementSyntax>();

        var parameterList = method.InTargetTypeArguments(
            method.Parameters.ParameterListSyntaxIncludingNullable
        );

        if (reuseCapturedSpans)
        {
            var capturedNames = method.InvocationHistory.ArgumentFields;
            var capturedParameters = method
                .Parameters.InputParameterMetadata.Where(parameter => parameter.IsSpan)
                .Select(parameter =>
                    ParameterSyntax(
                        parameter.NullableAwareKeptTypeSyntax,
                        capturedNames[parameter.Name]
                    )
                );
            parameterList = parameterList.WithParameters(
                parameterList.Parameters.InsertRange(0, capturedParameters)
            );
            invokeArguments.AddRange(
                method
                    .Parameters.InputParameterMetadata.Where(parameter => parameter.IsSpan)
                    .Select(parameter => Argument(IdentifierName(capturedNames[parameter.Name])))
            );
        }
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
            var castArgument =
                parameter.IsSpan
                    ? AdaptedSpan(
                        IdentifierName(parameter.Name),
                        method.InTargetTypeArguments(parameter.NullableAwareKeptTypeSyntax),
                        parameter.NullableAwareKeptTypeSyntax
                    )
                // dynamic uses none of the method's type parameters, so it passes on as the object it is: TypeCaster
                // would take it as a dynamic argument, which binds at runtime.
                : parameter.Model.Type.IsDynamic
                    ? SyntaxFactoryHelper.AsObject(
                        IdentifierName(parameter.Name),
                        parameter.Model.Type
                    )
                : TypeCasterSyntaxHelper.CastExpression(
                    parameter.Name,
                    method.InTargetTypeArguments(parameterType),
                    parameterType
                );

            switch (parameter.Model.RefKind)
            {
                case RefKind.Ref:
                    invokeArguments.Add(PassThroughLocal(SyntaxKind.RefKeyword, castArgument));
                    postInvokeActions.Add(CastBackToCaller(method));
                    break;
                case RefKind.Out:
                    invokeArguments.Add(
                        PassThroughLocal(SyntaxKind.OutKeyword, DefaultNonNullable)
                    );
                    postInvokeActions.Add(CastBackToCaller(method));
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

            StatementSyntax CastBackToCaller(in ImposterTargetMethodMetadata metadata) =>
                IdentifierName(parameter.Name)
                    .Assign(CastBack(parameter, adaptedParameterNames[parameter.Name], metadata))
                    .ToStatementSyntax();
        }

        if (method.SupportsBaseImplementation)
        {
            // The adapter declares the base invocation in terms of its own type parameters.
            var baseInvocation = method.MethodImposter.InvokeMethod.BaseInvocationParameter;
            var adapterBaseInvocation = new ParameterMetadata(
                baseInvocation.Name,
                method.InTargetTypeArguments(baseInvocation.Type),
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
            .Dot(
                IdentifierName(
                    reuseCapturedSpans
                        ? method.MethodImposter.InvokeWithCapturedSpansMethodName
                        : MethodImposterInvokeMethodMetadata.Name
                )
            )
            .Call(invokeArguments);

        var invocation = new List<StatementSyntax>();

        if (method.HasReturnValue)
        {
            invocation.Add(
                LocalVariableDeclarationSyntax(
                    Var,
                    adapterNames.InvokeResultVariableName,
                    invokeExpression
                )
            );
            invocation.Add(ReturnStatement(AdaptedResult(method, adapterNames)));
        }
        else
        {
            invocation.Add(invokeExpression.ToStatementSyntax());
        }

        return new MethodDeclarationBuilder(
            method.InTargetTypeArguments(method.NullableAwareReturnTypeSyntax),
            reuseCapturedSpans
                ? method.MethodImposter.InvokeWithCapturedSpansMethodName
                : MethodImposterInvokeMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(parameterList)
            .WithBody(
                Block(
                    body.Concat(
                        postInvokeActions.Count == 0
                            ? invocation
                            : new StatementSyntax[]
                            {
                                TryStatement(
                                    Block(invocation),
                                    default,
                                    FinallyClause(Block(postInvokeActions))
                                ),
                            }
                    )
                )
            )
            .Build();
    }

    // A ref or out argument goes back to the caller's type from the adapter's local, which a dynamic one already has.
    private static ExpressionSyntax CastBack(
        in MethodParameterMetadata parameter,
        string adaptedName,
        in ImposterTargetMethodMetadata method
    )
    {
        if (parameter.IsSpan)
        {
            var elementsType = parameter.NullableAwareKeptTypeSyntax;
            return AdaptedSpan(
                IdentifierName(adaptedName),
                elementsType,
                method.InTargetTypeArguments(elementsType)
            );
        }

        if (parameter.Model.Type.IsDynamic)
        {
            return IdentifierName(adaptedName);
        }

        var type = parameter.NullableAwareTypeSyntax;
        return TypeCasterSyntaxHelper.CastExpression(
            adaptedName,
            type,
            method.InTargetTypeArguments(type)
        );
    }

    private static ExpressionSyntax AdaptedResult(
        in ImposterTargetMethodMetadata method,
        in AdapterNames adapterNames
    )
    {
        if (method.ReturnType.IsSpan)
        {
            var result = IdentifierName(adapterNames.InvokeResultVariableName);
            var elementsType = method.ReturnType.ValueTypeSyntax;
            var targetElementsType = method.InTargetTypeArguments(elementsType);

            // The result may refer to the locals the adapter passes by reference, so with those it can't pass back as
            // it is.
            return method.Parameters.HasByReferenceParameters
                ? SpanCopy(result, elementsType, targetElementsType)
                : AdaptedSpan(result, elementsType, targetElementsType);
        }

        // A ref struct result passes back as it is: its type doesn't use the method's type parameters (IMP009
        // otherwise). So does a dynamic one, which uses none, and which TypeCaster would bind at runtime.
        if (method.Model.ReturnType.IsPassedThrough || method.Model.ReturnType.Type.IsDynamic)
        {
            return IdentifierName(adapterNames.InvokeResultVariableName);
        }

        var returnType = method.NullableAwareReturnTypeSyntax;
        return TypeCasterSyntaxHelper.CastExpression(
            adapterNames.InvokeResultVariableName,
            returnType,
            method.InTargetTypeArguments(returnType)
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
        var parameters = method.InTargetTypeArguments(
            CapturedArgumentsBuilder.InParameters(method)
        );
        var arguments = new List<ArgumentSyntax>();
        foreach (var parameter in method.Parameters.InputParameterMetadata)
        {
            var converted = parameter.Model.Type.IsDynamic
                ? AsObject(IdentifierName(parameter.Name), parameter.Model.Type)
                : TypeCasterSyntaxHelper.CastExpression(
                    parameter.Name,
                    method.InTargetTypeArguments(parameter.NullableAwareKeptTypeSyntax),
                    parameter.NullableAwareKeptTypeSyntax
                );
            // A differently typed setup gets its own span snapshot, just as Arguments.As did. A predicate may mutate
            // that array; its writes must not affect the snapshot passed to the next candidate setup.
            arguments.Add(
                Argument(
                    parameter.IsSpan
                        ? SpanElementsCopy(
                            ParenthesizedExpression(
                                CastExpression(parameter.NullableAwareTypeSyntax, converted)
                            )
                        )
                        : converted
                )
            );
        }
        return new MethodDeclarationBuilder(
            HasMatchingInvocationImposterGroupMethodMetadata.ReturnType,
            HasMatchingInvocationImposterGroupMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(parameters)
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(adapterNames.TargetFieldName)
                            .Dot(
                                IdentifierName(
                                    HasMatchingInvocationImposterGroupMethodMetadata.Name
                                )
                            )
                            .Call(arguments)
                    )
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildAdapterAsMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var targetTypeParameterNames = method
            .Model.TypeParameters.Select(p => p.Name + "Target1")
            .ToArray();
        var asMethodTypeParams = targetTypeParameterNames.Select(TypeParameter);
        var targetTypeArgs = targetTypeParameterNames.Select(IdentifierName);
        var genericImposterInterface = GenericName(
            Identifier(method.MethodImposter.Interface.Name),
            TypeArguments(targetTypeArgs)
        );

        return new MethodDeclarationBuilder(
            NullableType(genericImposterInterface),
            MethodImposterMetadata.AsMethodName
        )
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
        internal readonly Dictionary<string, string> AdaptedParameterNames;

        internal AdapterNames(in ImposterTargetMethodMetadata method)
        {
            var nameContext = method.ReservedParameterNames.CreateNameSet();
            TargetFieldName = nameContext.Use("_target");
            TargetConstructorParameterName = nameContext.Use("target");
            InvokeResultVariableName = nameContext.Use("result");
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
