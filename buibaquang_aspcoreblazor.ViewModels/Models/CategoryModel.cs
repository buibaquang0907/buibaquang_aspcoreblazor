using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Models.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
    }
}
