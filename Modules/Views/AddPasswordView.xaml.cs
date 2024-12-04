/*
    Program Author:  Reham Afzal 
    ID: w1071356   
    Assignment: Password Manager part 2
*/

namespace CSC317PassManagerP2Starter.Modules.Views;
using CSC317PassManagerP2Starter.Modules.Models;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    bool generatedPassword;

    public AddPasswordView()
    {
        InitializeComponent();
        generatedPassword = false;
    }

    public AddPasswordView(PasswordModel password = null)
    {
        InitializeComponent();
        if (password != null)
        {
            txtNewPlatform.Text = password.PlatformName;
            txtNewUserName.Text = password.PlatformUserName;
            txtNewPassword.Text = App.PasswordController.DecryptPassword(password.PasswordText);
            txtNewPasswordVerify.Text = txtNewPassword.Text;
        }
        generatedPassword = false;
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
        Application.Current.MainPage = new PasswordListView();
    }

    private void ButtonSubmitExisting(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNewPlatform.Text) ||
            string.IsNullOrWhiteSpace(txtNewUserName.Text) ||
            string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
            txtNewPassword.Text != txtNewPasswordVerify.Text)
        {
            lblErrorExistingPassword.Text = "Invalid input or passwords do not match!";
            lblErrorExistingPassword.IsVisible = true;
            return;
        }

        App.PasswordController.AddPassword(
            txtNewPlatform.Text,
            txtNewUserName.Text,
            txtNewPassword.Text
        );

        Application.Current.MainPage = new PasswordListView();
    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        if (!generatedPassword || string.IsNullOrWhiteSpace(lblGenPassword.Text) || lblGenPassword.Text == "<No Password Generated>")
        {
            lblErrorGeneratedPassword.Text = "Please generate a password first.";
            lblErrorGeneratedPassword.IsVisible = true;
            return;
        }

        App.PasswordController.AddPassword(
            txtNewPlatform.Text,
            txtNewUserName.Text,
            lblGenPassword.Text
        );

        Application.Current.MainPage = new PasswordListView();
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
        string newPassword = PasswordGeneration.BuildPassword(
            chkUpperLetter.IsChecked,
            chkDigit.IsChecked,
            txtReqSymbols.Text,
            (int)sprPassLength.Value
        );

        lblGenPassword.Text = newPassword;
        generatedPassword = true;
        lblErrorGeneratedPassword.IsVisible = false;
    }
}
