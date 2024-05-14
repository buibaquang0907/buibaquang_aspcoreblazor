using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Models.Models
{
    public class ProductRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Please enter name product")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please enter price product")]
        public double Price { get; set; }
        public string? Image { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
