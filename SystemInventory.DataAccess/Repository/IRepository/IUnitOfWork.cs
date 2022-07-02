﻿using System;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IWarehouseRepository Warehouse { get; }
        ICategoryRepository Category { get; }
        void Save();
    }
}
