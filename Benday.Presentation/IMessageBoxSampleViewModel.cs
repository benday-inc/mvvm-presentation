using System;
using System.Linq;

namespace Benday.Presentation;

public interface IMessageBoxSampleViewModel : IViewModelBase
{
    System.Windows.Input.ICommand MessageBoxCommand { get; set; }
    System.Windows.Input.ICommand ThrowExceptionCommand { get; set; }
    void DoSomethingThatNeedsAMessageBox();
    void DoSomethingThatThrowsAnException();
}


