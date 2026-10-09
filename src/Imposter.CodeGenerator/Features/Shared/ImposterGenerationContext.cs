using System;
using System.Globalization;
using System.Text;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterGenerationContext
{
    internal readonly ImposterTargetModel Target;

    internal readonly ImposterTargetMetadata Imposter;

    internal readonly string? ImposterNamespaceName;

    // Derived from the target alone, so it does not depend on the order in which targets are processed.
    internal readonly string HintName;

    internal readonly SupportedCSharpFeatures SupportedCSharpFeatures;

    internal ImposterGenerationContext(
        ImposterTargetModel target,
        bool putInTheSameNamespace,
        in SupportedCSharpFeatures supportedCSharpFeatures
    )
    {
        Target = target;
        Imposter = new ImposterTargetMetadata(Target, supportedCSharpFeatures);

        var targetName = GetTargetName(Target.Type);
        var sanitizedTargetName = SanitizeForNamespace(targetName);
        var hintNameSuffix = GetHintNameSuffix(targetName, sanitizedTargetName);

        ImposterNamespaceName = GetImposterNamespaceName(
            putInTheSameNamespace,
            Target.Type,
            Target.ContainingNamespace
        );
        HintName = putInTheSameNamespace
            ? $"{sanitizedTargetName}{hintNameSuffix}"
            : $"{DedicatedNamespacePrefix}.{sanitizedTargetName}{hintNameSuffix}";

        SupportedCSharpFeatures = supportedCSharpFeatures;
    }

    private const string DedicatedNamespacePrefix = "Imposters";

    // Null for the global namespace.
    internal static string? GetImposterNamespaceName(
        bool putInTheSameNamespace,
        TypeModel target,
        NamespaceModel targetNamespace
    ) =>
        putInTheSameNamespace
            ? targetNamespace.DisplayName
            : $"{DedicatedNamespacePrefix}.{SanitizeForNamespace(GetTargetName(target))}";

    // The target's fully qualified name without `global::`, e.g. `Sample.IPair<int, string>`.
    private static string GetTargetName(TypeModel target)
    {
        var display = target.FullyQualifiedName;

        const string globalPrefix = "global::";
        return display.StartsWith(globalPrefix, StringComparison.Ordinal)
            ? display[globalPrefix.Length..]
            : display;
    }

    // Sanitizing is lossy (`IFoo<int>` and `IFoo_int_` both become `IFoo_int_`), and duplicate hint names make
    // Roslyn drop every source of the generator. A sanitized name without `_` lost nothing, so it is used as is
    // and plain names cannot collide. Any other name gets a hash of the original name, so it differs from every
    // plain name and only collides with another hashed name on a hash collision.
    private static string GetHintNameSuffix(string targetName, string sanitizedTargetName) =>
        sanitizedTargetName.IndexOf('_') < 0
            ? "Imposter.g.cs"
            : $"Imposter.{Fnv1aHash(targetName).ToString("x8", CultureInfo.InvariantCulture)}.g.cs";

    // FNV-1a over UTF-16 code units: unlike string.GetHashCode, it is stable across processes and platforms.
    private static uint Fnv1aHash(string value)
    {
        const uint offsetBasis = 2166136261;
        const uint prime = 16777619;

        var hash = offsetBasis;
        foreach (var ch in value)
        {
            hash = unchecked((hash ^ ch) * prime);
        }

        return hash;
    }

    // Reduces a name to characters valid in a namespace and a hint name, e.g. `Sample.IPair<int, string>`
    // becomes `Sample.IPair_int__string_`.
    private static string SanitizeForNamespace(string value)
    {
        var builder = new StringBuilder(value.Length);

        foreach (var ch in value)
        {
            if (char.IsLetterOrDigit(ch) || ch is '_' or '.')
            {
                builder.Append(ch);
            }
            else
            {
                builder.Append('_');
            }
        }

        return builder.ToString();
    }
}
