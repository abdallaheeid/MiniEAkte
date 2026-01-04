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

    }
}
