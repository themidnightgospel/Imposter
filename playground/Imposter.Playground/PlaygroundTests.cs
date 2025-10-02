using Moq;
using Shouldly;
using Xunit;

public class PlaygroundTests
{
    [Fact]
    public void Test1()
    {
        var mq = new Mock<IService>();
        bool callback1 = false;
        bool callback2 = false;

        mq
            .Setup(it => it.Print<int>(It.IsAny<int>()))
            .Callback(() => callback2 = true);

        mq
            .Setup(it => it.Print<It.IsAnyType>(It.IsAny<It.IsAnyType>()))
            .Callback(() => callback1 = true);

        mq.Object.Print(1);

        callback1.ShouldBeTrue();
        mq.Verify(it => it.Print<It.IsAnyType>(It.IsAny<It.IsAnyType>()), Times.Exactly(1));
    }

    [Fact]
    public void Test2()
    {
        var mq = new Mock<IService>();
        bool callback1 = false;
        bool callback2 = false;

        mq
            .Setup(it => it.Print<It.IsAnyType>(It.IsAny<It.IsAnyType>()))
            .Callback(() => callback1 = true);

        mq
            .Setup(it => it.Print<int>(It.IsAny<int>()))
            .Callback(() => callback2 = true);

        mq.Object.Print(1);

        callback2.ShouldBeTrue();

        mq.Verify(it => it.Print<It.IsAnyType>(It.IsAny<It.IsAnyType>()), Times.Exactly(1));
    }

    [Fact]
    public void Test3()
    {
        var mq = new Mock<IService>();
        bool callback1 = false;
        bool callback2 = false;

        mq
            .Setup(it => it.Print<It.IsAnyType>(It.IsAny<It.IsAnyType>()))
            .Callback(() => callback1 = true);

        mq
            .Setup(it => it.Print<int>(It.IsAny<int>()))
            .Callback(() => callback2 = true);

        mq.Object.Print(1);
        mq.Object.Print("something");

        callback1.ShouldBeTrue();
        callback2.ShouldBeTrue();

        mq.Verify(it => it.Print(It.IsAny<int>()), Times.Exactly(1));
        mq.Verify(it => it.Print(It.IsAny<string>()), Times.Exactly(1));
        mq.Verify(it => it.Print(It.IsAny<It.IsAnyType>()), Times.Exactly(2));
    }

    [Fact]
    public void Test4()
    {
        var mq = new Mock<IService>();
        bool callback1 = false;
        bool callback2 = false;

        mq
            .Setup(it => it.Print<int>(It.IsAny<int>()))
            .Callback(() => callback2 = true);


        mq.Object.Print(1);

        mq
            .Setup(it => it.Print<int>(It.IsAny<int>()))
            .Callback(() => callback1 = true);

        mq.Object.Print(1);

        callback1.ShouldBeTrue();
        mq.Verify(it => it.Print<It.IsAnyType>(It.IsAny<It.IsAnyType>()), Times.Exactly(1));
    }


    [Fact]
    public void Test5()
    {
        var mq = new Mock<IService>();

        mq
            .Setup(it => it.Print2<int>(It.Is<int>(i => i == 1)))
            .Returns(1);

        mq
            .Setup(it => it.Print2<int>(It.Is<int>(i => i == 10)))
            .Returns(2);

        mq
            .Setup(it => it.Print2<int>(It.Is<int>(i => i > 8)))
            .Returns(3);

        mq.Object.Print2(1).ShouldBe(1);
        mq.Object.Print2(10).ShouldBe(3);
        mq.Object.Print2(9).ShouldBe(3);
    }
}

public interface IService
{
    void Print<T>(T value);

    int Print2<T>(T value);
}