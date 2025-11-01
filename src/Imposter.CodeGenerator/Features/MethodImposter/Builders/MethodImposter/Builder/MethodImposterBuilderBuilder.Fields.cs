using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static List<FieldDeclarationSyntax> GetFields(in ImposterTargetMethodMetadata method)
    {
        var fields = new List<FieldDeclarationSyntax>();

        if (method.Symbol.IsGenericMethod)
        {
            fields.Add(SinglePrivateReadonlyVariableField(method.MethodImposter.Builder.ImposterCollectionField.Type, method.MethodImposter.Builder.ImposterCollectionField.Name));
        }
        else
        {
            fields.Add(SinglePrivateReadonlyVariableField(method.MethodImposter.Builder.MethodImposterField.Type, method.MethodImposter.Builder.MethodImposterField.Name));
        }

        fields.Add(SinglePrivateReadonlyVariableField(method.InvocationHistory.Collection.Syntax, method.InvocationHistory.Collection.AsField.Name));

        if (method.Parameters.HasInputParameters)
        {
            fields.Add(SinglePrivateReadonlyVariableField(method.MethodImposter.Builder.ArgumentsCriteriaField.Type, method.MethodImposter.Builder.ArgumentsCriteriaField.Name));
        }

        return fields;
    }
}