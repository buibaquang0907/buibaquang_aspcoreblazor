using buibaquang_aspcoreblazor.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buibaquang_aspcoreblazor.Api
{
    public class OrderRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }

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
}
