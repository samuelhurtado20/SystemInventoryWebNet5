using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventoryWebNet5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.UserApp.ToList();
            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();

            foreach (var user in userList)
            {
                var roleId = _context.UserRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }

            return Json( new { data = userList  });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var user = _context.UserApp.FirstOrDefault(u => u.Id == id);
            
            if(user == null) return Json(new { success = false, mesg = "User not found" });

            if (user.LockoutEnd != null && user.LockoutEnd > System.DateTimeOffset.Now) user.LockoutEnd = System.DateTimeOffset.Now;
            else user.LockoutEnd = System.DateTimeOffset.Now.AddYears(100);

            _context.SaveChanges();
            return Json(new { success = true, msg = "Successful operation", data = user });
        }

        #endregion
    }
}
