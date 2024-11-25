using System;
using System.Linq;

namespace Benday.Presentation.UnitTests;

public class FilterableListViewModelFixture
{
    private FilterableListViewModel<SelectableItem>? _SystemUnderTest;

    public FilterableListViewModel<SelectableItem> SystemUnderTest
    {
        get
        {
            if (_SystemUnderTest == null)
            {
                _SystemUnderTest = new FilterableListViewModel<SelectableItem>();

                _SystemUnderTest.Initialize(CreateValues());
            }

            return _SystemUnderTest;
        }
    }

    private List<SelectableItem> CreateValues()
    {
        var values = new List<SelectableItem>();

        values.Add(new SelectableItem(false, "blue"));
        values.Add(new SelectableItem(false, "dark blue"));
        values.Add(new SelectableItem(false, "wild blueberry"));
        values.Add(new SelectableItem(false, "pumpkin"));
        values.Add(new SelectableItem(false, "mangosteen"));
        values.Add(new SelectableItem(false, "coffee"));
        values.Add(new SelectableItem(false, "peanut"));
        values.Add(new SelectableItem(false, "The thingy"));
        values.Add(new SelectableItem(false, "the stuff"));
        values.Add(new SelectableItem(false, "test THE code"));

        return values;
    }

    private List<SelectableItem> CreateAlternateSetOfValues()
    {
        var values = new List<SelectableItem>();

        values.Add(new SelectableItem(false, "qwer"));
        values.Add(new SelectableItem(false, "zxcv"));
        values.Add(new SelectableItem(false, "asdf"));

        return values;
    }

    [Fact]
    public void SetFilterToItemThatExists()
    {
        var originalItemCount = SystemUnderTest.Items.Count;

        var filterValue = "blue";

        SystemUnderTest.Filter = filterValue;

        Assert.NotEqual(originalItemCount, SystemUnderTest.Count);
        Assert.Equal(3, SystemUnderTest.Items.Count);

        foreach (var item in SystemUnderTest.Items)
        {
            Assert.Contains(filterValue, item.Text);
        }
    }

    [Fact]
    public void FilterIsCaseInsensitive()
    {
        var originalItemCount = SystemUnderTest.Items.Count;

        var filterValue = "the";

        SystemUnderTest.Filter = filterValue;

        Assert.NotEqual(originalItemCount, SystemUnderTest.Count);
        Assert.Equal(3, SystemUnderTest.Items.Count);

        foreach (var item in SystemUnderTest.Items)
        {
            Assert.Contains(filterValue, item.Text, StringComparison.OrdinalIgnoreCase);
        }
    }

    [Fact]
    public void ClearingFilterReturnsItemsToOriginalList()
    {
        var originalItemCount = SystemUnderTest.Items.Count;

        var filterValue = "the";

        SystemUnderTest.Filter = filterValue;

        Assert.NotEqual(originalItemCount, SystemUnderTest.Count);

        SystemUnderTest.Filter = string.Empty;

        Assert.Equal(originalItemCount, SystemUnderTest.Count);
    }

    [Fact]
    public void ChangeFilterOnAlreadyFilteredList()
    {
        SystemUnderTest.Filter = "the";

        SystemUnderTest.Filter = "ee";

        Assert.Equal(2, SystemUnderTest.Count);
    }

    [Fact]
    public void ReinitializingWithNewValuesResetsValuesAndFilter()
    {
        SystemUnderTest.Filter = "the";

        var newValues = CreateAlternateSetOfValues();

        SystemUnderTest.Initialize(newValues);

        Assert.Equal(string.Empty, SystemUnderTest.Filter);
        Assert.Equal(newValues.Count, SystemUnderTest.Items.Count);
    }

    [Fact]
    public void InitializeFromCollectionIsCalledThereShouldNotBeASelectedItem()
    {
        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        Assert.Null(SystemUnderTest.SelectedItem);
    }

    [Fact]
    public void SelectedItemPropertyShouldBePopulatedWhenIsSelectedIsSetOnAnItem()
    {
        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var expected = values[3];
        expected.IsSelected = true;

        var actual = SystemUnderTest.SelectedItem;

        Assert.NotNull(actual);
        Assert.Same(expected, actual);
    }

    [Fact]
    public void SelectedItemPropertyBeNullWhenNoSelectedItems()
    {
        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var expected = values[3];
        expected.IsSelected = true;

        // unselect the item
        expected.IsSelected = false;

        var actual = SystemUnderTest.SelectedItem;

        Assert.Null(actual);
    }

    [Fact]
    public void WhenSelectedItemIsSetViaAssignmentThenIsSelectedShouldBeSetToTrueOnItem()
    {
        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var expected = values[3];

        SystemUnderTest.SelectedItem = expected;

        Assert.True(expected.IsSelected);
    }

    [Fact]
    public void WhenInMultiSelectModeSelectedItemIsSetViaAssignmentThenIsSelectedShouldBeSetToTrueOnItem()
    {
        SystemUnderTest.AllowMultipleSelections = true;

        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var item0 = values[3];
        var item1 = values[5];

        SystemUnderTest.SelectedItem = item0;
        SystemUnderTest.SelectedItem = item1;

        Assert.True(item0.IsSelected);
        Assert.True(item1.IsSelected);
    }

    [Fact]
    public void WhenSelectedItemIsSetViaAssignmentToNullThenIsSelectedShouldBeSetToFalseOnItem()
    {
        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var expected = values[3];
        SystemUnderTest.SelectedItem = expected;

        // unselect the item via assignment
        SystemUnderTest.SelectedItem = null;

        Assert.False(expected.IsSelected);
    }

    [Fact]
    public void InitializedToNotAllowMultipleSelectionsByDefault()
    {
        Assert.False(SystemUnderTest.AllowMultipleSelections);
    }

    [Fact]
    public void WhenInMultiSelectModeThenMultipleItemsCanBeSelected()
    {
        SystemUnderTest.AllowMultipleSelections = true;

        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var item0 = values[3];
        var item1 = values[5];

        item0.IsSelected = true;
        item1.IsSelected = true;

        Assert.True(item0.IsSelected);
        Assert.True(item1.IsSelected);
    }

    [Fact]
    public void WhenInSingleSelectModeThenMultipleItemsCannotBeSelected()
    {
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var item0 = values[3];
        var item1 = values[5];

        item0.IsSelected = true;
        item1.IsSelected = true;

        Assert.Same(item1, SystemUnderTest.SelectedItem);
        Assert.False(item0.IsSelected);
        Assert.True(item1.IsSelected);
    }
}
