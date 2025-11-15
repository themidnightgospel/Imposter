using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    internal static IEnumerable<MemberDeclarationSyntax> BuildInvocationSetupInterfaces(
        ImposterTargetMethodMetadata method
    )
    {
        yield return BuildCallbackInterface(method);
        yield return BuildContinuationInterface(method);
        yield return BuildStartInterface(method);
    }

    private static InterfaceDeclarationSyntax BuildCallbackInterface(
        in ImposterTargetMethodMetadata method
    ) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(
                method.Symbol,
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
                method.Symbol,
                method.MethodInvocationImposterGroup.ContinuationInterface.Name
            )
            .AddBaseType(
                SimpleBaseType(method.MethodInvocationImposterGroup.CallbackInterface.Syntax)
            )
            .AddMembers(GetContinuationMethods(method))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();

    private static InterfaceDeclarationSyntax BuildStartInterface(
        in ImposterTargetMethodMetadata method
    ) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.MethodInvocationImposterGroup.Interface.Name)
            .AddBaseType(
                SimpleBaseType(method.MethodInvocationImposterGroup.CallbackInterface.Syntax)
            )
            .AddMembers(GetOutcomeMethods(method))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .Build();

    private static MethodDeclarationSyntax BuildCallbackInterfaceMethod(
        in ImposterTargetMethodMetadata method
    ) =>
        new MethodDeclarationBuilder(
            method.MethodInvocationImposterGroup.CallbackMethod.ReturnType,
            method.MethodInvocationImposterGroup.CallbackMethod.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    InterfaceParameter(
                        method.MethodInvocationImposterGroup.CallbackMethod.CallbackParameter,
                        method
                            .MethodInvocationImposterGroup
                            .CallbackMethod
                            .InterfaceCallbackParameterName
                    )
                )
            )
            .WithSemicolon()
            .Build();

    private static List<MemberDeclarationSyntax> GetContinuationMethods(
        in ImposterTargetMethodMetadata method
    )
    {
        return new List<MemberDeclarationSyntax>
        {
            new MethodDeclarationBuilder(
                method.MethodInvocationImposterGroup.ThenMethod.ReturnType,
                method.MethodInvocationImposterGroup.ThenMethod.Name
            )
                .WithSemicolon()
                .Build(),
        };
    }

    private static List<MemberDeclarationSyntax> GetOutcomeMethods(
        in ImposterTargetMethodMetadata method
    )
    {
        var methods = new List<MemberDeclarationSyntax>();

        var throwsMetadata = method.MethodInvocationImposterGroup.ThrowsMethod;
        var throwsMethodBuilder = new MethodDeclarationBuilder(
            throwsMetadata.ReturnType,
            throwsMetadata.Name
        )
            .WithTypeParameters(throwsMetadata.TypeParameterList)
            .AddConstraintClause(throwsMetadata.TypeParameterConstraintClause);

        methods.Add(throwsMethodBuilder.WithSemicolon().Build());

        methods.AddRange([
            new MethodDeclarationBuilder(
                method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType,
                method.MethodInvocationImposterGroup.ThrowsMethod.Name
            )
                .WithParameterList(
                    SyntaxFactoryHelper
                        .ParameterSyntax(
                            InterfaceParameter(
                                method
                                    .MethodInvocationImposterGroup
                                    .ThrowsMethod
                                    .ExceptionParameter,
                                method
                                    .MethodInvocationImposterGroup
                                    .ThrowsMethod
                                    .InterfaceExceptionParameterName
                            )
                        )
                        .ToSingleParameterListSyntax()
                )
                .WithSemicolon()
                .Build(),
            new MethodDeclarationBuilder(
                method.MethodInvocationImposterGroup.ThrowsMethod.ReturnType,
                method.MethodInvocationImposterGroup.ThrowsMethod.Name
            )
                .AddParameter(
                    SyntaxFactoryHelper.ParameterSyntax(
                        InterfaceParameter(
                            method
                                .MethodInvocationImposterGroup
                                .ThrowsMethod
                                .ExceptionGeneratorParameter,
                            method
                                .MethodInvocationImposterGroup
                                .ThrowsMethod
                                .InterfaceExceptionGeneratorParameterName
                        )
                    )
                )
                .WithSemicolon()
                .Build(),
        ]);

        if (method.HasReturnValue)
        {
            methods.AddRange([
                new MethodDeclarationBuilder(
                    method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType,
                    method.MethodInvocationImposterGroup.ReturnsMethod.Name
                )
                    .AddParameter(
                        SyntaxFactoryHelper.ParameterSyntax(
                            InterfaceParameter(
                                method
                                    .MethodInvocationImposterGroup
                                    .ReturnsMethod
                                    .ResultGeneratorParameter,
                                method
                                    .MethodInvocationImposterGroup
                                    .ReturnsMethod
                                    .InterfaceResultGeneratorParameterName
                            )
                        )
                    )
                    .WithSemicolon()
                    .Build(),
                new MethodDeclarationBuilder(
                    method.MethodInvocationImposterGroup.ReturnsMethod.ReturnType,
                    method.MethodInvocationImposterGroup.ReturnsMethod.Name
                )
                    .AddParameter(
                        SyntaxFactoryHelper.ParameterSyntax(
                            InterfaceParameter(
                                method.MethodInvocationImposterGroup.ReturnsMethod.ValueParameter,
                                method
                                    .MethodInvocationImposterGroup
                                    .ReturnsMethod
                                    .InterfaceValueParameterName
                            )
                        )
                    )
                    .WithSemicolon()
                    .Build(),
            ]);
        }

        if (method.MethodInvocationImposterGroup.ReturnsAsyncMethod is { } returnsAsyncMethod)
        {
            methods.Add(
                new MethodDeclarationBuilder(returnsAsyncMethod.ReturnType, returnsAsyncMethod.Name)
                    .AddParameter(
                        SyntaxFactoryHelper.ParameterSyntax(
                            InterfaceParameter(
                                returnsAsyncMethod.ValueParameter,
                                returnsAsyncMethod.InterfaceValueParameterName
                            )
                        )
                    )
                    .WithSemicolon()
                    .Build()
            );
        }

        if (method.MethodInvocationImposterGroup.ThrowsAsyncMethod is { } throwsAsyncMethod)
        {
            methods.Add(
                new MethodDeclarationBuilder(throwsAsyncMethod.ReturnType, throwsAsyncMethod.Name)
                    .AddParameter(
                        SyntaxFactoryHelper.ParameterSyntax(
                            InterfaceParameter(
                                throwsAsyncMethod.ExceptionParameter,
                                throwsAsyncMethod.InterfaceExceptionParameterName
                            )
                        )
                    )
                    .WithSemicolon()
                    .Build()
            );
        }

        if (
            method.MethodInvocationImposterGroup.UseBaseImplementationMethod is
            { } useBaseImplementationMethod
        )
        {
            methods.Add(
                new MethodDeclarationBuilder(
                    useBaseImplementationMethod.ReturnType,
                    useBaseImplementationMethod.Name
                )
                    .WithSemicolon()
                    .Build()
            );
        }

        return methods;
    }

    private static ParameterMetadata InterfaceParameter(
        ParameterMetadata metadata,
        string interfaceName
    ) => new(interfaceName, metadata.Type, metadata.DefaultValue);

    /*
    private static SyntaxList<TypeParameterConstraintClauseSyntax> GetTypeParameterConstraintClauses(in ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper.AddOrReplaceConstraintClause(
            method.GenericTypeConstraintClauses,
            method.MethodInvocationImposterGroup.ThrowsMethod.InterfaceTypeParameterConstraintClause);
*/
}
