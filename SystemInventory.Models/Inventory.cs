using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User App")]
        public string UserAppId { get; set; }

        [ForeignKey("UserAppId")]
        public UserApp UserApp { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM-dd-yyyy HH:mm}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM-dd-yyyy HH:mm}")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Warehouse")]
        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public bool Status { get; set; }

    }
}
