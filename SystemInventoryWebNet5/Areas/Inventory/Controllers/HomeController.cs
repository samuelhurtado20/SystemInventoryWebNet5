using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventory.Models.ViewModels;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventoryWebNet5.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCarViewModel ShoppingCarVM { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow, ApplicationDbContext db)
        {
            _logger = logger;
            _uow = uow;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _uow.Product.GetAll(properties: "Category,Brand");
            return View(products);
        }

        public IActionResult Details(int id)
        {
            ShoppingCarVM = new ShoppingCarViewModel
            {
                Company = _db.Company.FirstOrDefault()                
            };

            ShoppingCarVM.WarehouseProduct = _db.WarehouseProduct.Include(prop => prop.Product).Include(prop => prop.Product.Category).Include(prop => prop.Product.Brand)
                .FirstOrDefault(b => b.ProductId == id && b.WarehouseId == ShoppingCarVM.Company.Warehouse.Id);

            if(ShoppingCarVM.WarehouseProduct == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ShoppingCarVM.ShoppingCar = new ShoppingCar()
                {
                    Product = ShoppingCarVM.WarehouseProduct.Product,
                    ProductId = ShoppingCarVM.WarehouseProduct.ProductId
                };

                return View(ShoppingCarVM);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
