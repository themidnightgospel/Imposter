using System;
using System.Threading.Tasks;

namespace Imposter.CodeGenerator.Tests;

// TODO Generic interface ?
// TODO Generic methods
// TODO Ref and Out keywords
// TODO params keyword
// TODO capture out variables after invocation
// TODO Add callback
public interface IOrderApiService
{
    // 1. Void method
    void DoWork();

    // 2. Method with return type
    int GetNumber();

    // 3. Method with in parameter
    void Process(in int value);

    // 4. Method with ref parameter
    void Modify(ref int value);

    // 5. Method with out parameter
    void Initialize(out int value);

    // 6. Method with params parameter
    void AddNumbers(params int[] numbers);

    // 7. Method with optional parameters
    void Greet(string name = "Guest");

    // 8. Method with nullable reference types
    string? FindUser(string? username);

    /*
    // 9. Generic method
    T Echo<T>(T input);

    // 10. Generic method with constraints
    T CreateInstance<T>() where T : new();
    */

    // 11. Method returning a tuple
    (int sum, int count) Calculate(int[] numbers);

    // 12. Async-like method signature
    Task SaveAsync();

    // 13. Method returning ValueTask
    ValueTask<bool> TrySaveAsync();

    // 14. Overloaded method
    void Log(string message);
    void Log(string message, int level);

    // 16. Method with attribute
    [Obsolete("Use NewMethod instead")]
    void OldMethod();

    // 17. Method with multiple modifiers
    void MixModifiers(in int input, out int output, ref int temp);
}

/*
[GenerateImposter(typeof(IOrderApiService))]
*/
public class T
{
    
}