using System;
using System.Collections.Generic;
using System.Text;
using MiniEAkte.Domain.Enums;

namespace MiniEAkte.Domain.Entities
{
    public class CaseFile
    {
        public int Id { get; set; }
        public string FileNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public CaseStatus Status { get; set; } = CaseStatus.Open;

        public string Owner { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ClosedAt { get; set; } = DateTime.UtcNow;
    }
}
