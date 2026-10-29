using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationHistory;

internal static partial class InvocationHistoryBuilder
{
    private static List<FieldDeclarationSyntax> GetFields(in ImposterTargetMethodMetadata method)
    {
        var fields = new List<FieldDeclarationSyntax>();

        foreach (var parameter in method.Parameters.InputParameterMetadata)
        {
            fields.Add(
                SingleVariableField(
                    parameter.NullableAwareKeptTypeSyntax,
                    method.InvocationHistory.ArgumentFields[parameter.Name],
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                )
            );
        }

        if (method.KeepsResult)
        {
            fields.Add(
                SingleVariableField(
                    method.ReturnType.KeptTypeSyntax,
                    InvocationHistoryTypeMetadata.ResultFieldName,
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
                )
            );
        }

        fields.Add(
            SingleVariableField(
                WellKnownTypes.System.Exception.ToNullableType(),
                InvocationHistoryTypeMetadata.ExceptionFieldName,
                TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
            )
        );

        return fields;
    }
}
