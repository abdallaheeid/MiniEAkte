using MiniEAkte.Domain.Entities;
using MiniEAkte.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEAkte.Application.Services.CaseServices
{
    public interface ICaseFileService
    {
        Task<List<CaseFile>> GetAllCaseFilesAsync();
        Task CreateAsync(string fileNumber, string title, string owner, CaseStatus status);
        Task CloseAsync(int caseFileId);
        Task<CaseFile?> GetByIdAsync(int caseFileId);
        Task<List<Document>> GetDocumentsForCaseAsync(int caseFileId);
        Task<Document> AddDocumentAsync(int caseFileId, string sourceFilePath);
    }
}
