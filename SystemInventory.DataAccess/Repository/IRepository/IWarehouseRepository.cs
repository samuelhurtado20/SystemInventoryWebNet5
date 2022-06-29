using SystemInventory.Models;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Warehouse FindAndUpdate(Warehouse warehouse);
    }
}
