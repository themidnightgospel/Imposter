namespace Imposter.Abstractions;

public interface IHaveImposterVerifier<TVerifier>
{
    TVerifier Verify();
}