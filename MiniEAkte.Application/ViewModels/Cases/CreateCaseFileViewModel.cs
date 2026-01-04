using MiniEAkte.Application.Services.CaseServices;
using MiniEAkte.Application.ViewModels.Base;
using MiniEAkte.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace MiniEAkte.Application.ViewModels.Cases
{
    public class CreateCaseFileViewModel : INotifyPropertyChanged
    {
        private readonly ICaseFileService _caseFileService;

        private string _fileNumber = string.Empty;
        private string _title = string.Empty;
        private string _owner = string.Empty;
        private string _errorMessage = string.Empty;

        public CreateCaseFileViewModel
            (
            ICaseFileService caseFileService
        )
        {
            _caseFileService = caseFileService;
            CreateCommand = new AsyncRelayCommand(CreateAsync);
        }

        public string FileNumber
        {
            get => _fileNumber;
            set { _fileNumber = value; OnPropertyChanged(); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Owner
        {
            get => _owner;
            set { _owner = value; OnPropertyChanged(); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand CreateCommand { get; }

        public event Action? CaseFileCreated;

        private async Task CreateAsync()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(FileNumber) ||
                string.IsNullOrWhiteSpace(Title))
            {
                ErrorMessage = "File number and title are required.";
                return;
            }

            await _caseFileService.CreateAsync(
                FileNumber,
                Title,
                Owner,
                CaseStatus.Open);

            CaseFileCreated?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
