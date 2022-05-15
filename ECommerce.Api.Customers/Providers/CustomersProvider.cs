using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedCustomers();
        }

        private void SeedCustomers()
        {
            if (dbContext.Customers.Any())
            {
                return;
            }
            dbContext.Customers.Add(new Db.Customer { Id = 1, Name = "Reza", Address = "2525 Cavendish blvd" });
            dbContext.Customers.Add(new Db.Customer { Id = 2, Name = "Parsa", Address = "126 Fielding" });
            dbContext.Customers.Add(new Db.Customer { Id = 3, Name = "Fatemeh", Address = "15 Concordia" });
            dbContext.SaveChanges();
        }

        public async Task<(bool IsSuccess, IEnumerable<Model.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Model.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
            

        }

        public async Task<(bool IsSuccess, Model.Customer Customer, string ErrorMessage)> GetCustomersAsync(int Id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == Id);
                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Model.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
            
        }
    }
}
