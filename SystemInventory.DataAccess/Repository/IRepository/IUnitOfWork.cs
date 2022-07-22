using System;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IWarehouseRepository Warehouse { get; }
        ICategoryRepository Category { get; }
        IBrandRepository Brand { get; }
        IProductRepository Product { get; }
        IUserAppRepository UserApp { get; }
        ICompanyRepository Company { get; }
        IOrderRepository Order { get; }
        IOrderDetailRepository OrderDetail { get; }
        IShoppingCarRepository ShoppingCar { get; }
        void Save();
    }
}
