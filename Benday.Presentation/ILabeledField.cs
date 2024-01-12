using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benday.Presentation;

/// <summary>
/// Interface for a field control that has a label.
/// </summary>
public interface ILabeledField
{
    /// <summary>
    /// Sets the label text.
    /// </summary>
    /// <param name="value"></param>
    void SetLabelText(string value);
}
