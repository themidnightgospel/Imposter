using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// Discovers GenerateImposterAttribute usages and produces GenerateImposterDeclaration values for the generator pipeline.
/// </summary>
internal static partial class GenerateImposterDeclarationsProvider
{
    // An unbound generic such as typeof(IFoo<>) registers its definition. A type argument that failed to bind registers
    // nothing.
    private static INamedTypeSymbol? RegisteredTarget(AttributeData attribute) =>
        attribute.ConstructorArguments.Length > 0
        && attribute.ConstructorArguments[0].Value
            is INamedTypeSymbol { TypeKind: not TypeKind.Error } target
            ? target.IsUnboundGenericType
                ? target.OriginalDefinition
                : target
            : null;

    private static bool PutInTheSameNamespace(AttributeData attribute) =>
        attribute.ConstructorArguments.Length != 2
        || (bool)attribute.ConstructorArguments[1].Value!;
}
