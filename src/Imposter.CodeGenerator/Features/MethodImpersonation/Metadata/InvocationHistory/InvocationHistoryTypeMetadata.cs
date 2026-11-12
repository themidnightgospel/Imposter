using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;

internal readonly record struct InvocationHistoryTypeMetadata
{
    // Keep reserving this former field name so existing generated type-parameter names remain stable.
    internal const string ArgumentsFieldName = "Arguments";

    internal const string ResultFieldName = "_result";

    internal const string ExceptionFieldName = "_exception";
    internal const string OutcomeFieldName = "_outcome";
    internal const string SequenceFieldName = "_sequence";
    internal const string MemberPropertyName = "Member";
    internal const string ResultPropertyName = "Result";
    internal const string ExceptionPropertyName = "Exception";
    internal const string SequencePropertyName = "Sequence";
    internal const string IsVerifiedPropertyName = "IsVerified";
    internal readonly string SequenceVariableName;
    internal readonly IReadOnlyDictionary<string, string> ArgumentProperties;

    internal readonly IReadOnlyDictionary<string, string> ArgumentFields;

    internal readonly IReadOnlyDictionary<string, string> ConstructorParameterNames;

    internal readonly string Name;

    internal readonly TypeMetadata Interface;

    internal readonly InvocationHistoryCollectionMetadata Collection;

    internal readonly NameSyntax Syntax;

    public InvocationHistoryTypeMetadata(in ImposterTargetMethodMetadata method)
    {
        Name = method.MemberNames.Use($"{method.UniqueName}Invocation");
        Interface = new TypeMetadata($"I{method.UniqueName}MethodInvocationHistory");
        Collection = new InvocationHistoryCollectionMetadata(
            $"{method.UniqueName}MethodInvocationHistoryCollection",
            method.MemberNames
        );
        var fieldNames = new NameSet(
            method
                .Model.Parameters.Select(p => p.Name)
                .Concat(method.Model.TypeParameters.Select(p => p.Name))
                .Concat(
                    method.TargetGenericTypeArguments.Select(parameter =>
                        parameter.Identifier.ValueText
                    )
                )
                .Concat(
                    method.TargetGenericTypeArguments.Select(parameter =>
                        parameter.Identifier.ValueText
                    )
                )
                .Concat(
                    new[]
                    {
                        Name,
                        ResultFieldName,
                        ExceptionFieldName,
                        OutcomeFieldName,
                        SequenceFieldName,
                        InvocationHistoryCollectionMetadata.NextFieldName,
                    }
                )
        );
        ArgumentFields = method.Parameters.MatchedParameterMetadata.ToDictionary(
            p => p.Name,
            p => fieldNames.Use("captured" + p.Name.TrimStart('@'))
        );
        var constructorNames = method.ReservedParameterNames.CreateNameSet();
        ConstructorParameterNames = ArgumentFields
            .Values.Concat(new[] { ResultFieldName, ExceptionFieldName, SequenceFieldName })
            .ToDictionary(name => name, name => constructorNames.Use(name.TrimStart('_')));
        SequenceVariableName = method.MemberNames.Use("invocationSequence");
        var propertyNames = new NameSet(
            new[]
            {
                Name,
                "Result",
                "Exception",
                "Sequence",
                "Member",
                "Next",
                "IsVerified",
                "MarkVerified",
                "ToString",
                "Equals",
                "GetHashCode",
                "GetType",
                "ReferenceEquals",
                "MemberwiseClone",
                "Finalize",
                FormatValueMethodBuilder.Name,
                ResultFieldName,
                ExceptionFieldName,
                OutcomeFieldName,
                SequenceFieldName,
            }.Concat(method.Model.TypeParameters.Select(p => p.Name))
        );
        ArgumentProperties = method.Parameters.MatchedParameterMetadata.ToDictionary(
            p => p.Name,
            p =>
                propertyNames.Use(
                    char.ToUpperInvariant(p.Model.Name[0]) + p.Model.Name.Substring(1)
                )
        );
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, Name);
    }
}
