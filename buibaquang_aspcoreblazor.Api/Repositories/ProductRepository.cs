using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace buibaquang_aspcoreblazor.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductList();
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Delete(Product product);
        Task<Product> GetById(Guid id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetProductList()
        {
            return await _context.Products.ToListAsync();
        }
        
        public async Task<Product> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
