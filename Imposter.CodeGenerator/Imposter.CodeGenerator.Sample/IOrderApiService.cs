using System.Net.Http;
using System.Threading.Tasks;

namespace Imposter.CodeGenerator.Sample;

public interface IOrderApiService
{
    void Add(int a, int b);
    
    int Multiply(int a, int b);
    
    Task Send(HttpRequestMessage message);
}