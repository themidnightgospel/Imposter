namespace Imposter.Abstractions;

/// <summary>Provides access to the recorded interactions of a generated imposter.</summary>
public interface IImposter
{
    /// <summary>Returns a snapshot of recorded interactions in entry order.</summary>
    IReadOnlyList<IImposterInvocation> Invocations();
}
