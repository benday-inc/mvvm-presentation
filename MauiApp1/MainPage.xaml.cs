using MauiApp1.ViewModels;
using System.Text;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    int _Count = 0;

    public MainPage()
    {
        InitializeComponent();
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


        builder.AppendLine();

        await DisplayAlert("Button Clicked", builder.ToString(), "OK");
    }

    private void ToggleVisibility_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        vm.StringField.IsVisible = !vm.StringField.IsVisible;
        vm.IntField.IsVisible = !vm.IntField.IsVisible;
    }
}

