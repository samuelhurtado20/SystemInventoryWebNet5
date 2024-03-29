﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemInventory.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [Display(Name = "Response")]
        public string Response { get; set; }

        [Required]
        [Display(Name = "Category")]
        [EnumDataType(typeof(Enums.QuestionCategories))]
        public Enums.QuestionCategories Category { get; set; }

        public Uri Link { get; set; }

        public string Image { get; set; }

        public bool Status { get; set; }
    }
}
