using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static List<FieldDeclarationSyntax> GetFields(in ImposterTargetMethodMetadata method)
    {
        var fields = new List<FieldDeclarationSyntax>
        {
            SinglePrivateReadonlyVariableField(
                method.InvocationHistory.Collection.Syntax,
                method.InvocationHistory.Collection.AsField.Name
            ),
        };

        if (method.Parameters.HasInputParameters)
        {
            fields.Add(
                SinglePrivateReadonlyVariableField(
                    method.MethodImposter.Builder.ArgumentsCriteriaField.Type,
                    method.MethodImposter.Builder.ArgumentsCriteriaField.Name
                )
            );
        }

        return fields;
    }

    private static ParameterSyntax GetImposterParameter(in ImposterTargetMethodMetadata method) =>
        ParameterSyntax(
            method.Model.IsGenericMethod
                ? method.MethodImposter.Builder.ImposterCollectionParameter
                : method.MethodImposter.Builder.MethodImposterParameter
        );
}
