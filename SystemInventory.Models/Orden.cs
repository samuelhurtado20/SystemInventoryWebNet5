using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserAppId { get; set; }

        [ForeignKey("UserAppId")]
        public UserApp UserApp { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime SendDate { get; set; }

        public string ShippingNumber { get; set; }

        public string Carrier { get; set; }

        [Required]
        public double TotalOrder { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime MaximumPaymentDate { get; set; }

        public string TransactionID { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string CustomerName { get; set; }
    }
}
