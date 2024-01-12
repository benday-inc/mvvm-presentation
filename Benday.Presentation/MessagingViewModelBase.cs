using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Base class for a view model that might need to show a message box to the user.
/// </summary>
public abstract class MessagingViewModelBase : ViewModelBase
{
    private IMessageManager _MessageManager;

    public MessagingViewModelBase(IMessageManager messageManager)
    {
        _MessageManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager), $"{nameof(messageManager)} is null.");
    }

    /// <summary>
    /// The message manager to use for showing messages to the user.
    /// </summary>
    public IMessageManager Messages { get => _MessageManager; }
}


