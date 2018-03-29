using AuthenticationService;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TFSO.Core.Toolbox;

namespace TFSO.Core
{
    public class AuthenticationClient : IAuthenticationClient
    {
        #region
        private readonly Uri _baseAddress = new Uri(Configuration.Instance.Settings["baseAddress"]);
        private readonly Guid _applicationId = new Guid(Configuration.Instance.Settings["applicationId"]);
        private readonly string _sessionCookieName = "ASP.NET_SessionId";
        public string SessionId { get; private set; }
        #endregion

        #region Login
        public async Task LoginAsync(string username, string password)
        {
            var webServiceClient = new AuthenticateSoapClient(AuthenticateSoapClient.EndpointConfiguration.AuthenticateSoap);

            var credential = new Credential
            {
                Username = username,
                Password = GetMd5Hash(password),
                ApplicationId = _applicationId
            };

            SessionId = await webServiceClient.LoginAsync(credential);

            if (string.IsNullOrEmpty(SessionId))
            {
                throw new ArgumentException("LoginAsync operation failed");
            }
        }
        #endregion

        #region
        private static string GetMd5Hash(string text)
        {
            var message = System.Text.Encoding.Unicode.GetBytes(text);

            MD5 hashString = new MD5CryptoServiceProvider();

            var hashValue = hashString.ComputeHash(message);
            return hashValue.Aggregate("", (current, x) => current + x.ToString("x2").ToLower());
        }
        #endregion
    }
}
