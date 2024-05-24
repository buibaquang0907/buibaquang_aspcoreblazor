using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(200)]
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
