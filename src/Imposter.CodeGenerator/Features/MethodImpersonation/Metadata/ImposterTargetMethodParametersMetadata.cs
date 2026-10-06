using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly record struct ImposterTargetMethodParametersMetadata
{
    internal IReadOnlyList<IParameterSymbol> Parameters { get; }

    internal IReadOnlyList<IParameterSymbol> InputParameters { get; }

    internal IReadOnlyList<IParameterSymbol> OutputParameters { get; }

    internal IReadOnlyList<MethodParameterMetadata> AllParameterMetadata { get; }

    internal IReadOnlyList<MethodParameterMetadata> InputParameterMetadata { get; }

    internal readonly ParameterListSyntax ParameterListSyntaxIncludingNullable;

    internal readonly ParameterListSyntax InputParameterWithoutRefKindListSyntaxIncludingNullable;

    internal readonly ParameterListSyntax ArgParameterListSyntax;

    internal readonly ArgumentListSyntax ArgAnyArgumentListSyntax;

    internal readonly ArgumentListSyntax InputParametersAsArgumentListSyntaxWithoutRef;

    internal bool HasInputParameters => InputParameters.Count > 0;

    internal readonly bool HasOutputParameters;

    public ImposterTargetMethodParametersMetadata(IReadOnlyList<IParameterSymbol> symbolParameters)
    {
        Parameters = symbolParameters;
        InputParameters = symbolParameters.Where(it => it.RefKind is not RefKind.Out).ToArray();
        OutputParameters = symbolParameters.Where(it => it.RefKind is RefKind.Out).ToArray();
        HasOutputParameters = OutputParameters.Count > 0;

        AllParameterMetadata = symbolParameters
            .Select(it => new MethodParameterMetadata(it))
            .ToArray();
        InputParameterMetadata = AllParameterMetadata
            .Where(it => it.Symbol.RefKind is not RefKind.Out)
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
