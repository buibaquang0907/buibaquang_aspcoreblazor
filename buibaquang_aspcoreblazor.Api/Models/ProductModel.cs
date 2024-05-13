﻿using buibaquang_aspcoreblazor.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
