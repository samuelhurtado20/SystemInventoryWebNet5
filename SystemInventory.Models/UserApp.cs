using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class UserApp : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        //[Required]
        public string Address { get; set; }
        //[Required]
        public string City { get; set; }
        //[Required]
        public string Country { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
