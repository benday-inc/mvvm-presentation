/* Unmerged change from project 'Benday.Presentation.Controls (net8.0-android)'
Before:
namespace Benday.Presentation.Controls.Controls;
After:
using Benday;
using Benday.Presentation;
using Benday.Presentation.Controls;
using Benday.Presentation.Controls;
using Benday.Presentation.Controls.Controls;

namespace Benday.Presentation.Controls;
*/
using Benday.Presentation;

namespace Benday.Presentation.Controls;

/// <summary>
/// A control that contains a label and a combobox. This typically is bound to an instance of 
/// SingleSelectListViewModel or SelectableCollectionViewModel.
/// </summary>
public partial class ComboboxField : ContentView, ILabeledField
{
    public ComboboxField()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the label text for this field
    /// </summary>
    public string LabelText
    {
        get
        {
            return (string)GetValue(LabelTextProperty);
        }
        set
        {
            SetLabelText(value);
        }
    }

    /// <summary>
    /// Sets the label text for this field
    /// </summary>
    /// <param name="value"></param>
    public void SetLabelText(string value)
    {
        if (value == null)
        {
            SetValue(LabelTextProperty, string.Empty);
            _Label.Text = string.Empty;
        }
        else
        {
            SetValue(LabelTextProperty, value);
            _Label.Text = value;
        }
    }

    /// <summary>
    /// Dependency property for the label text
    /// </summary>
    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(
            propertyName: nameof(LabelText),
            returnType: typeof(string),
            declaringType: typeof(ComboboxField),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: DependencyPropertyUtility.LabelTextPropertyChanged);

}