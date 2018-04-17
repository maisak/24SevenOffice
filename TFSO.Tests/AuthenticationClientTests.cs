using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using TFSO.Tests.Toolbox;

namespace TFSO.Tests
{
    public class AuthenticationClientTests : TestsBase
    {
        [Test]
        public async Task GetIdentitiesTest()
        {
            var identities = await ServiceFactory.GetAuthenticationClient().GetIdentitiesAsync();
            Assert.Greater(identities.Count(), 0);
        }

        [Test]
        public async Task GetCurrentIdentityTest()
        {
            var identity = await ServiceFactory.GetAuthenticationClient().GetCurrentIdentityAsync();
            Assert.IsTrue(identity.IsCurrent);
        }

        [Test]
        public async Task SetIdentityTest()
        {
            var success = await ServiceFactory.GetAuthenticationClient().SetIdentityAsync(Configuration.Instance.Settings["identity"]);
            Assert.IsTrue(success);
        }
    }
}
