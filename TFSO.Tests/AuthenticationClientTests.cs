using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TFSO.Core.Toolbox;

namespace TFSO.Tests
{
    public class AuthenticationClientTests : TestsBase
    {
        [Test]
        public async Task GetIdentitiesTest()
        {
            var identities = await AuthenticationClient.GetIdentitiesAsync();
            Assert.Greater(identities.Count, 0);
        }

        [Test]
        public async Task SetIdentityTest()
        {
            var success = await AuthenticationClient.SetIdentityAsync(Configuration.Instance.Settings["identity"]);
            Assert.IsTrue(success);
        }
    }
}
