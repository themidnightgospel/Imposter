using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerParameterMetadata
{
    internal readonly ParameterModel Model;

    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal readonly ParameterSyntax ParameterSyntax;

    // The field that keeps this parameter in the arguments and criteria classes: the parameter name, unless one of
    // those classes declares a member of that name.
    internal readonly string FieldName;

    internal IndexerParameterMetadata(ParameterModel model, NameSet fieldNames)
    {
        Model = model;
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(model.Type);
        ArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);
        ParameterSyntax = SyntaxFactoryHelper.ParameterSyntaxIncludingNullable(model);
        FieldName = Name is "Equals" or "GetHashCode" or "Matches" ? fieldNames.Use(Name) : Name;
    }

    // Passes this parameter, or a copy of it, to a member that declares the same parameter.
    internal ArgumentSyntax ForwardingArgument(string variableName) =>
        SyntaxFactoryHelper.ForwardingArgument(variableName, Model.RefKind);
}
