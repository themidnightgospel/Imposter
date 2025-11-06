using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Adapter;
using Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Builder;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var methodImposterClassBuilder = ClassDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.MethodImposter.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword));

        if (method.Symbol.IsGenericMethod)
        {
            methodImposterClassBuilder = methodImposterClassBuilder.AddBaseType(SimpleBaseType(method.MethodImposter.GenericInterface.Syntax));
        }

        var invocationHistoryCollectionField = SyntaxFactoryHelper
            .SingleVariableField(
                method.InvocationHistory.Collection.Syntax,
                method.InvocationHistory.Collection.AsField.Name,
                TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));

        var invocationBehaviorField = SyntaxFactoryHelper.SingleVariableField(
            WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior,
            "_invocationBehavior",
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));

        return methodImposterClassBuilder
            .AddMember(BuildInvocationSetupsField(method))
            .AddMember(invocationHistoryCollectionField)
            .AddMember(invocationBehaviorField)
            .AddMember(SyntaxFactoryHelper.BuildConstructorAndInitializeMembers(method.MethodImposter.Name, [invocationHistoryCollectionField, invocationBehaviorField]))
            .AddMember(BuildAsMethodForGenericImposter(method))
            .AddMember(MethodImposterAdapterBuilder.Build(method))
            .AddMember(BuildInitializeOutParametersWithDefaultsMethod(method))
            .AddMember(BuildHasMatchingInvocationImposterGroupMethod(method))
            .AddMember(BuildFindMatchingInvocationImposterGroupMethod(method))
            .AddMember(InvokeMethod(method))
            .AddMember(MethodImposterBuilderBuilder.Build(method))
            .Build();

        static MethodDeclarationSyntax? BuildInitializeOutParametersWithDefaultsMethod(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasOutputParameters
                ? InitializeOutParametersMethodBuilder.Build(method)
                : null;
    }
}
