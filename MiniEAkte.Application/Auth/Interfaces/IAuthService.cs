using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEAkte.Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        void Logout();
    }
}
