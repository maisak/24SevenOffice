using NUnit.Framework;
using System.Threading.Tasks;
using TFSO.Core;
using TFSO.Core.Toolbox;

namespace TFSO.Tests
{
    [TestFixture]
    public class TestsBase
    {
        [OneTimeSetUp]
        public async Task SetUp()
        {
            var authClient = new AuthenticationClient();
            await authClient.LoginAsync(Configuration.Instance.Settings["username"], Configuration.Instance.Settings["password"]);
        }
    }
}
