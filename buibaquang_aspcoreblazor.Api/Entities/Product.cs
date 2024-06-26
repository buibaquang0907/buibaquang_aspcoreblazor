﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace buibaquang_aspcoreblazor.Api.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
    }
}
