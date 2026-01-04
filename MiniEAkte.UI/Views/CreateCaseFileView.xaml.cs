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
using MiniEAkte.Application.ViewModels.Cases;

namespace MiniEAkte.UI.Views
{
    /// <summary>
    /// Interaction logic for CreateCaseFileView.xaml
    /// </summary>
    public partial class CreateCaseFileView : Window
    {
        public CreateCaseFileView(CreateCaseFileViewModel createCaseFileViewModel)
        {
            InitializeComponent();
            DataContext = createCaseFileViewModel;

            createCaseFileViewModel.CaseFileCreated += () =>
            {
                MessageBox.Show("Case file created successfully.", "success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            };
        }
    }
}
