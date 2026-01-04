using MiniEAkte.Application.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using MiniEAkte.Application.ViewModels.Base;

namespace MiniEAkte.Application.ViewModels.Users
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        private readonly IAuthService _authService;

        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy;
        public event Action? LoginSucceeded;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            LoginCommand = new AsyncRelayCommand(LoginAsync, () => !IsBusy);
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
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
                ((AsyncRelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand LoginCommand { get; }

        private async Task LoginAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            var success = await _authService.LoginAsync(Username, Password);

            if (!success)
            {
                ErrorMessage = "Invalid username or password.";
                IsBusy = false;
                return;
            }

            // SUCCESS:
            // Navigation to MainWindow will happen in the View (later)
            IsBusy = false;

            LoginSucceeded?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
