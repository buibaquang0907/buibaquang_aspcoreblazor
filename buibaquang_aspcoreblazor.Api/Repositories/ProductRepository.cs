using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;
using buibaquang_aspcoreblazor.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace buibaquang_aspcoreblazor.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductList(ProductListSearch productListSearch);
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
        public async Task<IEnumerable<Product>> GetProductList(ProductListSearch productListSearch)
        {
            var query = _context.Products.AsQueryable();

            if (productListSearch != null)
            {
                if (!string.IsNullOrEmpty(productListSearch.Name))
                    query = query.Where(x => x.Name.Contains(productListSearch.Name));

                if (productListSearch.CategoryId.HasValue)
                    query = query.Where(x => x.CategoryId == productListSearch.CategoryId);

                if (productListSearch.Price.HasValue)
                {
                    int minPrice = 0;
                    int maxprice = int.MaxValue;
                    switch (productListSearch.Price)
                    {
                        case 500:
                            maxprice = 500;
                            break;
                        case 1500:
                            minPrice = 500;
                            maxprice = 1500;
                            break;
                        case 5000:
                            minPrice = 1500;
                            maxprice = 5000;
                            break;
                    }
                    query = query.Where(x => x.Price > minPrice && x.Price <= maxprice);
                }
            }

            return await query.ToListAsync();
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
