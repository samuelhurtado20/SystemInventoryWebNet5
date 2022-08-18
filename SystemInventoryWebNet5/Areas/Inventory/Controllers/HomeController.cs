using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventory.Models.ViewModels;
using SystemInventory.Utils;
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

        /// <summary>
        /// https://rickandmortyapi.com/api/character 
        /// togT6WJMztpZzx6fjw3RNg
        /// $env:DATABASE_URL = "postgresql://samuel:togT6WJMztpZzx6fjw3RNg@free-tier14.aws-us-east-1.cockroachlabs.cloud:26257/defaultdb?sslmode=verify-full&options=--cluster%3Dmoon-deer-4029"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            ShoppingCarVM = new ShoppingCarViewModel
            {
                Company = _db.Company.Include(prop => prop.Warehouse).FirstOrDefault()                
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Detalle(ShoppingCarViewModel shoppingCarVM)
        {
            var claimIdentidad = (ClaimsIdentity)User.Identity;
            var claim = claimIdentidad.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCarVM.ShoppingCar.UserAppId = claim.Value;

            ShoppingCar carDB = _uow.ShoppingCar.Get(
                       u => u.UserAppId == shoppingCarVM.ShoppingCar.UserAppId
                       && u.ProductId == shoppingCarVM.ShoppingCar.ProductId,
                       properties: "Product"
                );
            if (carDB == null)
            {
                // there are no elements on car
                _uow.ShoppingCar.Insert(shoppingCarVM.ShoppingCar);
            }
            else
            {
                carDB.Amount += shoppingCarVM.ShoppingCar.Amount;
                _uow.ShoppingCar.FindAndUpdate(carDB);
            }
            _uow.Save();

            // add to session
            var productNumber = _uow.ShoppingCar.GetAll(c => c.UserAppId == shoppingCarVM.ShoppingCar.UserAppId).ToList().Count();
            HttpContext.Session.SetInt32(DS.ssShoppingCar, productNumber);

            return RedirectToAction(nameof(Index));
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
//const { Client } = require("pg");

//const client = new Client(process.env.DATABASE_URL);

//(async () => {
//    await client.connect();
//    try
//    {
//        const results = await client.query("SELECT NOW()");
//        console.log(results);
//    }
//    catch (err)
//    {
//        console.error("error executing query:", err);
//    }
//    finally
//    {
//        client.end();
//    }
//})();