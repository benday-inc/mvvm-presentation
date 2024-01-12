using System;
using System.ComponentModel;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Interface for a view model that can be selected. This is typically used in a list of items where the user can select one or more items.
/// </summary>
public interface ISelectable : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets a value indicating whether the item is selected.
    /// </summary>
    bool IsSelected { get; set; }
}


