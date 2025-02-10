using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        public CartItemsController(AppDbContext _Context, IMapper mapper)
        {
            Context = _Context;
            _Mapper = mapper;
        }

        [HttpGet("GetCartItemsAll")]
        public async Task<IActionResult> GetCartItemsAll()
        {
            var cartItems = await Context.CartItems.ToListAsync();
            if (cartItems == null) return NotFound();
            return Ok(cartItems);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItem([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var CartItems = new List<CartItem>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var CartItem = await Context.CartItems.FindAsync(id);
                if (CartItem == null) continue;
                CartItems.Add(CartItem);
            }
            if (CartItems.Count == 0) return BadRequest();
            return Ok(CartItems);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromForm] CartItemDto cartItemDto)
        {
            if (!ModelState.IsValid || cartItemDto == null) return BadRequest();

            var cartItem = new CartItem
            {
                CartId = cartItemDto.CartId,
                Price = cartItemDto.Price,
                Product_Variant_Item_Id = cartItemDto.Product_Variant_Item_Id,
                Quantity = cartItemDto.Quantity,
                SubTotal = cartItemDto.Price * cartItemDto.Quantity
            };
            await Context.CartItems.AddAsync(cartItem);
            await Context.SaveChangesAsync();
            return Ok(cartItem);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(int id, CartItemDto cartItemDto)
        {
            if (id <= 0 || cartItemDto == null) return BadRequest();
            var cartItem = await Context.CartItems.FindAsync(id);
            if (cartItem == null) return NotFound();
            cartItem.Price = cartItemDto.Price;
            cartItem.Quantity = cartItemDto.Quantity;
            cartItem.CartId = cartItemDto.CartId;
            cartItem.Product_Variant_Item_Id = cartItemDto.Product_Variant_Item_Id;
            cartItem.SubTotal = cartItemDto.Price * cartItemDto.Quantity;
            Context.CartItems.Update(cartItem);
            await Context.SaveChangesAsync();
            return Ok(cartItem);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var CartItems = new List<CartItem>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var CartItem = await Context.CartItems.FindAsync(id);
                if (CartItem == null) continue;
                Context.CartItems.Remove(CartItem);
                CartItems.Add(CartItem);
            }
            await Context.SaveChangesAsync();
            if (CartItems.Count == 0) return BadRequest();
            return Ok(CartItems);
        }
    }
}
