using MauiApp1.ViewModels;
using System.Text;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();

        this.BindingContext = new Test123ViewModel();
    }

    private void CounterEntry_Completed(object sender, EventArgs e)
    {

    }

    public Test123ViewModel ViewModel
    {
        get
        {
            return (Test123ViewModel)this.BindingContext;
        }
        set
        {
            this.BindingContext = value;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var builder = new StringBuilder();

        var vm = ViewModel;

        builder.AppendLine("Button clicked.");
        builder.Append("String Field: ");
        builder.AppendLine(vm.StringField.Value);
        builder.Append("Visible: "); 
        builder.AppendLine(vm.StringField.IsVisible.ToString());

        builder.Append("Int Field: ");
        builder.AppendLine(vm.IntField.Value.ToString());
        builder.Append("Visible: ");
        builder.AppendLine(vm.IntField.IsVisible.ToString());

        builder.Append("Single Select Field: ");
        if (vm.SingleSelectField.SelectedItem == null)
        {
            builder.AppendLine("(value is null)");
        }
        else
        {
            builder.AppendLine(vm.SingleSelectField.SelectedItem.Text);
        }
        builder.Append("Visible: ");
        builder.AppendLine(vm.IntField.IsVisible.ToString());


        builder.AppendLine();

        await DisplayAlert("Button Clicked", builder.ToString(), "OK");
    }

    private void ToggleVisibility_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        vm.StringField.IsVisible = !vm.StringField.IsVisible;
        vm.IntField.IsVisible = !vm.IntField.IsVisible;
        vm.SingleSelectField.IsVisible = !vm.SingleSelectField.IsVisible;
    }

    private void ChangeSelelection_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;
        var field = vm.SingleSelectField;

        var itemCount = field.Items.Count;

        if (field.SelectedItem == null)
        {
            field.Items[0].IsSelected = true;
        }
        else
        {
            var indexOf = field.Items.IndexOf(field.SelectedItem);

            var newIndex = indexOf + 1;

            if (newIndex >= itemCount)
            {
                newIndex = 0;
            }

            field.Items[newIndex].IsSelected = true;
        }
    }
}

