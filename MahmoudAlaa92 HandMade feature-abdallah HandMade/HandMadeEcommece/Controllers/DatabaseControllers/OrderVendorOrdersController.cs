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
    public class OrderVendorOrdersController : ControllerBase
    {

        private readonly AppDbContext _context;

        public OrderVendorOrdersController(AppDbContext context)
        {
            _context = context;
        }


        //[HttpGet]
        //public async Task<IActionResult> getall()
        //{
        //    var orderVendors = await _context.OrderVendorOrders.ToListAsync();

        //    if (orderVendors != null && orderVendors.Any())
        //        return Ok(orderVendors);
        //    return NotFound("No order vendors found.");

        //}

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]int? id)
        {
            var orderVendors = await _context.OrderVendorOrders.ToListAsync();
            if (orderVendors.IsNullOrEmpty() && id>0)// != null && orderVendors.Any())
                orderVendors = await _context.OrderVendorOrders.Where(o=>o.VendorId==id).ToListAsync();
            if (orderVendors.IsNullOrEmpty())
                return Ok("NOT FOund");

            return Ok(orderVendors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderVendoerOrdersDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var o = new OrderVendor
            {
                VendorId = dto.VendorId,
                OrderId = dto.OrderId
            };
           await _context.OrderVendorOrders.AddAsync(o);
            await _context.SaveChangesAsync();

            return Ok(o);
        }

        [HttpDelete]
        public async Task<IActionResult> deletetransaction([FromQuery] int id, [FromQuery] int orderid)
        {

            var order = await _context.OrderVendorOrders
                                    .FirstOrDefaultAsync(t => t.VendorId == id&& t.OrderId==orderid);
            if (order !=null)
            {
                _context.OrderVendorOrders.Remove(order);
                await _context.SaveChangesAsync();
                return Ok(new { message = "order deleted successfully." });

            }
            return NotFound(new { message = "order not found." });



        }
    }
}
