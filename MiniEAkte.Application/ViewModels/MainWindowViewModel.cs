using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Application.ViewModels.Cases;
using MiniEAkte.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MiniEAkte.Application.ViewModels
{
    
    public class MainWindowViewModel
    {
        private readonly IAuthorizationService _auth;
        public CaseFileListViewModel CaseFiles { get; }

        public MainWindowViewModel(IAuthorizationService auth, CaseFileListViewModel caseFileListVm)
        {
            _auth = auth;
            CaseFiles = caseFileListVm;
        }

        public bool IsAdmin => _auth.HasRole(UserRole.Admin);
    }
}
