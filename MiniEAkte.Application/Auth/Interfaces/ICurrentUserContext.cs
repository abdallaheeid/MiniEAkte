using System;
using System.Collections.Generic;
using System.Text;
using MiniEAkte.Domain.Enums;

namespace MiniEAkte.Application.Auth.Interfaces
{
    public interface ICurrentUserContext
    {
        int? UserId { get; }
        IReadOnlyCollection<UserRole> Roles { get; }
        bool IsInRole(UserRole role);
        void Clear();
        public void SetUser(int userId, IEnumerable<UserRole> roles);

    }
}
