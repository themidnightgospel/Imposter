using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationHistory;

public static class InvocationHistoryInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method) =>
        new InterfaceDeclarationBuilder(method.InvocationHistory.Interface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildMatchesMethod(method))
            .Build();

    private static MethodDeclarationSyntax BuildMatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), InvocationHistoryMatchesMethodMetadata.Name)
            .WithSemicolon()
            .WithTypeParameters(method.TargetGenericTypeParameterListSyntax)
            .AddParameter(GetParameter(method))
            .Build();

        static ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasInputParameters
                ? SyntaxFactoryHelper.ParameterSyntax(method.ArgumentsCriteria.SyntaxWithTargetGenericTypeArguments, InvocationHistoryMatchesMethodMetadata.ArgumentsCriteriaParameterName)
                : null;
        }
    }
}
