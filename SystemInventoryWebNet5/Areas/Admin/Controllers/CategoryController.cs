using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventory.Utils;

namespace SystemInventoryWebNet5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.RoleAdmin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Category category = new();
            if (id == null) return View(category);

            category = _uow.Category.Get(id.GetValueOrDefault());
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            if (category.Id > 0)
            {
                _uow.Category.FindAndUpdate(category);
            }
            else
            {
                _uow.Category.Insert(category);
            }

            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _uow.Category.Get();
            return Json(new { data = all });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Category entity = _uow.Category.Get(id);
            if (entity == null) return Json(new { success = false, msg = "Error on delete" });

            _uow.Category.Delete(entity);
            _uow.Save();

            return Json(new { success = true, msg = "Successfully deleted" });
        }
    }
}
