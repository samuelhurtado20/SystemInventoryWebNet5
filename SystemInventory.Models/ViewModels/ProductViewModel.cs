using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemInventory.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> CategoyList { get; set; }

        [Display(Name = "Brand")]
        public IEnumerable<SelectListItem> BrandList { get; set; }

        [Display(Name = "Product (parent)")]
        public IEnumerable<SelectListItem> ProductList { get; set; }
    }
}
