using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Order FindAndUpdate(Order data)
        {
            Order entity = _context.Order.FirstOrDefault(w => w.Id == data.Id);
            if (entity is not null)
            {
                entity.TotalOrder = data.TotalOrder;
                entity.OrderStatus = data.OrderStatus;
                return entity;
            }

            return null;
        }
    }
}
