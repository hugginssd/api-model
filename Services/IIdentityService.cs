using ApiModel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Options
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string Email, string Password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
