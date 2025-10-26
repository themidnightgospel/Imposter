using System.Threading;
using System.Threading.Tasks;
using Imposter.Abstractions;
using Shouldly;
using Xunit;

namespace Imposter.CodeGenerator.Tests.Features.PropertySetup;

public class ThreadSafetyTests
{
    private readonly IPropertySetupSutImposter _sut = new IPropertySetupSutImposter();

    [Fact]
    public void GivenConcurrentOperations_WhenMixingGettersAndSetters_ShouldNotThrowExceptions()
    {
        var threads = new Thread[100];

        // Mix of getters and setters
        for (int i = 0; i < 100; i++)
        {
            var index = i;
            if (index % 2 == 0)
            {
                threads[i] = new Thread(() => _sut.Instance().Age = index);
            }
            else
            {
                threads[i] = new Thread(() =>
                {
                    var _ = _sut.Instance().Age;
                });
            }
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        // Should not throw - just testing for thread safety
        Should.NotThrow(() => _sut.Age.GetterCalled(Count.Exactly(50)));
        Should.NotThrow(() => _sut.Age.GetterCalled(Count.Exactly(50)));
    }

    [Fact]
    public async Task GivenSequentialReturnsSetup_WhenAccessedConcurrently_ShouldWorkCorrectly()
    {
        // Setup many sequential returns
        for (int i = 0; i < 100; i++)
        {
            _sut.Age.Returns(i);
        }

        var results = new int[100];
        var threads = new Thread[100];

        for (int i = 0; i < 100; i++)
        {
            var index = i;
            threads[i] = new Thread(() => results[index] = _sut.Instance().Age);
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        // All results should be unique values from 0-99 (in some order)
        results.ShouldBeUnique();
        results.ShouldAllBe(x => x >= 0 && x < 100);
    }
}