using ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TFSO.Core.Toolbox;

namespace TFSO.Core
{
    public class ProductClient
    {
        private const string SessionCookieName = "ASP.NET_SessionId";
        private const string BdcCategoryName = "BillDotCom";
        private readonly DateTime _defaultSearchDate = new DateTime(2000, 1, 1);
        private readonly ProductServiceSoapClient _productService;

        public ProductClient(string sessionId)
        {
            _productService = new ProductServiceSoapClient(ProductServiceSoapClient.EndpointConfiguration.ProductServiceSoap);
            var cookies = new CookieContainer();
            cookies.Add(_productService.Endpoint.Address.Uri, new Cookie(SessionCookieName, sessionId));
            _productService.Endpoint.EndpointBehaviors.Add(new CookieBehavior(cookies));
        }

        #region Save
        public async Task<Product> SaveProduct(Product product)
        {
            if (product.CategoryId <= 0)
            {
                var category = GetCategories().Result.FirstOrDefault(x => x.Name == BdcCategoryName);
                var bdcCategoryId = category?.Id ?? CreateCategory(new Category { Name = BdcCategoryName }).Result.Id;
                product.CategoryId = bdcCategoryId;
            }

            var products = await _productService.SaveProductsAsync(new[] { product });

            return products.FirstOrDefault();
        }
        #endregion

        #region Get
        internal async Task<List<Product>> GetAllProducts()
        {
            return await GetProducts(new ProductSearchParameters { DateChanged = _defaultSearchDate });
        }

        internal async Task<Product> GetProduct(string name)
        {
            var returnedProducts = await GetProducts(new ProductSearchParameters { Name = name });
            return returnedProducts[0];
        }

        public async Task<Product> GetProduct(int id)
        {
            var returnedProducts = await GetProducts(new ProductSearchParameters { Id = id });
            return returnedProducts.FirstOrDefault();
        }

        public async Task<List<Product>> GetProducts(DateTime from)
        {
            var psp = new ProductSearchParameters { DateChanged = from.AddSeconds(-from.Second).AddSeconds(-1) }; // this time manipulation is needed because 24so service is ROUNDING (not truncating) company creation time
            return await GetProducts(psp);
        }

        private async Task<List<Product>> GetProducts(ProductSearchParameters psp)
        {
            string[] returnProps = { "Id", "Name", "Description", "WebPrice", "Price", "CategoryId", "DateCreated", "DateChanged" };

            var products = await _productService.GetProductsAsync(psp, returnProps);

            return products.ToList();
        }
        #endregion

        #region Delete
        public async Task DeleteProducts(DateTime from)
        {
            var productsToDelete = await GetProducts(from);
            await DeleteProducts(productsToDelete.ToArray());
        }

        private async Task DeleteProducts(Product[] products)
        {
            await _productService.DeleteProductsAsync(products);
        }
        #endregion


        #region Categories
        internal async Task<List<Category>> GetCategories()
        {
            string[] returnProps = { "Id", "Name" };

            var products = await _productService.GetCategoriesAsync(returnProps);

            return products.ToList();
        }

        internal async Task<Category> CreateCategory(Category category)
        {
            var createdCategories = await _productService.SaveCategoriesAsync(new[] { category });

            return createdCategories.FirstOrDefault();
        }
        #endregion

    }
}
