using buibaquang_aspcoreblazor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public double TotalPrice { get; set; }
        public DateTime dateOrder { get; set; }
        [MaxLength(250)]
        [Required]
        public string shippingAddress { get; set; }
        public string payment { get; set; }
        public Status status { get; set;}
    }
}
