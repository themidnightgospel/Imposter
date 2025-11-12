using System.Threading.Tasks;
using Imposter.Abstractions;

[assembly: GenerateImposter(typeof(IMyService))]

public interface IMyService
{
    int GetNumber();
    Task<int> GetNumberAsync();
}


var imposter = new IMyServiceImposter();
var service = imposter.Instance();

imposter.GetNumber().Returns(42);
service.GetNumber(); // 42

imposter.GetNumberAsync().ReturnsAsync(7);
await service.GetNumberAsync(); // 7