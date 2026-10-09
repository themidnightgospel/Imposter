using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly record struct ArgumentCriteriaTypeMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax Syntax;

    internal readonly NameSyntax SyntaxWithTargetGenericTypeArguments;

    internal readonly AsMethodMetadata AsMethod;

    internal readonly MatchesMethodMetadata MatchesMethod;

    public ArgumentCriteriaTypeMetadata(in ImposterTargetMethodMetadata method)
    {
        // The class keeps each parameter in a field named after it, which can't share the class's name.
        var argumentsCriteriaName = method.MemberNames.Use($"{method.UniqueName}ArgumentsCriteria");
        Name = argumentsCriteriaName;
        Syntax = SyntaxFactoryHelper.WithMethodGenericArguments(
            method.GenericTypeArguments,
            argumentsCriteriaName
        );
        SyntaxWithTargetGenericTypeArguments = SyntaxFactoryHelper.WithMethodGenericArguments(
            method.TargetGenericTypeArguments,
            argumentsCriteriaName
        );

        var nameContext = new NameSet(method.Model.Parameters.Select(p => p.Name));
        MatchesMethod = new MatchesMethodMetadata(nameContext);
        AsMethod = new AsMethodMetadata(nameContext, method.ReservedParameterNames);
    }

    // A generic method's criteria convert themselves to the target type arguments with this method.
    internal readonly struct AsMethodMetadata
    {
        private const string BaseName = "As";

        internal readonly string Name;

        // The parameter of the lambdas that convert each matcher. It can't hide the criteria's fields, which are named
        // after the method's parameters.
        internal readonly IdentifierNameSyntax MatcherLambdaParameter;

        internal AsMethodMetadata(NameSet nameSet, in ReservedParameterNames reservedParameterNames)
        {
            Name = nameSet.Use(BaseName);
            MatcherLambdaParameter = SyntaxFactory.IdentifierName(
                reservedParameterNames.CreateNameSet().Use("it")
            );
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
