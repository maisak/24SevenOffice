using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyService;

namespace TFSO.Core
{
    public interface ICompanyClient
    {
        /// <summary>
        /// Search for all companies which were added/changed after date specified
        /// </summary>
        /// <param name="from">DateTime to start search from</param>
        /// <returns>List of companies</returns>
        Task<List<Company>> GetCompaniesAsync(DateTime from);

        /// <summary>
        /// Search company by its id.
        /// </summary>
        /// <param name="companyId">Id of a company to search</param>
        /// <returns>Company object</returns>
        Task<Company> GetCompanyAsync(int companyId);

        /// <summary>
        /// Search company by its name.
        /// </summary>
        /// <param name="companyName">Name of a company to search</param>
        /// <returns>Company object</returns>
        Task<Company> GetCompanyAsync(string companyName);
        
        /// <summary>
        /// Creates/updates a company.
        /// </summary>
        /// <param name="company">Company object</param>
        /// <returns>Created/updated company object</returns>
        Task<Company> SaveCompanyAsync(Company company);

        /// <summary>
        /// Deletes a company.
        /// </summary>
        /// <param name="from">DateTime to delete from</param>
        /// <returns>Task</returns>
        Task DeleteCompanies(DateTime from);
    }
}