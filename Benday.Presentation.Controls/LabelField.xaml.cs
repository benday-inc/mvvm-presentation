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
namespace Benday.Presentation.Controls;

/// <summary>
/// Control for displaying only a label.
/// </summary>
public partial class LabelField : ContentView
{
    public LabelField()
    {
        InitializeComponent();
    }

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

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(
            propertyName: nameof(LabelText),
            returnType: typeof(string),
            declaringType: typeof(LabelField),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: DependencyPropertyUtility.LabelTextPropertyChanged);

}