using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInventory.Models.ViewModels
{
    public class ShoppingCarViewModel
    {
        public Company Company { get; set; }
        public WarehouseProduct WarehouseProduct { get; set; }
        public ShoppingCar ShoppingCar { get; set; }
    }
}
