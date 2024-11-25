﻿using System;
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
    protected ObservableCollection<T> _Items;

    public SelectableCollectionViewModel()
    {
        AllowMultipleSelections = false;
        _Items = new ObservableCollection<T>();
        VerifyEventSubscriptionsForCollection();
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

        _Items = values;
        VerifyEventSubscriptionsForCollection();
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

        _Items = values;
        VerifyEventSubscriptionsForCollection();

        selectedItem.IsSelected = true;
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
    /// Resets the collection to the specified values.
    /// </summary>
    /// <param name="values"></param>
    public virtual void Initialize(IEnumerable<T> values)
    {
        Items = new ObservableCollection<T>(values);
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

            VerifyEventSubscriptionsForCollection();

            RaisePropertyChanged(ItemsPropertyName);
        }
    }

    private void VerifyEventSubscriptionsForCollection()
    {
        SubscribeToINotifyPropertyChanged(_Items);

        _Items.CollectionChanged +=
            new NotifyCollectionChangedEventHandler(_items_CollectionChanged);
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
                if (item is ISelectable temp)
                {
                    SubscribeToINotifyPropertyChanged(temp);
                }
                else
                {
                    throw new InvalidOperationException("Item does not implement ISelectable.");
                }
            }
        }
    }
    private void SubscribeToINotifyPropertyChanged(ISelectable item)
    {
        if (item != null)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(OnItemPropertyChanged);
        }
    }

    protected virtual void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISelectable.IsSelected) && 
            sender is T senderAsTyped)
        {
            if (senderAsTyped.IsSelected == true)
            {
                if (AllowMultipleSelections == false)
                {
                    foreach (var item in Items.Where(
                        x => x.IsSelected == true && 
                        x != senderAsTyped))
                    {
                        item.IsSelected = false;
                    }
                }

                if (_SelectedItem != senderAsTyped)
                {
                    SelectedItem = senderAsTyped;
                }
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
            if (_SelectedItem == null || _SelectedItem.IsSelected == false)
            {
                _SelectedItem = GetFirstSelectedItem(Items);
            }

            return _SelectedItem;
        }
        set
        {
            if (_SelectedItem == null && value == null)
            {
                // no change
            }
            else if (_SelectedItem == null && value != null)
            {
                Console.WriteLine($"SelectedItem is null, setting to {value.ToString()}.");

                if (value.IsSelected == false)
                {
                    value.IsSelected = true;
                }

                _SelectedItem = value;

                RaiseOnItemSelected();
                RaisePropertyChanged(nameof(SelectedItem));
            }
            else if (_SelectedItem != null && value == null)
            {
                Console.WriteLine("SelectedItem is {0}, setting to null.", _SelectedItem.ToString());

                if (AllowMultipleSelections == false)
                {
                    _SelectedItem.IsSelected = false;
                }

                _SelectedItem = value;

                RaiseOnItemSelected();

                RaisePropertyChanged(nameof(SelectedItem));
            }
            else if (_SelectedItem != null && _SelectedItem != value)
            {
                Console.WriteLine($"SelectedItem is {_SelectedItem.ToString()}, setting to {value}.");

                if (AllowMultipleSelections == false)
                {
                    _SelectedItem.IsSelected = false;
                }

                _SelectedItem = value;

                if (_SelectedItem != null)
                {
                    _SelectedItem.IsSelected = true;
                }

                RaiseOnItemSelected();

                RaisePropertyChanged(nameof(SelectedItem));
            }
            else
            {
                Console.WriteLine($"No change. SelectedItem is already {value}.");
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


