using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Benday.Presentation;

/// <summary>
/// Interface for a view model that has performs long-running actions that 
/// displays progress information using a progress bar.
/// </summary>
public interface IProgressBarViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets a value indicating whether the progress bar is visible.
    /// </summary>
    bool IsProgressBarVisible { get; set; }

    /// <summary>
    /// Gets or sets the progress bar message.
    /// </summary>
    string ProgressBarMessage { get; set; }

    /// <summary>
    /// Gets or sets value indicating if the operation is cancelable.
    /// </summary>
    bool IsCancelable { get; set; }

    /// <summary>
    /// Gets or sets the cancel operation command.
    /// </summary>
    ICommand CancelOperationCommand { get; }

    /// <summary>
    /// Event raised when the cancel operation is requested.
    /// </summary>
    event EventHandler OnCancelRequested;
}


