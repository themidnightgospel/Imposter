using System.Collections.Generic;
using Moq;
using Shouldly;
using Xunit;

namespace Imposter.Playground;

public class MoqBehaviour
{
    [Fact]
    public void MethodSetup_OnlyLastMatchingSetupIsCalled()
    {
        var mock = new Mock<IOrderService>();
        var invokedCallbacks = new List<int>();

        mock
            .Setup(it => it.PlaceOrder(It.Is<int>(i => i > 0)))
            .Callback((int i) => { invokedCallbacks.Add(i); })
            .Returns(11);

        mock
            .Setup(it => it.PlaceOrder(It.Is<int>(i => i > 1)))
            .Callback((int i) => { invokedCallbacks.Add(i); });

        mock
            .Setup(it => it.PlaceOrder(It.Is<int>(i => i > 2)))
            .Callback((int i) => { invokedCallbacks.Add(i); })
            .Returns(12);

        mock.Object.PlaceOrder(3).ShouldBe(12);

        invokedCallbacks.Count.ShouldBe(3);
    }

    [Fact]
    public void PropertySetupSetup_OnlyLastMatchingSetupIsCalled()
    {
        var mock = new Mock<IOrderService>();
        var setterCallbacks = new List<int>();

        // Setup multiple setter callbacks with different criteria
        mock
            .SetupSet(it => it.Age = It.Is<int>(i => i > 0))
            .Callback<int>(value => setterCallbacks.Add(value));

        mock
            .SetupSet(it => it.Age = It.Is<int>(i => i > 10))
            .Callback<int>(value => setterCallbacks.Add(value));

        mock
            .SetupSet(it => it.Age = It.Is<int>(i => i > 20))
            .Callback<int>(value => setterCallbacks.Add(value));

        mock.Object.Age = 25;

        setterCallbacks.Count.ShouldBe(1);
        setterCallbacks[0].ShouldBe(25);
    }

    public interface IOrderService
    {
        int PlaceOrder(int orderId);

        int Age { get; set; }
    }
}