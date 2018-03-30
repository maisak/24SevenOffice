using AuthenticationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TFSO.Core.Toolbox;

namespace TFSO.Core
{
    public class AuthenticationClient
    {
        #region
        private readonly Uri _baseAddress = new Uri(Configuration.Instance.Settings["baseAddress"]);
        private readonly Guid _applicationId = new Guid(Configuration.Instance.Settings["applicationId"]);
        private const string SessionCookieName = "ASP.NET_SessionId";
        private AuthenticateSoapClient _authService;
        public string SessionId { get; private set; }
        #endregion

        #region Login

        public async Task LoginAsync(string username, string password)
        {
            _authService = new AuthenticateSoapClient(AuthenticateSoapClient.EndpointConfiguration.AuthenticateSoap);

            var credential = new Credential
            {
                Username = username,
                Password = GetMd5Hash(password),
                ApplicationId = _applicationId
            };

            SessionId = await _authService.LoginAsync(credential);

            if (string.IsNullOrEmpty(SessionId))
            {
                throw new ArgumentException("LoginAsync operation failed");
            }

            ConfigureService();
        }

        #endregion

        #region Identities

        public async Task<List<Identity>> GetIdentitiesAsync()
        {
            var identities = await _authService.GetIdentitiesAsync();
            return identities.ToList();
        }

        public async Task<bool> SetIdentityAsync(string identity)
        {
            var identities = await GetIdentitiesAsync();
            var neededIdentity = identities.First(i => i.Client.Id.ToString() == identity);
            return await _authService.SetIdentityAsync(neededIdentity);
        }

        #endregion

        #region Internals

        private void ConfigureService()
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(_baseAddress, new Cookie(SessionCookieName, SessionId));

            _authService.Endpoint.EndpointBehaviors.Add(new CookieBehavior(cookieContainer));
        }

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
