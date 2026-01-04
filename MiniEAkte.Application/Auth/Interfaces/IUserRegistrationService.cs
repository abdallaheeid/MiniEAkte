using System;
using System.Collections.Generic;
using System.Text;

namespace MiniEAkte.Application.Auth.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<bool> RegisterAsync(string username, string email, string password);
    }
}
