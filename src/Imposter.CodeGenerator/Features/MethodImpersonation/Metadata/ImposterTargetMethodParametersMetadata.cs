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

    internal readonly ParameterListSyntax ParameterListSyntaxIncludingNullable;

    internal readonly ParameterListSyntax InputParameterWithoutRefKindListSyntaxIncludingNullable;

    internal readonly ArgumentListSyntax InputParametersAsArgumentListSyntaxWithoutRef;

    internal bool HasInputParameters => InputParameters.Count > 0;

    internal readonly bool HasOutputParameters;

    public ImposterTargetMethodParametersMetadata(IReadOnlyList<IParameterSymbol> symbolParameters)
    {
        Parameters = symbolParameters;
        InputParameters = symbolParameters.Where(it => it.RefKind is not RefKind.Out).ToArray();
        OutputParameters = symbolParameters.Where(it => it.RefKind is RefKind.Out).ToArray();
        HasOutputParameters = OutputParameters.Count > 0;

        ParameterListSyntaxIncludingNullable =
            SyntaxFactoryHelper.ParameterListSyntaxIncludingNullable(symbolParameters);
        InputParameterWithoutRefKindListSyntaxIncludingNullable =
            SyntaxFactoryHelper.ParameterListSyntaxIncludingNullable(
                InputParameters,
                includeRefKind: false
            );

        InputParametersAsArgumentListSyntaxWithoutRef = SyntaxFactoryHelper.ArgumentListSyntax(
            InputParameters,
            includeRefKind: false
        );
    }
}
