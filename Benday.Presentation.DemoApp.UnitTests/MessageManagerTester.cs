using Benday.Presentation;

namespace Benday.ControlsAndViewModelSample.UnitTests;

public class MessageManagerTester : IMessageManager
{
    public bool WasShowMessageExceptionCalled { get; set; }
    public Exception? ShowMessageExceptionArgument { get; set; }

    public Task ShowMessage(Exception ex)
    {
        WasShowMessageExceptionCalled = true;
        ShowMessageExceptionArgument = ex;
        return Task.CompletedTask;
    }

    public bool WasShowMessageStringStringCalled { get; set; }
    public string? ShowMessageStringStringMessageArgument { get; set; }
    public string? ShowMessageStringStringTitleArgument { get; set; }

    public Task ShowMessage(string message, string title)
    {
        WasShowMessageStringStringCalled = true;
        ShowMessageStringStringMessageArgument = message;
        ShowMessageStringStringTitleArgument = title;
        return Task.CompletedTask;
    }
    public void AssertShowMessageString()
    {
        Assert.True(WasShowMessageStringStringCalled, "ShowMessage(string, string) was not called.");

        var wasMessageArgumentNull = String.IsNullOrWhiteSpace(ShowMessageStringStringMessageArgument);
        var wasTitleArgumentNull = String.IsNullOrWhiteSpace(ShowMessageStringStringTitleArgument);

        Assert.False(wasMessageArgumentNull, "Message argument was null or empty.");
        Assert.False(wasTitleArgumentNull, "Title argument was null or empty.");
    }

    public void AssertShowMessageException()
    {
        Assert.True(WasShowMessageExceptionCalled, "ShowMessage(Exception) was not called.");

        Assert.NotNull(ShowMessageExceptionArgument);
    }    
}
