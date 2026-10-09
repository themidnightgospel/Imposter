using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly record struct ImposterTargetMethodParametersMetadata
{
    internal IReadOnlyList<ParameterModel> AllParameters { get; }

    internal IReadOnlyList<ParameterModel> InputParameters { get; }

    internal IReadOnlyList<ParameterModel> OutputParameters { get; }

    internal IReadOnlyList<MethodParameterMetadata> AllParameterMetadata { get; }

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

    public ImposterTargetMethodParametersMetadata(IReadOnlyList<ParameterModel> parameters)
    {
        AllParameters = parameters;
        InputParameters = parameters.Where(it => it.RefKind is not RefKind.Out).ToArray();
        OutputParameters = parameters.Where(it => it.RefKind is RefKind.Out).ToArray();
        HasOutputParameters = OutputParameters.Count > 0;

        AllParameterMetadata = parameters.Select(it => new MethodParameterMetadata(it)).ToArray();
        HasSpanParameters = AllParameterMetadata.Any(it => it.IsSpan);
        HasByReferenceParameters = parameters.Any(it => it.RefKind is not RefKind.None);
        InputParameterMetadata = AllParameterMetadata
            .Where(it => it.Model.RefKind is not RefKind.Out)
            .ToArray();

        ParameterListSyntaxIncludingNullable =
            SyntaxFactoryHelper.ParameterListSyntaxWithoutDefaultValues(AllParameterMetadata);
        InputParameterWithoutRefKindListSyntaxIncludingNullable =
            SyntaxFactoryHelper.ParameterListSyntaxWithoutDefaultValues(
                InputParameterMetadata,
                includeRefKind: false
            );
        ArgParameterListSyntax = SyntaxFactoryHelper.ArgParameters(AllParameterMetadata);
        ArgAnyArgumentListSyntax = SyntaxFactoryHelper.ArgAnyArgumentList(AllParameterMetadata);

        InputParametersAsArgumentListSyntaxWithoutRef = SyntaxFactoryHelper.ArgumentListSyntax(
            InputParameters,
            includeRefKind: false
        );
    }
}
