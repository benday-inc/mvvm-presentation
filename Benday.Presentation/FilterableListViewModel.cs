using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Benday.Presentation;


public class FilterableListViewModel<T> : SelectableCollectionViewModel<T> where T : class, ISelectableItem
{
    public override void Initialize(IEnumerable<T> values)
    {
        _OriginalItems = null;
        _Filter = string.Empty;
        RaisePropertyChanged(FilterPropertyName);

        base.Initialize(values);
    }

    private const string FilterPropertyName = "Filter";

    private string _Filter = string.Empty;
    public string Filter
    {
        get
        {
            return _Filter;
        }
        set
        {
            _Filter = value;
            FilterItems();
            RaisePropertyChanged(FilterPropertyName);
        }
    }

    protected ObservableCollection<T>? _OriginalItems;

    private void FilterItems()
    {
        if (_OriginalItems == null)
        {
            _OriginalItems = new ObservableCollection<T>(_Items);
        }

        var matches = from temp in _OriginalItems
                      where
                           string.IsNullOrWhiteSpace(temp.Text) == false &&
                           temp.Text.CaseInsensitiveContains(Filter)
                      select temp;

        var filtered = new ObservableCollection<T>(matches);

        Items = filtered;
    }
}
