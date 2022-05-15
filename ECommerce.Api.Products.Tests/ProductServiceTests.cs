using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Xunit;
using AutoMapper;

namespace ECommerce.Api.Products.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async void GetProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProducts))
                .Options;
            ProductsDbContext productsDbContext = new ProductsDbContext(options);

            ProductProfile profile = new ProductProfile();  
            var configueation = new MapperConfiguration(cfg=>cfg.AddProfile(profile));
            var mapper = new Mapper(configueation);

            ProductProvider productProvider = new ProductProvider(productsDbContext,null,mapper);

            var product = await productProvider.GetProductAsync();
            Assert.True(product.IsSuccess);

        }
    }
}