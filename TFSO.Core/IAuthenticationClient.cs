using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService;

namespace TFSO.Core
{
    public interface IAuthenticationClient
    {
        string SessionId { get; }
        Task LoginAsync(string username, string password);
        Task<List<Identity>> GetIdentitiesAsync();
        Task<bool> SetIdentityAsync(string identity);
    }
}
