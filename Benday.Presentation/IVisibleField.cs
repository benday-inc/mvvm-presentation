using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Interface for a view model class that will be used by a field control that can be hidden.
/// </summary>
public interface IVisibleField
{
    /// <summary>
    /// Gets or sets a value indicating whether the field is visible.
    /// </summary>
    bool IsVisible { get; set; }
}
