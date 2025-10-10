using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Builders.Imposter;

internal static partial class ImposterBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> MethodImposterFields(ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext
            .Methods
            .Select(method => method.Symbol.IsGenericMethod
                ? SyntaxFactoryHelper.SingleVariableField(method.MethodImposter.Collection.Syntax, method.MethodImposter.Collection.AsField.Name)
                : SyntaxFactoryHelper.SingleVariableField(method.MethodImposter.Syntax, method.MethodImposter.AsField.Name));
}