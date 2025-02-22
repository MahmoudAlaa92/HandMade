using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using HandMadeEcommece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeEcommece.Controllers.PagesControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _Context;
        private readonly IAuth _auth;
        public HomeController(AppDbContext context, IAuth auth)
        {
            _Context = context;
            _auth = auth;
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart(CartDto cartDto)
        {
            if (!ModelState.IsValid || cartDto == null) return BadRequest();

            var cartItems = await _Context.CartItems.Where(e => e.cart.UserId == cartDto.UserId).ToListAsync();
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
            await _Context.Carts.AddAsync(cart);
            await _Context.SaveChangesAsync();
            return Ok(cart);
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _Context.Products.Select(e => new
            {
                e.Id,
                e.Sku,
                e.Name,
                e.Slug,
                e.Price,
                e.Qty,
                e.OfferPrice,
                e.LongDescription,
                e.OfferEndDate,
                e.OfferStartDate,
                e.VendorId,
                e.ThumbImage
            }).ToListAsync();
            if(products.Count == 0)return NotFound("Products is not found.");
            return Ok(products);
        }

        [HttpPost("AddInWishList")]
        public async Task<IActionResult> AddInWishList(WishListDto wishListDto)
        {
            if (wishListDto == null || wishListDto.UserId <= 0 || wishListDto.ProductId <= 0) return BadRequest("Error in userId or productId.");
            var user = await _Context.Users.FindAsync(wishListDto.UserId);
            var product = await _Context.Products.FindAsync(wishListDto.ProductId);
            if (user == null || product == null) return NotFound("user or product not found.");
            var wishlist = new WishList { ProductId = wishListDto.ProductId,UserId=wishListDto.UserId};
            await _Context.WishList.AddAsync(wishlist);
            await _Context.SaveChangesAsync();
            return Ok(wishlist);
        }


        [HttpDelete("DeleteFromWishList")]
        public async Task<IActionResult>DeleteFromWishList(int id)
        {
            if (id <= 0) return BadRequest("Error in this id.");
            var wishlist = await _Context.WishList.FindAsync(id);
            if (wishlist == null) return NotFound("This wishlist is not found.");
            _Context.WishList.Remove(wishlist);
            await _Context.SaveChangesAsync();
            return Ok(wishlist);
        }

        [HttpPost("AddCartItem")]
        public async Task<IActionResult>AddCartItem(CartItem cartItemDto)
        {
            if (!ModelState.IsValid || cartItemDto == null) return BadRequest();

            var cartItem = new CartItem
            {
                CartId = cartItemDto.CartId,
                Price = cartItemDto.Price,
                ProductId = cartItemDto.ProductId,
                Quantity = cartItemDto.Quantity,
                SubTotal = cartItemDto.Price * cartItemDto.Quantity
            };
            await _Context.CartItems.AddAsync(cartItem);
            await _Context.SaveChangesAsync();
            return Ok(cartItem);
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var Categories = await _Context.Categories.Select(e => new
            {
                e.Id,
                e.Name,
                e.Icon,
                e.Slug,
                e.Status,
                e.UpdatedAt,
                e.CreatedAt
            }).ToListAsync();
            if (Categories == null) return NotFound();
            return Ok(Categories);
        }


        [HttpGet("GetBrands")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _Context.Brands.Select(e => new
            {
                e.Id,
                e.Logo,
                e.Name,
                e.Slug,
                e.Status,
                e.UpdatedAt,
                e.CreatedAt,
            }).ToListAsync();
            if (brands == null) return NotFound();
            return Ok(brands);
        }
    }
}
