using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController:ControllerBase
    {
        private readonly ICustomersProvider customerProvider;

        public CustomersController(ICustomersProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var customers = await customerProvider.GetCustomersAsync();
            if (customers.IsSuccess)
            {
                return Ok(customers.Customers);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetCustomersAsync(int Id)
        {
            var customer = await customerProvider.GetCustomersAsync(Id);
            if (customer.IsSuccess)
            {
                return Ok(customer.Customer);
            }
            return NotFound();
        }
    }
}
