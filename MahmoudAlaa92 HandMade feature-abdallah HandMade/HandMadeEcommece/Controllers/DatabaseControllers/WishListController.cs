using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WishListController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetAllListOfUser([FromQuery] int id)
        {
            //var wishlist = await _context.WishList.Where(w => w.UserId == id)
            //          .Select(w => w.ProductId)
            //          .ToListAsync();

            var wishlist = await _context.WishList.Where(w => w.UserId == id).GroupBy(w=>w.UserId).ToListAsync();
            //  ToListAsync();
            if (wishlist == null || !wishlist.Any())
                return NotFound($"No wishlist found for User ID: {id}");

            return Ok(wishlist);
        }


        //ther is an issue
        [HttpPost]
        public async Task<IActionResult> AddnewProduct([FromQuery] int id, [FromQuery] int product_id)
        {

             bool userExists = await _context.Users.AnyAsync(w => w.Id == id);
             if (userExists) {
                    bool exists = await _context.WishList.AnyAsync(w => w.UserId == id && w.ProductId == product_id);
                    if (exists)
                    {
                        return Ok(new { Exists = true, Message = "Product is already in the wishwqlist." });
                    }
            
                var newWishItem = new WishList();
                newWishItem.ProductId = product_id;
                newWishItem.UserId = id;

                await _context.WishList.AddAsync(newWishItem);
                await _context.SaveChangesAsync();
                return Ok(newWishItem);

                    }
            return BadRequest();
            //if (newWishItem == null)
            //    return NotFound("list doesnot exist");


        }




        [HttpDelete]
        public async Task<IActionResult> deleteItem([FromQuery] int user_id,[FromQuery] int product_id)
        {
            var items = await _context.WishList.Where(w => w.UserId == user_id && w.ProductId == product_id).ToListAsync();
            if (items.IsNullOrEmpty())
                return NotFound("no item found ");


            _context.WishList.RemoveRange(items);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "item deleted successfully" });


        }


         

    }
}
