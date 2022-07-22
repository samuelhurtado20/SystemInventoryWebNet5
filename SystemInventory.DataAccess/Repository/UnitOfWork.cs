using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IWarehouseRepository Warehouse { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IBrandRepository Brand { get; private set; }
        public IProductRepository Product { get; private set; }
        public IUserAppRepository UserApp { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IShoppingCarRepository ShoppingCar { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Warehouse = new WarehouseRepository(_db);
            Category = new CategoryRepository(_db);
            Brand = new BrandRepository(_db);
            Product = new ProductRepository(_db);
            UserApp = new UserAppRepository(_db);
            Company = new CompanyRepository(_db);

            Order = new OrderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            ShoppingCar = new ShoppingCarRepository(_db);
        }

        public void Save() => _db.SaveChanges();

        public void Dispose() => _db.Dispose();
    }
}
