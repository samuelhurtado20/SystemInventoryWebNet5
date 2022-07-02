using System;
using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
    {
        private readonly ApplicationDbContext _context;

        public WarehouseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Warehouse FindAndUpdate(Warehouse warehouse)
        {
            Warehouse entity = _context.Warehouse.FirstOrDefault(w => w.Id == warehouse.Id);
            if(entity is not null)
            {
                entity.Name = warehouse.Name;
                entity.Description = warehouse.Description;
                entity.Status = warehouse.Status;

                //_context.SaveChanges();
                return entity;
            }

            return null;
        }
    }
}
