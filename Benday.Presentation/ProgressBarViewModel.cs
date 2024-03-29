﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Windows.Input;

namespace Benday.Presentation;


/// <summary>
/// View model class that provides properties and methods for showing and updating a progress bar in a long-running operation. 
/// It also provides functionality for canceling the operation.
/// </summary>
public class ProgressBarViewModel : ViewModelBase, IProgressBarViewModel
{
    public ProgressBarViewModel()
    {
        _ProgressBarMessage = string.Empty;
    }

    private const string IsProgressBarVisiblePropertyName = "IsProgressBarVisible";

    private bool _IsProgressBarVisible;
    public bool IsProgressBarVisible
    {
        get
        {
            return _IsProgressBarVisible;
        }
        set
        {
            _IsProgressBarVisible = value;
            RaisePropertyChanged(IsProgressBarVisiblePropertyName);
        }
    }

    private const string ProgressBarMessagePropertyName = "ProgressBarMessage";

    private string _ProgressBarMessage;
    public string ProgressBarMessage
    {
        get
        {
            return _ProgressBarMessage;
        }
        set
        {
            _ProgressBarMessage = value;
            RaisePropertyChanged(ProgressBarMessagePropertyName);
        }
    }

    private const string IsCancelablePropertyName = "IsCancelable";

    private bool _IsCancelable;
    public bool IsCancelable
    {
        get
        {
            return _IsCancelable;
        }
        set
        {
            _IsCancelable = value;
            RaisePropertyChanged(IsCancelablePropertyName);
        }
    }

    private ICommand? _CancelOperationCommand;

    public ICommand CancelOperationCommand
    {
        get
        {
            if (_CancelOperationCommand == null)
            {
                _CancelOperationCommand = new RelayCommand(CancelOperation);
            }

            return _CancelOperationCommand;
        }
    }


    public event EventHandler? OnCancelRequested;

    private void CancelOperation()
    {
        if (OnCancelRequested != null)
        {
            OnCancelRequested(this, new EventArgs());
        }
    }
}


