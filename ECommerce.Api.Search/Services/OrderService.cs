using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, Order order, string ErrorMessage)> GetOrderAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrderService");
                var order = await client.GetAsync($"api/orders/{customerId}");
                if (order.IsSuccessStatusCode)
                {
                    var content = await order.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Order>(content, options);
                    return (true, result, "");
                }
                return (false, null, order.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return(false, null, ex.Message);
            }
           
        }
    }
}
