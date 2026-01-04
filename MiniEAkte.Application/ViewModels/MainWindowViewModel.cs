using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Application.ViewModels.Cases;
using MiniEAkte.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using MiniEAkte.Application.Services.CaseServices;
using MiniEAkte.Application.ViewModels.Base;
using MiniEAkte.Domain.Entities;

namespace MiniEAkte.Application.ViewModels
{
    
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ICaseFileService _caseFileService;
        private readonly IAuthorizationService _auth;
        private CaseFile? _selectedCaseFile;

        public CaseFileListViewModel CaseFiles { get; }
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

        public MainWindowViewModel(IAuthorizationService auth, ICaseFileService caseFileService,
            CaseFileListViewModel caseFileListVm)
        {
            _auth = auth;
            _caseFileService = caseFileService;
            CaseFiles = caseFileListVm;

            CloseCaseCommand = new AsyncRelayCommand(
                CloseCaseAsync,
                CanCloseCase);

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
