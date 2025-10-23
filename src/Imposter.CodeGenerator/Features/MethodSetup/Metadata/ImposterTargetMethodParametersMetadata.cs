using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

internal readonly record struct ImposterTargetMethodParametersMetadata
{
    internal IReadOnlyList<IParameterSymbol> Parameters { get; }

    internal IReadOnlyList<IParameterSymbol> InputParameters { get; }

    internal readonly ParameterListSyntax ParameterListSyntax;

    internal readonly ParameterListSyntax InputParameterWithoutRefKindListSyntax;

    internal readonly ArgumentListSyntax InputParametersAsArgumentListSyntaxWithoutRef;

    internal readonly ArgumentListSyntax InputParametersAsArgumentListSyntaxWithRef;

    internal readonly ArgumentListSyntax ParametersAsArgumentListSyntaxWithRef;

    internal bool HasInputParameters => InputParameters.Count > 0;

    internal readonly bool HasOutputParameters;

    public ImposterTargetMethodParametersMetadata(IReadOnlyList<IParameterSymbol> symbolParameters)
    {
        Parameters = symbolParameters;
        InputParameters = symbolParameters.Where(it => it.RefKind is not RefKind.Out).ToArray();
        HasOutputParameters = symbolParameters.Any(it => it.RefKind is RefKind.Out);

        ParameterListSyntax = SyntaxFactoryHelper.ParameterListSyntax(symbolParameters);
        InputParameterWithoutRefKindListSyntax = SyntaxFactoryHelper.ParameterListSyntax(InputParameters, includeRefKind: false);

        InputParametersAsArgumentListSyntaxWithoutRef = SyntaxFactoryHelper.ArgumenstListSyntax(InputParameters, includeRefKind: false);
        InputParametersAsArgumentListSyntaxWithRef = SyntaxFactoryHelper.ArgumenstListSyntax(InputParameters, includeRefKind: true);
        ParametersAsArgumentListSyntaxWithRef = SyntaxFactoryHelper.ArgumenstListSyntax(Parameters, includeRefKind: true);
    }
}