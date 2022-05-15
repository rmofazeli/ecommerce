using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly IMapper mapper;
        public ILogger<ProductProvider> logger { get; }

        public ProductProvider(ProductsDbContext dbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (dbContext.Products.Any())
            {
                return;
            }
            dbContext.Products.Add(new Db.Product { Id = 1, Name = "Mouse", Price = 20, Inventory = 2000 });
            dbContext.Products.Add(new Db.Product { Id = 2, Name = "Case", Price = 50, Inventory = 200 });
            dbContext.Products.Add(new Db.Product { Id = 3, Name = " ", Price = 17, Inventory = 450 });
            dbContext.Products.Add(new Db.Product { Id = 4, Name = "Monitor", Price = 250, Inventory = 600 });
            dbContext.Products.Add(new Db.Product { Id = 5, Name = "CPU", Price = 500, Inventory = 800 });
            dbContext.SaveChanges();
        }

        public async Task<(bool IsSuccess, IEnumerable<Model.Product> Products, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products!=null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Model.Product>>(products);
                    return (true, result,null);
                }
                return (false, null, "Not Found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
            
        }

        public async Task<(bool IsSuccess, Model.Product Product, string ErrorMessage)> GetProductAsync(int Id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == Id);
                if (product!=null)
                {
                    var result = mapper.Map<Db.Product,Model.Product>(product);
                    return (true, result,null);
                }
                return (false, null, "Not Found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return(false, null, ex.Message);
            }
            

        }
    }
}
