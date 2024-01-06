using Benday.Presentation;
using MauiApp1.ViewModels;
using System.Text;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private MessageBoxMessageManager _MessageManager = new();

    public MainPage()
    {
        InitializeComponent();

        this.BindingContext = new Test123ViewModel(_MessageManager);
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
}

