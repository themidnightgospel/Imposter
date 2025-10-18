using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> GetFields(in ImposterTargetMethodMetadata method)
    {
        if (method.Symbol.IsGenericMethod)
        {
            yield return SyntaxFactoryHelper.SingleVariableField(method.MethodImposter.Collection.Syntax, "_imposterCollection", TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));
        }
        else
        {
            yield return SyntaxFactoryHelper.SingleVariableField(method.MethodImposter.Syntax, "_imposter", TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));
        }

        yield return SyntaxFactoryHelper.SingleVariableField(method.InvocationHistory.Collection.Syntax, method.InvocationHistory.Collection.AsField.Name, TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));

        if (method.Parameters.HasInputParameters)
        {
            yield return SyntaxFactoryHelper.SingleVariableField(method.ArgumentsCriteria.Syntax, "_argumentsCriteria", TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));
        }
    }
}