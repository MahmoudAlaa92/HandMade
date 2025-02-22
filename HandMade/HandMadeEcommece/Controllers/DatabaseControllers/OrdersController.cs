using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HandMadeEcommece.Models.Dto;
using System.Linq.Expressions;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using HandMadeEcommece.Services;

namespace HandMadeEcommece.Controllers.ModelsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _Context;
        private readonly IAuth _auth;
        public OrdersController(AppDbContext Context, IAuth auth)
        {
            _Context = Context;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAll()
        {
            var orders = await _Context.Orders.ToListAsync();
            if (orders == null) return NotFound();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid || orderDto == null) return BadRequest();
            var order = new Order();
            order = await _auth.ProcessOfOrder(order, orderDto);
            await _Context.Orders.AddAsync(order);
            await _Context.SaveChangesAsync();
            return Ok(order);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
        {
            if (id <= 0 || orderDto == null) return BadRequest();
            var order = await _Context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            order = await _auth.ProcessOfOrder(order, orderDto);
            _Context.Orders.Update(order);
            await _Context.SaveChangesAsync();
            return Ok(order);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id <= 0) return BadRequest();
            var order = await _Context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            _Context.Orders.Remove(order);
            await _Context.SaveChangesAsync();
            return Ok(order);
        }

    }
}
