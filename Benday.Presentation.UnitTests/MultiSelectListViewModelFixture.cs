using System;

namespace Benday.Presentation.UnitTests;

public class MultiSelectListViewModelFixture
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
    public void MultiSelectListViewModel_SelectedItem()
    {
        var values = CreateValues();

        var instance = new MultiSelectListViewModel(values);

        Assert.Null(instance.SelectedItem);
        Assert.NotNull(instance.SelectedItems);
        Assert.Empty(instance.SelectedItems);

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

        // select a second item that is in the collection after values[2]
        values[3].IsSelected = true;
        Assert.Same(expectedSelectedItem, instance.SelectedItem);
    }

    [Fact]
    public void MultiSelectListViewModel_SelectedItems()
    {
        var values = CreateValues();

        var instance = new MultiSelectListViewModel(values);

        Assert.Null(instance.SelectedItem);
        Assert.NotNull(instance.SelectedItems);
        Assert.Empty(instance.SelectedItems);


        ISelectableItem expectedSelectedItem0;
        ISelectableItem expectedSelectedItem1;
        ISelectableItem actualSelectedItem0;
        ISelectableItem actualSelectedItem1;

        // select an item
        expectedSelectedItem0 = values[2];
        expectedSelectedItem0.IsSelected = true;

        Assert.NotNull(instance.SelectedItems);
        Assert.Single(instance.SelectedItems);


        expectedSelectedItem1 = values[3];
        expectedSelectedItem1.IsSelected = true;

        Assert.NotNull(instance.SelectedItems);
        Assert.Equal(2, instance.SelectedItems.Count);


        var actualSelectedItems = instance.SelectedItems;

        actualSelectedItem0 = actualSelectedItems[0];
        actualSelectedItem1 = actualSelectedItems[1];

        Assert.Same(expectedSelectedItem0, actualSelectedItem0);
        Assert.Same(expectedSelectedItem1, actualSelectedItem1);
    }
}
