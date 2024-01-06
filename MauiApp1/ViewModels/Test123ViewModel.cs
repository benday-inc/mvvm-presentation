using Benday.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp1.ViewModels;
public class Test123ViewModel : MessagingViewModelBase
{
    public Test123ViewModel(IMessageManager messageManager) : base(messageManager)
    {
        StringField = new ViewModelField<string>(String.Empty);
        IntField = new ViewModelField<int>(123);
        SingleSelectField = new SingleSelectListViewModel(GetSampleItems("Combobox"));
        ListboxSingleSelectField = new SingleSelectListViewModel(GetSampleItems("Single Select Listbox"));
        LabelField = new ViewModelField<string>(String.Empty);        
    }

    public ViewModelField<string> StringField { get; private set; }
    public ViewModelField<string> LabelField { get; private set; }
    public ViewModelField<int> IntField { get; private set; } 
    public SingleSelectListViewModel SingleSelectField { get; private set; }
    public SingleSelectListViewModel ListboxSingleSelectField { get; private set; }
    
    private static IList<ISelectableItem> GetSampleItems(
        string controlName)
    {
        var items = new List<ISelectableItem>();

        items.Add(new SelectableItem(false, $"{controlName}: Item 1"));
        items.Add(new SelectableItem(true, $"{controlName}: Item 2"));
        items.Add(new SelectableItem(false, $"{controlName}: Item 3"));
        items.Add(new SelectableItem(false, $"{controlName}: Item 4"));
        items.Add(new SelectableItem(false, $"{controlName}: Item 5"));
        items.Add(new SelectableItem(false, $"{controlName}: Item 6"));
        items.Add(new SelectableItem(false, $"{controlName}: Item 7"));

        return items;
    }


    private ICommand _ShowMessageCommand;
    public ICommand ShowMessageCommand
    {
        get
        {
            if (_ShowMessageCommand == null)
            {
                _ShowMessageCommand = 
                    new ExceptionHandlingRelayCommand(
                        Messages, () =>
                        {
                            Messages.ShowMessage(
                                $"Hi. It's {DateTime.Now}.", 
                                "Current Time");
                        });
            }

            return _ShowMessageCommand;
        }
    }

}
