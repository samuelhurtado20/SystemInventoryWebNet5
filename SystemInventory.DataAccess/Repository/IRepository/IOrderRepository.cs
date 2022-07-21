using SystemInventory.Models;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order FindAndUpdate(Order entity);
    }
}
