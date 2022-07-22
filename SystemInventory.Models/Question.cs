using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Question")]
        public string Title { get; set; }

        [Required]
        [MaxLength]
        [Display(Name = "Response")]
        public string Response { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        public Uri Link { get; set; }

        public string Image { get; set; }

        public bool Status { get; set; }
    }
}
