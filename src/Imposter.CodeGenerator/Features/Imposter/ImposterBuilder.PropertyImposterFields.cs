using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Imposter;

internal static partial class ImposterBuilder
{
    private static IEnumerable<FieldDeclarationSyntax> PropertyImposterFields(in ImposterGenerationContext imposterGenerationContext) =>
        imposterGenerationContext
            .Imposter
            .Properties
            .Select(property => SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(property.ImposterBuilder.Syntax, property.AsField.Name));
}