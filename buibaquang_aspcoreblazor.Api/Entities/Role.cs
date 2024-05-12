using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string auth { get; set; }
    }
}
