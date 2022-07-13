using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SystemInventoryWebNet5.DataAccess.Data;
using SystemInventory.Utils;
using SystemInventory.Models.ViewModels;

namespace SystemInventoryWebNet5.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    [Authorize(Roles = DS.RoleAdmin + "," + DS.RoleInventory)]
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public InventoryViewModel inventoryVM { get; set; }

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _context.WarehouseProduct.Include(x => x.Product).Include(x => x.Warehouse).ToList();
            return Json(new { data = all });
        }
        #endregion
    }
}
