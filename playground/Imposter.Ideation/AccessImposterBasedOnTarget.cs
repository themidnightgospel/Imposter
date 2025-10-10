namespace Imposter.Ideation;

// The idea is to access the imposter based on the imposter target interface not by it's name
// reason is that, every time you rename the imposter target, imposter class will also be renamed and you'll have to go and update every usage of it

public interface IAnimal
{
}

public interface ICat : IAnimal
{
}

public class AnimalImposter
{
}

public class CatImposter : AnimalImposter, ICat
{
}

public class Imposter<T>
{
}

public class ImposterFactory
{
    /* Doesn't work
    public Imposter<T> Of<T>()
        where T : IAnimal
        => new Imposter<T>();

    public Imposter<T> Of<T>()
        where T : ICat
        => new Imposter<T>();
*/
}