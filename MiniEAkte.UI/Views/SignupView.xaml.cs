using MiniEAkte.Application.ViewModels.Users;
using System.Windows;
using System.Windows.Controls;

namespace MiniEAkte.UI.Views
{
    /// <summary>
    /// Interaction logic for SignupView.xaml
    /// </summary>
    public partial class SignupView : Window
    {
        public SignupView(SignupViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignupViewModel vm &&
                sender is PasswordBox passwordBox)
            {
                vm.Password = passwordBox.Password;
            }
        }

        private void RepeatPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignupViewModel vm &&
                sender is PasswordBox passwordBox)
            {
                vm.RepeatPassword = passwordBox.Password;
            }
        }
    }
}
