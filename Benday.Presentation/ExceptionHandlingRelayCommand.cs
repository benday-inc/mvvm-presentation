using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;

namespace Benday.Presentation;
public class ExceptionHandlingRelayCommand : IRelayCommand
{
    private IMessageManager _MsgManager;
    private readonly Action _Action;

    public ExceptionHandlingRelayCommand(IMessageManager msgManager, Action execute)        
    {
        _Action = execute;
        _MsgManager = msgManager ?? throw new ArgumentNullException(nameof(msgManager), $"{nameof(msgManager)} is null.");
    }

#pragma warning disable CS0067 // The event 'ExceptionHandlingRelayCommand.CanExecuteChanged' is never used
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067 // The event 'ExceptionHandlingRelayCommand.CanExecuteChanged' is never used

    public bool CanExecute(object? parameter)
    {
        return true;
    }

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
