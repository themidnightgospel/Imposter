using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly record struct ImposterTargetMethodParametersMetadata
{
    internal IReadOnlyList<ParameterModel> AllParameters { get; }

    // The parameters the arguments, the history and the criteria keep: neither an out parameter nor one the imposter
    // only passes through.
    internal IReadOnlyList<ParameterModel> InputParameters { get; }

    internal IReadOnlyList<ParameterModel> OutputParameters { get; }

    internal IReadOnlyList<MethodParameterMetadata> AllParameterMetadata { get; }

    // The parameters setups and the arguments criteria match: all but the ones the imposter only passes through.
    internal IReadOnlyList<MethodParameterMetadata> MatchedParameterMetadata { get; }

    internal IReadOnlyList<MethodParameterMetadata> InputParameterMetadata { get; }

    internal readonly ParameterListSyntax ParameterListSyntaxIncludingNullable;

    internal readonly ParameterListSyntax InputParameterWithoutRefKindListSyntaxIncludingNullable;

    internal readonly ParameterListSyntax ArgParameterListSyntax;

    internal readonly ArgumentListSyntax ArgAnyArgumentListSyntax;

    internal readonly ArgumentListSyntax InputParametersAsArgumentListSyntaxWithoutRef;

    internal bool HasInputParameters => InputParameters.Count > 0;

    internal readonly bool HasOutputParameters;

    internal readonly bool HasSpanParameters;

    internal readonly bool HasByReferenceParameters;

    internal readonly bool HasPassedThroughParameters;

    // An async lambda or method can't declare a ref struct (CS4012) or by-reference (CS1988) parameter.
    internal bool HasAsyncIncompatibleParameters =>
        HasSpanParameters || HasByReferenceParameters || HasPassedThroughParameters;

    public ImposterTargetMethodParametersMetadata(IReadOnlyList<ParameterModel> parameters)
    {
        AllParameters = parameters;
        InputParameters = parameters
            .Where(it => it.RefKind is not RefKind.Out && !it.IsPassedThrough)
            .ToArray();
        OutputParameters = parameters.Where(it => it.RefKind is RefKind.Out).ToArray();
        HasOutputParameters = OutputParameters.Count > 0;

        AllParameterMetadata = parameters.Select(it => new MethodParameterMetadata(it)).ToArray();
        MatchedParameterMetadata = AllParameterMetadata
            .Where(it => !it.Model.IsPassedThrough)
            .ToArray();
        HasSpanParameters = AllParameterMetadata.Any(it => it.IsSpan);
        HasByReferenceParameters = parameters.Any(it => it.RefKind is not RefKind.None);
        HasPassedThroughParameters = parameters.Any(it => it.IsPassedThrough);
        InputParameterMetadata = MatchedParameterMetadata
            .Where(it => it.Model.RefKind is not RefKind.Out)
            .ToArray();

        ParameterListSyntaxIncludingNullable =
            SyntaxFactoryHelper.ParameterListSyntaxWithoutDefaultValues(AllParameters);
        InputParameterWithoutRefKindListSyntaxIncludingNullable =
            SyntaxFactoryHelper.ParameterListSyntaxWithoutDefaultValues(
                InputParameters,
                includeRefKind: false
            );
        ArgParameterListSyntax = SyntaxFactoryHelper.ArgParameters(AllParameters);
        ArgAnyArgumentListSyntax = SyntaxFactoryHelper.ArgumentListSyntax(
            MatchedParameterMetadata.Select(parameter =>
                Argument(parameter.ArgTypeSyntax.Dot(IdentifierName("Any")).Call())
            )
        );

        InputParametersAsArgumentListSyntaxWithoutRef = SyntaxFactoryHelper.ArgumentListSyntax(
            InputParameters,
            includeRefKind: false
        );
    }
}
