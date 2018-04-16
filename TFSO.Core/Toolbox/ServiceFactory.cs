namespace TFSO.Core.Toolbox
{
    public class ServiceFactory
    {
        private readonly string _baseAddress;
        private readonly string _applicationId;

        private AuthenticationClient _authClient;
        private CompanyClient _companyClient;
        private ProductClient _productClient;

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

        public ProductClient GetProductClient()
        {
            return _productClient ?? (_productClient = new ProductClient(_authClient.SessionId));
        }
    }
}
