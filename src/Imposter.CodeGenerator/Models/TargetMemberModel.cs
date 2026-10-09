using System;

namespace Imposter.CodeGenerator.Models;

/// <summary>
/// A member the imposter impersonates. <see cref="RequiresExplicitInterfaceImplementation"/> is true when another member
/// of the target collides with it. <see cref="Setup"/> describes it for the setup views, which only interface targets
/// get.
/// </summary>
internal sealed record TargetMemberModel<TMember>(
    TMember Member,
    bool RequiresExplicitInterfaceImplementation,
    InterfaceSetupMemberModel? Setup
)
    where TMember : IEquatable<TMember>;
