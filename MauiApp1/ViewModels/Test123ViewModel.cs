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


    private ICommand? _ShowMessageCommand;
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

    private ICommand? _ShowSummaryCommand;
    public ICommand ShowSummaryCommand
    {
        get
        {
            if (_ShowSummaryCommand == null)
            {
                _ShowSummaryCommand =
                    new ExceptionHandlingRelayCommand(
                        Messages, ShowSummary);
            }

            return _ShowSummaryCommand;
        }
    }
    
    private void ShowSummary()
    {
        var builder = new StringBuilder();

        builder.AppendLine("Button clicked.");
        builder.Append("String Field: ");
        builder.AppendLine(StringField.Value);
        builder.Append("Visible: ");
        builder.AppendLine(StringField.IsVisible.ToString());
        builder.Append("Valid: ");
        builder.AppendLine(StringField.IsValid.ToString());

        builder.Append("Int Field: ");
        builder.AppendLine(IntField.Value.ToString());
        builder.Append("Visible: ");
        builder.AppendLine(IntField.IsVisible.ToString());
        builder.Append("Valid: ");
        builder.AppendLine(IntField.IsValid.ToString());

        SummarizeSingleSelect(builder,
                       SingleSelectField,
                                  "Combobox Single Select Field");

        SummarizeSingleSelect(builder,
                       ListboxSingleSelectField,
                                  "Listbox Single Select Field");

        Messages.ShowMessage(builder.ToString(), "Summary");
    }

    private static void SummarizeSingleSelect(
        StringBuilder builder, SingleSelectListViewModel viewModel,
        string description)
    {
        builder.AppendLine("***");
        builder.Append($"{description}: ");
        if (viewModel.SelectedItem == null)
        {
            builder.AppendLine("(value is null)");
        }
        else
        {
            builder.AppendLine(viewModel.SelectedItem.Text);
        }

        builder.Append("Visible: ");
        builder.AppendLine(viewModel.ToString());
        builder.Append("Valid: ");
        builder.AppendLine(viewModel.IsValid.ToString());

        builder.AppendLine();
    }
}
