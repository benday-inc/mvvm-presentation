
using Benday.Presentation.DemoApp.Api.ViewModels;

namespace Benday.Presentation.DemoApp;

public partial class MainPage : ContentPage
{
    private MessageBoxMessageManager _MessageManager = new();

    public MainPage()
    {
        InitializeComponent();

        this.BindingContext = new MainPageViewModel(_MessageManager);
    }   
}

