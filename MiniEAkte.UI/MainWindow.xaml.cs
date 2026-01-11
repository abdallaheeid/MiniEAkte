using Microsoft.Extensions.DependencyInjection;
using MiniEAkte.Application.ViewModels;
using MiniEAkte.Application.ViewModels.Cases;
using MiniEAkte.UI.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiniEAkte.UI.MainUIViewModel;

namespace MiniEAkte.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        DataContext = mainWindowViewModel;
    }

    private async void CreateCaseFile_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var dialog = App.Services.GetRequiredService<CreateCaseFileView>();
            dialog.Owner = this;
            dialog.ShowDialog();

            await ((MainWindowViewModel)DataContext).CaseFilesViewModel.LoadAsync();
        }
        catch (Exception ee)
        {
            // ReSharper disable once AsyncVoidThrowException
            throw new Exception("Error by Creating new Case", ee);
        }
    }

    private void OpenCaseFileDetailsWindow(object sender, MouseButtonEventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
            return;

        if (vm.SelectedCaseFile == null)
            return;

        var viewModel = ActivatorUtilities.CreateInstance<CaseFileDetailsViewModel>(
            App.Services,
            vm.SelectedCaseFile.Id);

        var view = new CaseFileDetailsView(viewModel)
        {
            Owner = this
        };

        view.ShowDialog();
    }
}