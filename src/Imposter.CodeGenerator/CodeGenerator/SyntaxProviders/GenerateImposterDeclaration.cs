using System;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// A <c>[GenerateImposter]</c> registration, compared by value so unchanged registrations reuse their output.
/// <see cref="Target"/> is null when one of <see cref="Diagnostics"/> stops the imposter's generation.
/// <see cref="ImposterTypeName"/> identifies the imposter type, so registrations that would share it can be found.
/// </summary>
internal sealed record GenerateImposterDeclaration(
    bool PutInTheSameNamespace,
    string TargetDisplayName,
    LocationModel? TargetLocation,
    string ImposterTypeName,
    string ImposterTypeDisplayName,
    bool CanCollide,
    EquatableArray<DiagnosticModel> Diagnostics,
    ImposterTargetModel? Target
)
{
    /// <summary>
    /// Whether the class of the target's <c>Imposter()</c> extensions is named with the target's arity, because a
    /// same-named target of another arity shares its namespace.
    /// </summary>
    internal bool ExtensionClassNameIncludesArity { get; init; }

    /// <summary>
    /// Whether the imposter's file is named with a hash of the target's name, because another target's file name
    /// differs from it only in letter case, which Roslyn ignores when it compares them.
    /// </summary>
    internal bool HintNameIncludesHash { get; init; }

    // Equality still compares the whole target model. The hash reads only what tells registrations apart, so removing
    // repeated registrations and finding colliding ones doesn't hash every model in depth.
    public override int GetHashCode() =>
        (PutInTheSameNamespace, TargetDisplayName, ImposterTypeName).GetHashCode();

    // A closed generic registration (IMP006) generates its open type's imposter, in the open type's namespace (see
    // ImposterTargetValidator.GeneratedTarget).
    internal static GenerateImposterDeclaration From(
        INamedTypeSymbol registeredTarget,
        bool putInTheSameNamespace,
        MemberAccess memberAccess
    )
    {
        var target = ImposterTargetValidator.GeneratedTarget(registeredTarget);
        var members = TargetMembers.Of(target, memberAccess);
        var (diagnostics, canGenerate) = ImposterTargetValidator.Validate(
            registeredTarget,
            members,
            memberAccess
        );
        var imposterNamespaceName = ImposterGenerationContext.GetImposterNamespaceName(
            putInTheSameNamespace,
            target.ToImposterIdentityString(),
            NamespaceModel.From(target.ContainingNamespace)
        );
        var declaration = new GenerateImposterDeclaration(
            putInTheSameNamespace,
            registeredTarget.ToDisplayString(),
            LocationModel.From(target),
            ImposterTypeCollisions.GetImposterTypeName(target, imposterNamespaceName),
            ImposterTypeCollisions.GetImposterTypeDisplayName(target, imposterNamespaceName),
            ImposterTargetValidator.IsInterfaceOrNonSealedClass(target),
            diagnostics,
            Target: null
        );

        return canGenerate
            ? WithTargetModel(
                declaration,
                () => ImposterTargetModel.From(target, members, memberAccess)
            )
            : declaration;
    }

    // Cancellation must propagate: reporting it as a crash would leave the driver with a cached result that has no
    // source and an error.
    private static GenerateImposterDeclaration WithTargetModel(
        GenerateImposterDeclaration declaration,
        Func<ImposterTargetModel> createTarget
    )
    {
        try
        {
            return declaration with { Target = createTarget() };
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
#if DEBUG
            throw;
#else
            return declaration with
            {
                Diagnostics = new([
                    .. declaration.Diagnostics,
                    CrashDiagnosticsReporter.ToDiagnosticModel(ex),
                ]),
            };
#endif
        }
    }
}
