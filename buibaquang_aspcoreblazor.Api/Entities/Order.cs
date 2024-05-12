using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime dateOrder { get; set; }
        public string shippingAddress { get; set; }
        public string payment { get; set; }
        public string status { get; set;}
    }
}
