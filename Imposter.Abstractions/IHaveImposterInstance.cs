namespace Imposter.Abstractions;

public interface IHaveImposterInstance<out TInstance>
{
    TInstance Instance();
}