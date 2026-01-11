using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Application.Services.CaseServices;
using MiniEAkte.Application.ViewModels.Base;
using MiniEAkte.Application.ViewModels.Cases;
using MiniEAkte.Domain.Entities;
using MiniEAkte.Domain.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
namespace MiniEAkte.UI.MainUIViewModel
{
    
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ICaseFileService _caseFileService;
        private readonly IAuthorizationService _auth;
        private CaseFile? _selectedCaseFile;

        public ObservableCollection<CaseFile> CaseFiles => CaseFilesViewModel.CaseFiles;
        public ICollectionView CaseFilesView { get; }

        public CaseFileListViewModel CaseFilesViewModel { get; }

        public CaseFile? SelectedCaseFile
        {
            get => _selectedCaseFile;
            set
            {
                _selectedCaseFile = value;
                OnPropertyChanged();
                ((AsyncRelayCommand)CloseCaseCommand).RaiseCanExecuteChanged();
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                CaseFilesView.Refresh();
            }
        }

        public MainWindowViewModel(IAuthorizationService auth, ICaseFileService caseFileService,
            CaseFileListViewModel caseFileListVm)
        {
            _auth = auth;
            _caseFileService = caseFileService;
            CaseFilesViewModel = caseFileListVm;

            CloseCaseCommand = new AsyncRelayCommand(
                CloseCaseAsync,
                CanCloseCase);
            CaseFilesView = CollectionViewSource.GetDefaultView(CaseFilesViewModel.CaseFiles);
            CaseFilesView.Filter = FilterCases;

        }

        private bool FilterCases(object obj)
        {
            if (obj is not CaseFile caseFile)
                return false;

            if (string.IsNullOrWhiteSpace(SearchText))
                return true;

            var text = SearchText.ToLowerInvariant();

            return
                caseFile.FileNumber != null && (caseFile.FileNumber.ToLowerInvariant().Contains(text) ||
                                                caseFile.Title.ToLowerInvariant().Contains(text) ||
                                                caseFile.Status.ToString().ToLowerInvariant().Contains(text) ||
                                                caseFile.Owner.ToLowerInvariant().Contains(text));
        }


        public ICommand CloseCaseCommand { get; }

        public bool IsAdmin => _auth.HasRole(UserRole.Admin);

        private bool CanCloseCase()
        {
            return SelectedCaseFile != null
                   && IsAdmin
                   && SelectedCaseFile.Status != CaseStatus.Closed;
        }

        private async Task CloseCaseAsync()
        {
            if (SelectedCaseFile == null) return;

            await _caseFileService.CloseAsync(SelectedCaseFile.Id);
            SelectedCaseFile.Status = CaseStatus.Closed;
            OnPropertyChanged(nameof(SelectedCaseFile));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
