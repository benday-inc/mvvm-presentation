using Benday.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels;
public class Test123ViewModel : ViewModelBase
{
    public Test123ViewModel()
    {
        StringField = new ViewModelField<string>(String.Empty);
        IntField = new ViewModelField<int>(123);
        SingleSelectField = new SingleSelectListViewModel(GetSampleItems("Combobox"));
        ListboxSingleSelectField = new SingleSelectListViewModel(GetSampleItems("Listbox"));
        LabelField = new ViewModelField<string>(String.Empty);        
    }

    public ViewModelField<string> StringField { get; private set; }
    public ViewModelField<string> LabelField { get; private set; }
    public ViewModelField<int> IntField { get; private set; } 
    public SingleSelectListViewModel SingleSelectField { get; private set; }
    public SingleSelectListViewModel ListboxSingleSelectField { get; private set; }

    private static IEnumerable<ISelectableItem> GetSampleItems(
        string controlName)
    {
        var items = new List<ISelectableItem>();

        items.Add(new SelectableItem(false, $"{controlName}: Item 1"));
        items.Add(new SelectableItem(true, $"{controlName}: Item 2"));
        items.Add(new SelectableItem(false, $"{controlName}: Item 3"));

        return items;
    }

}
