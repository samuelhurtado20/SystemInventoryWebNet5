using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class ShoppingCarRepository : Repository<ShoppingCar>, IShoppingCarRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCarRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ShoppingCar FindAndUpdate(ShoppingCar data)
        {
            ShoppingCar entity = _context.ShoppingCar.FirstOrDefault(w => w.Id == data.Id);
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
