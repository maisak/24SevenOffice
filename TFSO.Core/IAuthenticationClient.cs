using AuthenticationService;
using System.Collections.Generic;
using System.Threading.Tasks;

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
