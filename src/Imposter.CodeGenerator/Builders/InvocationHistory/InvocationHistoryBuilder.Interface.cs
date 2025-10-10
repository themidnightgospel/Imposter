using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationHistory;

public static partial class InvocationHistoryBuilder
{
    static MemberDeclarationSyntax BuildHistoryInterface(ImposterTargetMethodMetadata method)
    {
        var matchesMethod = new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "Matches")
            .WithSemicolon()
            .AddTypeParameters(method.Symbol.TypeParameters.Select(p => TypeParameter(p.Name)))
            .AddParameterIf(method.HasInputParameters, () => SyntaxFactoryHelper.ParameterSyntax(method.ArgumentsCriteriaType.Syntax, "criteria"))
            .Build();

        return InterfaceDeclaration(method.InvocationHistory.Interface.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .AddMembers(matchesMethod);
    }
}