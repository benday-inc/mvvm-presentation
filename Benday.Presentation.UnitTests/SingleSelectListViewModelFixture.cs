using System;

namespace Benday.Presentation.UnitTests;

public class SingleSelectListViewModelFixture
{
    private List<ISelectableItem> CreateValues()
    {
        var values = new List<ISelectableItem>();

        for (var i = 0; i < 10; i++)
        {
            ISelectableItem temp = new SelectableItem();

            temp.IsSelected = false;
            temp.Text = $"text_{i}";
            temp.Value = $"value_{i}";

            values.Add(temp);
        }

        return values;
    }

    [Fact]
    public void SingleSelectListViewModel_SelectedItem()
    {
        var values = CreateValues();

        var instance = new SingleSelectListViewModel(values);

        Assert.Null(instance.SelectedItem);

        ISelectableItem expectedSelectedItem;
        ISelectableItem? actualSelectedItem;

        // select an item
        expectedSelectedItem = values[3];
        expectedSelectedItem.IsSelected = true;
        actualSelectedItem = instance.SelectedItem;
        Assert.NotNull(actualSelectedItem);
        Assert.Same(expectedSelectedItem, actualSelectedItem);

        // unselect the item
        expectedSelectedItem.IsSelected = false;
        Assert.Null(instance.SelectedItem);

        // select a different item
        expectedSelectedItem = values[2];
        expectedSelectedItem.IsSelected = true;
        actualSelectedItem = instance.SelectedItem;
        Assert.NotNull(actualSelectedItem);
        Assert.Same(expectedSelectedItem, actualSelectedItem);
    }

    [Fact]
    public void SingleSelectListViewModel_SelectedItem_CallSetterSetsIsSelectedToTrueOnSelectedItem()
    {
        var values = CreateValues();

        var instance = new SingleSelectListViewModel(values);

        Assert.Null(instance.SelectedItem);

        var makeThisTheSelectedItem = values[0];

        Assert.False(makeThisTheSelectedItem.IsSelected);

        instance.SelectedItem = makeThisTheSelectedItem;

        Assert.True(makeThisTheSelectedItem.IsSelected);
    }

    [Fact]
    public void SingleSelectListViewModel_WhenSelectedItemChanges_OldSelectedItemShouldBeChangedToIsSelectedFalse()
    {
        var values = CreateValues();

        var instance = new SingleSelectListViewModel(values);

        // make sure that there is a selected item
        var originalSelectedItem = values[0];
        instance.SelectedItem = originalSelectedItem;
        Assert.True(originalSelectedItem.IsSelected);

        // get the item that's going to be the new selected item
        var newSelectedItem = values[1];
        Assert.False(newSelectedItem.IsSelected);

        // change the selected item
        instance.SelectedItem = newSelectedItem;
        Assert.True(newSelectedItem.IsSelected);
        Assert.False(originalSelectedItem.IsSelected);
    }

    [Fact]
    public void SingleSelectListViewModel_WhenSelectedItemChangesToNull_OldSelectedItemShouldBeChangedToIsSelectedFalse()
    {
        var values = CreateValues();

        var instance = new SingleSelectListViewModel(values);

        // make sure that there is a selected item
        var originalSelectedItem = values[0];
        instance.SelectedItem = originalSelectedItem;

        // set selecteditem to null
        instance.SelectedItem = null;
        Assert.Null(instance.SelectedItem);

        Assert.False(originalSelectedItem.IsSelected);
    }

    [Fact]
    public void SingleSelectListViewModel_SelectItemByText_OldSelectedItemShouldBeChangedToIsSelectedFalse()
    {
        var values = CreateValues();

        var instance = new SingleSelectListViewModel(values);

        // make sure that there is a selected item
        var originalSelectedItem = values[0];
        instance.SelectedItem = originalSelectedItem;
        Assert.True(originalSelectedItem.IsSelected);

        // get the item that's going to be the new selected item
        var newSelectedItem = values[1];
        Assert.False(newSelectedItem.IsSelected);

        // change the selected item
        instance.SelectByText(newSelectedItem.Text);

        Assert.True(newSelectedItem.IsSelected);
        Assert.False(originalSelectedItem.IsSelected);
        Assert.Same(newSelectedItem, instance.SelectedItem);
    }
}
