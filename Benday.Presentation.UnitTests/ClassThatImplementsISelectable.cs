using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benday.Presentation.UnitTests;
public class ClassThatImplementsISelectable : ViewModelBase, ISelectable
{
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

    public string Text { get; set; } = String.Empty;
    public string Value { get; set; } = String.Empty;
}
