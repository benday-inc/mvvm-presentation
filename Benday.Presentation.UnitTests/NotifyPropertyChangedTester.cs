using FluentAssertions;
using System;
using System.ComponentModel;

namespace Benday.Presentation.UnitTests;

public class NotifyPropertyChangedTester
{
    public NotifyPropertyChangedTester(INotifyPropertyChanged viewModel)
    {
        if (viewModel == null)
        {
            throw new ArgumentNullException(nameof(viewModel), "Argument cannot be null.");
        }

        Changes = new List<string>();

        viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    protected virtual void OnPropertyChangedEvent(object? sender, PropertyChangedEventArgs e)
    {
        Changes.Add($"{e.PropertyName}");
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChangedEvent(sender, e);
    }

    public List<string> Changes { get; private set; }

    public void AssertChange(int changeIndex, string expectedPropertyName)
    {
        Changes.Should().NotBeNull("Changes collection was null.");

        Changes.Should().HaveCountGreaterThan(changeIndex, $"Changes collection contains '{Changes.Count}' items and does not have an element at index '{changeIndex}'.");

        Changes[changeIndex].Should().Be(expectedPropertyName, $"Change at index '{changeIndex}' is '{Changes[changeIndex]}' and is not equal to '{expectedPropertyName}'.");
    }

    public void AssertChange(string expectedPropertyName)
    {
        Changes.Should().NotBeNull("Changes collection was null.");

        Changes.Should().Contain(expectedPropertyName, $"Changes collection does not contain a change for a property named '{expectedPropertyName}'.");
    }
}
