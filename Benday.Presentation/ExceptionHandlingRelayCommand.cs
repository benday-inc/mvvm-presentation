using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// A relay command that handles exceptions by showing them to the user. This class wraps an existing action and
/// executes it when the command is invoked. If the action throws an exception, the exception is caught and is shown via IMessageManager.ShowMessage().
/// </summary>
public class ExceptionHandlingRelayCommand : IRelayCommand
{
    /// <summary>
    /// The message manager instance to use for showing exceptions.
    /// </summary>
    private IMessageManager _MsgManager;

    /// <summary>
    /// The action to execute when the command is invoked.
    /// </summary>
    private readonly Action _Action;

    /// <summary>
    /// Creates a new instance of the <see cref="ExceptionHandlingRelayCommand"/> class.
    /// </summary>
    /// <param name="msgManager">Instance of IMessageManager to use</param>
    /// <param name="execute">The action to perform</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ExceptionHandlingRelayCommand(IMessageManager msgManager, Action execute)
    {
        _Action = execute;
        _MsgManager = msgManager ?? throw new ArgumentNullException(nameof(msgManager), $"{nameof(msgManager)} is null.");
    }

#pragma warning disable CS0067 // The event 'ExceptionHandlingRelayCommand.CanExecuteChanged' is never used
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'ExceptionHandlingRelayCommand.CanExecuteChanged' is never used

    /// <summary>
    /// Returns true if the command can be executed.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns>This class always returns true.</returns>
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    /// <summary>
    /// Executes the action and handles any exceptions by showing them to the user.
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object? parameter)
    {
        try
        {
            _Action.Invoke();
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    public void NotifyCanExecuteChanged()
    {

    }

    private void HandleException(Exception ex)
    {
        _MsgManager.ShowMessage(ex);
    }
}
