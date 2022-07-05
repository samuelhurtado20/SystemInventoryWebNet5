using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;

namespace SystemInventoryWebNet5.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[EnableCors("MyPolicy")]
    public class WarehouseController : Controller
    {
        private readonly IUnitOfWork _uow;

        public WarehouseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult Upsert(int? id)
        {
            Warehouse warehouse = new Warehouse();
            if(id == null) return View(warehouse);

            warehouse = _uow.Warehouse.Get(id.GetValueOrDefault());
            if(warehouse == null) return NotFound();

            return View(warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Warehouse warehouse)
        {
            if (!ModelState.IsValid) return View(warehouse);

            if (warehouse.Id > 0)
            {
                _uow.Warehouse.FindAndUpdate(warehouse);
            }
            else
            {
                _uow.Warehouse.Insert(warehouse);
            }

            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //[Route("GetAll")]
        public IActionResult GetAll()
        {
            var all = _uow.Warehouse.Get();
            return Json(new { data = all });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Warehouse warehouse = _uow.Warehouse.Get(id);
            if (warehouse == null) return Json(new { success = false, msg = "Error on delete" });

            _uow.Warehouse.Delete(warehouse.Id);
            _uow.Save();

            return Json(new {success = true, msg = "Successfully deleted" });
        }
    }
}
