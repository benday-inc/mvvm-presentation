using MauiApp1.ViewModels;

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

    private void Button_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        Console.WriteLine("Button clicked.");
        Console.Write("String Field: ");
        Console.WriteLine(vm.StringField.Value);

        Console.Write("Int Field: ");
        Console.WriteLine(vm.IntField.Value);
        Console.WriteLine();
    }
}

