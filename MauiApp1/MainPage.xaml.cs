using Benday.Presentation;
using MauiApp1.ViewModels;
using System.Text;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();

        this.BindingContext = new Test123ViewModel();
    }

    private void CounterEntry_Completed(object sender, EventArgs e)
    {

    }

    public Test123ViewModel ViewModel
    {
        get
        {
            return (Test123ViewModel)this.BindingContext;
        }
        set
        {
            this.BindingContext = value;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var builder = new StringBuilder();

        var vm = ViewModel;

        builder.AppendLine("Button clicked.");
        builder.Append("String Field: ");
        builder.AppendLine(vm.StringField.Value);
        builder.Append("Visible: ");
        builder.AppendLine(vm.StringField.IsVisible.ToString());
        builder.Append("Valid: ");
        builder.AppendLine(vm.StringField.IsValid.ToString());

        builder.Append("Int Field: ");
        builder.AppendLine(vm.IntField.Value.ToString());
        builder.Append("Visible: ");
        builder.AppendLine(vm.IntField.IsVisible.ToString());
        builder.Append("Valid: ");
        builder.AppendLine(vm.IntField.IsValid.ToString());

        SummarizeSingleSelect(builder, 
            vm.SingleSelectField, 
            "Combobox Single Select Field");

        SummarizeSingleSelect(builder,
            vm.ListboxSingleSelectField,
            "Listbox Single Select Field");

        await DisplayAlert("Button Clicked", builder.ToString(), "OK");
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

    private void ToggleVisibility_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        vm.StringField.IsVisible = !vm.StringField.IsVisible;
        vm.IntField.IsVisible = !vm.IntField.IsVisible;
        vm.SingleSelectField.IsVisible = !vm.SingleSelectField.IsVisible;
        vm.ListboxSingleSelectField.IsVisible = !vm.ListboxSingleSelectField.IsVisible;
    }

    private void ToggleEnabled_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;
        
        vm.StringField.IsEnabled = !vm.StringField.IsEnabled;
        vm.IntField.IsEnabled = !vm.IntField.IsEnabled;
        vm.SingleSelectField.IsEnabled = !vm.SingleSelectField.IsEnabled;
        vm.ListboxSingleSelectField.IsEnabled = !vm.ListboxSingleSelectField.IsEnabled;
    }

    private void ChangeComboboxSelection_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        ChangeFieldSelection(vm.SingleSelectField);
    }

    private void ChangeListboxSelection_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        ChangeFieldSelection(vm.ListboxSingleSelectField);
    }

    private static void ChangeFieldSelection(SingleSelectListViewModel field)
    {
        var itemCount = field.Items.Count;

        if (field.SelectedItem == null)
        {
            field.Items[0].IsSelected = true;
        }
        else
        {
            var indexOf = field.Items.IndexOf(field.SelectedItem);

            var newIndex = indexOf + 1;

            if (newIndex >= itemCount)
            {
                newIndex = 0;
            }

            field.Items[newIndex].IsSelected = true;
        }
    }
}

