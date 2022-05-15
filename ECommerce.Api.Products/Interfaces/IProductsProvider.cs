using ECommerce.Api.Products.Model;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductAsync();
        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int Id);
    }
}
