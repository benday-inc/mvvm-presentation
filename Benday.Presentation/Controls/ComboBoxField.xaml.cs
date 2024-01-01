namespace Benday.Presentation.Controls;

public partial class ComboBoxField : ContentView, ILabeledField
{
	public ComboBoxField()
	{
		InitializeComponent();
	}

    public string LabelText
    {
        get
        {
            return (string)this.GetValue(LabelTextProperty);
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
            this.SetValue(LabelTextProperty, String.Empty);
            _Label.Text = String.Empty;
        }
        else
        {
            this.SetValue(LabelTextProperty, value);
            _Label.Text = value;
        }
    }

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(
            propertyName: nameof(LabelText),
            returnType: typeof(string),
            declaringType: typeof(ComboBoxField),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: DependencyPropertyUtility.LabelTextPropertyChanged);

}