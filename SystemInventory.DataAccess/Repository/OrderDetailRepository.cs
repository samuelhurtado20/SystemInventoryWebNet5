using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public OrderDetail FindAndUpdate(OrderDetail data)
        {
            OrderDetail entity = _context.OrderDetail.FirstOrDefault(w => w.Id == data.Id);
            if (entity is not null)
            {
                entity.Amount = data.Amount;
                entity.Price = data.Price;
                return entity;
            }

            return null;
        }
    }
}
