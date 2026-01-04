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
using Microsoft.Extensions.DependencyInjection;
using MiniEAkte.Application.ViewModels;
using MiniEAkte.UI.Views;

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

            await ((MainWindowViewModel)DataContext).CaseFiles.LoadAsync();
        }
        catch (Exception ee)
        {
            throw new Exception("Error by Creating new Case", ee);
        }
    }
}