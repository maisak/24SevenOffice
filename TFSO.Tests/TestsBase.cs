using NUnit.Framework;
using System.Threading.Tasks;
using TFSO.Core;
using TFSO.Tests.Toolbox;

namespace TFSO.Tests
{
    [TestFixture]
    public class TestsBase
    {
        internal AuthenticationClient AuthenticationClient;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            AuthenticationClient = new AuthenticationClient(Configuration.Instance.Settings["baseAddress"], Configuration.Instance.Settings["applicationId"]);
            await AuthenticationClient.LoginAsync(Configuration.Instance.Settings["username"], Configuration.Instance.Settings["password"]);
        }
    }
}
