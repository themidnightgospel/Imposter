using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared.Builders;

internal static class InterfaceMethodBuilder
{
    // A method an interface declares: its signature, without a body.
    internal static MethodDeclarationSyntax InterfaceMethod(
        TypeSyntax returnType,
        string name,
        params ParameterMetadata[] parameters
    ) =>
        new MethodDeclarationBuilder(returnType, name)
            .AddParameters(parameters.Select(it => SyntaxFactoryHelper.ParameterSyntax(it)))
            .WithSemicolon()
            .Build();
}
