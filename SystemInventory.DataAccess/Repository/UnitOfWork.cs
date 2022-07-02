﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IWarehouseRepository Warehouse { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Warehouse = new WarehouseRepository(_db);
            Category = new CategoryRepository(_db);
        }

        public void Save() => _db.SaveChanges();

        public void Dispose() => _db.Dispose();
    }
}