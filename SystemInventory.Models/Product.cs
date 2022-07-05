using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInventory.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [MaxLength(100)]
        [Display(Name = "Product Desc.")]
        public string Description { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Serial number")]
        public string SerialNumber { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Cost")]
        public double Cost { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public int? ParentId { get; set; }
        public virtual Product Parent { get; set; }
    }
}

