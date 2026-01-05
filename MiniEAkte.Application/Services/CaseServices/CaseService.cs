using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Domain.Entities;
using MiniEAkte.Domain.Enums;
using MiniEAkte.Infra.Data;

namespace MiniEAkte.Application.Services.CaseServices
{
    public class CaseService : ICaseFileService
    {

        private readonly AppDbContext _db;
        private readonly IAuthorizationService _auth;

        public CaseService(AppDbContext db, IAuthorizationService auth)
        {
            _db = db;
            _auth = auth;
        }

        public async Task<List<CaseFile>> GetAllCaseFilesAsync()
        {
            return await _db.CaseFiles.AsNoTracking().OrderByDescending(c=>c.CreatedAt).ToListAsync();
        }

        public async Task CreateAsync(
            string fileNumber,
            string title,
            string owner,
            CaseStatus status)
        {
            _auth.DemandRole(UserRole.Admin);

            var exists = await _db.CaseFiles
                .AnyAsync(c => c.FileNumber == fileNumber);

            if (exists)
                throw new InvalidOperationException("File number already exists.");

            var caseFile = new CaseFile
            {
                FileNumber = fileNumber,
                Title = title,
                Owner = owner,
                Status = status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.CaseFiles.Add(caseFile);
            await _db.SaveChangesAsync();
        }

        public async Task CloseAsync(int caseFileId)
        {
            _auth.DemandRole(UserRole.Admin);

            var caseFile = await _db.CaseFiles.SingleOrDefaultAsync(c => c.Id == caseFileId);

            if (caseFile == null)
            {
                throw new InvalidOperationException("Case File not found");
            }

            if (caseFile.Status == CaseStatus.Closed)
            {
                throw new InvalidOperationException("Case File is already closed");
            }

            caseFile.Status = CaseStatus.Closed;
            caseFile.ClosedAt = DateTime.UtcNow;
            caseFile.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

        }

        public async Task<CaseFile?> GetByIdAsync(int caseFileId)
        {
            return await _db.CaseFiles.AsNoTracking().SingleOrDefaultAsync(c => c.Id == caseFileId);
        }

        public async Task<List<Document>> GetDocumentsForCaseAsync(int caseFileId)
        {
            return await _db.Documents.AsNoTracking().Where(d => d.CaseFileId == caseFileId)
                .OrderByDescending(d => d.CreatedAt).ToListAsync();
        }

        public async Task<Document> AddDocumentAsync(int caseFileId, string sourceFilePath)
        {
            _auth.DemandRole(UserRole.Admin);

            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("Source file not found.");

            var caseFile = await _db.CaseFiles.SingleOrDefaultAsync(c => c.Id == caseFileId);
            if (caseFile == null)
                throw new InvalidOperationException("Case File not found");

            if (caseFile.Status == CaseStatus.Closed)
                throw new InvalidOperationException("Cannot add document to a closed case");

            var baseFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "MiniEAkte",
                "Files",
                $"case_{caseFileId}");

            Directory.CreateDirectory(baseFolder);

            var extension = Path.GetExtension(sourceFilePath);
            var storedFileName = $"{Guid.NewGuid()}{extension}";
            var destinationPath = Path.Combine(baseFolder, storedFileName);

            File.Copy(sourceFilePath, destinationPath);

            var document = new Document
            {
                CaseFileId = caseFileId,
                DocumentType = extension.Trim('.').ToUpperInvariant(),
                Subject = Path.GetFileName(sourceFilePath),
                Tags = string.Empty,
                FilePath = destinationPath,
                CreatedAt = DateTime.UtcNow
            };

            _db.Documents.Add(document);
            await _db.SaveChangesAsync();

            return document;
        }

    }
}
