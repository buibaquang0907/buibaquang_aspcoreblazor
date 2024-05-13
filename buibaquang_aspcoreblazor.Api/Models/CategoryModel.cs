using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Models
{
    public class CategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
    }
}
