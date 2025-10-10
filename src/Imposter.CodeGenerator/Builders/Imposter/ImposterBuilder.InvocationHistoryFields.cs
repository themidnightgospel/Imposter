using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Builders.Imposter;

internal static partial class ImposterBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> InvocationHistoryCollectionFields(ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext
            .Methods
            .Select(method => SyntaxFactoryHelper.SingleVariableField(method.InvocationHistory.Collection.Syntax, method.InvocationHistory.Collection.AsField.Name));
}