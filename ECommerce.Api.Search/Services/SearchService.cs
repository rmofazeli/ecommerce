using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ILogger<SearchService> logger;

        public SearchService(IOrderService orderService,IProductService productService, ILogger<SearchService> logger)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, dynamic searchResult)> SearchAsync(int customerId)
        {
            try
            {
                var order = await orderService.GetOrderAsync(customerId);
                var products = await productService.GetProductsAsync();

                foreach (var item in order.order.Items)
                {
                    item.Productame = products.IsSuccess?
                        products.products.FirstOrDefault(p => p.Id == item.ProductId).Name:
                        "Product name is not available now!";
                }
                if (order.IsSuccess)
                {
                    var result = new
                    {
                        orders = order.order
                    };
                    return (true, result);
                }
                return (false, "Not Found!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false,ex.Message);
            }
            
        }
    }
}
