using System.Collections.Generic;
using Imposter.CodeGenerator.Builders.MethodImposter.Builder;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    internal static FieldDeclarationSyntax GetInvocationSetupsField(ImposterTargetMethodMetadata method)
    {
        var invocationSetupsFieldType = SyntaxFactoryHelper.List(method.InvocationSetupType.Syntax);

        return FieldDeclaration(
                VariableDeclaration(invocationSetupsFieldType)
                    .AddVariables(
                        VariableDeclarator(Identifier("_invocationSetups"))
                            .WithInitializer(
                                EqualsValueClause(ObjectCreationExpression(invocationSetupsFieldType).WithArgumentList(ArgumentList()))
                            )
                    )
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword),
                Token(SyntaxKind.ReadOnlyKeyword)
            );
    }

    internal static FieldDeclarationSyntax GetInvocationHistoriesField(ImposterTargetMethodMetadata method)
    {
        var invocationHistoryFieldType = SyntaxFactoryHelper.List(method.InvocationHistory.Syntax);

        return FieldDeclaration(
                VariableDeclaration(invocationHistoryFieldType)
                    .AddVariables(
                        VariableDeclarator(Identifier("_invocationHistory"))
                            .WithInitializer(
                                EqualsValueClause(ObjectCreationExpression(invocationHistoryFieldType).WithArgumentList(ArgumentList()))
                            )
                    )
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword),
                Token(SyntaxKind.ReadOnlyKeyword)
            );
    }

    private static MemberDeclarationSyntax BuildMethodImposterBuilderInterface(ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper
            .InterfaceDeclarationBuilder(method.Symbol, method.MethodImposter.BuilderInterface)
            .AddBaseType(SimpleBaseType(method.InvocationSetupType.Interface.Syntax))
            .AddBaseType(SimpleBaseType(method.InvocationVerifierInterface.Syntax))
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword).WithLeadingTriviaComment(method.DisplayName)));


    private static ClassDeclarationSyntax BuildMethodImposter(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        var methodImposterClassBuilder = SyntaxFactoryHelper
            .ClassDeclarationBuilder(method.Symbol, method.MethodImposter);

        if (method.Symbol.IsGenericMethod)
        {
            methodImposterClassBuilder = methodImposterClassBuilder.AddBaseType(SimpleBaseType(method.MethodImposter.GenericInterface.Syntax));
        }

        return methodImposterClassBuilder
            .AddMember(GetInvocationSetupsField(method))
            .AddMember(GetInvocationHistoriesField(method))
            .AddMemberIf(method.Symbol.IsGenericMethod, () => BuildAsMethodForGenericImposter(method))
            .AddMemberIf(method.Symbol.IsGenericMethod, () => BuildAdapterClass(method))
            .AddMemberIf(method.HasOutParameters, () => SyntaxFactoryHelper.InitializeOutParametersWithDefaultsMethod(method.Symbol.Parameters))
            .AddMember(BuildHasMatchingSetupMethod(method))
            .AddMember(BuildFindMatchingSetupMethod(method))
            .AddMember(InvokeMethod(method))
            .AddMember(MethodImposterBuilderBuilder.Build(method, invocationSetupBuilderInterface))
            .Build()
            .WithLeadingTriviaComment(method.DisplayName);
    }

    internal static IEnumerable<MemberDeclarationSyntax> Build(ImposterTargetMethodMetadata method, InterfaceDeclarationSyntax invocationSetupBuilderInterface)
    {
        if (method.Symbol.IsGenericMethod)
        {
            yield return BuildNonGenericMethodImposterInterface(method);
            yield return BuildGenericMethodImposterInterface(method);
        }

        yield return BuildMethodInvocationVerifierInterface(method);
        yield return BuildMethodImposterBuilderInterface(method);
        yield return BuildMethodImposter(method, invocationSetupBuilderInterface);
    }
}