using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TFSO.Tests
{
    public class CompanyClientTests : TestsBase
    {
        [Test]
        public async Task GetCompaniesTest()
        {
            var companies = await ServiceFactory.GetCompanyClient().GetCompaniesAsync(new DateTime(2000, 1, 1));
            Assert.Greater(companies.Count, 0);
        }
    }
}
