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

    internal IndexerParameterMetadata(ParameterModel model)
    {
        Model = model;
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(model.Type);
        ArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);
        ParameterSyntax = SyntaxFactoryHelper.ParameterSyntax(model);
    }
}
