using System;
using System.Collections.Generic;
using System.Text;
using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Domain.Enums;

namespace MiniEAkte.Application.Auth.Services
{
    public class CurrentUserContext : ICurrentUserContext
    {
        public int? UserId { get; private set; }

        public IReadOnlyCollection<UserRole> Roles { get; private set; }
            = Array.Empty<UserRole>();

        public void SetUser(int userId, IEnumerable<UserRole> roles)
        {
            UserId = userId;
            Roles = roles.ToList().AsReadOnly();
        }

        public bool IsInRole(UserRole role)
            => Roles.Contains(role);

        public void Clear()
        {
            UserId = null;
            Roles = Array.Empty<UserRole>();
        }
    }
}
