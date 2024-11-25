using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// A view model that can be selected. This is useful for list boxes and combo boxes when you want to bind to a list of items.
/// </summary>
public class SingleSelectListViewModel : SelectableCollectionViewModel<ISelectableItem>,
    IVisibleField
{
    protected SingleSelectListViewModel() : base()
    {
        IsVisible = true;
    }

    public SingleSelectListViewModel(IEnumerable<ISelectableItem> values)
        : this(new ObservableCollection<ISelectableItem>(values))
    {
        if (values == null)
            throw new ArgumentNullException("values", "values is null.");

        IsVisible = true;
    }

    public SingleSelectListViewModel(IEnumerable<ISelectableItem> values, ISelectableItem selectedItem)
        : this(new ObservableCollection<ISelectableItem>(values), selectedItem)
    {
        if (values == null)
            throw new ArgumentNullException("values", "values is null.");
        if (selectedItem == null)
            throw new ArgumentNullException("selectedItem", "selectedItem is null.");

        IsVisible = true;
    }

    public SingleSelectListViewModel(ObservableCollection<ISelectableItem> values) : base(values)
    {
        IsVisible = true;
    }

    public SingleSelectListViewModel(ObservableCollection<ISelectableItem> values, ISelectableItem selectedItem)
        : base(values, selectedItem)
    {
        IsVisible = true;
    }

    /// <summary>
    /// Initializes the view model with the specified values. Values are copied into a new ObservableCollection.
    /// </summary>
    /// <param name="values"></param>
    public override void Initialize(IEnumerable<ISelectableItem> values)
    {
        Items = new ObservableCollection<ISelectableItem>(values);

        IsVisible = true;
    }

    /// <summary>
    /// Searches the list of items for the specified text and selects the item if found.
    /// If not found, the selected item is set to null.
    /// </summary>
    /// <param name="text"></param>
    public void SelectByText(string text)
    {
        SelectedItem = GetByText(Items, text);
    }

    private ISelectableItem? GetByText(ObservableCollection<ISelectableItem> values, string text)
    {
        var selected = (from temp in values
                        where temp.Text == text
                        select temp).FirstOrDefault();

        return selected;
    }

    protected ISelectableItem? GetByValue(ObservableCollection<ISelectableItem> values, string value)
    {
        var selected = (from temp in values
                        where temp.Value == value
                        select temp).FirstOrDefault();

        return selected;
    }

    /// <summary>
    /// Searches the list of items for the specified value and selects the item if found.
    /// </summary>
    /// <param name="value"></param>
    public virtual void SelectByValue(string value)
    {
        SelectedItem = GetByValue(Items, value);
    }

    /// <summary>
    /// Searches the list of items for the specified value and selects the item if found.
    /// </summary>
    /// <param name="value"></param>
    public void SelectByValue(int value)
    {
        SelectedItem = GetByValue(Items, value.ToString());
    }

    private const string IsVisiblePropertyName = "IsVisible";

    private bool _IsVisible;
    /// <summary>
    /// Gets or sets the IsVisible property. This observable property.
    /// </summary>
    public bool IsVisible
    {
        get
        {
            return _IsVisible;
        }
        set
        {
            _IsVisible = value;
            RaisePropertyChanged(IsVisiblePropertyName);
        }
    }

    private const string ValidationMessagePropertyName = "ValidationMessage";

    private string _ValidationMessage = string.Empty;
    /// <summary>
    /// Gets or sets the ValidationMessage property. This observable property.
    /// NOTE: this class does not do any validation logic on its own.  It is up to the consumer to set this value.
    /// </summary>
    public string ValidationMessage
    {
        get
        {
            return _ValidationMessage;
        }
        set
        {
            _ValidationMessage = value;
            RaisePropertyChanged(ValidationMessagePropertyName);
        }
    }

}


