using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata;

internal readonly record struct ArgumentCriteriaTypeMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly NameSyntax SyntaxWithTargetGenericTypeArguments;

    internal readonly AsMethodMetadata AsMethod;

    internal readonly MatchesMethodMetadata MatchesMethod;

    public ArgumentCriteriaTypeMetadata(ImposterTargetMethodMetadata method)
    {
        var argumentsCriteriaName = $"{method.UniqueName}ArgumentsCriteria";
        Name = argumentsCriteriaName;
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, argumentsCriteriaName);
        SyntaxWithTargetGenericTypeArguments =
            SyntaxFactoryHelper.WithMethodGenericArguments(method.TargetGenericTypeArguments, argumentsCriteriaName);

        var nameContext = new NameSet(method.Symbol.Parameters.Select(p => p.Name));
        MatchesMethod = new MatchesMethodMetadata(nameContext);
        AsMethod = new AsMethodMetadata(nameContext);
    }

    internal readonly struct AsMethodMetadata
    {
        private const string BaseName = "As";

        internal readonly string Name;

        internal AsMethodMetadata(NameSet nameSet)
        {
            Name = nameSet.Use(BaseName);
        }
    }

    internal readonly struct MatchesMethodMetadata
    {
        private const string BaseName = "Matches";
        private const string ParameterBaseName = "arguments";

        internal readonly string Name;
        internal readonly string ParameterName;

        internal MatchesMethodMetadata(NameSet nameSet)
        {
            Name = nameSet.Use(BaseName);
            ParameterName = nameSet.Use(ParameterBaseName);
        }
    }
}
