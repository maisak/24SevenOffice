using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TFSO.Core
{
    public interface IAuthenticationClient
    {
        Task LoginAsync(string username, string password);
        string SessionId { get; }
    }
}
