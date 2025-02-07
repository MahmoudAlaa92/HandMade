using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrById([FromQuery] int? id)
        {
            var order = await _context.OrderProducts.ToListAsync();
            if (order.IsNullOrEmpty() && id > 0)     //!= null && orderVendors.Any())
                order = await _context.OrderProducts.Where(o => o.OrderId == id).ToListAsync();

            if (order.IsNullOrEmpty())
                return Ok("NOT FOund");

            return Ok(order);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] productOrdersDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var o = new OrderProduct
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId
            };
            await _context.OrderProducts.AddAsync(o);
            await _context.SaveChangesAsync();
            return Ok(o);
        }



        [HttpDelete]
        public async Task<IActionResult> delete([FromQuery] int prodid, [FromQuery] int orderid)
        {

            var order = await _context.OrderProducts
                                    .FirstOrDefaultAsync(t => t.ProductId == prodid && t.OrderId == orderid);
            if (order != null)
            {
                _context.OrderProducts.Remove(order);
                await _context.SaveChangesAsync();
                return Ok(new { message = "order deleted successfully." });

            }
            return NotFound(new { message = "order not found." });

        }
    }
}