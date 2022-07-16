using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInventory.Models.ViewModels
{
    public class InventoryViewModel
    {
        public Inventory Inventory { get; set; }
        public InventoryDetail InventoryDetail { get; set; }
        public List<InventoryDetail> InventoryDetails { get; set; }
        public IEnumerable<SelectListItem> WarehouseList { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
