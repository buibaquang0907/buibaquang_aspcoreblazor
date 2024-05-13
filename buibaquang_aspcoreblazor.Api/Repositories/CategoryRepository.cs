using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace buibaquang_aspcoreblazor.Api.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoryList();
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(Category category);
        Task<Category> GetById(Guid id);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Create(Category category)
        {
            await _context.Categorys.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Delete(Category category)
        {
            _context.Categorys.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _context.Categorys.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategoryList()
        {
            return await _context.Categorys.ToListAsync();
        }

        public async Task<Category> Update(Category category)
        {
            _context.Categorys.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
