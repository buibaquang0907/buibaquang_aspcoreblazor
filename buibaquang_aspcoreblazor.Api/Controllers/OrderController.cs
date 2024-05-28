﻿using buibaquang_aspcoreblazor.Api.Data;
using buibaquang_aspcoreblazor.Api.Entities;
using buibaquang_aspcoreblazor.Api.Extensions;
using buibaquang_aspcoreblazor.Api.Models;
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
        public async Task<ActionResult> GetAll()
        {
            var orders = await _orderRepository.GetOrderList();
            var ordermModel = orders.Select(x => new Order
            {
                Id = x.Id,
                Product = x.Product,
                User = x.User,
                TotalPrice = x.TotalPrice,
                dateOrder = x.dateOrder,
                payment = x.payment,
                shippingAddress = x.shippingAddress,
                status = x.status
            });
            return Ok(orders);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var order = await _orderRepository.GetById(id);

            if (order == null)
                return NotFound($"{id} is not found");

            var orderById = new Order()
            {
                Id = order.Id,
                Product = order.Product,
                User = order.User,
                TotalPrice = order.TotalPrice,
                dateOrder = order.dateOrder,
                payment = order.payment,
                shippingAddress = order.shippingAddress,
                status = order.status
            };

            return Ok(orderById);
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
        public async Task<IActionResult> Update(Guid id, OrderUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderDb = await _orderRepository.GetById(id);
            if (orderDb == null)
                return NotFound($"{id} is not found");

            orderDb.shippingAddress = request.shippingAddress;
            orderDb.status = request.status;

            var order = await _orderRepository.Update(orderDb);
            return Ok(new Order()
            {
                Id = order.Id,
            });
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _orderRepository.GetById(id);
            if (order == null)
                return NotFound($"{id} is not found");

            var orderDel = await _orderRepository.Delete(order);
            return Ok(orderDel != null ? "success" : "false");
        }
    }
}