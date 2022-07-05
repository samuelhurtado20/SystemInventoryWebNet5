using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Product FindAndUpdate(Product product)
        {
            Product entity = _context.Product.FirstOrDefault(p => p.Id == product.Id);
            if (entity is not null)
            {
                entity.Name = product.Name;
                entity.Description = product.Description;
                entity.SerialNumber = product.SerialNumber;
                entity.Price = product.Price;
                entity.Cost = product.Cost;
                entity.CategoryId = product.CategoryId;
                entity.ParentId = product.ParentId == 0 ? null : product.ParentId;
                entity.ImageUrl = product.ImageUrl == null ? null : product.ImageUrl;
                return entity;
            }

            return null;
        }
    }
}
