using System;
using System.Collections.Generic;
using System.Text;
using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Domain.Enums;

namespace MiniEAkte.Application.Auth.Services
{
    public  class AuthorizationService : IAuthorizationService
    {
        private readonly ICurrentUserContext _currentUserContext;

        public AuthorizationService(ICurrentUserContext currentUserContext)
        {
            _currentUserContext = currentUserContext;
        }

        public bool HasRole(UserRole role)
        {
            return _currentUserContext.IsInRole(role);
        }

        public void DemandRole(UserRole role)
        {
            if (!HasRole(role))
            {
                throw new UnauthorizedAccessException(
                    $"User does not have required role: {role}");
            }
        }
    }
}
