using System;
using System.Linq;

namespace Benday.Presentation;

public abstract class MessagingViewModelBase : ViewModelBase
{
    private IMessageManager _MessageManager;

    public MessagingViewModelBase(IMessageManager messageManager)
    {
        _MessageManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager), $"{nameof(messageManager)} is null.");
    }

    public IMessageManager Messages { get => _MessageManager; }
}


