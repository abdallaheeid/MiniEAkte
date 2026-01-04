using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Infra.Data;
using MiniEAkte.Infra.Identity;

namespace MiniEAkte.Application.Auth.Services
{
    public class AuthService(
        AppDbContext db,
        PasswordHasher passwordHasher,
        ICurrentUserContext currentUser) : IAuthService
    {

        private readonly AppDbContext _db = db;
        private readonly PasswordHasher _passwordHasher = passwordHasher;
        private readonly ICurrentUserContext _currentUser = currentUser;

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await _db.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return false;

            var valid = _passwordHasher.VerifyHashedPassword(
                user.PasswordHash,
                password,
                user);

            if (!valid)
                return false;

            _currentUser.SetUser(user.Id, user.Roles);
            return true;
        }

        public void Logout()
        {
            _currentUser.Clear();
        }
    }
}
