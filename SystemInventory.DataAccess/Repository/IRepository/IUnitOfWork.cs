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
        void Save();
    }
}
