using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    private static List<FieldDeclarationSyntax> GetFields(in ImposterTargetMethodMetadata method)
    {
        var fields = new List<FieldDeclarationSyntax>();

        if (method.Parameters.InputParameters.Count > 0)
        {
            fields.Add(SingleVariableField(method.Arguments.Syntax, InvocationHistoryTypeMetadata.ArgumentsFieldName, TokenList(Token(SyntaxKind.InternalKeyword))));
        }

        if (method.HasReturnValue)
        {
            fields.Add(SingleVariableField(method.ReturnTypeSyntax, InvocationHistoryTypeMetadata.ResultFieldName, TokenList(Token(SyntaxKind.InternalKeyword))));
        }

        fields.Add(SingleVariableField(WellKnownTypes.System.Exception, InvocationHistoryTypeMetadata.ExceptionFieldName, TokenList(Token(SyntaxKind.InternalKeyword))));

        return fields;
    }
}