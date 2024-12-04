/*
    Program Author: Reham Afzal 
    ID: w10171356 
    Assignment: Password Manager part 2
*/

using System.Collections.ObjectModel;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class PasswordListView : ContentPage
{
    private ObservableCollection<PasswordRow> _rows = new ObservableCollection<PasswordRow>();

    public PasswordListView()
    {
        InitializeComponent();
        App.PasswordController.GenTestPasswords(); // Generate test passwords
        App.PasswordController.PopulatePasswordView(_rows); // Populate password view
        collPasswords.ItemsSource = _rows; // Bind password list to the CollectionView
    }

    private void TextSearchBar(object sender, TextChangedEventArgs e)
    {
        App.PasswordController.PopulatePasswordView(_rows, e.NewTextValue); // Filter passwords based on search input
    }

    private void CopyPassToClipboard(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as Button).CommandParameter);
        var password = App.PasswordController.GetPassword(ID);
        if (password != null)
        {
            var decryptedPassword = App.PasswordController.DecryptPassword(password.PasswordText);
            Clipboard.SetTextAsync(decryptedPassword);
            DisplayAlert("Success", "Password copied to clipboard.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Password not found.", "OK");
        }
    }

    private void EditPassword(object sender, EventArgs e)
    {
        var btnSender = sender as Button;
        if (btnSender == null) return;

        int ID = Convert.ToInt32(btnSender.CommandParameter);
        var passwordRow = _rows.FirstOrDefault(row => row.PasswordID == ID);
        if (passwordRow == null)
        {
            DisplayAlert("Error", "Password not found.", "OK");
            return;
        }

        if (passwordRow.Editing)
        {
            passwordRow.SavePassword(); // Save changes
            btnSender.Text = "Edit";
        }
        else
        {
            passwordRow.Editing = true; // Enable edit mode
            btnSender.Text = "Save";
        }
    }

    private void DeletePassword(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as Button).CommandParameter);

        if (App.PasswordController.RemovePassword(ID))
        {
            App.PasswordController.PopulatePasswordView(_rows); // Refresh password list
            DisplayAlert("Success", "Password deleted successfully.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Password not found.", "OK");
        }
    }

    private void ButtonAddPassword(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AddPasswordView(); // Navigate to Add Password page
    }

    private void ToggleShowPassword(object sender, ToggledEventArgs e)
    {
        var switchControl = sender as Switch;
        if (switchControl == null) return;

        var passwordRow = switchControl.BindingContext as PasswordRow;
        if (passwordRow != null)
        {
            passwordRow.IsShown = switchControl.IsToggled;

            if (!passwordRow.IsShown)
            {
                passwordRow.PlatformPassword = "********"; // Mask password
            }
            else
            {
                var passwordModel = App.PasswordController.GetPassword(passwordRow.PasswordID);
                if (passwordModel != null)
                {
                    passwordRow.PlatformPassword = App.PasswordController.DecryptPassword(passwordModel.PasswordText);
                }
            }

            collPasswords.ItemsSource = null;
            collPasswords.ItemsSource = _rows; // Refresh UI
        }
    }
}
