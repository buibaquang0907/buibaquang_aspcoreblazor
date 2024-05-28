using buibaquang_aspcoreblazor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Models
{
    public class OrderUpdate
    {
        [MaxLength(250)]
        [Required]
        public string shippingAddress { get; set; }

        [Required]
        public Status status { get; set; }
    }
}
