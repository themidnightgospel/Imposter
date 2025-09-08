namespace Imposter.Abstractions;

/// <summary>
/// TODO Add Doc
/// </summary>
public static class ImposterExtensions
{
    public static TVerifier Verify<TVerifier>(this IHaveImposterVerifier<TVerifier> imposter)
        => imposter.Verify();

    public static TInstance Instance<TInstance>(this IHaveImposterInstance<TInstance> imposter)
        => imposter.Instance();
}