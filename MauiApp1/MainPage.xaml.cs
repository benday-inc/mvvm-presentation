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

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _Count++;

        if (_Count == 1)
            CounterBtn.Text = $"Clicked {_Count} time";
        else
            CounterBtn.Text = $"Clicked {_Count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
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

        builder.Append("Int Field: ");
        builder.AppendLine(vm.IntField.Value.ToString());
        builder.AppendLine();

        await DisplayAlert("Button Clicked", builder.ToString(), "OK");
    }
}

