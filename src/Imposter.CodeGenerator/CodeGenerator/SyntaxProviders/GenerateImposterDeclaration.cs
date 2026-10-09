using System;
using System.Linq;
using Imposter.CodeGenerator.Features.Shared;
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

    internal static GenerateImposterDeclaration From(
        INamedTypeSymbol target,
        bool putInTheSameNamespace,
        MemberAccess memberAccess
    )
    {
        var (diagnostics, canGenerate) = ImposterTargetValidator.Validate(target, memberAccess);
        var imposterNamespaceName = ImposterGenerationContext.GetImposterNamespaceName(
            putInTheSameNamespace,
            TypeModel.From(target),
            NamespaceModel.From(target.ContainingNamespace)
        );
        var declaration = new GenerateImposterDeclaration(
            putInTheSameNamespace,
            target.ToDisplayString(),
            LocationModel.From(target),
            ImposterTypeCollisions.GetImposterTypeName(target, imposterNamespaceName),
            ImposterTypeCollisions.GetImposterTypeDisplayName(target, imposterNamespaceName),
            ImposterTargetValidator.IsInterfaceOrNonSealedClass(target),
            diagnostics,
            Target: null
        );

        return canGenerate ? WithTargetModel(declaration, target, memberAccess) : declaration;
    }

    // Cancellation must propagate: reporting it as a crash would leave the driver with a cached result that has no
    // source and an error.
    private static GenerateImposterDeclaration WithTargetModel(
        GenerateImposterDeclaration declaration,
        INamedTypeSymbol target,
        MemberAccess memberAccess
    )
    {
        try
        {
            return declaration with { Target = ImposterTargetModel.From(target, memberAccess) };
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
#if DEBUG
            throw;
#else
            return declaration with
            {
                Diagnostics = declaration
                    .Diagnostics.Append(CrashDiagnosticsReporter.ToDiagnosticModel(ex))
                    .ToEquatableArray(),
            };
#endif
        }
    }
}
