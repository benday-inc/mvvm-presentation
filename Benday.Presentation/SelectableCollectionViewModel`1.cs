﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Benday.Presentation;

public class SelectableCollectionViewModel<T> : ViewModelBase where T : class, ISelectable
{
    protected ObservableCollection<T>? _Items; // = new ObservableCollection<T>();

    public SelectableCollectionViewModel()
    {
        AllowMultipleSelections = false;
        Items = new ObservableCollection<T>();
    }

    public bool HasOnItemSelectedSubscriber
    {
        get
        {
            return OnItemSelected != null;
        }
    }

    public event EventHandler? OnItemSelected;

    private void RaiseOnItemSelected()
    {
        if (OnItemSelected != null)
        {
            OnItemSelected(this, new EventArgs());
        }
    }

    public SelectableCollectionViewModel(ObservableCollection<T> values)
    {
        if (values == null)
            throw new ArgumentNullException("values", "values is null.");

        Items = values;

        RefreshSelectedItem();
    }

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

    public virtual void Initialize(IEnumerable<T> values)
    {
        Items = new ObservableCollection<T>(values);

        RefreshSelectedItem();
    }

    protected void RefreshSelectedItem()
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

    public void Clear()
    {
        if (Items != null)
        {
            Items.Clear();
        }
    }

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

    public List<T> GetSelectedItems()
    {
        var selected = (from temp in Items
                        where temp.IsSelected == true
                        select temp).ToList();

        return selected;
    }

    private const string SelectedItemPropertyName = "SelectedItem";
    private T? _SelectedItem;
    public T? SelectedItem
    {
        get
        {
            _SelectedItem = GetFirstSelectedItem(Items);

            return _SelectedItem;
        }
        set
        {
            if (_SelectedItem != value)
            {
                if (AllowMultipleSelections == false)
                {
                    DeselectCurrentItem();
                }

                _SelectedItem = value;

                if (_SelectedItem != null)
                {
                    _SelectedItem.IsSelected = true;
                }
            }

            RaiseOnItemSelected();

            RaisePropertyChanged(SelectedItemPropertyName);
        }
    }

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

    private bool _IsValid;
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
}


