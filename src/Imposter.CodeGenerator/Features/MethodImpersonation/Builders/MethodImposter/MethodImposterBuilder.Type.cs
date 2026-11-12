using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Adapter;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var methodImposterClassBuilder = new ClassDeclarationBuilder(
            method.MethodImposter.Name,
            method.GenericTypeParameterListSyntax
        )
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.InternalKeyword));

        methodImposterClassBuilder.AddBaseType(
            SimpleBaseType(WellKnownTypes.Imposter.Abstractions.IImposterVerificationSource)
        );

        if (method.Model.IsGenericMethod)
        {
            methodImposterClassBuilder = methodImposterClassBuilder.AddBaseType(
                SimpleBaseType(method.MethodImposter.GenericInterface.Syntax)
            );
        }

        return methodImposterClassBuilder
            .AddMember(BuildInvocationImposterGroupsField(method))
            .AddMembers(MethodImposterStateBuilder.BuildMembers(method, method.MethodImposter.Name))
            .AddMember(BuildAsMethodForGenericImposter(method))
            .AddMember(MethodImposterAdapterBuilder.Build(method))
            // Only generic method imposters are looked up by matching setup (through their generic interface).
            .AddMember(
                method.Model.IsGenericMethod
                    ? BuildHasMatchingInvocationImposterGroupMethod(method)
                    : null
            )
            .AddMember(BuildFindMatchingInvocationImposterGroupMethod(method))
            .AddMember(BuildConfiguredSetupsMethod(method))
            .AddMember(InvokeMethod(method, !method.Model.IsGenericMethod))
            .AddMember(
                method.Model.IsGenericMethod
                && method.Parameters.InputParameterMetadata.Any(p => p.IsSpan)
                    ? InvokeMethod(method, recordHistory: false, reuseCapturedSpans: true)
                    : null
            )
            .AddMember(MethodImposterBuilderBuilder.Build(method))
            .AddMembers(MethodVerificationSourceBuilder.Build(method, false))
            .Build();
    }
}
