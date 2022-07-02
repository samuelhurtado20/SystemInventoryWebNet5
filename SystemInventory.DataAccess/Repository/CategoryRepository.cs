using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Category FindAndUpdate(Category categoria)
        {
            Category entity = _context.Category.FirstOrDefault(w => w.Id == categoria.Id);
            if (entity is not null)
            {
                entity.Name = categoria.Name;
                entity.Status = categoria.Status;
                return entity;
            }

            return null;
        }
    }
}
