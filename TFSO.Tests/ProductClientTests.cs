using NUnit.Framework;
using System.Threading.Tasks;

namespace TFSO.Tests
{
    public class ProductClientTests : TestsBase
    {
        [Test]
        public async Task GetCategoriesTest()
        {
            var categories = await ServiceFactory.GetProductClient().GetCategories();
            Assert.Greater(categories.Count, 0);
        }
    }
}
