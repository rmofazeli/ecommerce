namespace ECommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic searchResult)> SearchAsync(int customerId);
    }
}
