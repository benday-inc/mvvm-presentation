
using FluentAssertions;

namespace Benday.Presentation.UnitTests;

public class SelectableCollectionViewModelWithISelectableFixture
{
    public SelectableCollectionViewModelWithISelectableFixture()
    {
        _SystemUnderTest = null;
        OnSelectedItemEventHandlerCallCount = 0;
    }

    public int OnSelectedItemEventHandlerCallCount { get; set; }

    private SelectableCollectionViewModel<ClassThatImplementsISelectable>? _SystemUnderTest;

    public SelectableCollectionViewModel<ClassThatImplementsISelectable> SystemUnderTest
    {
        get
        {
            if(_SystemUnderTest == null)
            {
                _SystemUnderTest = new SelectableCollectionViewModel<ClassThatImplementsISelectable>();
            }

            return _SystemUnderTest;
        }
    }
        

    private List<ClassThatImplementsISelectable> CreateValues()
    {
        var values = new List<ClassThatImplementsISelectable>();

        for(var i = 0; i < 10; i++)
        {
            var temp = new ClassThatImplementsISelectable();

            temp.IsSelected = false;
            temp.Text = $"text_{i}";
            temp.Value = $"value_{i}";

            values.Add(temp);
        }

        return values;
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
    public void SingleSelectMode_OnlyOneSelectedItem_IsSelected_Assignment()
    {
        // arrange
        var values = CreateValues();
        SystemUnderTest.Initialize(values);
        var itemCount = values.Count;

        // act & assert

        Assert.True(itemCount > 0);

        for (int i = 0; i < itemCount; i++)
        {
            var item = SystemUnderTest.Items[i];

            item.IsSelected = true;

            Assert.True(item.IsSelected);

            for (int j = 0; j < itemCount; j++)
            {
                if (j == i)
                {
                    continue;
                }
                var otherItem = SystemUnderTest.Items[j];
                Assert.False(otherItem.IsSelected);
            }
        }
    }

    [Fact]
    public void SingleSelectMode_OnlyOneSelectedItem_SelectedItem_Assignment()
    {
        // arrange
        var values = CreateValues();
        SystemUnderTest.Initialize(values);
        var itemCount = values.Count;

        // act & assert

        Assert.True(itemCount > 0);

        for (int i = 0; i < itemCount; i++)
        {
            var item = SystemUnderTest.Items[i];

            SystemUnderTest.SelectedItem = item;

            Assert.True(item.IsSelected);

            for (int j = 0; j < itemCount; j++)
            {
                if (j == i)
                {
                    continue;
                }
                var otherItem = SystemUnderTest.Items[j];
                Assert.False(otherItem.IsSelected);
            }
        }
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
    public void WhenSelectedItemIsSetViaAssignmentToNullThenIsSelectedShouldBeSetToFalseOnAllItems()
    {
        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        // select an item
        var item0 = values[3];
        var item1 = values[5];
        var item2 = values[7];

        item0.IsSelected = true;
        item1.IsSelected = true;
        item2.IsSelected = true;

        // unselect the item via assignment
        SystemUnderTest.SelectedItem = null;

        foreach(var item in SystemUnderTest.Items)
        {
            Assert.False(item.IsSelected);
        }

        Assert.False(item0.IsSelected);
        Assert.False(item1.IsSelected);
        Assert.False(item2.IsSelected);
    }

    [Fact]
    public void InitializedToNotAllowMultipleSelectionsByDefault()
    { Assert.False(SystemUnderTest.AllowMultipleSelections); }

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

    [Fact]
    public void OnItemSelected_TriggeredIfNewSelectedItem()
    {
        // arrange
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        var item0 = SystemUnderTest.Items[0];
        var item1 = SystemUnderTest.Items[1];

        SystemUnderTest.OnItemSelected += SystemUnderTest_OnItemSelected;

        Assert.True(SystemUnderTest.HasOnItemSelectedSubscriber);

        Assert.Null(SystemUnderTest.SelectedItem);

        Assert.Equal(0, OnSelectedItemEventHandlerCallCount);

        // act
        SystemUnderTest.SelectedItem = item0;

        // assert
        Assert.NotEqual(0, OnSelectedItemEventHandlerCallCount);
    }

    [Fact]
    public void OnItemSelected_TriggeredIfNewSelectedItem_UsingItemsAdd()
    {
        // arrange
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();

        foreach (var item in values)
        {
            SystemUnderTest.Items.Add(item);
        }

        var item0 = SystemUnderTest.Items[0];
        var item1 = SystemUnderTest.Items[1];

        SystemUnderTest.OnItemSelected += SystemUnderTest_OnItemSelected;

        Assert.True(SystemUnderTest.HasOnItemSelectedSubscriber);

        Assert.Null(SystemUnderTest.SelectedItem);

        Assert.Equal(0, OnSelectedItemEventHandlerCallCount);

        // act
        SystemUnderTest.SelectedItem = item0;

        // assert
        Assert.NotEqual(0, OnSelectedItemEventHandlerCallCount);
    }

    [Fact]
    public void ItemAddedViaInitializeRespondToIsSelectedAssignment()
    {
        // arrange
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        var item0 = SystemUnderTest.Items[0];
        var item1 = SystemUnderTest.Items[1];
        var item2 = SystemUnderTest.Items[2];

        Assert.False(item1.IsSelected);

        item1.IsSelected = true;

        // act
        
        // assert

        foreach (var item in SystemUnderTest.Items)
        {
            Assert.True(item.HasPropertyChangedSubscriber);
        }

        Assert.True(item1.IsSelected);

        // assert all other items are not selected
        var otherItems = SystemUnderTest.Items.Where(i => i != item1);

        Assert.Equal(0, otherItems.Count(i => i.IsSelected));

        SystemUnderTest.SelectedItem.Should().NotBeNull();
        SystemUnderTest.SelectedItem?.Should().BeSameAs(item1);
    }

    [Fact]
    public void ItemAddedViaInsertRespondToIsSelectedAssignment()
    {
        // arrange
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();

        foreach (var item in values)
        {
            SystemUnderTest.Items.Add(item);
        }

        var item0 = SystemUnderTest.Items[0];
        var item1 = SystemUnderTest.Items[1];
        var item2 = SystemUnderTest.Items[2];

        var newItem = new ClassThatImplementsISelectable();

        Assert.False(newItem.IsSelected);

        item1.IsSelected = true;

        // act
        SystemUnderTest.Items.Insert(0, newItem);
        Assert.False(newItem.IsSelected);

        newItem.IsSelected = true;

        // assert

        foreach (var item in SystemUnderTest.Items)
        {
            Assert.True(item.HasPropertyChangedSubscriber);
        }

        Assert.True(newItem.IsSelected);

        // assert all other items are not selected
        var otherItems = SystemUnderTest.Items.Where(i => i != newItem);

        Assert.Equal(0, otherItems.Count(i => i.IsSelected));

        SystemUnderTest.SelectedItem.Should().NotBeNull();
        SystemUnderTest.SelectedItem?.Should().BeSameAs(newItem);
    }

    [Fact]
    public void ItemAddedViaAddRespondToIsSelectedAssignment()
    {
        // arrange
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();

        foreach (var item in values)
        {
            SystemUnderTest.Items.Add(item);
        }

        var item0 = SystemUnderTest.Items[0];
        var item1 = SystemUnderTest.Items[1];
        var item2 = SystemUnderTest.Items[2];

        // act
        item1.IsSelected = true;

        // assert
        Assert.True(item1.IsSelected);

        // assert all other items are not selected
        var otherItems = SystemUnderTest.Items.Where(i => i != item1);

        Assert.Equal(0, otherItems.Count(i => i.IsSelected));

        SystemUnderTest.SelectedItem.Should().NotBeNull();
        SystemUnderTest.SelectedItem?.Should().BeSameAs(item1);

        foreach (var item in SystemUnderTest.Items)
        {
            Assert.True(item.HasPropertyChangedSubscriber);
        }
    }


    [Fact]
    public void OnItemSelected_NotTriggeredIfSameItemIsSelectedAgain()
    {
        // arrange
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        var item0 = SystemUnderTest.Items[0];
        var item1 = SystemUnderTest.Items[1];

        SystemUnderTest.SelectedItem = item0;

        SystemUnderTest.OnItemSelected += SystemUnderTest_OnItemSelected;

        Assert.True(SystemUnderTest.HasOnItemSelectedSubscriber);

        Assert.Same(item0, SystemUnderTest.SelectedItem);

        Assert.Equal(0, OnSelectedItemEventHandlerCallCount);

        // act

        // reselect item 0
        SystemUnderTest.SelectedItem = item0;

        // assert
        Assert.Equal(0, OnSelectedItemEventHandlerCallCount);
    }

    [Fact]
    public void OnItemSelected_TriggeredIfDifferentItemIsSelected()
    {
        // arrange
        SystemUnderTest.AllowMultipleSelections = false;

        var values = CreateValues();
        SystemUnderTest.Initialize(values);

        var item0 = SystemUnderTest.Items[0];
        var item1 = SystemUnderTest.Items[1];

        item0.IsSelected = true;

        SystemUnderTest.OnItemSelected += SystemUnderTest_OnItemSelected;

        Assert.True(SystemUnderTest.HasOnItemSelectedSubscriber);

        Assert.Same(item0, SystemUnderTest.SelectedItem);

        Assert.Equal(0, OnSelectedItemEventHandlerCallCount);

        // act

        // select item 1
        SystemUnderTest.SelectedItem = item1;

        // assert
        Assert.NotEqual(0, OnSelectedItemEventHandlerCallCount);
        Assert.Same(item1, SystemUnderTest.SelectedItem);
    }

    private void SystemUnderTest_OnItemSelected(object? sender, EventArgs e) { OnSelectedItemEventHandlerCallCount++; }
  
}