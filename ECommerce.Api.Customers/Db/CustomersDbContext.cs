using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Db
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
