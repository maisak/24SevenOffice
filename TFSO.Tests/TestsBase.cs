using NUnit.Framework;
using System.Threading.Tasks;
using TFSO.Core;
using TFSO.Core.Toolbox;

namespace TFSO.Tests
{
    [TestFixture]
    public class TestsBase
    {
        internal IAuthenticationClient AuthenticationClient;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            AuthenticationClient = new AuthenticationClient();
            await AuthenticationClient.LoginAsync(Configuration.Instance.Settings["username"], Configuration.Instance.Settings["password"]);
        }
    }
}
