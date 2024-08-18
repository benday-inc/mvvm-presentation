using System;

namespace Benday.Presentation.UnitTests;

public class NavigatableListFixture
{
    public NavigatableListFixture()
    {
        _SystemUnderTest = null;
    }

    private NavigatableList<string>? _SystemUnderTest;

    public NavigatableList<string> SystemUnderTest
    {
        get
        {
            if (_SystemUnderTest == null)
            {
                _SystemUnderTest = new NavigatableList<string>();

                _SystemUnderTest.Add("Value 0");
                _SystemUnderTest.Add("Value 1");
                _SystemUnderTest.Add("Value 2");
                _SystemUnderTest.Add("Value 3");
                _SystemUnderTest.Add("Value 4");
            }

            return _SystemUnderTest;
        }
    }

    [Fact]
    public void WhenConstructedIsAtFirstIsTrue()
    {
        Assert.True(SystemUnderTest.IsAtFirst);
    }

    [Fact]
    public void WhenConstructedValueIsFirstItem()
    {
        Assert.Equal("Value 0", SystemUnderTest.Value);
    }

    [Fact]
    public void WhenConstructedCurrentIndexIsZero()
    {
        Assert.Equal(0, SystemUnderTest.CurrentIndex);
    }

    [Fact]
    public void WhenAtSecondValueIsAtFirstIsFalse()
    {
        SystemUnderTest.Next();

        Assert.False(SystemUnderTest.IsAtFirst);
    }

    [Fact]
    public void WhenNavigatedToFirstItemUsingPreviousIsAtFirstIsTrue()
    {
        SystemUnderTest.Next();

        SystemUnderTest.Previous();

        Assert.True(SystemUnderTest.IsAtFirst);
    }

    [Fact]
    public void WhenNotAtFirstIsAtFirstIsFalse()
    {
        SystemUnderTest.Next();

        Assert.False(SystemUnderTest.IsAtFirst);
    }

    [Fact]
    public void WhenNotAtLastIsAtLastIsFalse()
    {
        SystemUnderTest.Next();

        Assert.False(SystemUnderTest.IsAtLast);
    }

    [Fact]
    public void WhenNextIsCalledTheSecondValueIsCurrent()
    {
        SystemUnderTest.Next();

        Assert.Equal(1, SystemUnderTest.CurrentIndex);
        Assert.Equal("Value 1", SystemUnderTest.Value);
    }

    [Fact]
    public void WhenIsAtFirstMovePreviousDoesNothing()
    {
        Assert.True(SystemUnderTest.IsAtFirst);

        SystemUnderTest.Previous();

        Assert.True(SystemUnderTest.IsAtFirst);
    }

    [Fact]
    public void WhenMoveToLastIsCalledThePositionIsSetToLast()
    {
        SystemUnderTest.MoveToLast();

        Assert.True(SystemUnderTest.IsAtLast);
        Assert.Equal(4, SystemUnderTest.CurrentIndex);
    }

    [Fact]
    public void WhenMoveToFirstIsCalledThePositionIsSetToFirst()
    {
        SystemUnderTest.Next();
        SystemUnderTest.Next();
        SystemUnderTest.Next();

        SystemUnderTest.MoveToFirst();

        Assert.True(SystemUnderTest.IsAtFirst);
        Assert.Equal(0, SystemUnderTest.CurrentIndex);
    }

    [Fact]
    public void WhenIsAtLastMoveNextDoesNothing()
    {
        SystemUnderTest.MoveToLast();

        SystemUnderTest.Next();

        Assert.True(SystemUnderTest.IsAtLast);
    }

    [Fact]
    public void WhenThereAreNoItemsCurrentIndexIsNegativeOne()
    {
        SystemUnderTest.Clear();

        Assert.Equal(-1, SystemUnderTest.CurrentIndex);
    }

    [Fact]
    public void WhenThereAreNoItemsValueReturnsNull()
    {
        SystemUnderTest.Clear();

        Assert.Null(SystemUnderTest.Value);
    }
}
