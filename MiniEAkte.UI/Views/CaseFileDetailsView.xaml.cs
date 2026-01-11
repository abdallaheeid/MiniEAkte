using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MiniEAkte.Application.ViewModels.Cases;

namespace MiniEAkte.UI.Views
{
    /// <summary>
    /// Interaction logic for CaseFileDetailsView.xaml
    /// </summary>
    public partial class CaseFileDetailsView : Window
    {
        public CaseFileDetailsView(CaseFileDetailsViewModel caseFileDetailsViewModel)
        {
            InitializeComponent();
            DataContext = caseFileDetailsViewModel;
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {

            var fileDialog = new OpenFileDialog
            {
                Title = "Select a PDF File",
                Multiselect = false,
                Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*"
            };

            if (fileDialog.ShowDialog() == true && DataContext is CaseFileDetailsViewModel vm)
            {
                vm.UploadDocumentCommand.Execute(fileDialog.FileName);
            }

        }

        private void OpenPreviewWindow(object sender, MouseButtonEventArgs e)
        {
            // NO Business Logic in the View!

            if (DataContext is not CaseFileDetailsViewModel vm)
                return;
            if (vm.SelectedDocument is null)
                return;

            var previewWindow = App.Services.GetRequiredService<DocumentPreviewView>();

            if(previewWindow.Initialize(vm.SelectedDocument))
                previewWindow.ShowDialog();
        }
    }
}
