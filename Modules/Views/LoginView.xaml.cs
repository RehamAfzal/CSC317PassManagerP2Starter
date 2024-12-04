/*
    Program Author: Reham Afzal 
    ID: w10171356    
    Assignment: Password Manager part 2
*/

namespace CSC317PassManagerP2Starter.Modules.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void ProcessLogin(object sender, EventArgs e)
        {
            string username = txtUserName.Text?.Trim();
            string password = txtPassword.Text?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowErrorMessage("Username and password cannot be empty.");
                return;
            }

            var authError = Authenticate(username, password);

            if (authError != AuthenticationError.NONE)
            {
                ShowErrorMessage(authError == AuthenticationError.INVALIDUSERNAME 
                                 ? "Invalid username." 
                                 : "Invalid password.");
            }
            else
            {
                Application.Current.MainPage = new PasswordListView();
            }
        }

        private AuthenticationError Authenticate(string username, string password)
        {
            if (username == "test" && password == "ab1234")
                return AuthenticationError.NONE;
            return username != "test" ? AuthenticationError.INVALIDUSERNAME : AuthenticationError.INVALIDPASSWORD;
        }

        private void ShowErrorMessage(string message)
        {
            lblError.Text = message;
            lblError.IsVisible = true;
        }
    }

    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
}
