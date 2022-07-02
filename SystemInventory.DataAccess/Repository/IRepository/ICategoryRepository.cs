using SystemInventory.Models;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category FindAndUpdate(Category entity);
    }
}
