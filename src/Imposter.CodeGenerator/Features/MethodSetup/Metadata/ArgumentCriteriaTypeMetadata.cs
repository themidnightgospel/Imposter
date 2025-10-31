using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

internal readonly record struct ArgumentCriteriaTypeMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly NameSyntax SyntaxWithTargetGenericTypeArguments;
    
    public ArgumentCriteriaTypeMetadata(ImposterTargetMethodMetadata method)
    {
        Name = $"{method.UniqueName}ArgumentsCriteria";
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(method.GenericTypeArguments, $"{method.UniqueName}ArgumentsCriteria");
        SyntaxWithTargetGenericTypeArguments = SyntaxFactoryHelper.WithMethodGenericArguments(method.TargetGenericTypeArguments, $"{method.UniqueName}ArgumentsCriteria");
    }
    
    internal readonly struct AsMethodMetadata
    {
        internal const string Name = "As";
    }

    internal readonly struct MatchesMethodMetadata
    {
        internal const string Name = "Matches";
    }
}