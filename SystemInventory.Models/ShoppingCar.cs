using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class ShoppingCar
    {
        public ShoppingCar()
        {
            Amount = 1;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserAppId { get; set; }

        [ForeignKey("UserAppId")]
        public UserApp UserApp { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Value between 1 to 100")]
        public int Amount { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
