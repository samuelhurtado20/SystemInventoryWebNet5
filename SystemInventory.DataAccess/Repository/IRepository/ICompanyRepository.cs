using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInventory.Models;

namespace SystemInventory.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company FindAndUpdate(Company entity);
    }
}
