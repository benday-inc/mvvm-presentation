using FluentAssertions;
using System;
using System.ComponentModel;

namespace Benday.Presentation.UnitTests;

public class ProgressBarViewModelTester : NotifyPropertyChangedTester
{
    public ProgressBarViewModelTester(IProgressBarViewModel viewModel) : base(viewModel)
    {
        ViewModelInstance = viewModel;

        IsVisibleValues = new List<bool>();
        MessageValues = new List<string>();
    }

    public IProgressBarViewModel ViewModelInstance { get; }

    protected override void OnPropertyChangedEvent(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsProgressBarVisible" || e.PropertyName == "ProgressBarMessage")
        {
            base.OnPropertyChangedEvent(sender, e);

            if (e.PropertyName == "IsProgressBarVisible")
            {
                IsVisibleValues.Add(ViewModelInstance.IsProgressBarVisible);
            }
            else if (e.PropertyName == "ProgressBarMessage")
            {
                MessageValues.Add(ViewModelInstance.ProgressBarMessage);
            }
        }
    }

    public List<bool> IsVisibleValues { get; private set; }

    public List<string> MessageValues { get; private set; }

    public void AssertProgressBarWasVisibleAndEndedInvisible()
    {
        IsVisibleValues.Should().HaveCountGreaterOrEqualTo(2)
            .And.Contain(true, "Progress bar was never visible.")
            .And.ContainItemsAssignableTo<bool>();

        IsVisibleValues[^1].Should().BeFalse("Did not end with progress bar not visible.");
    }

    public void AssertMessage(string expectedMessage)
    {
        MessageValues.Should().Contain(expectedMessage, "Never got the message '{0}'.", expectedMessage);
    }
}
