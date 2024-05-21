using buibaquang_aspcoreblazor.Api.Entities;
using buibaquang_aspcoreblazor.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace buibaquang_aspcoreblazor.Api.Data
{
    public class AppDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public async Task SeedAsync(AppDbContext dbContext, ILogger<AppDbContextSeed> logger)
        {
            if (!dbContext.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Bui",
                    LastName = "Quang",
                    UserName = "admin",
                    Email = "admin1@gmail.com",
                    PhoneNumber = "1234567890",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Aa@123");
                dbContext.Users.Add(user);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
