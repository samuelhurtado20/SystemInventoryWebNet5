using SystemInventory.Models;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        OrderDetail FindAndUpdate(OrderDetail data);
    }
}
