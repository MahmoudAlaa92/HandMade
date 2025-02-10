using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WishListsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetAllListOfUser([FromQuery] int id)
        {

            var wishlist = await _context.WishList.Where(w => w.UserId == id).GroupBy(w => w.UserId).ToListAsync();
            if (wishlist == null || !wishlist.Any())
                return NotFound($"No wishlist found for User ID: {id}");

            return Ok(wishlist);
        }



        [HttpPost]
        public async Task<IActionResult> AddnewProduct([FromForm] WishListDto wishListDto)
        {
            if (!ModelState.IsValid || wishListDto == null || wishListDto.ProductId <= 0 || wishListDto.UserId <= 0) return BadRequest();
            bool userExists = await _context.Users.AnyAsync(w => w.Id == wishListDto.UserId);
            if (userExists)
            {
                bool exists = await _context.WishList.AnyAsync(w => w.UserId == wishListDto.UserId && w.ProductId == wishListDto.UserId);
                if (exists)
                {
                    return Ok(new { Exists = true, Message = "Product is already in the wishlist." });
                }

                var newWishItem = new WishList();
                newWishItem.ProductId = wishListDto.ProductId;
                newWishItem.UserId = wishListDto.UserId;

                await _context.WishList.AddAsync(newWishItem);
                await _context.SaveChangesAsync();
                return Ok(newWishItem);

            }
            return BadRequest();
        }




        [HttpDelete]
        public async Task<IActionResult> deleteItem([FromQuery]WishListDto wishListDto)
        {
            var items = await _context.WishList.Where(w => w.UserId == wishListDto.UserId && w.ProductId == wishListDto.ProductId).ToListAsync();
            if (items.IsNullOrEmpty())
                return NotFound("no item found ");


            _context.WishList.RemoveRange(items);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "item deleted successfully" });


        }
    }
}
