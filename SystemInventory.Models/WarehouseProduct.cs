using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class WarehouseProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Warehouse")]
        public int BodegaId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        [Required]
        [Display(Name = "Product")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public int Amount { get; set; }
    }
}
