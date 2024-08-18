using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// View model class for representing a collection of objects that implements ISelectable. By default it only allows one item to be selected at a time and it will
/// manage the IsSelected property of the items in the collection.  If you want to allow multiple selections, set AllowMultipleSelections to true.
/// </summary>
/// <typeparam name="T"></typeparam>
public class SelectableCollectionViewModel<T> : ViewModelBase where T : class, ISelectable
{
    protected ObservableCollection<T> _Items = new ObservableCollection<T>();

    public SelectableCollectionViewModel()
    {
        AllowMultipleSelections = false;
    }

    /// <summary>
    /// Utility property to determine if the view model has a subscriber to the OnItemSelected event.
    /// </summary>
    public bool HasOnItemSelectedSubscriber
    {
        get
        {
            return OnItemSelected != null;
        }
    }

    /// <summary>
    /// Event that is raised when an item is selected.
    /// </summary>
    public event EventHandler? OnItemSelected;

    private void RaiseOnItemSelected()
    {
        if (OnItemSelected != null)
        {
            OnItemSelected(this, new EventArgs());
        }
    }

    /// <summary>
    /// Initializes a new instance of the SelectableCollectionViewModel class with existing items or an existing ObservableCollection instance.
    /// </summary>
    /// <param name="values"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SelectableCollectionViewModel(ObservableCollection<T> values)
    {
        if (values == null)
            throw new ArgumentNullException("values", "values is null.");

        Items = values;

        RefreshSelectedItem();
    }

    /// <summary>
    /// Initializes a new instance of the SelectableCollectionViewModel class with existing items or an existing ObservableCollection instance.
    /// </summary>
    /// <param name="values"></param>
    /// <param name="selectedItem">Item in the values collection that should be selected</param>
    /// <exception cref="ArgumentNullException"></exception>
    public SelectableCollectionViewModel(ObservableCollection<T> values, T selectedItem)
        : this(values)
    {
        if (selectedItem == null)
        {
            throw new ArgumentNullException("selectedItem", "selectedItem is null.");
        }

        Items = values;

        selectedItem.IsSelected = true;

        RefreshSelectedItem();
    }

    /// <summary>
    /// Resets the collection to the specified values.
    /// </summary>
    /// <param name="values"></param>
    public virtual void Initialize(IEnumerable<T> values)
    {
        Items = new ObservableCollection<T>(values);

        RefreshSelectedItem();
    }

    protected void RefreshSelectedItem(bool directAssign = false)
    {
        if (Items == null)
        {
            // do nothing
        }
        else
        {
            var selected = (from temp in Items
                            where temp.IsSelected == true
                            select temp).FirstOrDefault();

            SelectedItem = selected;
        }
    }

    /// <summary>
    /// Clears the collection.
    /// </summary>
    public void Clear()
    {
        if (Items != null)
        {
            Items.Clear();
        }
    }

    /// <summary>
    /// Adds an item to the collection.
    /// </summary>
    /// <param name="addThis"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Add(T addThis)
    {
        if (Items == null)
        {
            throw new InvalidOperationException("Items collection has not been initialized.");
        }
        else
        {
            Items.Add(addThis);
        }
    }

    /// <summary>
    /// Removes an item from the collection.
    /// </summary>
    /// <param name="removeThis"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Remove(T removeThis)
    {
        if (Items == null)
        {
            throw new InvalidOperationException("Items collection has not been initialized.");
        }
        else
        {
            Items.Remove(removeThis);
        }
    }

    public readonly string ItemsPropertyName = "Items";

    /// <summary>
    /// Gets or sets the collection of items.
    /// </summary>
    public ObservableCollection<T> Items
    {
        get
        {
            if (_Items == null)
            {
                _Items = new ObservableCollection<T>();
            }

            return _Items;
        }
        protected set
        {
            _Items = value;

            SubscribeToINotifyPropertyChanged(_Items);

            _Items.CollectionChanged +=
                new NotifyCollectionChangedEventHandler(_items_CollectionChanged);

            RaisePropertyChanged(ItemsPropertyName);
        }
    }

    void _items_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null && e.NewItems.Count > 0)
        {
            SubscribeToINotifyPropertyChanged(e.NewItems);
        }
    }

    private void SubscribeToINotifyPropertyChanged(System.Collections.IList items)
    {
        if (items == null || items.Count == 0)
        {
            // do nothing
        }
        else
        {
            foreach (var item in items)
            {
                if (item is ISelectableItem)
                {
                    var temp = item as ISelectableItem;

                    if (temp != null)
                    {
                        SubscribeToINotifyPropertyChanged(temp);
                    }
                }
            }
        }
    }
    private void SubscribeToINotifyPropertyChanged(ISelectableItem item)
    {
        if (item != null)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(OnItemPropertyChanged);
        }
    }

    protected virtual void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is T && e.PropertyName == "IsSelected")
        {
            var senderAsISelectableItem = sender as T;

            if (senderAsISelectableItem != null && senderAsISelectableItem.IsSelected == true)
            {
                if (AllowMultipleSelections == false)
                {
                    foreach (var item in Items)
                    {
                        if (item != senderAsISelectableItem &&
                            item.IsSelected == true)
                        {
                            item.IsSelected = false;
                        }
                    }
                }

                SelectedItem = GetFirstSelectedItem(Items);
            }
        }
    }

    private T? GetFirstSelectedItem(ObservableCollection<T> values)
    {
        var selected = (from temp in values
                        where temp.IsSelected == true
                        select temp).FirstOrDefault();

        return selected;
    }

    /// <summary>
    /// Gets the selected items.
    /// </summary>
    /// <returns>Selected items in a new instance of List</returns>
    public List<T> GetSelectedItems()
    {
        var selected = (from temp in Items
                        where temp.IsSelected == true
                        select temp).ToList();

        return selected;
    }

    private const string SelectedItemPropertyName = "SelectedItem";
    private T? _SelectedItem;

    /// <summary>
    /// Gets or sets the selected item.
    /// </summary>
    public T? SelectedItem
    {
        get
        {
            _SelectedItem = GetFirstSelectedItem(Items);

            return _SelectedItem;
        }
        set
        {
            if (_SelectedItem == null && value == null)
            {
                Console.WriteLine("SelectedItem is already null.");
            }
            else if (_SelectedItem == null && value != null)
            {
                Console.WriteLine("SelectedItem is null, setting to {0}.", value.ToString());

                value.IsSelected = true;
                _SelectedItem = value;

                RaiseOnItemSelected();

                RaisePropertyChanged(SelectedItemPropertyName);
            }
            else if (_SelectedItem != null && value == null)
            {
                Console.WriteLine("SelectedItem is {0}, setting to null.", _SelectedItem.ToString());

                if (AllowMultipleSelections == false)
                {
                    DeselectCurrentItem();
                }

                _SelectedItem = value;

                RaiseOnItemSelected();

                RaisePropertyChanged(SelectedItemPropertyName);
            }
            else if (_SelectedItem != null && _SelectedItem != value)
            {
                Console.WriteLine("SelectedItem is {0}, setting to {1}.", _SelectedItem.ToString(), value.ToString());

                if (AllowMultipleSelections == false)
                {
                    DeselectCurrentItem();
                }

                _SelectedItem = value;

                if (_SelectedItem != null)
                {
                    _SelectedItem.IsSelected = true;
                }

                RaiseOnItemSelected();

                RaisePropertyChanged(SelectedItemPropertyName);
            }
            else
            {
                Console.WriteLine("SelectedItem is already {0}.", value?.ToString());
            }
        }
    }

    /// <summary>
    /// Gets the number of items in the collection.
    /// </summary>
    public int Count
    {
        get
        {
            return Items.Count;
        }
    }

    private void DeselectCurrentItem()
    {
        var currentSelected = GetFirstSelectedItem(Items);

        if (currentSelected != null)
        {
            currentSelected.IsSelected = false;
        }
    }

    private const string AllowMultipleSelectionsPropertyName = "AllowMultipleSelections";

    private bool _AllowMultipleSelections;
    
    /// <summary>
    /// Gets or sets a value indicating whether multiple selections are allowed.
    /// </summary>
    public bool AllowMultipleSelections
    {
        get
        {
            return _AllowMultipleSelections;
        }
        set
        {
            _AllowMultipleSelections = value;
            RaisePropertyChanged(AllowMultipleSelectionsPropertyName);
        }
    }

    private const string IsValidPropertyName = "IsValid";

    private bool _IsValid = true;
    /// <summary>
    /// Gets or sets a value indicating whether this view model is valid. 
    /// NOTE: this property does not do any validation, it is just a flag that can be used by the view.
    /// </summary>
    public bool IsValid
    {
        get
        {
            return _IsValid;
        }
        set
        {
            _IsValid = value;
            RaisePropertyChanged(IsValidPropertyName);
        }
    }

    private const string IsEnabledPropertyName = "IsEnabled";

    private bool _IsEnabled = true;
    /// <summary>
    /// Gets or sets a value indicating whether this view model is enabled.
    /// </summary>
    public bool IsEnabled
    {
        get
        {
            return _IsEnabled;
        }
        set
        {
            _IsEnabled = value;
            RaisePropertyChanged(IsEnabledPropertyName);
        }
    }
}


