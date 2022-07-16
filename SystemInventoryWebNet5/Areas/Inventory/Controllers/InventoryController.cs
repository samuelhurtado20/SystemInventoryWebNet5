using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SystemInventory.Models;
using SystemInventory.Models.ViewModels;
using SystemInventory.Utils;
using SystemInventoryWebNet5.DataAccess.Data;

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

        [HttpPost]
        public IActionResult AddProductPost(int product, int amount, int inventoryId)
        {
            inventoryVM.Inventory.Id = inventoryId;
            if (inventoryVM.Inventory.Id == 0) // Graba el Registro en inventario
            {
                inventoryVM.Inventory.Status = false;
                inventoryVM.Inventory.StartDate = DateTime.Now;

                // get user connected
                var claimIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
                inventoryVM.Inventory.UserAppId = claim.Value;

                _context.Inventory.Add(inventoryVM.Inventory);
                _context.SaveChanges();
            }
            else
            {
                inventoryVM.Inventory = _context.Inventory.SingleOrDefault(i => i.Id == inventoryId);
            }

            var warehouseProduct = _context.WarehouseProduct.Include(b => b.Product).FirstOrDefault(b => b.ProductId == product &&
                                                                                            b.WarehouseId == inventoryVM.Inventory.WarehouseId);
            var detail = _context.InventoryDetail.Include(p => p.Product).FirstOrDefault(d => d.ProductId == product &&
                                                                                         d.InventoryId == inventoryVM.Inventory.Id);
            if (detail == null)
            {
                inventoryVM.InventoryDetail = new()
                {
                    ProductId = product,
                    InventoryId = inventoryVM.Inventory.Id
                };
                if (warehouseProduct != null)
                {
                    inventoryVM.InventoryDetail.PreviousStock = warehouseProduct.Amount;
                }
                else
                {
                    inventoryVM.InventoryDetail.PreviousStock = 0;
                }

                inventoryVM.InventoryDetail.Amount = amount;
                _context.InventoryDetail.Add(inventoryVM.InventoryDetail);
                _context.SaveChanges();

            }
            else
            {
                detail.Amount += amount;
                _context.SaveChanges();
            }

            return RedirectToAction("NewInventory", "Inventory", new { inventoryId = inventoryVM.Inventory.Id });
        }


        public IActionResult Add(int id)
        {
            inventoryVM = new();
            var detail = _context.InventoryDetail.FirstOrDefault(d => d.Id == id);
            inventoryVM.Inventory = _context.Inventory.FirstOrDefault(i => i.Id == detail.InventoryId);

            detail.Amount++;
            _context.SaveChanges();
            return RedirectToAction("NewInventory", "Inventory", new { inventoryId = inventoryVM.Inventory.Id });
        }

        public IActionResult Decrease(int id)
        {
            inventoryVM = new();
            var detalle = _context.InventoryDetail.FirstOrDefault(d => d.Id == id);
            inventoryVM.Inventory = _context.Inventory.FirstOrDefault(i => i.Id == detalle.InventoryId);

            if (detalle.Amount == 1) _context.InventoryDetail.Remove(detalle); 
            else detalle.Amount -= 1;

            _context.SaveChanges();
            return RedirectToAction("NewInventory", "Inventory", new { inventoryId = inventoryVM.Inventory.Id });
        }


        public IActionResult SetStock(int Id)
        {
            var inventory = _context.Inventory.FirstOrDefault(i => i.Id == Id);
            var details = _context.InventoryDetail.Where(d => d.InventoryId == Id).ToList();

            foreach (var item in details)
            {
                var warehouseProduct = _context.WarehouseProduct.Include(p => p.Product).FirstOrDefault(b => b.ProductId == item.ProductId &&
                                                                                                   b.WarehouseId == inventory.WarehouseId);

                if (warehouseProduct != null)
                {
                    warehouseProduct.Amount += item.Amount;
                }
                else
                {
                    warehouseProduct = new();
                    warehouseProduct.WarehouseId = inventory.WarehouseId;
                    warehouseProduct.ProductId = item.ProductId;
                    warehouseProduct.Amount = item.Amount;
                    _context.WarehouseProduct.Add(warehouseProduct);
                }
            }

            // update inventory header 
            inventory.Status = true;
            inventory.EndDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult NewInventory(int? inventoryId)
        {
            inventoryVM = new ();
            inventoryVM.WarehouseList = _context.Warehouse.ToList().Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.Id.ToString()
            });
            inventoryVM.ProductList = _context.Product.ToList().Select(b => new SelectListItem
            {
                Text = b.Name,
                Value = b.Id.ToString()
            });

            inventoryVM.InventoryDetails = new List<InventoryDetail>();

            if (inventoryId != null && inventoryId > 0)
            {
                inventoryVM.Inventory = _context.Inventory.SingleOrDefault(i => i.Id == inventoryId);
                inventoryVM.InventoryDetails = _context.InventoryDetail.Include(p => p.Product).Include(m => m.Product.Brand).
                    Where(d => d.InventoryId == inventoryId).ToList();
            }

            return View(inventoryVM);
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
