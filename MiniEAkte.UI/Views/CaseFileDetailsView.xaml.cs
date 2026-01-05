using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
    }
}
