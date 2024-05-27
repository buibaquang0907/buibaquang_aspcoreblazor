using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;
using buibaquang_aspcoreblazor.Api.Extensions;
using buibaquang_aspcoreblazor.Api.Repositories;
using buibaquang_aspcoreblazor.Models.Enums;
using buibaquang_aspcoreblazor.Models.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace buibaquang_aspcoreblazor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly AppDbContext _context;
        public OrderController(IOrderRepository orderRepository, AppDbContext context)
        {
            _orderRepository = orderRepository;
            _context = context;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products.FindAsync(request.ProductId);
            var user = await _context.Users.FindAsync(request.UserId);

            if (product == null)
                return NotFound("Product not found");
            if (user == null)
                return NotFound("User not found");

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                Product = product,
                User = user,
                TotalPrice = request.TotalPrice,
                dateOrder = request.dateOrder,
                shippingAddress = request.shippingAddress,
                payment = request.payment,
                status = request.status
            };

            var createdOrder = await _orderRepository.Create(order);
            return Ok(createdOrder);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
