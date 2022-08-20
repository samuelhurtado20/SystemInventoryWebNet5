using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using SystemInventory.DataAccess.Repository.IRepository;
using SystemInventory.Models.ViewModels;
using SystemInventory.Utils;
using SystemInventoryWebNet5.DataAccess.Data;

namespace SystemInventoryWebNet5.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly IUnitOfWork _uow;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        //[BindProperty]
        public ShoppingCarViewModel ShoppingCarVM { get; set; }

        public CarController(ILogger<CarController> logger, IUnitOfWork uow, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _uow = uow;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var claimIdentidad = (ClaimsIdentity)User.Identity;
            var claim = claimIdentidad.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCarVM = new()
            {
                Order = new SystemInventory.Models.Order(),
                ShoppingCarList = _uow.ShoppingCar.GetAll(u => u.UserAppId == claim.Value, properties: "Product")
            };

            return View();
        }

        public IActionResult add(int carroId)
        {
            var carroCompras = _uow.ShoppingCar.Get(c => c.Id == carroId, properties: "Producto");
            carroCompras.Amount += 1;
            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult minus(int carroId)
        {
            var carroCompras = _uow.ShoppingCar.Get(c => c.Id == carroId, properties: "Product");
            if (carroCompras.Amount == 1)
            {
                var numeroProductos = _uow.ShoppingCar.GetAll(u => u.UserAppId ==
                                                                 carroCompras.UserAppId).ToList().Count();
                _uow.ShoppingCar.Delete(carroCompras);
                _uow.Save();
                HttpContext.Session.SetInt32(DS.ssShoppingCar, numeroProductos - 1);
            }
            else
            {
                carroCompras.Amount -= 1;
                _uow.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult remove(int carId)
        {
            var shoppingCar = _uow.ShoppingCar.Get(c => c.Id == carId, properties: "Product");

            var productNum = _uow.ShoppingCar.GetAll(u => u.UserAppId ==
                                                                 shoppingCar.UserAppId).ToList().Count();
            _uow.ShoppingCar.Delete(shoppingCar);
            _uow.Save();
            HttpContext.Session.SetInt32(DS.ssShoppingCar, productNum - 1);

            return RedirectToAction(nameof(Index));
        }

    }
}
