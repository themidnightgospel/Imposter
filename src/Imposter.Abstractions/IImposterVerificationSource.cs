using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Supplies diagnostic findings from a generated member's verification state.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IImposterVerificationSource
{
    /// <summary>Describes interactions that no successful check has covered.</summary>
    IEnumerable<string> UncoveredInteractions();

    /// <summary>Describes all recorded interactions.</summary>
    IEnumerable<string> Interactions();

    /// <summary>Describes configured, non-lenient setups that have not answered a call.</summary>
    IEnumerable<string> UnusedSetups();
}
