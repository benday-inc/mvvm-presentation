using Benday.Presentation;
using System.ComponentModel;

/// <summary>
/// This is a demo class that is used to demonstrate how to use the ISelectable interface.
/// </summary>
public class MiscellaneousItem : ViewModelBase, ISelectable
{
    private bool _IsSelected;
    public bool IsSelected
    {
        get
        {
            return _IsSelected;
        }
        set
        {
            _IsSelected = value;
            RaisePropertyChanged(nameof(IsSelected));
        }
    }

    private string _Description = string.Empty;
    public string Description
    {
        get
        {
            return _Description;
        }
        set
        {
            _Description = value;
            RaisePropertyChanged(nameof(Description));
        }
    }

    public override string ToString()
    {
        return Description;
    }
}
