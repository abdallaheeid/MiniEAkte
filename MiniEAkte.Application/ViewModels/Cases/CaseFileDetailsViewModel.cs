using MiniEAkte.Application.Services.CaseServices;
using MiniEAkte.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MiniEAkte.Application.ViewModels.Base;
using AsyncRelayCommand = CommunityToolkit.Mvvm.Input.AsyncRelayCommand;

namespace MiniEAkte.Application.ViewModels.Cases
{
    public class CaseFileDetailsViewModel : INotifyPropertyChanged
    {

        private readonly ICaseFileService _caseFileService;
        private readonly int _caseFileId;
        private CaseFile? _caseFile;
        private string? _selectedFilePath;

        public CaseFile? CaseFile
        {
            get => _caseFile;
            private set
            {
                _caseFile = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Document> Documents { get; }
            = new();

        public CaseFileDetailsViewModel(
            int caseFileId,
            ICaseFileService caseFileService)
        {
            _caseFileId = caseFileId;
            _caseFileService = caseFileService;

            UploadDocumentCommand = new AsyncRelayCommand<string>(OnUploadDocumentAsync);

            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            CaseFile = await _caseFileService.GetByIdAsync(_caseFileId);

            if (CaseFile == null)
                return;

            var docs = await _caseFileService
                .GetDocumentsForCaseAsync(_caseFileId);

            Documents.Clear();
            foreach (var doc in docs)
                Documents.Add(doc);
        }

        public ICommand UploadDocumentCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task OnUploadDocumentAsync(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;
            _selectedFilePath = filePath;

            var document = await _caseFileService.AddDocumentAsync(_caseFileId, filePath);
            Documents.Insert(0, document);

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
