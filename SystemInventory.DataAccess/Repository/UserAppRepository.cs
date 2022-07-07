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
    public class UserAppRepository : Repository<UserApp>, IUserAppRepository
    {
        private readonly ApplicationDbContext _context;
        public UserAppRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
