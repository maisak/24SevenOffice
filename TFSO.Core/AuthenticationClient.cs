using AuthenticationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TFSO.Core.Toolbox;

[assembly: InternalsVisibleTo("TFSO.Tests")]
namespace TFSO.Core
{
    public class AuthenticationClient
    {
        #region
        private readonly Uri _baseAddress;
        private readonly Guid _applicationId;
        private const string SessionCookieName = "ASP.NET_SessionId";
        private AuthenticateSoapClient _authService;
        private IEnumerable<Identity> _identities;
        #endregion

        #region Publics

        public string SessionId { get; private set; }

        #endregion

        public AuthenticationClient(string baseAddress, string applicationId)
        {
            _baseAddress = new Uri(baseAddress);
            _applicationId = new Guid(applicationId);
        }

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

        public async Task<IEnumerable<Identity>> GetIdentitiesAsync()
        {
            return _identities ?? (_identities = await _authService.GetIdentitiesAsync());
        }

        public async Task<Identity> GetCurrentIdentityAsync()
        {
            var list = await GetIdentitiesAsync();
            return list.FirstOrDefault(x => x.IsCurrent);
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
