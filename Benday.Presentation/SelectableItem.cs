using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// A view model that can be selected. This is useful for list boxes and combo boxes when you want to bind to a list of items
/// but don't want to use a complex object. 
/// </summary>
public class SelectableItem : ViewModelBase, ISelectableItem
{
    public SelectableItem()
    {

    }

    /// <summary>
    /// Initializes a new instance of the SelectableItem class.
    /// </summary>
    /// <param name="isSelected"></param>
    /// <param name="text"></param>
    public SelectableItem(bool isSelected, string text)
        : this(isSelected, text, text, text)
    {

    }

    /// <summary>
    /// Initializes a new instance of the SelectableItemViewModel class.
    /// </summary>
    /// <param name="isSelected"></param>
    /// <param name="text"></param>
    /// <param name="value"></param>
    public SelectableItem(bool isSelected, string text, string value, string tooltipText)
    {
        _IsSelected = isSelected;
        _Text = text;
        _Value = value;
        _TooltipText = tooltipText;
    }

    public SelectableItem(bool isSelected, string text, int value)
    {
        _IsSelected = isSelected;
        _Text = text;
        _Id = value;
        _Value = value.ToString();
    }

    private const string IsSelectedPropertyName = "IsSelected";

    private bool _IsSelected;
    /// <summary>
    /// Gets or sets the IsSelected property. This observable property.
    /// </summary>
    public bool IsSelected
    {
        get
        {
            return _IsSelected;
        }
        set
        {
            if (_IsSelected != value)
            {
                _IsSelected = value;
                RaisePropertyChanged(IsSelectedPropertyName);
            }
        }
    }

    private const string TextPropertyName = "Text";

    private string _Text = string.Empty;
    /// <summary>
    /// Gets or sets the Text property. This observable property.
    /// </summary>
    public string Text
    {
        get
        {
            return _Text;
        }
        set
        {
            _Text = value;
            RaisePropertyChanged(TextPropertyName);
        }
    }

    private const string ValuePropertyName = "Value";

    private string _Value = string.Empty;
    /// <summary>
    /// Gets or sets the Value property. This observable property.
    /// </summary>
    public string Value
    {
        get
        {
            return _Value;
        }
        set
        {
            _Value = value;
            RaisePropertyChanged(ValuePropertyName);
        }
    }

    /// <summary>
    /// Returns the value of the Text property.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Text;
    }

    private const string IdPropertyName = "Id";

    private int _Id;
    /// <summary>
    /// Gets or sets the Id property. This observable property.
    /// </summary>
    public int Id
    {
        get
        {
            return _Id;
        }
        set
        {
            _Id = value;
            RaisePropertyChanged(IdPropertyName);
        }
    }

    private const string TooltipTextPropertyName = "TooltipText";

    private string _TooltipText = string.Empty;
    /// <summary>
    /// Gets or sets the TooltipText property. This observable property.
    /// </summary>
    public string TooltipText
    {
        get
        {
            return _TooltipText;
        }
        set
        {
            _TooltipText = value;
            RaisePropertyChanged(TooltipTextPropertyName);
        }
    }

}


