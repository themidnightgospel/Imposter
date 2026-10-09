using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct EventParameterMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal readonly ParameterSyntax ParameterSyntax;

    // Passes this parameter on to a member that declares it the same way: a handler, a callback or the sync raise.
    internal readonly ArgumentSyntax ForwardingArgument;

    // This parameter's element in the raise and handler-invocation history tuples: the parameter name, unless C#
    // reserves it for tuple elements.
    internal readonly string TupleElementName;

    internal EventParameterMetadata(ParameterModel model, NameSet tupleElementNames)
    {
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(model.Type);
        ArgTypeSyntax = WellKnownTypes.Imposter.Abstractions.Arg(TypeSyntax);
        ParameterSyntax = SyntaxFactoryHelper.ParameterSyntax(model);
        ForwardingArgument = SyntaxFactoryHelper.ForwardingArgument(Name, model.RefKind);
        TupleElementName = TupleElementNames.IsReserved(Name) ? tupleElementNames.Use(Name) : Name;
    }
}
