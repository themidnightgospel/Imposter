using Imposter.CodeGenerator.Models;

namespace Imposter.CodeGenerator.Features.Shared;

/// <summary>
/// What generating one imposter reads from its registration. It leaves out the target's location, which moves with
/// every edit above the registration, so such edits reuse the generated imposter.
/// </summary>
internal readonly record struct ImposterGenerationTarget(
    ImposterTargetModel Target,
    bool PutInTheSameNamespace,
    bool ExtensionClassNameIncludesArity
);
