using Microsoft.EntityFrameworkCore;
using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Domain.Entities;
using MiniEAkte.Domain.Enums;
using MiniEAkte.Infra.Data;
using MiniEAkte.Infra.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEAkte.Application.Auth.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {

        private readonly AppDbContext _db;
        private readonly PasswordHasher _passwordHasher;

        public UserRegistrationService(AppDbContext db, PasswordHasher passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            bool exists = await _db.Users.AnyAsync(u => u.Username == username || u.Email == email);

            if (exists)
                return false;

            var user = new User()
            {
                Username = username,
                Email = email,
                Roles = new List<UserRole> { UserRole.Clerk }
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return true;

        }
    }
}
