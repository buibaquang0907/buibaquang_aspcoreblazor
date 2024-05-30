using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;

namespace buibaquang_aspcoreblazor.Api.Repositories
{
    public interface IAuthRepository
    {
        public Task<User> CreateUser(User user);
    }
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
