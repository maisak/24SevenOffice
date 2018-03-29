using NUnit.Framework;
using System.Threading.Tasks;
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
