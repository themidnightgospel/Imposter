using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Imposter;

internal static partial class ImposterBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> MethodImposterFields(in ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext
            .Imposter
            .Methods
            .Select(method => method.Symbol.IsGenericMethod
                ? SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(method.MethodImposter.Collection.Syntax, method.MethodImposter.Collection.AsField.Name)
                : SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(method.MethodImposter.Syntax, method.MethodImposter.AsField.Name));
}