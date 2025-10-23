using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Imposter;

internal static partial class ImposterBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> InvocationHistoryCollectionFields(in ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext
            .Imposter
            .Methods
            .Select(method => SyntaxFactoryHelper
                .SinglePrivateReadonlyVariableField(method.InvocationHistory.Collection.Syntax, method.InvocationHistory.Collection.AsField.Name,
                    method.InvocationHistory.Collection.Syntax.New()
                ));
}