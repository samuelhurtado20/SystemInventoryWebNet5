using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SystemInventory.Models.ViewModels
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public IEnumerable<SelectListItem> WarehouseList { get; set; }
    }
}
