namespace Benday.Presentation.Controls;

public partial class TextboxField : ContentView
{
    public TextboxField()
    {
        InitializeComponent();        
    }

    private void Entry_Completed(object sender, EventArgs e)
    {

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
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(TextboxField), string.Empty, BindingMode.TwoWay, propertyChanged: DependencyPropertyUtility.LabelTextPropertyChanged);

}
