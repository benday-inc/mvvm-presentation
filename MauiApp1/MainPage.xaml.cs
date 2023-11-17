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
}

