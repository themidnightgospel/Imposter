using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal static partial class ImposterBuilder
{
    private static IEnumerable<PropertyDeclarationSyntax> BuildImposterProperties(in ImposterGenerationContext imposterGenerationContext)
    {
        return imposterGenerationContext
            .Imposter
            .Properties
            .Select(property =>
                SyntaxFactoryHelper.ReadOnlyPropertyDeclarationSyntax(
                    property.PropertyImposterInterface.Syntax,
                    property.Symbol.Name,
                    IdentifierName(property.AsField.Name)
                )
            );
    }
}