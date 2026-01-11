using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEAkte.Application.ViewModels.Documents
{
    public class DocumentPreviewViewModel
    {

        public string FilePath { get; }
        public string FileName => Path.GetFileName(FilePath);

        public DocumentPreviewViewModel(string filePath)
        {
            FilePath = filePath;
        }

    }
}
