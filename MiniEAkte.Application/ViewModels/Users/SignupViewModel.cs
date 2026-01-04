using MiniEAkte.Application.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using MiniEAkte.Application.Auth.Interfaces;

namespace MiniEAkte.Application.ViewModels.Users
{
    public class SignupViewModel : INotifyPropertyChanged // When a property changes -> Update the UI
    {
        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _repeatPassword = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy;

        private readonly IUserRegistrationService _userRegistrationService;

        public SignupViewModel(IUserRegistrationService registrationService)
        {
            _userRegistrationService = registrationService;
            SignUpCommand = new AsyncRelayCommand(SignUpAsync, () => !IsBusy);
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string RepeatPassword
        {
            get => _repeatPassword;
            set { _repeatPassword = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((AsyncRelayCommand)SignUpCommand).RaiseCanExecuteChanged();
            }
        }


        public ICommand SignUpCommand { get; }


        private async Task SignUpAsync()
        {
            ErrorMessage = string.Empty;
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessage = "Username is required.";
                IsBusy = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Email is required.";
                IsBusy = false;
                return;
            }

            if (!IsValidEmail(Email))
            {
                ErrorMessage = "Email format is invalid.";
                IsBusy = false;
                return;
            }

            if (Password.Length < 6)
            {
                ErrorMessage = "Password must be at least 6 characters.";
                IsBusy = false;
                return;
            }

            if (Password != RepeatPassword)
            {
                ErrorMessage = "Passwords do not match.";
                IsBusy = false;
                return;
            }

            bool success = await _userRegistrationService.RegisterAsync(
                Username,
                Email,
                Password
            );

            if (!success)
            {
                ErrorMessage = "Username or email already exists.";
                IsBusy = false;
                return;
            }

            ErrorMessage = "Account created successfully.";
            IsBusy = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool IsValidEmail(String email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
