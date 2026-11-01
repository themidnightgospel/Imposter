using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Adapter;
using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var methodImposterClassBuilder = ClassDeclarationBuilderFactory
            .CreateForMethod(method.Model, method.MethodImposter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword));

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
            .AddMember(InvokeMethod(method))
            .AddMember(MethodImposterBuilderBuilder.Build(method))
            .Build();
    }
}
