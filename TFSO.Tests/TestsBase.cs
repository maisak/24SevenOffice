using NUnit.Framework;
using System.Threading.Tasks;
using TFSO.Core.Toolbox;
using TFSO.Tests.Toolbox;

namespace TFSO.Tests
{
    [TestFixture]
    public class TestsBase
    {
        internal ServiceFactory ServiceFactory;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            ServiceFactory = new ServiceFactory(Configuration.Instance.Settings["baseAddress"], 
                                                Configuration.Instance.Settings["applicationId"]);
            var authClient = ServiceFactory.GetAuthenticationClient();
            await authClient.LoginAsync(Configuration.Instance.Settings["username"], Configuration.Instance.Settings["password"]);
            await authClient.SetIdentityAsync(Configuration.Instance.Settings["identity"]);
        }
    }
}
