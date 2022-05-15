using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrderProviders : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public OrderProviders(OrdersDbContext dbContext, IMapper mapper, ILogger<OrderProviders> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;

            SeedOrders();
        }

        public async Task<(bool IsSuccess, Models.Order order, string ErrorMessage)> GetOrdersAaync(int CustomerId)
        {
            try
            {
                var order = await dbContext.Orders.FirstOrDefaultAsync(x => x.CustomerId == CustomerId);
                if (order != null)
                {
                    var result = mapper.Map<Db.Order, Models.Order>(order);
                    return (true, result, null);
                }
                return (false, null, "Not Found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return(false, null, ex.Message);
            }
           
        }

        private void SeedOrders()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order
                {
                    CustomerId = 1,
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    Total = 254,
                    Items = new List<Db.OrderItem> {
                        new Db.OrderItem{
                            Id = 1,
                            ProductId = 1,
                            Quantity = 10,
                            UnitPrice =20
                        },
                        new Db.OrderItem
                        {
                            Id = 2,
                            ProductId = 2,
                            UnitPrice =46,
                            Quantity =5
                        }
                    }
                });
                dbContext.SaveChanges();
            }
        }


    }
}
