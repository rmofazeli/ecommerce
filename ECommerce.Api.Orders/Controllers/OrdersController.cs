using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet]
        [Route("{CustomerId}")]
        public async Task<IActionResult> GetOrdersAsync(int CustomerId)
        {
            var order = await ordersProvider.GetOrdersAaync(CustomerId);
            if (order.IsSuccess)
            {
                    return Ok(order.order);
            }
            return NotFound();
        }
    }
}
