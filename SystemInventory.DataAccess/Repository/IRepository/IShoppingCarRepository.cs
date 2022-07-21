using SystemInventory.Models;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IShoppingCarRepository : IRepository<ShoppingCar>
    {
        ShoppingCar FindAndUpdate(ShoppingCar entity);
    }
}
