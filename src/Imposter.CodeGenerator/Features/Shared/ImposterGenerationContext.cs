using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Imposter.CodeGenerator.Models;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterGenerationContext
{
    internal readonly ImposterTargetModel Target;

    internal readonly ImposterTargetMetadata Imposter;

    internal readonly string? ImposterNamespaceName;

    internal readonly SupportedCSharpFeatures SupportedCSharpFeatures;

    // The static class of the target's Imposter() extensions. It ends in the target's arity when a same-named target of
    // another arity shares the namespace, e.g. IFooImposterExtensions1: no other target's class name can end that way.
    internal readonly string ExtensionClassName;

    // A member whose generated types would clash with another member's name names them after a unique name other than
    // those in avoidedUniqueNames (see ImposterBuilder.UniqueNamesOfClashingTypes).
    internal ImposterGenerationContext(
        in ImposterGenerationTarget generationTarget,
        in SupportedCSharpFeatures supportedCSharpFeatures,
        IEnumerable<string> avoidedUniqueNames
    )
    {
        Target = generationTarget.Target;
        var putInTheSameNamespace = generationTarget.PutInTheSameNamespace;
        Imposter = new ImposterTargetMetadata(Target, avoidedUniqueNames);
        var arity = generationTarget.ExtensionClassNameIncludesArity
            ? Target.TypeParameters.Count.ToString(CultureInfo.InvariantCulture)
            : "";
        ExtensionClassName = $"{Target.Name}ImposterExtensions{arity}";

        ImposterNamespaceName = GetImposterNamespaceName(
            putInTheSameNamespace,
            Target.IdentityName,
            Target.ContainingNamespace
        );
        SupportedCSharpFeatures = supportedCSharpFeatures;
    }

    internal static string GetHintName(
        string targetIdentityName,
        bool putInTheSameNamespace,
        bool includesHash
    )
    {
        var targetName = GetTargetName(targetIdentityName);
        var sanitizedTargetName = SanitizeForNamespace(targetName);
        var hintName =
            $"{sanitizedTargetName}{GetHintNameSuffix(targetName, sanitizedTargetName, includesHash)}";

        return putInTheSameNamespace ? hintName : $"{DedicatedNamespacePrefix}.{hintName}";
    }

    private const string DedicatedNamespacePrefix = "Imposters";

    // Null for the global namespace.
    internal static string? GetImposterNamespaceName(
        bool putInTheSameNamespace,
        string targetIdentityName,
        NamespaceModel targetNamespace
    ) =>
        putInTheSameNamespace
            ? targetNamespace.DisplayName
            : $"{DedicatedNamespacePrefix}.{SanitizeForNamespace(GetTargetName(targetIdentityName))}";

    // The target's fully qualified name without `global::`, e.g. `Sample.IPair<int, string>`.
    private static string GetTargetName(string targetIdentityName)
    {
        const string globalPrefix = "global::";
        return targetIdentityName.StartsWith(globalPrefix, StringComparison.Ordinal)
            ? targetIdentityName[globalPrefix.Length..]
            : targetIdentityName;
    }

    // Sanitizing is lossy (`IFoo<int>` and `IFoo_int_` both become `IFoo_int_`), and duplicate hint names make
    // Roslyn drop every source of the generator. A sanitized name without `_` lost nothing, so it is used as is,
    // unless another target's plain name differs from it only in letter case, which Roslyn's comparison of hint names
    // ignores (see ImposterTypeCollisions). Any other name gets a hash of the original name, so it differs from every
    // plain name and only collides with another hashed name on a hash collision.
    private static string GetHintNameSuffix(
        string targetName,
        string sanitizedTargetName,
        bool includesHash
    ) =>
        !includesHash && sanitizedTargetName.IndexOf('_') < 0
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
