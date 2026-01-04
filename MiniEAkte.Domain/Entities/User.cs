using System;
using System.Collections.Generic;
using System.Text;
using MiniEAkte.Domain.Enums;

namespace MiniEAkte.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<UserRole> Roles { get; set; } = new();

        public bool HasRole(UserRole role)
        {
            return Roles.Contains(role);
        } 
    }
}
