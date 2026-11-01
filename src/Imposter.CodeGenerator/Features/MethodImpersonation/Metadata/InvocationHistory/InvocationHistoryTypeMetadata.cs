using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;

internal readonly record struct InvocationHistoryTypeMetadata
{
    // Keep reserving this former field name so existing generated type-parameter names remain stable.
    internal const string ArgumentsFieldName = "Arguments";

    internal const string ResultFieldName = "Result";

    internal const string ExceptionFieldName = "Exception";

    internal readonly IReadOnlyDictionary<string, string> ArgumentFields;

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly InvocationHistoryCollectionMetadata Collection;

    internal readonly NameSyntax Syntax;

    public InvocationHistoryTypeMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}MethodInvocationHistory";
        Interface = new TypeMetadata($"I{Name}");
        Collection = new InvocationHistoryCollectionMetadata(
            $"{Name}Collection",
            method.MemberNames
        );
        var fieldNames = new NameSet(
            method
                .Model.Parameters.Select(p => p.Name)
                .Concat(method.Model.TypeParameters.Select(p => p.Name))
                .Concat(
                    new[]
                    {
                        Name,
                        ResultFieldName,
                        ExceptionFieldName,
                        InvocationHistoryCollectionMetadata.NextFieldName,
                    }
                )
        );
        ArgumentFields = method.Parameters.InputParameterMetadata.ToDictionary(
            p => p.Name,
            p => fieldNames.Use("captured" + p.Name.TrimStart('@'))
        );
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
    }
}
