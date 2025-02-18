using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using HandMadeEcommece.Models.Dto;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        public CartsController(AppDbContext _Context, IMapper mapper)
        {
            Context = _Context;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartsAll()
        {
            var Carts = await Context.Carts.ToListAsync();
            if (Carts == null) return NotFound();
            return Ok(Carts);
        }


        //[HttpGet]
        //public async Task<IActionResult> GetCart([FromQuery] List<int> ids)
        //{
        //    if (ids == null) return BadRequest();
        //    var Carts = new List<Cart>();
        //    foreach (var id in ids)
        //    {
        //        if (id <= 0) continue;
        //        var Cart = await Context.Carts.FindAsync(id);
        //        if (Cart == null) continue;
        //        Carts.Add(Cart);
        //    }
        //    if (Carts.Count == 0) return BadRequest();
        //    return Ok(Carts);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartDto cartDto)
        {
            if (!ModelState.IsValid || cartDto == null) return BadRequest();

            var cartItems = await Context.CartItems.Where(e=>e.cart.UserId == cartDto.UserId).ToListAsync();
            var price = 0.0m;
            foreach (var cartItem in cartItems)
            {
                price += cartItem.SubTotal;
            }




            var cart = new Cart
            {
                IsActived = cartDto.IsActived,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TotalPrice = price,
                UserId = cartDto.UserId
            };
            await Context.Carts.AddAsync(cart);
            await Context.SaveChangesAsync();
            return Ok(cart);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCart(int id, CartDto cartDto)
        {
            if (id <= 0 || cartDto == null) return BadRequest();
            var cart = await Context.Carts.FindAsync(id);
            if (cart == null) return NotFound();
            var cartItems = await Context.CartItems.Where(e => e.cart.UserId == cart.UserId).ToListAsync();
            var price = 0.0m;
            foreach (var cartItem in cartItems)
            {
                price += cartItem.SubTotal;
            }
            cart.IsActived = cartDto.IsActived;
            cart.CreatedAt = DateTime.UtcNow;
            cart.UpdatedAt = DateTime.UtcNow;
            cart.TotalPrice = price;
            cart.UserId = cartDto.UserId;
            Context.Carts.Update(cart);
            await Context.SaveChangesAsync();
            return Ok(cart);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var Carts = new List<Cart>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var Cart = await Context.Carts.FindAsync(id);
                if (Cart == null) continue;
                Context.Carts.Remove(Cart);
                Carts.Add(Cart);
            }
            await Context.SaveChangesAsync();
            if (Carts.Count == 0) return BadRequest();
            return Ok(Carts);
        }
    }
}
