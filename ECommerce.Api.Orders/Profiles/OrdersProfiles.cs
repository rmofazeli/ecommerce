namespace ECommerce.Api.Orders.Profiles
{
    public class OrdersProfiles : AutoMapper.Profile
    {
        public OrdersProfiles()
        {
            CreateMap<Db.Order,Models.Order>();
            CreateMap<Db.OrderItem, Models.OrderItem>();
        }
    }
}
