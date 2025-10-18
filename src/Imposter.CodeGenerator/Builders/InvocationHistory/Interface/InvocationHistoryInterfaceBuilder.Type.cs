using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationHistory.Interface;

public static class InvocationHistoryInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method) =>
        InterfaceDeclaration(method.InvocationHistory.Interface.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .AddMembers(BuildMatchesMethod(method));

    private static MethodDeclarationSyntax BuildMatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "Matches")
            .WithSemicolon()
            .AddTypeParameters(method.Symbol.TypeParameters.Select(p => TypeParameter(p.Name)))
            .AddParameter(GetParameter(method))
            .Build();

        static ParameterSyntax? GetParameter(in ImposterTargetMethodMetadata method)
        {
            return method.Parameters.HasInputParameters
                ? SyntaxFactoryHelper.ParameterSyntax(method.ArgumentsCriteria.Syntax, "criteria")
                : null;
        }
    }
}