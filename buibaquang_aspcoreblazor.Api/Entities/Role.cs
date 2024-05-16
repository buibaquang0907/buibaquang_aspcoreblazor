using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Entities
{
    public class Role : IdentityRole<Guid>
    {
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
    }
}
