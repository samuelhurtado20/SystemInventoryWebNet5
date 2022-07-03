using System.ComponentModel.DataAnnotations;

namespace SystemInventory.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Brand Name")]
        public string Name { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
