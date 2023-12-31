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
        SingleSelectField = new SingleSelectListViewModel(GetSampleItems());
        Console.WriteLine("ctor exiting");
    }

    public ViewModelField<string> StringField { get; private set; }
    public ViewModelField<int> IntField { get; private set; } 
    public SingleSelectListViewModel SingleSelectField { get; private set; }

    private static IEnumerable<ISelectableItem> GetSampleItems()
    {
        var items = new List<ISelectableItem>();

        items.Add(new SelectableItem(false, "Item 1"));
        items.Add(new SelectableItem(true, "Item 2"));
        items.Add(new SelectableItem(false, "Item 3"));

        return items;
    }

}
