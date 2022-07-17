using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [MaxLength(60)]
        public string Country { get; set; }

        [Required]
        [MaxLength(60)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(40)]
        public string Phone { get; set; }

        [Required]
        public int WarehouseSaleId { get; set; }

        [ForeignKey("WarehouseSaleId")]
        public Warehouse Warehouse { get; set; }

        public string LogoUrl { get; set; }
    }
}
