using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models;
using SystemInventory.Models.ViewModels;
using SystemInventory.Utils;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.RoleAdmin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            var company = _unitOfWork.Company.GetAll();
            return View(company);
        }

        public IActionResult Upsert(int? id)
        {
            CompanyViewModel companyVM = new() { 
                Company = new Company(),
                WarehouseList = _unitOfWork.Warehouse.GetAll().Select(c=> new SelectListItem {
                  Text = c.Name,
                  Value =c.Id.ToString()
                }),
            };

            if (id == null)
            {
                return View(companyVM);
            }

            companyVM.Company = _unitOfWork.Company.Get(id.GetValueOrDefault());
            if (companyVM.Company == null)
            {
                return NotFound();
            }

            return View(companyVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CompanyViewModel companyVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count>0)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"pictures\companies");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (companyVM.Company.LogoUrl!=null)
                    {
                        var imagenPath = Path.Combine(webRootPath, companyVM.Company.LogoUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    companyVM.Company.LogoUrl = @"\pictures\companies\" + filename + extension;
                }
                else
                {
                    if (companyVM.Company.Id!=0)
                    {
                        Company companiaDB = _unitOfWork.Company.Get(companyVM.Company.Id);
                        companyVM.Company.LogoUrl = companiaDB.LogoUrl;
                    }
                }


                if (companyVM.Company.Id == 0)
                {
                    _unitOfWork.Company.Insert(companyVM.Company);
                }
                else
                {
                    _unitOfWork.Company.FindAndUpdate(companyVM.Company);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                companyVM.WarehouseList = _unitOfWork.Warehouse.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });


                if (companyVM.Company.Id!=0)
                {
                    companyVM.Company = _unitOfWork.Company.Get(companyVM.Company.Id);
                }

            }

            return View(companyVM.Company);
        }

        #region API
        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _unitOfWork.Company.GetAll(properties: "Warehouse");
            return Json(new { data = all });
        }
       

        #endregion
    }
}