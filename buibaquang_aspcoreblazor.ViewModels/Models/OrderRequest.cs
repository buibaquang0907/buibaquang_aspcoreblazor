using buibaquang_aspcoreblazor.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Models
{
    public class OrderRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<OrderProductRequest> Products { get; set; } = new List<OrderProductRequest>();

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        public DateTime dateOrder { get; set; }

        [MaxLength(250)]
        [Required]
        public string shippingAddress { get; set; }

        [Required]
        public string payment { get; set; }

        [Required]
        public Status status { get; set; }
    }

    public class OrderProductRequest
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
