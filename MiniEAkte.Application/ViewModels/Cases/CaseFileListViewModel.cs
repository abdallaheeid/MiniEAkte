using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MiniEAkte.Application.Services.CaseServices;
using MiniEAkte.Domain.Entities;

namespace MiniEAkte.Application.ViewModels.Cases
{
    public class CaseFileListViewModel
    {
        private readonly ICaseFileService _caseFileService;
        public ObservableCollection<CaseFile> CaseFiles { get; private set; } = new ();

        public CaseFileListViewModel(ICaseFileService caseFileService)
        {
            _caseFileService = caseFileService;
            LoadAsync();
        }


        public async Task LoadAsync()
        {
            var items = await _caseFileService.GetAllCaseFilesAsync();

            CaseFiles.Clear();
            foreach (var item in items)
            {
                CaseFiles.Add(item);
            }
        }

    }
}
