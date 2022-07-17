using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventory.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Company FindAndUpdate(Company company)
        {
            Company entity = _context.Company.FirstOrDefault(w => w.Id == company.Id);
            if (entity is not null)
            {
                entity.Name = company.Name;
                entity.Address = company.Address;
                entity.City = company.City;
                entity.Country = company.Country;
                entity.Description = company.Description;
                entity.Phone = company.Phone;
                entity.LogoUrl = company.LogoUrl;
                return entity;
            }

            return null;
        }
    }
}
