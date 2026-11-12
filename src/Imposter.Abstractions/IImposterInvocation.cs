namespace Imposter.Abstractions;

/// <summary>A recorded interaction with an imposter.</summary>
public interface IImposterInvocation
{
    /// <summary>Gets the name of the member that was called.</summary>
    string Member { get; }

    /// <summary>Gets the global order stamp assigned when the invocation entered the imposter.</summary>
    long Sequence { get; }

    /// <summary>Gets the synchronous exception thrown by the invocation, if any.</summary>
    Exception? Exception { get; }
}
