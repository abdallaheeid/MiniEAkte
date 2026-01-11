using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MiniEAkte.Application.ViewModels.Documents;
using MiniEAkte.Domain.Entities;

namespace MiniEAkte.UI.Views
{
    /// <summary>
    /// Interaction logic for DocumentPreviewView.xaml
    /// </summary>
    public partial class DocumentPreviewView : Window
    {
        private DocumentPreviewViewModel _vm = null!;

        public DocumentPreviewView()
        {
            InitializeComponent();
        }

        public bool Initialize(Document document)
        {

            if (document.FilePath != null && !document.FilePath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Only PDF files can be previewed.", "error");
                Close();
                return false;

            }

            if (!File.Exists(document.FilePath))
            {
                MessageBox.Show("File Not Found", "error");
                Close();
                return false;
            }

            _vm = new DocumentPreviewViewModel(document.FilePath);
            DataContext = _vm;

            Loaded += async (_, __) =>
            {
                await PdfViewer.EnsureCoreWebView2Async();
                PdfViewer.Source = new Uri(_vm.FilePath);
            };

            return true;
        }

        private void OpenExternally_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = _vm.FilePath,
                UseShellExecute = true
            });
        }
    }
}
