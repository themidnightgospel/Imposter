using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static IEnumerable<MemberDeclarationSyntax> BuildInvocationSetupInterfaces(
        in ImposterTargetMethodMetadata method
    ) =>
        [
            BuildCallbackInterface(method),
            BuildContinuationInterface(method),
            BuildStartInterface(method),
        ];

    private static InterfaceDeclarationSyntax BuildCallbackInterface(
        in ImposterTargetMethodMetadata method
    ) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(
                method.Model,
                method.MethodInvocationImposterGroup.CallbackInterface.Name
            )
            .AddMember(BuildCallbackInterfaceMethod(method))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();

    private static InterfaceDeclarationSyntax BuildContinuationInterface(
        in ImposterTargetMethodMetadata method
    ) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(
                method.Model,
                method.MethodInvocationImposterGroup.ContinuationInterface.Name
            )
            .AddBaseType(
                SimpleBaseType(method.MethodInvocationImposterGroup.CallbackInterface.Syntax)
            )
            .AddMember(BuildThenInterfaceMethod(method))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();

    private static InterfaceDeclarationSyntax BuildStartInterface(
        in ImposterTargetMethodMetadata method
    ) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Model, method.MethodInvocationImposterGroup.Interface.Name)
            .AddBaseType(
                SimpleBaseType(method.MethodInvocationImposterGroup.CallbackInterface.Syntax)
            )
            .AddMembers(GetOutcomeMethods(method))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();

    private static MethodDeclarationSyntax BuildCallbackInterfaceMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var callback = method.MethodInvocationImposterGroup.CallbackMethod;

        return InterfaceMethod(
            callback.ReturnType,
            callback.Name,
            InterfaceParameter(callback.CallbackParameter, callback.InterfaceCallbackParameterName)
        );
    }

    private static MethodDeclarationSyntax BuildThenInterfaceMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        InterfaceMethod(
            method.MethodInvocationImposterGroup.ThenMethod.ReturnType,
            method.MethodInvocationImposterGroup.ThenMethod.Name
        );

    private static List<MemberDeclarationSyntax> GetOutcomeMethods(
        in ImposterTargetMethodMetadata method
    )
    {
        var throws = method.MethodInvocationImposterGroup.ThrowsMethod;
        List<MemberDeclarationSyntax> methods =
        [
            new MethodDeclarationBuilder(throws.ReturnType, throws.Name)
                .WithTypeParameters(throws.TypeParameterList)
                .AddConstraintClause(throws.TypeParameterConstraintClause)
                .WithSemicolon()
                .Build(),
            InterfaceMethod(
                throws.ReturnType,
                throws.Name,
                InterfaceParameter(
                    throws.ExceptionParameter,
                    throws.InterfaceExceptionParameterName
                )
            ),
            InterfaceMethod(
                throws.ReturnType,
                throws.Name,
                InterfaceParameter(
                    throws.ExceptionGeneratorParameter,
                    throws.InterfaceExceptionGeneratorParameterName
                )
            ),
        ];

        if (method.HasReturnValue)
        {
            var returns = method.MethodInvocationImposterGroup.ReturnsMethod;
            methods.Add(
                InterfaceMethod(
                    returns.ReturnType,
                    returns.Name,
                    InterfaceParameter(
                        returns.ResultGeneratorParameter,
                        returns.InterfaceResultGeneratorParameterName
                    )
                )
            );
            methods.Add(
                InterfaceMethod(
                    returns.ReturnType,
                    returns.Name,
                    InterfaceParameter(returns.ValueParameter, returns.InterfaceValueParameterName)
                )
            );
        }

        if (method.MethodInvocationImposterGroup.ReturnsAsyncMethod is { } returnsAsync)
        {
            methods.Add(
                InterfaceMethod(
                    returnsAsync.ReturnType,
                    returnsAsync.Name,
                    InterfaceParameter(
                        returnsAsync.ValueParameter,
                        returnsAsync.InterfaceValueParameterName
                    )
                )
            );
        }

        if (method.MethodInvocationImposterGroup.ThrowsAsyncMethod is { } throwsAsync)
        {
            methods.Add(
                InterfaceMethod(
                    throwsAsync.ReturnType,
                    throwsAsync.Name,
                    InterfaceParameter(
                        throwsAsync.ExceptionParameter,
                        throwsAsync.InterfaceExceptionParameterName
                    )
                )
            );
        }

        if (
            method.MethodInvocationImposterGroup.UseBaseImplementationMethod is
            { } useBaseImplementation
        )
        {
            methods.Add(
                InterfaceMethod(useBaseImplementation.ReturnType, useBaseImplementation.Name)
            );
        }

        return methods;
    }

    private static MethodDeclarationSyntax InterfaceMethod(
        TypeSyntax returnType,
        string name,
        params ParameterMetadata[] parameters
    ) =>
        new MethodDeclarationBuilder(returnType, name)
            .AddParameters(parameters.Select(it => SyntaxFactoryHelper.ParameterSyntax(it)))
            .WithSemicolon()
            .Build();

    private static ParameterMetadata InterfaceParameter(
        ParameterMetadata metadata,
        string interfaceName
    ) => new(interfaceName, metadata.Type, metadata.DefaultValue);
}
