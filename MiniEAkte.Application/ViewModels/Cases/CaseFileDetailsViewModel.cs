using MiniEAkte.Application.Services.CaseServices;
using MiniEAkte.Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MiniEAkte.Domain.Enums;


namespace MiniEAkte.Application.ViewModels.Cases
{
    public class CaseFileDetailsViewModel : INotifyPropertyChanged
    {

        private readonly ICaseFileService _caseFileService;
        private readonly int _caseFileId;
        private CaseFile? _caseFile;

        public bool CanUploadDocument => 
            CaseFile != null && 
            CaseFile.Status != CaseStatus.Closed;

        public bool CanDeleteDocument =>
            SelectedDocument != null &&
            CaseFile != null &&
            CaseFile.Status != CaseStatus.Closed;

        public Document? SelectedDocument { get; set; }

        public CaseFile? CaseFile
        {
            get => _caseFile;
            private set
            {
                _caseFile = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanUploadDocument));
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
            DeleteDocumentCommand = new AsyncRelayCommand<string>(OnDeleteDocumentAsync);

            _ = LoadAsync();
        }

        private async Task OnDeleteDocumentAsync(string? arg)
        {
            if (CanDeleteDocument && SelectedDocument != null)
            {
                await _caseFileService.DeleteDocumentAsync(SelectedDocument.Id);
                Documents.Remove(SelectedDocument);
            }
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
        public ICommand DeleteDocumentCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task OnUploadDocumentAsync(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            var document = await _caseFileService.AddDocumentAsync(_caseFileId, filePath);
            Documents.Insert(0, document);

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
