using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEAkte.Domain.Entities
{
    public class Document
    {
        public int Id { get; set; }

        public int CaseFileId { get; set; }

        public string DocumentType { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;

        public string? FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
