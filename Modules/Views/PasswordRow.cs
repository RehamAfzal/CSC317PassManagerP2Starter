/*
    Program Author: Reham Afzal 
    ID: w10171356 
    Assignment: Password Manager part 2
*/

using System;
using System.ComponentModel;
using CSC317PassManagerP2Starter.Modules.Models;

namespace CSC317PassManagerP2Starter.Modules.Views
{
    public class PasswordRow : BindableObject, INotifyPropertyChanged
    {
        private PasswordModel _pass;
        private bool _isVisible = false;
        private bool _editing = false;

        public PasswordRow(PasswordModel source)
        {
            _pass = source;
        }

        // Binding Properties
        public string Platform
        {
            get => _pass?.PlatformName ?? "";
            set
            {
                if (_pass.PlatformName != value)
                {
                    _pass.PlatformName = value;
                    RefreshRow();
                }
            }
        }

        public string PlatformUserName
        {
            get => _pass?.PlatformUserName ?? "";
            set
            {
                if (_pass.PlatformUserName != value)
                {
                    _pass.PlatformUserName = value;
                    RefreshRow();
                }
            }
        }

        public string PlatformPassword
        {
            get
            {
                if (IsShown)
                {
                    // Show decrypted password if visible
                    return App.PasswordController.DecryptPassword(_pass.PasswordText);
                }
                else
                {
                    // Return masked password if not visible
                    return "********";
                }
            }
            set
            {
                // Encrypt and temporarily store password in the model (not yet saved to backend)
                _pass.PasswordText = App.PasswordController.EncryptPassword(value);
                RefreshRow();
            }
        }

        public int PasswordID => _pass?.ID ?? -1;

        public bool IsShown
        {
            get => _isVisible;
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    RefreshRow();
                }
            }
        }

        public bool Editing
        {
            get => _editing;
            set
            {
                if (_editing != value)
                {
                    _editing = value;
                    RefreshRow();
                }
            }
        }

        // Refreshes UI when properties change
        private void RefreshRow()
        {
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(PlatformUserName));
            OnPropertyChanged(nameof(PlatformPassword));
            OnPropertyChanged(nameof(IsShown));
            OnPropertyChanged(nameof(Editing));
        }

        public void SavePassword()
        {
            if (Editing)
            {
                // Save changes to the backend through the controller
                App.PasswordController.UpdatePassword(_pass);
                Editing = false; // Disable edit mode after saving
            }
        }
    }
}
