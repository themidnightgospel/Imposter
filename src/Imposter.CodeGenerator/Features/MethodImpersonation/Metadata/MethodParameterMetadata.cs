using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;

internal readonly struct MethodParameterMetadata
{
    internal readonly ParameterModel Model;

    internal readonly string Name;

    internal readonly TypeSyntax TypeSyntax;

    internal readonly TypeSyntax NullableAwareTypeSyntax;

    internal readonly TypeSyntax ArgTypeSyntax;

    internal MethodParameterMetadata(ParameterModel model)
    {
        Model = model;
        Name = SyntaxFactoryHelper.EscapeKeyword(model.Name);
        TypeSyntax = SyntaxFactoryHelper.TypeSyntax(model.Type);
        NullableAwareTypeSyntax = SyntaxFactoryHelper.TypeSyntaxIncludingNullable(model.Type);
        ArgTypeSyntax =
            model.RefKind == RefKind.Out
                ? WellKnownTypes.Imposter.Abstractions.OutArg(NullableAwareTypeSyntax)
                : WellKnownTypes.Imposter.Abstractions.Arg(NullableAwareTypeSyntax);
    }
}
