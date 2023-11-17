using Benday.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels;
public class Test123ViewModel : ViewModelBase
{
    public ViewModelField<string> StringField { get; private set; } = new ViewModelField<string>(String.Empty);
    public ViewModelField<int> IntField { get; private set; } = new ViewModelField<int>(123);

}
