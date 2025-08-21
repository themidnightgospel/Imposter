using Moq;
using Shouldly;

namespace Imposter.Tests;

public interface IPlaygroundService
{
    string GetData(int id);
}

public class Playground
{
    [Fact]
    public void AfterCallbackThrowsException_InvocationIsStillRecorded()
    {
        var mock = new Mock<IPlaygroundService>();
        mock.Setup(m => m.GetData(It.IsAny<int>()))
            .Returns("success")
            .Callback(() => throw new InvalidOperationException("Callback failed!"));

        try
        {
            var result = mock.Object.GetData(1); // Returns "success", then throws    
        }
        catch (Exception ex)
        {
            
        }
        
        mock.Verify(it => it.GetData(It.IsAny<int>()), Times.Exactly(1));
    }
    
    [Fact]
    public void BeforeCallbackThrowsException_InvocationIsStillRecorded()
    {
        var mock = new Mock<IPlaygroundService>();
        mock.Setup(m => m.GetData(It.IsAny<int>()))
            .Callback(() => throw new InvalidOperationException("Callback failed!"))
            .Returns("success");

        try
        {
            var result = mock.Object.GetData(1); // Returns "success", then throws    
        }
        catch (Exception ex)
        {
            
        }
        
        mock.Verify(it => it.GetData(It.IsAny<int>()), Times.Exactly(1));
    }
    
    [Fact]
    public void ThrowingExceptionIsSetup_CallbackIsInvokedAndInvocationIsStillRecorded()
    {
        var beforeCallbackInvoked = false;
        var mock = new Mock<IPlaygroundService>();
        mock.Setup(m => m.GetData(It.IsAny<int>()))
            .Callback(() => beforeCallbackInvoked = true)
            .Throws<InvalidOperationException>();

        try
        {
            var result = mock.Object.GetData(1); // Returns "success", then throws    
        }
        catch (Exception ex)
        {
            
        }
        
        beforeCallbackInvoked.ShouldBeTrue();
        mock.Verify(it => it.GetData(It.IsAny<int>()), Times.Exactly(1));
    }}