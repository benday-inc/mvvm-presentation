using System;
using System.Diagnostics;

namespace Benday.ControlsAndViewModelSample;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Debug.WriteLine("Tapped");

        if (e.Parameter is null)
        { 
            Debug.WriteLine("Parameter for link is null");
        }
        else
        {
            var url = e.Parameter.ToString();

            Task.Run(async () => await Launcher.OpenAsync(url!));
        }

    }
}