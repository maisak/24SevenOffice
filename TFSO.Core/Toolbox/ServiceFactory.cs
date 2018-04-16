using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TFSO.Core.Toolbox
{
    public class ServiceFactory
    {
        private readonly string _baseAddress;
        private readonly string _applicationId;

        private AuthenticationClient _authClient;
        private CompanyClient _companyClient;

        public ServiceFactory(string baseAddress, string applicationId)
        {
            _baseAddress = baseAddress;
            _applicationId = applicationId;
        }

        public AuthenticationClient GetAuthenticationClient()
        {
            return _authClient ?? (_authClient = new AuthenticationClient(_baseAddress, _applicationId));
        }

        public CompanyClient GetCompanyClient()
        {
            return _companyClient ?? (_companyClient = new CompanyClient(_authClient.SessionId));
        }

        public CompanyClient GetProductClient()
        {
            return _companyClient ?? (_companyClient = new CompanyClient(_authClient.SessionId));
        }
    }
}
