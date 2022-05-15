namespace ECommerce.Api.Search.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Productame { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
