using System.ComponentModel;

namespace Imposter.Abstractions;

/// <summary>Provides coverage bookkeeping for generated invocation records.</summary>
/// <remarks>Intended for generated code and verification infrastructure.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IVerifiableInvocation : IImposterInvocation
{
    /// <summary>Whether a successful verification covered this invocation.</summary>
    bool IsVerified { get; }

    /// <summary>Marks this invocation as covered by a successful verification.</summary>
    void MarkVerified();
}
