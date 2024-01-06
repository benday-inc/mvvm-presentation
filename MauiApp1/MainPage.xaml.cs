using Benday.Presentation;
using MauiApp1.ViewModels;
using System.Text;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private MessageBoxMessageManager _MessageManager = new();

    public MainPage()
    {
        InitializeComponent();

        this.BindingContext = new Test123ViewModel(_MessageManager);
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

    private void ToggleVisibility_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        vm.StringField.IsVisible = !vm.StringField.IsVisible;
        vm.LabelField.IsVisible = !vm.LabelField.IsVisible;
        vm.IntField.IsVisible = !vm.IntField.IsVisible;
        vm.SingleSelectField.IsVisible = !vm.SingleSelectField.IsVisible;
        vm.ListboxSingleSelectField.IsVisible = !vm.ListboxSingleSelectField.IsVisible;
    }

    private void ToggleEnabled_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;
        
        vm.StringField.IsEnabled = !vm.StringField.IsEnabled;
        vm.LabelField.IsEnabled = !vm.LabelField.IsEnabled;
        vm.IntField.IsEnabled = !vm.IntField.IsEnabled;
        vm.SingleSelectField.IsEnabled = !vm.SingleSelectField.IsEnabled;
        vm.ListboxSingleSelectField.IsEnabled = !vm.ListboxSingleSelectField.IsEnabled;
    }

    private void UpdateLabelField_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        vm.LabelField.Value = $"Label: {DateTime.Now}";
    }

    private bool _IsValidationErrorsVisible = false;

    private void ToggleValidationErrors_Clicked(object sender, EventArgs e)
    {
        var vm = ViewModel;

        _IsValidationErrorsVisible = !_IsValidationErrorsVisible;

        var message = _IsValidationErrorsVisible ?
            "Validation errors are now visible." :
            "Validation errors are now hidden.";

        vm.SingleSelectField.IsValid = !_IsValidationErrorsVisible;
        vm.SingleSelectField.ValidationMessage = message;

        vm.ListboxSingleSelectField.IsValid = !_IsValidationErrorsVisible;
        vm.ListboxSingleSelectField.ValidationMessage = message;

        vm.StringField.IsValid = !_IsValidationErrorsVisible;
        vm.StringField.ValidationMessage = message;

        vm.IntField.IsValid = !_IsValidationErrorsVisible;
        vm.IntField.ValidationMessage = message;

        vm.LabelField.IsValid = !_IsValidationErrorsVisible;
        vm.LabelField.ValidationMessage = message;
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

