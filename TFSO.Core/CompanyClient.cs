using CompanyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TFSO.Core.Toolbox;

namespace TFSO.Core
{
    public class CompanyClient : ICompanyClient
    {
        #region
        private const string SessionCookieName = "ASP.NET_SessionId";
        private const string DomainName = "api.24sevenoffice.com";
        private readonly DateTime _defaultSearchDate = new DateTime(2000, 1, 1);
        private readonly CompanyServiceSoapClient _companyService; 
        #endregion

        public CompanyClient(string sessionId)
        {
            _companyService = new CompanyServiceSoapClient(CompanyServiceSoapClient.EndpointConfiguration.CompanyServiceSoap);
            var cookies = new CookieContainer();
            cookies.Add(_companyService.Endpoint.Address.Uri, new Cookie(SessionCookieName, sessionId) { Domain = DomainName });
            _companyService.Endpoint.EndpointBehaviors.Add(new CookieBehavior(cookies));
        }

        #region Save

        public async Task<Company> SaveCompanyAsync(Company company)
        {
            var result = await _companyService.SaveCompaniesAsync(new[] { company });
            return result.FirstOrDefault();
        }

        #endregion

        #region Get

        public async Task<Company> GetCompanyAsync(int companyId)
        {
            var csp = new CompanySearchParameters { CompanyId = companyId, ChangedAfter = _defaultSearchDate };
            var returnedCompanies = await GetCompaniesAsync(csp);
            return returnedCompanies.FirstOrDefault();
        }
        
        public async Task<Company> GetCompanyAsync(string companyName)
        {
            var csp = new CompanySearchParameters { CompanyName = companyName, ChangedAfter = _defaultSearchDate };
            var returnedCompanies = await GetCompaniesAsync(csp);
            return returnedCompanies.FirstOrDefault();
        }

        public async Task<List<Company>> GetCompaniesAsync(DateTime from)
        {
            var csp = new CompanySearchParameters { ChangedAfter = from.AddSeconds(-from.Second).AddSeconds(-1) }; // this time manipulation is needed because 24so service is ROUNDING (not truncating) company creation time
            return await GetCompaniesAsync(csp);
        }

        private async Task<List<Company>> GetCompaniesAsync(CompanySearchParameters csp)
        {
            string[] returnProps = { "Id", "Name", "Type", "Addresses", "EmailAddresses", "PhoneNumbers", "NickName", "OrganizationNumber", "VatNumber", "DateCreated", "DateChanged" };
            var companies = await _companyService.GetCompaniesAsync(csp, returnProps);

            return companies.ToList();
        } 

        #endregion

        #region Delete

        public async Task DeleteCompanies(DateTime from)
        {
            var companiesToDelete = await GetCompaniesAsync(from);
            await DeleteCompanies(companiesToDelete.ToArray());
        }

        private async Task DeleteCompanies(Company[] companies)
        {
            await _companyService.DeleteCompaniesAsync(companies);
        }

        #endregion
        
    }
}
