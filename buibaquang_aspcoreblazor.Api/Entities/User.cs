using System.ComponentModel.DataAnnotations;

namespace buibaquang_aspcoreblazor.Api.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public Role Role { get; set; }
    }
}
