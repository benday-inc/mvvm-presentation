using System;
using System.Linq;

namespace Benday.Presentation;
public interface IProgressBarSampleViewModel : IViewModelBase, IProgressBarViewModel
{
    void RunProgressBar();
    System.Windows.Input.ICommand RunProgressBarCommand { get; set; }
}


