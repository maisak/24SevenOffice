using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
