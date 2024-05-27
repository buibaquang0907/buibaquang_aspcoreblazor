using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace buibaquang_aspcoreblazor.Api.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrderList();
        Task<Order> Create(Order order);
        Task<Order> Update(Order order);
        Task<Order> Delete(Order order);
        Task<Order> GetById(Guid id);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> Create(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Delete(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.Include(o => o.Product).Include(o => o.User).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrderList()
        {
            return await _context.Orders.Include(o => o.Product).Include(o => o.User).ToListAsync();
        }

        public async Task<Order> Update(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
