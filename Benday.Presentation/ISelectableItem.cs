using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Interface for a view model that can be selected. This is typically used in a list of items where the user can select one or more items.
/// This interface provides a way to get the text and value of the item when using complex objects in a list.
/// </summary>
public interface ISelectableItem : ISelectable
{
    /// <summary>
    /// Gets or sets the text to display to the user.
    /// </summary>
    string Text { get; set; }

    /// <summary>
    /// Gets or sets the value of the item.
    /// </summary>
    string Value { get; set; }

    /// <summary>
    /// Gets or sets the id of the item.  Typically an integer primary key.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Gets or sets the tooltip text.
    /// </summary>
    string TooltipText { get; set; }
}


