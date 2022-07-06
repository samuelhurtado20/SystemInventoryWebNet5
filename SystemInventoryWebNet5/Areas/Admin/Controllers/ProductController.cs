using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventory.Models.ViewModels;

namespace SystemInventoryWebNet5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnviroment;

        public ProductController(IUnitOfWork uow, IWebHostEnvironment hostEnviroment)
        {
            _uow = uow;
            _hostEnviroment = hostEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productVM = new()
            {
                Product = new Product(),
                CategoyList = _uow.Category.Get().Select(c => new SelectListItem
                {
                    Text = c.Name.ToString(),
                    Value = c.Id.ToString(),
                }),
                BrandList = _uow.Brand.Get().Select(c => new SelectListItem
                {
                    Text = c.Name.ToString(),
                    Value = c.Id.ToString(),
                }),
                ProductList = _uow.Product.Get().Where(p => p.Id != id).Select(c => new SelectListItem
                {
                    Text = c.Name.ToString(),
                    Value = c.Id.ToString(),
                })
            };

            if (id == null) return View(productVM);

            productVM.Product = _uow.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null) return NotFound();

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                productVM.CategoyList = _uow.Category.Get().Select(c => new SelectListItem
                {
                    Text = c.Name.ToString(),
                    Value = c.Id.ToString(),
                });
                productVM.BrandList = _uow.Brand.Get().Select(c => new SelectListItem
                {
                    Text = c.Name.ToString(),
                    Value = c.Id.ToString(),
                });
                productVM.ProductList = _uow.Product.Get().Where(p => p.Id != productVM.Product.Id).Select(c => new SelectListItem
                {
                    Text = c.Name.ToString(),
                    Value = c.Id.ToString(),
                });

                return View(productVM);
            }

            // load picture
            string webrootpath = _hostEnviroment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webrootpath, @"pictures/products");
                var ext = Path.GetExtension(files[0].FileName);
                if (productVM.Product.ImageUrl != null)
                {
                    var currentImg = Path.Combine(webrootpath, productVM.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(currentImg))
                    {
                        System.IO.File.Delete(currentImg);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + ext), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }

                productVM.Product.ImageUrl = @"\pictures\products\" + fileName + ext;
            }

            if (productVM.Product.Id > 0)
            {
                _uow.Product.FindAndUpdate(productVM.Product);
            }
            else
            {
                _uow.Product.Insert(productVM.Product);
            }

            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _uow.Product.GetAll(properties: "Category,Brand");
            return Json(new { data = all });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Product entity = _uow.Product.Get(id);
            if (entity == null) return Json(new { success = false, msg = "Error on delete" });

            var currentImg = Path.Combine(_hostEnviroment.WebRootPath, entity.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(currentImg))
            {
                System.IO.File.Delete(currentImg);
            }

            _uow.Product.Delete(entity);
            _uow.Save();

            return Json(new { success = true, msg = "Successfully deleted" });
        }
    }
}
