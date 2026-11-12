using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerInvocationMetadata
{
    internal readonly string Name;
    internal readonly NameSyntax Type;
    internal readonly IReadOnlyDictionary<string, string> Properties;
    internal readonly string SequenceLocal;
    internal readonly string ExceptionLocal;
    internal readonly string ResultLocal;
    internal readonly string ArgumentsLocal;

    internal IndexerInvocationMetadata(in ImposterIndexerCoreMetadata core, bool getter)
    {
        Name = core.UniqueName + (getter ? "GetterInvocation" : "SetterInvocation");
        Type = SyntaxFactory.IdentifierName(Name);
        var names = new NameSet(
            new[]
            {
                Name,
                "Arguments",
                "Result",
                "Value",
                "Member",
                "Sequence",
                "Exception",
                "Next",
                "CompleteInvocation",
                "_value",
                "ToString",
                "Equals",
                "GetHashCode",
                "GetType",
                "ReferenceEquals",
                "MemberwiseClone",
                "Finalize",
            }
        );
        foreach (var parameter in core.MatchedParameters)
            names.Use(parameter.FieldName);
        Properties = core.MatchedParameters.ToDictionary(
            parameter => parameter.Name,
            parameter =>
                names.Use(
                    char.ToUpperInvariant(parameter.Model.Name[0])
                        + parameter.Model.Name.Substring(1)
                )
        );
        var locals = core.CreateParameterNameSet();
        SequenceLocal = locals.Use("invocationSequence");
        ExceptionLocal = locals.Use("invocationException");
        ResultLocal = locals.Use("invocationResult");
        ArgumentsLocal = locals.Use("invocationArguments");
    }
}
