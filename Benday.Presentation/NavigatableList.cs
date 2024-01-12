using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Collection class that allows for navigation forwards or backwards through the collection elements
/// </summary>
/// <typeparam name="T"></typeparam>
public class NavigatableList<T> : List<T>
{
    /// <summary>
    /// Returns true if the current index is at the first element in the collection.
    /// </summary>
    public bool IsAtFirst
    {
        get
        {
            if (CurrentIndex == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Returns true if the current index is at the last element in the collection.
    /// </summary>
    public bool IsAtLast
    {
        get
        {
            if (CurrentIndex + 1 >= Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private int _CurrentIndex;
    /// <summary>
    /// Gets or sets the current index.
    /// </summary>
    public int CurrentIndex
    {
        get
        {
            if (Count == 0)
            {
                _CurrentIndex = -1;
            }
            else if (_CurrentIndex < 0)
            {
                _CurrentIndex = -1;
            }
            else if (_CurrentIndex >= Count)
            {
                _CurrentIndex = Count - 1;
            }

            return _CurrentIndex;
        }
        private set
        {
            _CurrentIndex = value;
        }
    }

    /// <summary>
    /// Gets the current value.  Returns default(T) if the current index is -1 or out of bounds for the collection.
    /// </summary>
    public T? Value
    {
        get
        {
            if (CurrentIndex == -1)
            {
                return default;
            }
            else
            {
                return this[CurrentIndex];
            }
        }
    }

    /// <summary>
    /// Move to the next element in the collection.
    /// </summary>
    public void Next()
    {
        CurrentIndex++;
    }

    /// <summary>
    /// Move to the previous element in the collection.
    /// </summary>
    public void Previous()
    {
        if (CurrentIndex > 0)
        {
            CurrentIndex--;
        }
    }

    /// <summary>
    /// Move to the first element in the collection.
    /// </summary>
    public void MoveToFirst()
    {
        CurrentIndex = 0;
    }

    /// <summary>
    /// Move to the last element in the collection.
    /// </summary>
    public void MoveToLast()
    {
        var proposedIndex = Count - 1;

        if (proposedIndex < 0)
        {
            proposedIndex = 0;
        }

        CurrentIndex = proposedIndex;
    }
}


