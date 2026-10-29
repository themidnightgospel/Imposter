using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Imposter.CodeGenerator.CodeGenerator;

/// <summary>
/// The last source generated under each hint name, with what it was generated from. Removing or inserting a
/// registration moves the ones after it, and the driver compares targets by position, so it hands the moved ones to
/// the source output again although they did not change; those reuse their source instead of building it again.
/// </summary>
/// <remarks>
/// One cache serves one generator pipeline, so projects don't share it. Each hint name keeps only its latest source,
/// which is the one the driver holds while the target is registered, and loses it once no target generates under that
/// name, so the cache holds only what the driver holds already.
/// </remarks>
internal sealed class ImposterSourceCache
{
    private readonly ConcurrentDictionary<string, Entry> _entries = new(StringComparer.Ordinal);

    // Null when the hint name's latest source was generated from another target or C# version.
    internal SourceText? Get(
        string hintName,
        in ImposterGenerationTarget target,
        LanguageVersion languageVersion
    ) =>
        _entries.TryGetValue(hintName, out var entry)
        && entry.Target.Equals(target)
        && entry.LanguageVersion == languageVersion
            ? entry.Source
            : null;

    internal void Set(
        string hintName,
        in ImposterGenerationTarget target,
        LanguageVersion languageVersion,
        SourceText source
    ) => _entries[hintName] = new Entry(target, languageVersion, source);

    // Drops the sources of hint names that are no longer generated, such as those of removed or renamed targets.
    internal void KeepOnly(ImmutableArray<string> hintNames)
    {
        foreach (var staleHintName in _entries.Keys.Except(hintNames, StringComparer.Ordinal))
        {
            _entries.TryRemove(staleHintName, out _);
        }
    }

    private sealed record Entry(
        ImposterGenerationTarget Target,
        LanguageVersion LanguageVersion,
        SourceText Source
    );
}
