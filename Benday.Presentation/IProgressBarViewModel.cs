using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Benday.Presentation;



public interface IProgressBarViewModel : INotifyPropertyChanged
{
    bool IsProgressBarVisible { get; set; }
    string ProgressBarMessage { get; set; }
    bool IsCancelable { get; set; }

    ICommand CancelOperationCommand { get; }

    event EventHandler OnCancelRequested;
}


