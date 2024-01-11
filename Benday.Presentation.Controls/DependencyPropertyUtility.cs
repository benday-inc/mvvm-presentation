namespace Benday.Presentation.Controls;

public static class DependencyPropertyUtility
{
    public static void LabelTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var target = bindable as ILabeledField;

        var tempValue = newValue as string;

        if (target != null && tempValue != null)
        {
            target.SetLabelText(tempValue);
        }
    }

}