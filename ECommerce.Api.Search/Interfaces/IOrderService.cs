namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, Models.Order order, string ErrorMessage)> GetOrderAsync(int customerId);
    }
}
