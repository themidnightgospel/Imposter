using System;
using System.Collections.Generic;
using Moq;
using NSubstitute;
using NSubstitute.Exceptions;
using Shouldly;
using Xunit;

namespace Imposter.Playground;

public class MoqBehaviour
{
    public class TestClas
    {
        public virtual int VirtualMethod()
        {
            Console.WriteLine("Test");
            return 1;
        }
    }

    public class MyClass
    {
        public virtual string this[int i]
        {
            get => $"Value {i}";
            set { Console.WriteLine($"Set[{i}] = {value}"); }
        }
    }

    [Fact]
    public void IndexerCallbase()
    {
        var mock = new Mock<MyClass> { CallBase = true };
        mock.Object[5].ShouldBe("Value 5");
    }

    [Fact]
    public void Callback()
    {
        var mock1 = new Mock<TestClas>()
        {
            CallBase = true
        };

        mock1.Object.VirtualMethod().ShouldBe(1);

        var mock2 = new Mock<TestClas>()
        {
            CallBase = true
        };

        mock2.Setup(s => s.VirtualMethod()).Returns(33);
        mock2.Object.VirtualMethod().ShouldBe(33);
    }

    [Fact]
    public void InOrderVerification()
    {
        var service = Substitute.For<IOrderService>();

// Act
        service.RemoveOrder(1);
        service.PlaceOrder(1);

// Assert
        Received.InOrder(() =>
        {
            service.RemoveOrder(1);
            service.PlaceOrder(1);
        });

        Should.Throw<CallSequenceNotFoundException>(() => Received.InOrder(() =>
        {
            service.PlaceOrder(1);
            service.RemoveOrder(1);
        }));
    }

    [Fact]
    public void StrictMode()
    {
        var mock = new Mock<IOrderService>(MockBehavior.Strict);

        mock.Setup(s => s.PlaceOrder(It.IsAny<int>())).Returns(42);

        var result = mock.Object.PlaceOrder(12);
        result.ShouldBe(42);

        // Callback doesn't count
        mock.Setup(s => s.RemoveOrder(It.IsAny<int>()))
            .Callback((int a) => { });

        Should.Throw<MockException>(() => mock.Object.RemoveOrder(231));

        Should.Throw<MockException>(() => mock.Object.Age);

        mock.SetupGet(it => it.Age).Returns(11);
        Should.NotThrow(() => mock.Object.Age);

        Should.Throw<MockException>(() => mock.Object.Age = 11);

        mock.SetupSet(it => it.Age = It.IsAny<int>());
        Should.NotThrow(() => mock.Object.Age = 11);
    }

    //[Fact]
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

    //[Fact]
    public void MethodSetup_MultiCallbacks_OnlyLastMatchingSetupIsCalled()
    {
        var mock = new Mock<IOrderService>();
        var invokedCallbacks = new List<int>();

        mock
            .Setup(it => it.PlaceOrder(It.Is<int>(i => i > 2)))
            .Callback((int i) => { invokedCallbacks.Add(i); })
            .Returns(12)
            .Callback((int i) => { invokedCallbacks.Add(i); })
            .Throws(new Exception());

//        mock.Object.PlaceOrder(3).ShouldBe(12);
        Should.Throw<Exception>(() => mock.Object.PlaceOrder(3));

        invokedCallbacks.Count.ShouldBe(3);
    }

    [Fact]
    public void MethodSetup_OnlyLastMatchingSetupIsCalled2()
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

        mock
            .Setup(it => it.PlaceOrder(It.Is<int>(i => i == 3)))
            .Callback((int i) => { invokedCallbacks.Add(i); });

        mock.Object.PlaceOrder(3).ShouldBe(0);

        // So it invoked last matching setup, even when it had no "Returns" on it
        invokedCallbacks.Count.ShouldBe(1);
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

        int RemoveOrder(int orderId);

        int Age { get; set; }
    }
}