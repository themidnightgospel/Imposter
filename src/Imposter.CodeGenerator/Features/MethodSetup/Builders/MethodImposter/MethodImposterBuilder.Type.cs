using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.Adapter;
using Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter.Builder;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterTargetMethodMetadata method, in InterfaceDeclarationSyntax invocationSetupBuilderInterface)
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

        return methodImposterClassBuilder
            .AddMember(BuildInvocationSetupsField(method))
            .AddMember(invocationHistoryCollectionField)
            .AddMember(SyntaxFactoryHelper.BuildConstructorAndInitializeMembers(method.MethodImposter.Name, [invocationHistoryCollectionField]))
            .AddMember(BuildAsMethodForGenericImposter(method))
            .AddMember(MethodImposterAdapterBuilder.Build(method))
            .AddMember(BuildInitializeOutParametersWithDefaultsMethod(method))
            .AddMember(BuildHasMatchingSetupMethod(method))
            .AddMember(BuildFindMatchingSetupMethod(method))
            .AddMember(InvokeMethod(method))
            .AddMember(MethodImposterBuilderBuilder.Build(method, invocationSetupBuilderInterface))
            .Build();

        static MethodDeclarationSyntax? BuildInitializeOutParametersWithDefaultsMethod(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasOutputParameters
                ? InitializeOutParametersMethodBuilder.Build(method)
                : null;
    }
}