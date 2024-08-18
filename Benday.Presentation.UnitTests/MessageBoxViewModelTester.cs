using System;
using FluentAssertions; 

namespace Benday.Presentation.UnitTests;

public class MessageBoxViewModelTester : NotifyPropertyChangedTester
{
    public MessageBoxViewModelTester(IViewModelBase viewModel) : base(viewModel)
    {
        ViewModelInstance = viewModel;

        ViewModelInstance.MessageBoxRequested +=
            new MessageBoxEventHandler(_ViewModelInstance_MessageBoxRequested);
    }

    public bool WasMessageBoxRequested { get; set; }

    public MessageBoxEventArgs? LastEventArgs { get; set; }

    private void _ViewModelInstance_MessageBoxRequested(object sender, MessageBoxEventArgs args)
    {
        WasMessageBoxRequested = true;

        LastEventArgs = args;
    }

    public IViewModelBase ViewModelInstance { get; }

    public void AssertException<T>() where T : Exception
    {
        LastEventArgs.Should().NotBeNull("LastEventArgs was null.");
        LastEventArgs?.Exception.Should().NotBeNull("Did not receive an exception.");
        LastEventArgs?.Exception.Should().BeOfType<T>("Exception was not the expected type.");
    }

    public void AssertMessage(string expectedMessage)
    {
        LastEventArgs.Should().NotBeNull("LastEventArgs was null.");
        LastEventArgs?.Message.Should().NotBeNull("Did not receive a message.");
        LastEventArgs?.Message.Should().Be(expectedMessage, "Message was not correct.");
    }

    public void AssertMessage(string expectedMessage, bool expectedIsUnexpectedException)
    {
        AssertMessage(expectedMessage);
        LastEventArgs?.IsUnexpectedException.Should().Be(expectedIsUnexpectedException, "IsUnexpectedException was wrong.");
    }
}
