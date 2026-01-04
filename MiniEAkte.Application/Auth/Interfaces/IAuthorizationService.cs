using System;
using System.Collections.Generic;
using System.Text;
using MiniEAkte.Domain.Enums;

namespace MiniEAkte.Application.Auth.Interfaces
{
    public interface IAuthorizationService
    {
        bool HasRole(UserRole role);
        void DemandRole(UserRole role);
    }
}
