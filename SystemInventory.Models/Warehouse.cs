using System.ComponentModel.DataAnnotations;

namespace SystemInventory.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Inventory Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Inventory Desc.")]
        public string Description { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
